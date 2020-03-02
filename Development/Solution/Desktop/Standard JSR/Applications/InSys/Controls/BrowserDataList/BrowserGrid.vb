Option Explicit On
Option Strict On
Imports System.Web
Imports System.Collections.Generic

'<System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
'<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
Public Class BrowserGrid
    Inherits System.Windows.Forms.WebBrowser
    Implements GSCOM.Interfaces.IDataListGrid

#Region "Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.

#End Region

#Region " Declarations"
    Private mBackGroundColor As Color = Color.White
    ' Private mHeaderBackColor As Color = Color.Gray
    Private mRowHeadersWidth As Integer = 24
    Private mRowHeight As Integer = 18
    Private mSettingsKey As String = String.Empty
    Private mMousePosition As Point
    Public Event Finalizing(ByVal sender As Object, ByVal e As EventArgs)
    Friend Event RefreshedColors(ByVal sender As Object, ByVal e As EventArgs) Implements Interfaces.IDataListGrid.RefreshedColors
    Public Shadows Event KeyDown As KeyEventHandler Implements Interfaces.IDataListGrid.KeyDown
    Friend Property Menu As Database.Menu
    Friend pDataListBase As BrowserDataList
    Private WithEvents mDocument As HtmlDocument

    Protected tmpFiles As List(Of String)

#End Region

#Region "New"
    Public Sub New()
        MyBase.New()

        Me.tmpFiles = New List(Of String)

        'This call is required by the Windows Form Designer.
        Dim c As Color = Color.FromKnownColor(KnownColor.ActiveCaption)
        Dim r, g, b As Integer
        Dim d As Decimal = CDec(2)
        d = 255
        r = CInt((c.R + d) / 4)
        g = CInt((c.G + d) / 4)
        b = CInt((c.B + d) / 4)
        mBackGroundColor = Color.FromArgb(r, g, b)
        Me.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EvenBackColor = System.Drawing.Color.White
        Me.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.Location = New System.Drawing.Point(0, 0)
        'Me.Name = MainView
        Me.OddBackColor = System.Drawing.Color.AliceBlue
        Me.Size = New System.Drawing.Size(632, 559)
        Me.TabIndex = 0
        Me.DoubleBuffered = True
        Me.AllowWebBrowserDrop = True
        Me.WebBrowserShortcutsEnabled = True
        Me.ObjectForScripting = Me
        Me.IsWebBrowserContextMenuEnabled = True
        'If nDB IsNot Nothing Then
        'If nDB.GetUserID <> 1 Then
        Me.ScriptErrorsSuppressed = True
        'End If
        'End If
    End Sub

#End Region

    'Public Property CalendarView As Boolean
    Public ViewMode As BrowserGridViewMode = BrowserGridViewMode.List
    Public Property CalendarViewDateColumnName As String
    Public Property CalendarViewDisplayColumnName As String

    ' Public Property rID As Integer
    Friend Property ReportInfo As Html.ReportInfo

    Public Sub ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
        'Dim vCheckedRowIDs() As Integer = Me.CheckedRowID
        SetDataSource(Me.Checkeds)
    End Sub

#Region " Properties"
    Private mOddBackColor As Color = Color.AliceBlue
    Public Property OddBackColor() As Color Implements Interfaces.IDataListGrid.OddBackColor
        Get
            Return mOddBackColor
        End Get
        Set(ByVal Value As Color)
            mOddBackColor = Value
            'Dim r, g, b As Integer
            'r = CInt(mOddBackColor.R / 1.75)
            'g = CInt(mOddBackColor.G / 1.75)
            'b = CInt(mOddBackColor.B / 1.75)
            'mHeaderBackColor = Color.FromArgb(r, g, b)
            'SetDataSource()
            RaiseEvent RefreshedColors(Me, EventArgs.Empty)
        End Set
    End Property

    Private mEvenBackColor As Color = Color.White
    Public Property EvenBackColor() As Color Implements Interfaces.IDataListGrid.EvenBackColor
        Get
            Return mEvenBackColor
        End Get
        Set(ByVal Value As Color)
            mEvenBackColor = Value
            'SetDataSource()
            RaiseEvent RefreshedColors(Me, EventArgs.Empty)
        End Set
    End Property

#End Region

#Region " Subs"

#Region " InitSettings"
    Public Sub InitSettings(ByVal uniqueKey As String)
        'ROBBIE disable settings
        'mSettings = New Settings(Me, uniqueKey)
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Me_ColumnAdded"
    'Private Sub Me_ColumnAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserColumnEventArgs)
    '    Dim dc As DataColumn
    '    Dim d, t As Boolean
    '    dc = CType(Me.DataSource, DataTable).Columns(e.Column.DataPropertyName)
    '    If dc IsNot Nothing Then
    '        Select Case dc.DataType.ToString
    '            Case System.Decimal, System.Int32, System.DateTime
    '                e.Column.DefaultCellStyle.Alignment = WebBrowserContentAlignment.MiddleRight
    '        End Select
    '        Select Case dc.DataType.ToString
    '            Case System.DateTime
    '                d = (InStr(dc.ColumnName, Date, CompareMethod.Binary)  0)
    '                t = (InStr(dc.ColumnName, Time, CompareMethod.Binary)  0)
    '                If d And t Then
    '                    e.Column.DefaultCellStyle.Format = GSCOM.Common.DefaultDateTimeFormat
    '                ElseIf d Then
    '                    e.Column.DefaultCellStyle.Format = GSCOM.Common.DefaultDateFormat
    '                ElseIf t Then
    '                    e.Column.DefaultCellStyle.Format = GSCOM.Common.DefaultTimeFormat
    '                Else ' default
    '                    e.Column.DefaultCellStyle.Format = GSCOM.Common.DefaultDateFormat
    '                End If
    '            Case System.Decimal
    '                Dim dt As GSCOM.SQL.ZDataTable
    '                dt = TryCast(e.Column.WebBrowser.DataSource, GSCOM.SQL.ZDataTable)
    '                If dt Is Nothing Then
    '                    e.Column.DefaultCellStyle.Format = #,##0.00
    '                Else
    '                    Dim vScale As Byte '20090401
    '                    Dim dra() As DataRow
    '                    dra = dt.SchemaTable.Select(ColumnName= & GSCOM.SQL.SQLFormat(e.Column.DataPropertyName))
    '                    If dra.Length  0 Then
    '                        vScale = CByte(dra(0)(Scale))
    '                    End If
    '                    If CBool(vScale = 4) Then
    '                        e.Column.DefaultCellStyle.Format = #,##0 & IIf(vScale  0, . & Strings.StrDup(vScale, 0), ).ToString
    '                    Else
    '                        e.Column.DefaultCellStyle.Format = #,##0.00
    '                    End If
    '                End If
    '            Case System.Boolean
    '                If dc.AllowDBNull Then
    '                    'CType(e.Column, WebBrowserCheckBoxColumn).ThreeState = True
    '                Else
    '                    'CType(e.Column, WebBrowserCheckBoxColumn).ThreeState = False
    '                End If
    '        End Select
    '    End If
    'End Sub

#End Region

#End Region

#Region "Overrides"

    Public Overloads Sub Dispose()
        For Each tmp As String In tmpFiles
            If IO.File.Exists(tmp) Then IO.File.Delete(tmp)
        Next

        GC.Collect()
        GC.Collect()
        GC.SuppressFinalize(Me)
    End Sub

#Region "Finalize"
    Protected Overrides Sub Finalize()
        'mSettings = Nothing
        ' mContextMenu = Nothing
        'mOptions = Nothing
        RaiseEvent Finalizing(Me, EventArgs.Empty)
        MyBase.Finalize()
    End Sub

#End Region

#End Region

    Public Shadows Property Name() As String Implements Interfaces.IDataListGrid.Name
        Get
            Return MyBase.Name
        End Get
        Set(ByVal value As String)
            MyBase.Name = value
        End Set
    End Property

    Public Sub ClearColumns() Implements Interfaces.IDataListGrid.ClearColumns
        'Me.Columns.Clear()
    End Sub

    Private mGridColor As Color = Color.AliceBlue
    Public Shadows Property GridColor() As Color Implements Interfaces.IDataListGrid.GridColor
        Get
            Return mGridColor
        End Get
        Set(ByVal value As Color)
            mGridColor = value
        End Set
    End Property

    'Private mHasSortedColumn As Boolean = False
    Public ReadOnly Property HasSortedColumn() As Boolean Implements Interfaces.IDataListGrid.HasSortedColumn
        Get
            ' Return mHasSortedColumn
            Return mSortedColumnName <> ""
        End Get
    End Property

    Private mSortedColumnName As String
    Public ReadOnly Property SortedColumnName() As String Implements Interfaces.IDataListGrid.SortedColumnName
        Get
            Return mSortedColumnName
        End Get
    End Property

    'Public Shadows Property Visible() As Boolean Implements Interfaces.IDataListGrid.Visible
    '    Get
    '        Return MyBase.Visible
    '    End Get
    '    Set(ByVal value As Boolean)
    '        MyBase.Visible = value
    '    End Set
    'End Property

    Private mSortOrder As GSCOM.Common.SortOrder = GSCOM.Common.SortOrder.Ascending
    Public Shadows ReadOnly Property SortOrder() As GSCOM.Common.SortOrder Implements Interfaces.IDataListGrid.SortOrder
        Get
            Return mSortOrder
        End Get
    End Property

    'Private mReadOnly As Boolean = True
    'Private Shadows Property [ReadOnly]() As Boolean Implements Interfaces.IDataListGrid.ReadOnly
    '    Get
    '        Return mReadOnly
    '    End Get
    '    Set(ByVal value As Boolean)
    '        mReadOnly = value
    '    End Set
    'End Property

    Private mDataSource As DataTable
    Public Shadows Property DataSource() As DataTable Implements Interfaces.IDataListGrid.DataSource
        Get

            Return mDataSource
        End Get
        Set(ByVal value As DataTable)
            mDataSource = value
            SetDataSource()
            If mDataSource IsNot Nothing Then
                AddHandler Me.DataSource.DefaultView.ListChanged, AddressOf ListChanged
            End If

            RaiseEvent DataSourceChanged(Me, New EventArgs)
        End Set
    End Property
    Public Property SelectionMode As Boolean
    Event DataSourceChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public ReadOnly Property AllowNew As Boolean
        Get
            Return pDataListBase.NewButton.Enabled And pDataListBase.NewButton.Visible
        End Get
    End Property

    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property RowFieldContent As String
    Public Property RowFieldHeader As String
    Public Property RowCategoryHeader As String
    Public Property RowCategory As String
    Public Property RowField As String
    ' Public Property CellContent As String
    Public Property mColor As String
    Public Property aaMenu As Integer
    Public Property StatusTable As String
    Public Property ListSource As String '= nDB.GetMenuValue(Me.Menu, "ListSource").ToString

    Friend Sub SetDataSource(Optional ByVal pCheckedRowIDs As Collections.Generic.List(Of Integer) = Nothing)
        Dim s As String = ""

        ' Dim f As String
        If mDataSource IsNot Nothing Then
            Dim tmp As String
            Dim a As Html.HtmlTreeGrid
            Select Case Me.ViewMode
                Case BrowserGridViewMode.Calendar
                    Dim cg As New Html.HtmlCalendarGrid
                    cg.DisplayColumnName = Me.CalendarViewDisplayColumnName
                    cg.DateColumnName = Me.CalendarViewDateColumnName
                    cg.AllowNew = Me.AllowNew
                    a = cg
                Case BrowserGridViewMode.PlotCalendar
                    Dim pcg As New Html.HtmlPlotCalendarGrid
                    pcg.ListSource = nDB.GetMenuValue(Me.Menu, "ListSource").ToString 'Me.ListSource
                    pcg.StatusTable = nDB.GetMenuValue(Me.Menu, "StatusTable").ToString
                    Me.ListSource = Strings.Right(pcg.ListSource, pcg.ListSource.Length - 1)
                    pcg.StartDate = CDate(Me.StartDate)
                    pcg.EndDate = CDate(Me.EndDate)
                    pcg.RowFieldContent = Me.RowFieldContent
                    pcg.RowField = Me.RowField
                    pcg.RowCategory = Me.RowCategory
                    pcg.RowFieldHeader = Me.RowFieldHeader
                    pcg.RowCategoryHeader = Me.RowCategoryHeader
                    'pcg.CellContent = Me.CellContent
                    a = pcg

                Case BrowserGridViewMode.Report
                    a = New Html.HtmlTreeGrid
                    a.PrinterFriendlyHeader = True
                    a.PrinterFriendlyFooter = True
                Case Else
                    a = New Html.HtmlTreeGrid
                    If Me.ViewMode = BrowserGridViewMode.Hierarchical Then
                        a.Hierarchical = True
                    End If

            End Select
            'Dim vInfoSet As InfoSet
            'vInfoSet = Me.pDataListBase.SetCompanyMenu(CInt(Me.Me.Menu))

            'a.ReportInfo = New Html.ReportInfo(vInfoSet, Nothing, Me.DataSource, Me.Me.Menu)

            a.ReportInfo = Me.ReportInfo
            a.SchemaTable = Me.SchemaTable
            a.Name = nDB.GetMenuValue(Me.Menu, Database.Tables.tMenu.Field.Name).ToString
            a.TabHeaderColor = nDB.GetMenuValue(Me.Menu, Database.Tables.tMenu.Field.DarkColorRGB).ToString
            a.CaptionBackColor = nDB.GetMenuValue(Me.Menu, Database.Tables.tMenu.Field.DarkColorRGB).ToString
            a.CaptionForeColor = "ffffff"
            a.mAltBackColor = nDB.GetMenuValue(Me.Menu, Database.Tables.tMenu.Field.ColorRGB).ToString
            a.ImageFile = nDB.GetMenuValue(Me.Menu, Database.Tables.tMenu.Field.ImageFile).ToString
            a.MenuID = Me.Menu
            a.SortedColumnName = mSortedColumnName
            a.SortOrder = mSortOrder
            a.ShowCaption = False
            a.SelectionMode = Me.SelectionMode
            a.AutoGenerateColumns = True

            'For Each i As Integer In Me.Checkeds
            a.CheckedRowIDs = Me.Checkeds.ToArray()

            'Next
            Dim dv As New DataView(pDataListBase.ColumnTable, "GroupSeqNo<>0", "GroupSeqNo", DataViewRowState.CurrentRows)
            For Each drv As DataRowView In dv
                s = drv("Name").ToString
                If Strings.Left(s, 3) = "ID_" Then
                    s = Strings.Right(s, s.Length - 3)
                End If
                a.Groups.Add(s, drv.Item("EffectiveLabel").ToString, "", "")
            Next

            If mDataSource IsNot Nothing AndAlso SchemaTable IsNot Nothing Then
                a.DataSource = mDataSource

                Dim dra() As DataRow
                For Each c As GSCOM.Html.HTMLTreeGridColumn In a.Columns
                    dra = nDB.MenuSet.tMenuTabField.Select("Name=" & GSCOM.SQL.SQLFormat(c.ColumnName) & " AND (SystemAggregateFunction IS NOT NULL) AND (MenuTabMenuID=" & Me.Menu & ")")
                    If dra.Length > 0 Then
                        If dra.Length > 1 Then MsgBox("More than 1 menutabfield is retrieved", vbExclamation)
                        Dim mtf As New Database.MenuTabFieldRow(dra(0))
                        c.AggregateFunction = mtf.SystemAggregateFunction()
                    End If
                Next

                s = HttpUtility.UrlDecode(a.GetHtml)

            Else
                s = ""
            End If
            tmp = IO.Path.GetTempFileName
            '--\ use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save --\
            Dim tf As New IO.StreamWriter(tmp, False, System.Text.Encoding.UTF8)
            tf.Write(s)
            tf.Flush()
            '-- use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save -/
            Try
                If Not Me.IsDisposed Then 'sample scenario: menu info was initialized thru listing, the listing was closed by user, the info was invoked thru menu shortcut (alt-click), by this time, the listing is already disposed!!
                    Me.Navigate(tmp)
                    Me.tmpFiles.Add(tmp)
                End If
                'f.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Friend Event ItemActivate(ByVal sender As Object, ByVal e As Interfaces.ItemActivateEventArgs) Implements Interfaces.IDataListGrid.ItemActivate

    'Private Sub GSWebBrowser_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserCellMouseEventArgs) Handles Me.CellMouseDoubleClick
    '    If e.RowIndex  -1 Then
    '        Dim ev As New Interfaces.ItemActivateEventArgs
    '        ev.RowIndex = e.RowIndex
    '        RaiseEvent ItemActivate(sender, ev)
    '    End If
    'End Sub

    'use mybase event for shadow
    'Private Sub MainView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Enter
    '            If Me.CurrentRow IsNot Nothing Then
    '                Dim ev As New Interfaces.ItemActivateEventArgs
    '                ev.RowIndex = Me.CurrentRow.Index
    '                RaiseEvent ItemActivate(sender, ev)
    '                e.Handled = True
    '            End If
    '    End Select
    '    RaiseEvent KeyDown(sender, e)
    'End Sub

    'Public Shadows Sub BringToFront() Implements Interfaces.IDataListGrid.BringToFront
    '    MyBase.BringToFront()
    'End Sub

    'Private mHasSelectedRow As Boolean
    'Public ReadOnly Property HasSelectedRow() As Boolean Implements Interfaces.IDataListGrid.HasSelectedRow
    '    Get
    '        Return mHasSelectedRow
    '    End Get
    'End Property
    'Private mSelectedRowID As Integer
    Public ReadOnly Property SelectedRowID As Integer Implements Interfaces.IDataListGrid.SelectedRowID

        Get
            'If Me.SelectedRows.Count > 0 Then
            '    Return CInt((Me.SelectedRows(0).Cells("ID").Value))
            'End If
            Dim vID As Integer
            vID = CInt(Me.Document.InvokeScript("fselecteddataid"))
            Return vID
        End Get
    End Property

    Public Property CheckedRowID As Integer() 'Implements Interfaces.IDataListGrid.SelectedRowID
        Get
            'Try
            '    If Me.Document Is Nothing Then Return Nothing
            '    Dim a As String
            'Dim sa() As String
            'a = CStr(Me.Document.InvokeScript("fcheckeddataid"))
            'If a <> "" Then
            '    sa = a.Split(","c)
            '    Dim ret As Integer()
            '    If sa.Length > 0 Then
            '        ReDim ret(sa.Length - 1)
            '        For ctr As Integer = 0 To sa.Length - 1
            '            ret(ctr) = CInt(sa(ctr))
            '        Next
            '        Return ret
            '    Else
            '        Return Nothing
            '    End If
            'Else
            '    Return Nothing
            '    End If
            'Catch ex As Exception
            '    Return Nothing
            'End Try
            If Me.Checkeds.Count = 0 Then
                Return Nothing
            Else
                Return Me.Checkeds.ToArray
            End If

        End Get
        Set(ByVal value As Integer())
            Me.Checkeds.Clear()
            If value IsNot Nothing Then
                For Each i As Integer In value
                    Me.Checkeds.Add(i)
                Next
            End If
        End Set
    End Property

    'Private mSelectedRowColumnValue As Object
    'Public Property SelectedRowColumnValue(ByVal pColumnName As String) As Object Implements Interfaces.IDataListGrid.SelectedRowColumnValue
    '    Get
    '        Return mSelectedRowColumnValue
    '    End Get
    '    Set(ByVal value As Object)
    '        mSelectedRowColumnValue = value
    '    End Set
    'End Property

#Region "Properties"

    'Public Overrides Property Dock() As System.Windows.Forms.DockStyle Implements Interfaces.IDataListGrid.Dock
    '    Get
    '        Dock = MyBase.Dock()
    '    End Get
    '    Set(ByVal Value As System.Windows.Forms.DockStyle)
    '        MyBase.Dock = Value
    '    End Set
    'End Property

#End Region

    'Private Sub GSWebBrowser_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserCellParsingEventArgs) Handles Me.CellParsing
    '    If e.Value.ToString =  Then
    '        e.Value = Nothing
    '        e.ParsingApplied = True

    '    End If

    'End Sub

    Public Property SchemaTable As DataTable Implements Interfaces.IDataListGrid.SchemaTable

    Public Sub LoadInfo(Optional ByVal xID As Integer = 0)
        Dim e As New GSCOM.Interfaces.ItemActivateEventArgs
        If xID = 0 Then
            e.RowIndex = Me.SelectedRowID
        Else
            e.RowIndex = xID
        End If
        RaiseEvent ItemActivate(Me, e)
    End Sub
    Public Sub NewInfo(ByVal pDate As Date)
        If Me.ViewMode = BrowserGridViewMode.Calendar Then
            Dim pInfoSet As InfoSet

            If Me.CallingInfoSet Is Nothing Then

                pInfoSet = GetInfoSet(Me.Menu)
                If pInfoSet Is Nothing Then
                    pInfoSet = ActiveModule.NewInfo(Me.Menu, Me.DataSource, 0)
                Else
                    pInfoSet.LoadInfo(0)
                End If
            Else
                pInfoSet = Me.CallingInfoSet
            End If

            Application.DoEvents()
            Dim dt As DataTable = pInfoSet.mDataset.Tables(0)
            If Not dt.Columns(Me.CalendarViewDateColumnName).ReadOnly Then
                dt.Rows(0).Item(Me.CalendarViewDateColumnName) = pDate
            End If
            If pInfoSet IsNot Nothing Then
                If Me.CallingInfoSet Is Nothing Then
                    pInfoSet.ShowDialog()

                End If
            End If

            If Me.CallingInfoSet IsNot Nothing Then
                CType(Me.pDataListBase.ParentForm, Form).Close()

            End If
        End If

    End Sub

    Public Sub NewInfo2(ByVal StartDate As Date, ByVal EndDate As Date, ByVal HotelRoom As String, ByVal HotelRoomType As String)
        If Me.ViewMode = BrowserGridViewMode.PlotCalendar Then
            If nDB.GetCompanyID.ToString <> "" Then

                Dim s As String
                Dim StatusTable As String
                StatusTable = nDB.GetMenuValue(Me.Menu, "Datasource").ToString
                s = "SELECT ID,ID_" & Strings.Right(StatusTable, StatusTable.Length - 1) & "Status,ID_" & Me.RowField & " FROM " & StatusTable & " WHERE ("
                s &= GSCOM.SQL.SQLFormat(StartDate) & " BETWEEN StartDate AND DATEADD(day,-1,EndDate) OR "
                s &= GSCOM.SQL.SQLFormat(DateAdd(DateInterval.Day, -1, EndDate)) & " BETWEEN StartDate AND DATEADD(day,-1,EndDate)) AND "

                s &= "ID_" & Me.RowField & " = " & HotelRoom

                Dim dt As DataTable
                dt = GSCOM.SQL.TableQuery(s, gConnection)
                If dt.Rows.Count > 0 Then
                    If CDbl(dt.Rows(0).Item("ID_" & Me.RowField & "Status").ToString) = 2 Or CDbl(dt.Rows(0).Item("ID_" & Me.RowField & "Status").ToString) = 1 Or CDbl(dt.Rows(0).Item("ID_" & Me.RowField).ToString) = 4 Or CDbl(dt.Rows(0).Item("ID_" & Me.RowField & "Status").ToString) = 10 Then
                        MsgBox(Me.ListSource & " is already taken", MsgBoxStyle.Exclamation)
                        Exit Sub
                    ElseIf CDbl(dt.Rows(0).Item("ID_" & Me.RowField & "Status").ToString) = 7 Then
                        MsgBox(Me.ListSource & " is Out of service", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                End If

                Dim pInfoSet As InfoSet
                If Me.CallingInfoSet Is Nothing Then

                    pInfoSet = GetInfoSet(Me.Menu)
                    If pInfoSet Is Nothing Then
                        pInfoSet = ActiveModule.NewInfo(Me.Menu, Me.DataSource, 0)
                    Else
                        pInfoSet.LoadInfo(0)
                    End If
                Else
                    pInfoSet = Me.CallingInfoSet
                End If

                Application.DoEvents()

                dt = pInfoSet.mDataset.Tables(0)
                dt.Rows(0).Item("StartDate") = StartDate
                dt.Rows(0).Item("EndDate") = EndDate
                If HotelRoomType IsNot Nothing Then
                    dt.Rows(0).Item("ID_" & Me.RowCategory) = HotelRoomType
                End If

                dt.Rows(0).Item("ID_" & Me.RowField) = HotelRoom
                If pInfoSet IsNot Nothing Then
                    If Me.CallingInfoSet Is Nothing Then
                        pInfoSet.ShowDialog()
                    End If
                End If
            Else
                MsgBox("Please Login as company.", MsgBoxStyle.Information)
            End If
            If Me.CallingInfoSet IsNot Nothing Then
                CType(Me.pDataListBase.ParentForm, Form).Close()

            End If

        End If

    End Sub
    Public Sub ShowInfo(ByVal pID As Integer)
        'MsgBox(pID)
        If Me.ViewMode = BrowserGridViewMode.PlotCalendar Then
            Dim pInfoSet As InfoSet
            pInfoSet = GetInfoSet(Me.Menu)
            If pInfoSet Is Nothing Then
                pInfoSet = ActiveModule.NewInfo(Me.Menu, Me.DataSource, 0)
            Else
                pInfoSet.LoadInfo(pID)
            End If
        End If

    End Sub

    Public Sub SortBy(ByVal pColumnName As String)
        If pColumnName <> "ImageFile" And Strings.StrComp(Strings.Right(pColumnName, 5), "Color", CompareMethod.Binary) <> 0 Then
            If mSortOrder <> Windows.Forms.SortOrder.Ascending Or mSortedColumnName <> pColumnName Then
                mSortOrder = GSCOM.Common.SortOrder.Ascending
                Me.DataSource.DefaultView.Sort = pColumnName & " ASC"
            Else
                mSortOrder = GSCOM.Common.SortOrder.Descending
                Me.DataSource.DefaultView.Sort = pColumnName & " DESC"
            End If
            mSortedColumnName = pColumnName
            Dim vSelectedRowID As Integer = Me.SelectedRowID ' save the id because the html will be reset
            SetDataSource()
            'Application.DoEvents()

        End If
    End Sub

    'Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    MyBase.OnKeyPress(e)
    '    Select Case e.KeyChar

    '        Case "a"c
    '            e = Nothing
    '            'Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
    '            '    e.Handled = True

    '    End Select
    'End Sub

    'Private Sub BrowserGrid_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles Me.Navigating
    '    If e.Url.Segments IsNot Nothing AndAlso e.Url.Segments.Length > 0 Then
    '        Dim vID As String = e.Url.Segments(e.Url.Segments.Length - 1)
    '        If IsNumeric(vID) Then
    '            Dim a As New GSCOM.Interfaces.ItemActivateEventArgs
    '            'mSelectedRowID = CInt(vID)
    '            a.RowIndex = Me.SelectedRowID '  mSelectedRowID
    '            RaiseEvent ItemActivate(Me, a)
    '            e.Cancel = True
    '        End If
    '    End If

    'End Sub

    'Private Sub BrowserGrid_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Enter
    '            LoadInfo(Me.SelectedRowID)
    '    End Select
    'End Sub

    'Private Function GetScrollToElementScript(ByVal elementID As String, ByVal scrollElement As String) As String
    '    Try
    '        Dim StringBuilder As New System.Text.StringBuilder
    '        With StringBuilder
    '            .Append("<script language=""javascript"">" & vbCrLf)
    '            .Append(vbTab & "<!--" & vbCrLf)

    '            .Append(vbTab & vbTab & "el = document.getElementById('" & elementID & "');" & vbCrLf)

    '            If scrollElement = "window" Then
    '                .Append(vbTab & vbTab & "window.scroll(findPosX(el), findPosY(el));" & vbCrLf)
    '            Else
    '                .Append(vbTab & vbTab & "var scrollElement = document.getElementById('" & scrollElement & "');" & vbCrLf)

    '                '---Scroll the to the element inside of the other making sure to offset the elements scroll position
    '                '   by the position of the element being scrolled.
    '                .Append(vbTab & vbTab & "scrollElement.scrollLeft = findPosX(el) - findPosX(scrollElement);" & vbCrLf)
    '                .Append(vbTab & vbTab & "scrollElement.scrollTop = findPosY(el) - findPosY(scrollElement);" & vbCrLf)
    '            End If

    '            .Append(vbTab & vbTab & "function findPosX(obj)" & vbCrLf)
    '            .Append(vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "var curleft = 0;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "if (obj.offsetParent)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "while (obj.offsetParent)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & vbTab & "curleft += obj.offsetLeft" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & vbTab & "obj = obj.offsetParent;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "}" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "else if (obj.x)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "curleft += obj.x;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "return curleft;" & vbCrLf)
    '            .Append(vbTab & vbTab & "}" & vbCrLf)

    '            .Append(vbTab & vbTab & "function findPosY(obj)" & vbCrLf)
    '            .Append(vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "var curtop = 0;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "if (obj.offsetParent)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "while (obj.offsetParent)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "{" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & vbTab & "curtop += obj.offsetTop" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & vbTab & "obj = obj.offsetParent;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "}" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "}" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "else if (obj.y)" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & vbTab & "curtop += obj.y;" & vbCrLf)
    '            .Append(vbTab & vbTab & vbTab & "return curtop;" & vbCrLf)
    '            .Append(vbTab & vbTab & "}" & vbCrLf)

    '            .Append(vbTab & "//-->" & vbCrLf)
    '            .Append("</script>" & vbCrLf)
    '        End With

    '        Return StringBuilder.ToString
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Friend CallingInfoSet As InfoSet

    Private Sub BrowserGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub BrowserGrid_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
        RaiseEvent KeyDown(sender, New KeyEventArgs(e.KeyCode))
    End Sub

    Dim Checkeds As New Collections.Generic.List(Of Integer)
    Sub AddRemoveChecked(ByVal xID As Integer, ByVal pAdd As Boolean)
        If pAdd Then
            If Not Checkeds.Contains(xID) Then Checkeds.Add(xID)
        Else
            If Checkeds.Contains(xID) Then Checkeds.Remove(xID)
        End If
    End Sub
End Class

Public Enum BrowserGridViewMode
    List = 0
    Calendar = 1
    PlotCalendar = 2
    Hierarchical = 3
    Report = 4
End Enum