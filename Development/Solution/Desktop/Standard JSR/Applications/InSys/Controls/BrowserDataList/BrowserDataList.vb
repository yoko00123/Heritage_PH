Option Explicit On
Option Strict On

Friend Class BrowserDataList
    Inherits GSCOM.UI.DataList.DataListBase


#Region "Declarations"
    Friend WithEvents mGrid As New BrowserGrid
    Dim WithEvents mCalendarViewButton As New ToolStripMenuItem("Calendar View", gImageList.Images("calendar.png"))
    Dim WithEvents mListViewButton As New ToolStripButton("List View", gImageList.Images("list.png"))
    Dim WithEvents mReportViewButton As New ToolStripButton("Report View", gImageList.Images("report.png"))
    Dim WithEvents mHierarchicalButton As New ToolStripButton("Hierarchical View", gImageList.Images("hierarchical.png"))
    Dim WithEvents mSpanViewButton As ToolStripButton
    Dim WithEvents mAlertButton As New ToolStripButton("Alert", gImageList.Images("_alert.png"))
    Dim WithEvents mStartDateBox As New ToolStripTextBox
    Dim WithEvents mEndDateBox As New ToolStripTextBox
    Dim WithEvents mPlotGridRowField As New ToolStripTextBox
    Dim WithEvents mChangeTitle As ToolStripButton = New ToolStripButton("Report Setting", GSCOM.UI.ReportSettingImage)
    Dim mEndDateLabel As ToolStripLabel = New ToolStripLabel("End Date")
    Dim mStartDateLabel As ToolStripLabel = New ToolStripLabel("Start Date")
    Dim mRowLabel As ToolStripLabel = New ToolStripLabel("Row Field")
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
        mGrid.pDataListBase = Me
        MyBase.InitGrid(mGrid)
        mGrid.Dock = DockStyle.Fill
        mGrid.BringToFront()
        mCalendarViewButton.CheckOnClick = True
        mListViewButton.Enabled = False
        mCalendarViewButton.Visible = False
        mHierarchicalButton.Visible = False
        'mAlertButton.Visible = True
        mAlertButton.Visible = False 'COMMENT BY: JAMESON
        AddItem(New ToolStripSeparator)
        AddItem(mListViewButton)
        AddItem(mReportViewButton)
        AddItem(mCalendarViewButton)
        AddItem(mHierarchicalButton)
        AddItem(New ToolStripSeparator)
        AddItem(mChangeTitle)
        AddItem(mAlertButton)
        mAlertButton.Alignment = ToolStripItemAlignment.Left
        mChangeTitle.Alignment = ToolStripItemAlignment.Left
    End Sub

    Public Shadows Sub Dispose()
        mGrid.Dispose()
    End Sub

#End Region

#Region "AddItem"
    Private Sub AddItem(ByVal i As ToolStripItem, Optional ByVal pToolStrip As ToolStrip = Nothing)
        If i IsNot Nothing Then
            i.Alignment = ToolStripItemAlignment.Right
            If TypeOf i Is ToolStripButton Or TypeOf i Is ToolStripMenuItem Then
                i.DisplayStyle = ToolStripItemDisplayStyle.Image
            ElseIf TypeOf i Is ToolStripLabel Then

            ElseIf TypeOf i Is ToolStripTextBox Then
                i.TextAlign = ContentAlignment.MiddleRight
                DirectCast(i, ToolStripTextBox).BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            End If
            If pToolStrip Is Nothing Then
                MyBase.MainStrip.Items.Add(i)
            Else
                pToolStrip.Items.Add(i)
            End If
        End If
    End Sub
#End Region

#Region "Init"
    Public Overloads Sub Init(ByVal pDataSource As String, ByVal pConnection As SqlClient.SqlConnection, ByVal pFixedFilter As String, ByVal pColumnTable As DataTable, ByVal pSelectString As String, ByVal pSort As String, ByVal pMenu As Database.Menu)
        Me.mGrid.Menu = pMenu
        Me.mGrid.SchemaTable = New GSCOM.SQL.SchemaTable(gConnection, CStr(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.TableName)))
        MyBase.Init(pDataSource, pConnection, pFixedFilter, pColumnTable, pSelectString, pSort)
        If mGrid.Menu <> 0 Then 'the pmenu is not yet initialized
            If CBool(nDB.GetMenuValue(mGrid.Menu, Database.Tables.tMenu.Field.IsSpanView)) Then
                'And pColumnTable.Select("Name='StartDate'").Length > 0 _
                'And pColumnTable.Select("Name='EndDate'").Length > 0 Then
                If mSpanViewButton Is Nothing Then
                    mSpanViewButton = New ToolStripButton("Span View", gImageList.Images("span.png"))
                    mEndDateBox.Text = Strings.Format(DateAdd(DateInterval.Month, 1, Now.Date), GSCOM.Common.DefaultDateFormat)
                    mStartDateBox.Text = Strings.Format(Now.Date, GSCOM.Common.DefaultDateFormat)

                    AddItem(mSpanViewButton)
                    AddItem(mEndDateBox, Me.ExtraStrip)
                    AddItem(mEndDateLabel, Me.ExtraStrip)
                    AddItem(mStartDateBox, Me.ExtraStrip)
                    AddItem(mStartDateLabel, Me.ExtraStrip)
                    AddItem(mPlotGridRowField, Me.ExtraStrip)
                    mRowLabel.Text = nDB.GetMenuValue(mGrid.Menu, Database.Tables.tMenu.Field.ListRowFieldHeader).ToString
                    If mRowLabel.Text = "" Then mRowLabel.Text = nDB.GetMenuValue(mGrid.Menu, Database.Tables.tMenu.Field.ID_ListMenu).ToString
                    AddItem(mRowLabel, Me.ExtraStrip)
                    SetViewModeToPlotCalendar()
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub mGrid_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGrid.DataSourceChanged
        Dim vHasItem As Boolean
        Me.mCalendarViewButton.DropDownItems.Clear()
        If (mGrid.DataSource IsNot Nothing) Then
            If mGrid.DataSource.Columns.Count > 1 Then
                For Each c As DataColumn In mGrid.DataSource.Columns
                    If c.DataType Is GetType(Date) Then
                        If InStr(c.ColumnName, "Time", CompareMethod.Binary) = 0 Then
                            Dim b As New ToolStripMenuItem(c.Caption, Nothing, AddressOf CalendarViewItemClick)
                            b.Name = Me.ToString & "_" & c.ColumnName
                            mCalendarViewButton.DropDownItems.Add(b)
                            vHasItem = True
                        End If
                    End If
                Next
            End If
            If mGrid.DataSource.Columns.Contains("ParentID") _
        And mGrid.DataSource.Columns.Contains("ImageFile") _
        And mGrid.DataSource.Columns.Contains("Name") Then
                Static vFirstTime As Boolean = True
                mHierarchicalButton.Visible = True
                If vFirstTime Then
                    mHierarchicalButton.PerformClick()
                    vFirstTime = False
                End If
            Else
                mHierarchicalButton.Visible = False
            End If

            'If mSpanViewButton IsNot Nothing AndAlso nDB.GetMenuValue(mGrid.Menu, Database.Tables.tMenu.Field.ID_ListMenu) IsNot DBNull.Value Then
            '    If mGrid.DataSource.Columns.Contains("StartDate") _
            '            And mGrid.DataSource.Columns.Contains("EndDate") Then '_
            '        'And mGrid.DataSource.Columns.Contains("Name") Then
            '        mSpanViewButton.Visible = True
            '        SetViewModeToPlotCalendar()
            '    Else
            '        mSpanViewButton.Visible = False
            '    End If

            'End If




        End If
        If mCalendarViewButton.DropDownItems.Count = 1 Then
            mCalendarViewButton.DropDownItems(0).Visible = False
        Else
            mCalendarViewButton.Enabled = True
        End If

  


        mCalendarViewButton.Visible = vHasItem
        mListViewButton.Visible = vHasItem Or mHierarchicalButton.Visible Or mReportViewButton.Visible
    End Sub

    Private Sub CalendarViewItemClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim n As String = CType(sender, ToolStripItem).Name
        Dim pfix As String = Me.ToString & "_"
        ' If e.ClickedItem IsNot mCalendarViewButton Then

        If Strings.Left(n, Me.ToString.Length + 1) = pfix Then
            mListViewButton.Enabled = True
            mReportViewButton.Enabled = True
            mHierarchicalButton.Enabled = True
            If Me.mSpanViewButton IsNot Nothing Then
                Me.mSpanViewButton.Enabled = True
            End If
            mGrid.ViewMode = BrowserGridViewMode.Calendar
            Me.AddHierarchyParents = False
            Me.ExtraStrip.Visible = False
            mGrid.CalendarViewDateColumnName = Strings.Right(n, n.Length - pfix.Length)

            If mGrid.DataSource.Columns.Contains("Name") Then
                mGrid.CalendarViewDisplayColumnName = "Name"
            ElseIf mGrid.DataSource.Columns.Contains("LastName") Then
                mGrid.CalendarViewDisplayColumnName = "LastName"
            ElseIf mGrid.DataSource.Columns.Contains("FirstName") Then
                mGrid.CalendarViewDisplayColumnName = "FirstName"
            Else
                For Each c As DataColumn In mGrid.DataSource.Columns
                    'If Not c.AllowDBNull Then 'NOTE: SCHEMA IS DISREGARDED IN LISTS
                    If mGrid.DataSource.Select(c.ColumnName & " IS NULL").Length = 0 Then
                        If c.ColumnName <> "ID" And c.ColumnName <> "ImageFile" And c.ColumnName <> mGrid.CalendarViewDateColumnName Then
                            mGrid.CalendarViewDisplayColumnName = c.ColumnName
                            Exit For
                        End If
                    End If
                Next
                If mGrid.CalendarViewDisplayColumnName = "" Then
                    If mGrid.DataSource.Columns.Contains("ImageFile") Then
                        mGrid.CalendarViewDisplayColumnName = "ImageFile"
                    Else
                        mGrid.CalendarViewDisplayColumnName = "ID"
                    End If
                End If
            End If
            'mGrid.CalendarViewDisplayColumnName = mGrid.DataSource.Columns(1).ColumnName
            mGrid.SetDataSource()
        End If
        '   End If
    End Sub

    Private Sub mGridButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mListViewButton.Click
        mGrid.ViewMode = BrowserGridViewMode.List
        Me.AddHierarchyParents = False
        Me.ExtraStrip.Visible = False
        mListViewButton.Enabled = False
        mCalendarViewButton.Enabled = True
        mHierarchicalButton.Enabled = True
        mReportViewButton.Enabled = True
        If Me.mSpanViewButton IsNot Nothing Then
            Me.mSpanViewButton.Enabled = True
        End If
        mGrid.SetDataSource()
    End Sub

    Private Sub mHierarchicalButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mHierarchicalButton.Click
        mGrid.ViewMode = BrowserGridViewMode.Hierarchical
        Me.AddHierarchyParents = True
        Me.ExtraStrip.Visible = False
        mListViewButton.Enabled = True
        mReportViewButton.Enabled = True
        mHierarchicalButton.Enabled = False
        mCalendarViewButton.Enabled = True
        If Me.mSpanViewButton IsNot Nothing Then
            Me.mSpanViewButton.Enabled = True
        End If

        mGrid.SetDataSource()
    End Sub

    Private Sub mCalendarViewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mCalendarViewButton.Click
        If mCalendarViewButton.DropDownItems.Count = 1 Then
            CalendarViewItemClick(mCalendarViewButton.DropDownItems(0), Nothing)
            mCalendarViewButton.Enabled = False
        End If
    End Sub

    Private Sub mRoomReservationButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mSpanViewButton.Click
        SetViewModeToPlotCalendar()
    End Sub

    Sub SetViewModeToPlotCalendar()
        If IsDate(mStartDateBox.Text) And IsDate(mEndDateBox.Text) Then
            mGrid.ViewMode = BrowserGridViewMode.PlotCalendar
            Me.AddHierarchyParents = False
            Me.ExtraStrip.Visible = True
            If Me.mSpanViewButton IsNot Nothing Then
                Me.mSpanViewButton.Enabled = True
            End If
            'mListViewButton.Enabled = False
            'mCalendarViewButton.Enabled = True
            mListViewButton.Enabled = True
            With mGrid
                .StartDate = CDate(mStartDateBox.Text)
                .EndDate = CDate(mEndDateBox.Text)
                .RowFieldContent = mPlotGridRowField.Text
                .RowFieldHeader = nDB.GetMenuValue(mGrid.Menu, "ListRowFieldHeader").ToString
                .RowField = nDB.GetMenuValue(mGrid.Menu, "ListRowField").ToString
                .StatusTable = nDB.GetMenuValue(mGrid.Menu, "StatusTable").ToString
                If nDB.GetMenuValue(mGrid.Menu, "ListRowCategoryHeader") IsNot DBNull.Value Then
                    .RowCategoryHeader = nDB.GetMenuValue(mGrid.Menu, "ListRowCategoryHeader").ToString
                    .RowCategory = nDB.GetMenuValue(mGrid.Menu, "ListRowCategory").ToString
                    '.CellContent = nDB.get
                End If

            End With
            mGrid.SetDataSource()
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'MainFilter
        '
        Me.MainFilter.Size = New System.Drawing.Size(168, 487)
        '
        'BrowserDataList
        '
        Me.Name = "BrowserDataList"
        Me.ResumeLayout(False)

    End Sub

    Private Sub mChangeTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mChangeTitle.Click
        Dim vInfoSet As InfoSet = MainModule.NewReport(mGrid.Menu)
        GoReport(vInfoSet)
    End Sub

    Private Sub GoReport(ByVal pCompanyMenuInfo As InfoSet)
        mReportViewButton.Enabled = False
        mListViewButton.Enabled = True
        mCalendarViewButton.Enabled = True
        If Me.mSpanViewButton IsNot Nothing Then
            Me.mSpanViewButton.Enabled = True
        End If
        mGrid.ViewMode = BrowserGridViewMode.Report
        Me.ExtraStrip.Visible = False
        'If vInfoSet IsNot Nothing Then
        mGrid.ReportInfo = New Html.ReportInfo(pCompanyMenuInfo, Nothing, mGrid.DataSource, mGrid.Menu)
        'End If
        mGrid.SetDataSource()
    End Sub
    Private Sub mReportViewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mReportViewButton.Click
        Dim vInfoSet As InfoSet = Me.SetCompanyMenu(CInt(Me.mGrid.Menu))
        GoReport(vInfoSet)
    End Sub

    Public Function SetCompanyMenu(ByVal xMenu As Integer) As InfoSet
        Dim pM As Database.Menu = CType(577, Database.Menu)
        Dim ss As String = "SELECT ID FROM tCompanyMenu cm WHERE cm.ID_Company=" & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & " AND cm.ID_Menu=" & xMenu
        Dim vID As Object = GSCOM.SQL.ExecuteScalar(ss, gConnection)
        Dim pInfoSet As InfoSet = Nothing
        If IsNothing(vID) Then
            'pInfoSet = MainModule.NewReport(mGrid.Menu)
            'If pInfoSet.Row.RowState = DataRowState.Added Then
            '    Return Nothing
            'Else
            '    Return pInfoSet
            'End If
            Return Nothing
        Else
            pInfoSet = GetInfoSet(pM)
            If pInfoSet IsNot Nothing Then
                pInfoSet.LoadInfo(CInt(vID))
            Else
                pInfoSet = ActiveModule.NewInfo(pM, Nothing, CInt(vID))
            End If
            Return pInfoSet
        End If
    End Function

    Private Sub mAlertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mAlertButton.Click
        Dim aMenu As Database.Menu = CType(92, Database.Menu)
        Dim aInfoSet As InfoSet = Nothing
        Dim dt As DataTable
        Dim s As String

        s = "select * from tAlertType"

        dt = GSCOM.SQL.TableQuery(s, gConnection)

        aInfoSet = GetInfoSet(aMenu)

        If aInfoSet Is Nothing Then
            aInfoSet = ActiveModule.NewInfo(aMenu, Nothing, 0)
        Else
            aInfoSet.LoadInfo(0)
        End If

        dt = aInfoSet.mDataset.Tables(0)
        dt.Rows(0).Item("ID_Menu") = mGrid.Menu

        aInfoSet.ShowDialog()
    End Sub

    Private Sub BrowserDataList_ButtonClick(ByVal sender As Object, ByVal e As Interfaces.ZIDataList.ButtonClickEventArgs) Handles Me.ButtonClick
        If Me.Text = nDB.GetMenuValue(Database.Menu.MAINTENANCE_INSYSORBIT_LogDevice, Database.Tables.tMenu.Field.Name).ToString Then
            Dim dt As DataTable
            dt = TryCast(mGrid.DataSource, DataTable)
            If e.ButtonText = "Requery" Then
                If dt IsNot Nothing Then
                    'If dt.Columns.Contains("IsConnected") Then

                    '    For Each dr As DataRow In dt.Select
                    '            '        Dim a As New FSDevice.Device
                    '        If dr("IPAddress").ToString.ToUpper = "USB" Then
                    '            If a.ConnectUSB Then
                    '                dr("IsConnected") = True
                    '                dr("Color") = "Green"
                    '                a.Disconnect()
                    '            Else
                    '                dr("IsConnected") = False
                    '                dr("Color") = "Red"
                    '            End If
                    '        Else

                    '            a.IP = dr("IPAddress").ToString
                    '            a.Port = 4370
                    '            If My.Computer.Network.Ping(a.IP) Then
                    '                Dim b As Boolean = a.CanConnect
                    '                dr("IsConnected") = b
                    '                If b Then
                    '                    dr("Color") = "Green"
                    '                Else
                    '                    dr("Color") = "Red"
                    '                End If
                    '            Else
                    '                dr("Color") = "Red"
                    '            End If
                    '        End If


                    '    Next

                    '    mGrid.SetDataSource()
                    'End If
                    mGrid.SetDataSource()
                End If
            End If

        End If
    End Sub
End Class

