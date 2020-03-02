Option Explicit On
Option Strict On

Public MustInherit Class DataListControl
    Inherits System.Windows.Forms.UserControl
    Implements GSCOM.Interfaces.ZIDataList

    Protected m_TableName As String

    Public ReadOnly Property TableName() As String
        Get
            TableName = m_TableName
        End Get
    End Property

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Me.NewButton.Image = GSCOM.UI.Common.NewButtonImage
        Me.OpenButton.Image = GSCOM.UI.Common.OpenButtonImage
        Me.RequeryButton.Image = GSCOM.UI.Common.RequeryButtonImage
        Me.FilterBarButton.Image = GSCOM.UI.Common.FilterButtonImage
    End Sub

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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MainFilter As GSCOM.UI.DataFilter.DataFilter
    Protected WithEvents MyCaption As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RequeryButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CloseButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FilterBarButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataListControl))
        Me.MainFilter = New GSCOM.UI.DataFilter.DataFilter
        Me.MyCaption = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.NewButton = New System.Windows.Forms.ToolStripButton
        Me.OpenButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.RequeryButton = New System.Windows.Forms.ToolStripButton
        Me.CloseButton = New System.Windows.Forms.ToolStripButton
        Me.FilterBarButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainFilter
        '
        Me.MainFilter.AutoScroll = True
        Me.MainFilter.Dock = System.Windows.Forms.DockStyle.Right
        Me.MainFilter.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.MainFilter.Location = New System.Drawing.Point(332, 0)
        Me.MainFilter.Name = "MainFilter"
        Me.MainFilter.Size = New System.Drawing.Size(168, 334)
        Me.MainFilter.TabIndex = 7
        '
        'MyCaption
        '
        Me.MyCaption.BackColor = System.Drawing.Color.OliveDrab
        Me.MyCaption.Dock = System.Windows.Forms.DockStyle.Top
        Me.MyCaption.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyCaption.ForeColor = System.Drawing.Color.White
        Me.MyCaption.Location = New System.Drawing.Point(0, 0)
        Me.MyCaption.Name = "MyCaption"
        Me.MyCaption.Size = New System.Drawing.Size(500, 16)
        Me.MyCaption.TabIndex = 5
        Me.MyCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewButton, Me.OpenButton, Me.ToolStripSeparator1, Me.RequeryButton, Me.CloseButton, Me.FilterBarButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip1.Size = New System.Drawing.Size(500, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 1
        '
        'NewButton
        '
        Me.NewButton.Enabled = False
        Me.NewButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(35, 22)
        Me.NewButton.Text = "New"
        '
        'OpenButton
        '
        Me.OpenButton.Enabled = False
        Me.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(40, 22)
        Me.OpenButton.Text = "Open"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'RequeryButton
        '
        Me.RequeryButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RequeryButton.Name = "RequeryButton"
        Me.RequeryButton.Size = New System.Drawing.Size(54, 22)
        Me.RequeryButton.Text = "Requery"
        '
        'CloseButton
        '
        Me.CloseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.CloseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CloseButton.Image = CType(resources.GetObject("CloseButton.Image"), System.Drawing.Image)
        Me.CloseButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(23, 22)
        Me.CloseButton.Text = "Close"
        '
        'FilterBarButton
        '
        Me.FilterBarButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.FilterBarButton.Checked = True
        Me.FilterBarButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilterBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FilterBarButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.FilterBarButton.Name = "FilterBarButton"
        Me.FilterBarButton.Size = New System.Drawing.Size(23, 22)
        Me.FilterBarButton.Text = "FilterBar"
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(500, 334)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 16)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(500, 359)
        Me.ToolStripContainer1.TabIndex = 2
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'DataListControl
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Controls.Add(Me.MyCaption)
        Me.Name = "DataListControl"
        Me.Size = New System.Drawing.Size(500, 375)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Variables"
    Protected mReportPath As String

    Protected m_SelectString As String
    Public Property m_SelectStringBuild As String
    
    Protected m_Connection As Data.SqlClient.SqlConnection

    Friend Event Finalizing()


    Public AutoFillTable As Boolean = True
    Protected mInited As Boolean

    Friend Event ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs) 'Implements GSCOM.Interfaces.

    Friend Event CtrlButtonClick(ByVal sender As ToolStripButton, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)


#End Region

#Region "Overrides"
#Region "Finalize"
    Protected Overrides Sub Finalize()
        RaiseEvent Finalizing()
        MyBase.Finalize()
    End Sub
#End Region
#End Region

#Region "Properties"

#Region "SelectString"
    Friend Property SelectString() As String
        Get
            SelectString = m_SelectString
        End Get
        Set(ByVal Value As String)
            m_SelectString = Value
        End Set
    End Property

#End Region

#Region "DataSource"
    Protected mDataSource As DataTable

    Public MustOverride Property DataSource() As DataTable Implements GSCOM.Interfaces.ZIDataList.DataSource

    Friend Function GetLookUpFilter(ByVal pText As String) As GSCOM.UI.DataFilter.LookUpFilter
        Dim fc As GSCOM.UI.DataFilter.FilterControl
        fc = MainFilter.GetFilter(pText)
        If fc IsNot Nothing Then
            Return CType(fc.GetFilterBase, GSCOM.UI.DataFilter.LookUpFilter)
        End If
        Return Nothing
    End Function

    Friend Function GetDateTimeFilter(ByVal pText As String) As GSCOM.UI.DataFilter.DateTimeFilter
        Dim fc As GSCOM.UI.DataFilter.FilterControl
        fc = MainFilter.GetFilter(pText)
        If fc IsNot Nothing Then
            Return CType(fc.GetFilterBase, GSCOM.UI.DataFilter.DateTimeFilter)
        End If
        Return Nothing
    End Function

    'Protected Function GetFilterValue(ByVal pText As String) As Object
    '    Dim fc As GSCOM.UI.DataFilter.FilterControl
    '    fc = MainFilter.GetFilter(pText)
    '    If fc IsNot Nothing Then
    '        Return fc.GetFilterBase.Value
    '    End If
    '    Return Nothing
    'End Function

    '20071026
    Friend Function GetNumericFilter(ByVal pText As String) As GSCOM.UI.DataFilter.NumericFilter
        Dim fc As GSCOM.UI.DataFilter.FilterControl
        fc = MainFilter.GetFilter(pText)
        If fc IsNot Nothing Then
            Return CType(fc.GetFilterBase, GSCOM.UI.DataFilter.NumericFilter)
        End If
        Return Nothing
    End Function

#End Region

    Private mFixedFilter As String

    Property FixedFilter() As String Implements GSCOM.Interfaces.ZIDataList.FixedFilter
        Get
            Return mFixedFilter
        End Get
        Set(ByVal value As String)
            mFixedFilter = value
        End Set
    End Property


    Property Caption() As String Implements GSCOM.Interfaces.ZIDataList.Text
        Get
            Caption = MyCaption.Text
        End Get
        Set(ByVal Value As String)
            MyCaption.Text = Value
        End Set
    End Property


#End Region

#Region "Sub"
    'Dim p As New PictureBox
#Region "Init"
    Public Sub Init(ByVal tableName As String, ByVal c As Data.SqlClient.SqlConnection, ByVal uniqueKey As String) ', ByVal vAutoFillTable As Boolean)
        Dim s As String
        s = uniqueKey & "." & tableName
        m_TableName = tableName
        m_Connection = c
        'AutoFillTable = vAutoFillTable
        FillDatabase("1=0")
        RefreshFilters()
        mInited = True
        'Me.Controls.Add(p)
    End Sub

#End Region

   

    Private mParameterTable As New Database.ParameterDataTable

    Friend ReadOnly Property ParameterTable() As Database.ParameterDataTable
        Get
            Return mParameterTable
        End Get
    End Property

    Protected Function PassParameters(ByVal s As String) As String
        Return Database.gPassParameters(mParameterTable, s)
    End Function

#Region "FillDatabase"
    Private Sub FillDatabase(Optional ByVal vRowFilter As String = "")
        Dim sda As SqlClient.SqlDataAdapter = Nothing
        Dim m_table As DataTable = Nothing
        Dim s As String = m_TableName
        Try
            '20060417
            If Not AutoFillTable Then Exit Sub
            'CheckCount()
            sda = New SqlClient.SqlDataAdapter
            'robbie 20061207---------------------------function-\
            mParameterTable.Rows.Clear()
            If vRowFilter = "1=0" Then
                'mParameterTable.Rows.Add("@ID_PayrollPeriod", "0")
                'mParameterTable.Rows.Add("@ID_EmployeeDailyScheduleView", "0") 'must preceed @ID_Employee to avoid "0DailyScheduleView"
                'mParameterTable.Rows.Add("@ID_EmployeeStatus", "0") 'must preceed @ID_Employee to avoid "0Status
                'mParameterTable.Rows.Add("@ID_Employee", "0")
                'mParameterTable.Rows.Add("@ID_PayrollScheme", "0")
                'mParameterTable.Rows.Add("@ID_Month", "0")
                'mParameterTable.Rows.Add("@ID_Branch", "0")
                'mParameterTable.Rows.Add("@ID_Department", "0")
                'mParameterTable.Rows.Add("@ID_Designation", "0")
                'mParameterTable.Rows.Add("@ID_PayrollFrequency", "0")
                For Each dr As DataRow In nDB.SystemQueryParameterTable.Select
                    mParameterTable.Rows.Add(dr("Name").ToString, dr("DefaultValue").ToString)
                Next

                mParameterTable.Rows.Add("@Year", "0")
                mParameterTable.Rows.Add("@FromDate", "NULL")
                mParameterTable.Rows.Add("@ToDate", "NULL")
                mParameterTable.Rows.Add("@StartDate", "NULL")
                mParameterTable.Rows.Add("@EndDate", "NULL")
                mParameterTable.Rows.Add("@Date", "NULL")
            Else
                Dim f As GSCOM.UI.DataFilter.LookUpFilter
                Dim o As Object
                '''''''''''''''''''''''
                'f = GetLookUpFilter("Pay Period")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_PayrollPeriod", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Employee Daily Schedule View")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_EmployeeDailyScheduleView", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Employee")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_Employee", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Pay Scheme")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_PayrollScheme", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("EmployeeStatus")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_EmployeeStatus", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Branch")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_Branch", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Department")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_Department", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Designation")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_Designation", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Pay Frequency")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_PayrollFrequency", GSCOM.SQL.SQLFormat(o))
                'End If
                ''''''''''''''''''''''''
                'f = GetLookUpFilter("Month")
                'If f IsNot Nothing Then
                '    o = f.UpperFilter.SelectedValue
                '    mParameterTable.Rows.Add("@ID_Month", GSCOM.SQL.SQLFormat(o))
                'End If
                For Each dr As DataRow In nDB.SystemQueryParameterTable.Select
                    'f = GetLookUpFilter(dr("Label").ToString)
                    f = GetLookUpFilter(Strings.Right(dr("Name").ToString, dr("Name").ToString.Length - 1))
                    If f IsNot Nothing Then
                        o = f.UpperFilter.SelectedValue
                        mParameterTable.Rows.Add(dr("Name").ToString, GSCOM.SQL.SQLFormat(o))
                    End If
                Next
                '20071026----------------------------------------\
                Dim nf As GSCOM.UI.DataFilter.NumericFilter
                nf = GetNumericFilter("Year")
                If nf IsNot Nothing Then
                    o = CDec(nf.UpperFilter.Text)
                    mParameterTable.Rows.Add("@Year", GSCOM.SQL.SQLFormat(o))
                End If

           
                '20071026----------------------------------------/




                Dim dtf As GSCOM.UI.DataFilter.DateTimeFilter
                dtf = GetDateTimeFilter("Date")
                If dtf IsNot Nothing Then
                    o = dtf.UpperFilter.Value
                    mParameterTable.Rows.Add("@FromDate", GSCOM.SQL.SQLFormat(o))
                    mParameterTable.Rows.Add("@StartDate", GSCOM.SQL.SQLFormat(o))
                    mParameterTable.Rows.Add("@Date", GSCOM.SQL.SQLFormat(o))
                    o = dtf.LowerFilter.Value
                    mParameterTable.Rows.Add("@ToDate", GSCOM.SQL.SQLFormat(o))
                    mParameterTable.Rows.Add("@EndDate", GSCOM.SQL.SQLFormat(o))
                End If
            End If
            s = PassParameters(s)
            m_table = New DataTable(s)
            Try
                If m_SelectString = "" Then
                    sda.SelectCommand = New SqlClient.SqlCommand("SELECT * FROM " & m_table.TableName, m_Connection)
                Else
                    sda.SelectCommand = New SqlClient.SqlCommand(m_SelectString & "FROM " & m_table.TableName, m_Connection)
                End If
                sda.SelectCommand.CommandTimeout = 0
                If vRowFilter <> "" Then
                    sda.SelectCommand.CommandText &= " WHERE " & vRowFilter
                End If

                sda.Fill(m_table)
            Catch ex As Exception
                sda.SelectCommand = New SqlClient.SqlCommand("SELECT * FROM " & m_table.TableName, m_Connection)
                sda.SelectCommand.CommandTimeout = 0
                If vRowFilter <> "" Then
                    sda.SelectCommand.CommandText &= " WHERE " & vRowFilter
                End If

                sda.Fill(m_table)
            End Try

            m_SelectStringBuild = sda.SelectCommand.CommandText
            DataSource = m_table

        Catch ex As Exception

            Throw ex
        Finally
            If Not IsNothing(sda) Then sda.Dispose()
            If Not IsNothing(m_table) Then m_table.Dispose()
        End Try
    End Sub

#End Region

#Region "CheckCount"
    Public Sub CheckCount()
        Dim sda As SqlClient.SqlDataAdapter
        Dim t As DataTable
        Dim q As String
        Try
            sda = New SqlClient.SqlDataAdapter
            t = New DataTable("CountCheck")
            q = "SELECT COUNT(*) FROM " & m_TableName
            sda.SelectCommand = New SqlClient.SqlCommand(q, m_Connection)
            sda.Fill(t)
            MsgBox(t.Rows(0).Item(0))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private mAutoGenerateFilters As Boolean = True
    Public Property AutoGenerateFilters() As Boolean Implements Interfaces.ZIDataList.AutoGenerateFilters
        Get
            Return mAutoGenerateFilters
        End Get
        Set(ByVal value As Boolean)
            mAutoGenerateFilters = value
        End Set
    End Property

#Region "Go"
    Public Sub RefreshFilters() 'Implements GSCOM.Interfaces.ZIDataList.RefreshFilters
        If mAutoGenerateFilters Then
            MainFilter.ClearFilters()
            MainFilter.CreateFilters(CType(DataSource, DataTable).Columns)
        End If
    End Sub

#End Region

#Region "BeginProgress"
    'Private Sub BeginProgress(ByVal max As Integer)
    '    MyProgressBar.Value = 0
    '    MyProgressBar.Maximum = max
    '    MyControlPanel.Visible = False
    '    CancelPanel.Visible = True
    '    'MyCancelButton.Focus()
    'End Sub

#End Region

#Region "EndProgress"
    'Private Sub EndProgress()
    '    CancelPanel.Visible = False
    '    MyControlPanel.Visible = True
    'End Sub
#End Region

#End Region

#Region "Events"

    '#Region "SortHighlightEnabled_CheckedChanged"
    '    Private Sub SortHighlightEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortHighlightEnabled.CheckedChanged
    '        SortHighlight = SortHighlightEnabled.Checked
    '    End Sub

    '#End Region

#Region "ReloadButton_Click"
    Private Sub ReloadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshFilters()
    End Sub

#End Region

#Region "ColumnsButton_Click"
    'Private Sub ColumnsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColumnsButton.Click
    '    Dim a As New GSCOM.UI.DataList.ColumnsDialog(m_TableName, m_Connection, MainView.Columns)
    '    If a.ShowDialog() = DialogResult.OK Then
    '        m_SelectString = a.SelectString
    '        FillDatabase()
    '        Go()
    '    End If
    'End Sub

#End Region

#Region "MyCancelButton_Click"
    'Private Sub MyCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyCancelButton.Click
    '    MainView.CancellFill()
    'End Sub

#End Region

#Region "Mainview Events"


#Region "MainView_RefreshedColors"
    'Private Sub MainView_RefreshedColors(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainView.RefreshedColors
    '    Dim r, g, b As Integer
    '    r = CInt(MainView.OddBackColor.R / 1.75)
    '    g = CInt(MainView.OddBackColor.G / 1.75)
    '    b = CInt(MainView.OddBackColor.B / 1.75)
    '    MyCaption.BackColor = MainView.HighlightBackColor
    '    MyCaption.BackColor = Color.FromArgb(r, g, b)
    '    MainView.GridColor = MyCaption.BackColor
    'End Sub

#End Region

#Region "MainFilter_Changed"
    Private Sub MainFilter_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainFilter.Changed
        Try
            CType(mDataSource, DataTable).DefaultView.RowFilter = MainFilter.GetFilterString
            'ROB 20060728

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#End Region

#End Region

#Region "ReportPath"
    Public Property ReportPath() As String Implements GSCOM.Interfaces.ZIDataList.ReportPath
        Get
            ReportPath = mReportPath
        End Get
        Set(ByVal value As String)
            mReportPath = value
        End Set
    End Property

#End Region

#Region "OddBackColor"
    Public Property OddBackColor() As Color Implements Interfaces.ZIDataList.OddBackColor
        Get
            Return Nothing
        End Get
        Set(ByVal value As Color)
            'NO CODE
        End Set
    End Property

#End Region

    Public Sub EnableButton(ByVal pButtonText() As String) Implements GSCOM.Interfaces.ZIDataList.EnableButton
        'NOCODE
    End Sub

    Public Event FilteredRowCountChanged As EventHandler Implements Interfaces.ZIDataList.FilteredRowCountChanged
    Public mFilteredRowCount As Integer

    Public ReadOnly Property FilteredRowCount() As Integer Implements Interfaces.ZIDataList.FilteredRowCount
        Get
            Return mFilteredRowCount
        End Get
    End Property

    Private Sub RequeryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequeryButton.Click
        ClickButton(RequeryButton)
    End Sub

    Private Sub FilterBarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterBarButton.Click
        ToggleFilterBarVisible()
    End Sub

    Private Sub ToggleFilterBarVisible()
        FilterBarVisible = Not FilterBarVisible
    End Sub

    Public Property FilterBarVisible() As Boolean
        Get
            Return FilterBarButton.Checked()
        End Get
        Set(ByVal value As Boolean)
            FilterBarButton.Checked = value
            MainFilter.Visible = FilterBarButton.Checked
        End Set
    End Property

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        ClickButton(CloseButton)
    End Sub

#Region "EndEdit"
    Public Sub EndEdit()
        ToolStrip1.Focus()
        MainFilter.Focus()
        ToolStrip1.Focus()
    End Sub

#End Region

#Region "ClickButton"
    Private Sub ClickButton(ByVal sender As ToolStripButton)
        Dim vID As Integer
        Dim ctlr As Boolean = My.Computer.Keyboard.CtrlKeyDown
        Dim shft As Boolean = My.Computer.Keyboard.ShiftKeyDown
        'ROBBIE NOTE: this function should only raise the event ButtonClicked..
        '.. only if the user is able to manually click the button 
        If sender.Enabled AndAlso sender.Visible Then
            Select Case sender.Text
                Case RequeryButton.Text
                    'ROBBIE ENDEDIT!!!!!!!!!!!!!!!!!!!!!!!!!!
                    'TO TRIGGER VALIDATION EVENTS
                    Me.EndEdit()

                    FillDatabase(MainFilter.GetFilterString)
            End Select
            'If MainView.SelectedRows.Count > 0 Then
            ' vID = CInt(MainView.SelectedRows(0).Cells("ID").Value)
            'End If
            Dim e As New GSCOM.UI.DataList.DataList.ButtonClickEventArgs(Me)
            With e
                .ButtonText = sender.Text
                .ListCaption = MyCaption.Text
                .SelectedID = vID
            End With

            If ctlr AndAlso shft Then RaiseEvent CtrlButtonClick(sender, e)
            RaiseEvent ButtonClick(Me, e)
        End If
    End Sub

#End Region

    Friend ReadOnly Property Filter() As GSCOM.UI.DataFilter.DataFilter Implements Interfaces.ZIDataList.Filter
        Get
            Return MainFilter
        End Get
    End Property
End Class

