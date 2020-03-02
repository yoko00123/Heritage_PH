Option Explicit On
Option Strict On



Friend Class InfoSetReportViewer
    Public mPage As TabPage
    Protected mList As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Property DataSource As String
    Property ReportFile As String
    Property ReportTitle As String
    'Public Property SubDataSource As DataRow()
    Public ReadOnly Property List As CrystalDecisions.Windows.Forms.CrystalReportViewer
        Get
            Return mList
        End Get
    End Property

    Public Property Table As DataTable

    'Private Property FixedFilter As String
    '    Get
    '        Return mPage.MainList.FixedFilter
    '    End Get
    '    Set(ByVal value As String)
    '        mPage.MainList.FixedFilter = value
    '    End Set
    'End Property

    Friend Property Enabled As Boolean
        Get
            Return mList.Enabled
        End Get
        Set(ByVal value As Boolean)
            mList.Enabled = value
        End Set
    End Property

    'Protected mID As String
    'Protected mChildColumnName As String

    'Friend Property ParentColumnName As String

    Protected Friend Sub Go(ByVal dt As DataTable)
        Me.Table = dt
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd.Load(IO.Path.Combine(nDB.GetSetting(Database.SettingEnum.ReportPath), Me.ReportFile))
        For Each dtt As CrystalDecisions.CrystalReports.Engine.Table In rd.Database.Tables

        Next

        '      rd.SetDatabaseLogon(gDBUser, gDBPassword) ', gDBDataSource, gDBInitialCatalog)
        rd.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
        For Each rrd As CrystalDecisions.CrystalReports.Engine.ReportDocument In rd.Subreports
            rrd = rd.OpenSubreport(rrd.Name)
            For Each dsc As CrystalDecisions.Shared.IConnectionInfo In rrd.DataSourceConnections
                dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
            Next
            rrd.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
        Next
        rd.SetDataSource(Me.Table)
        rd.SummaryInfo.ReportTitle = Me.ReportTitle
        mList.ReportSource = rd
        mList.Zoom(1)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub


    'Protected Friend Sub Go(ByVal ds As DataSet, ms As Database.MenuSetClass)
    '    'Me.Table = dt
    '    'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    'rd.Load(IO.Path.Combine(nDB.GetSetting(Database.SettingEnum.ReportPath), Me.ReportFile))
    '    'rd.SetDataSource(Me.Table)
    '    'rd.SummaryInfo.ReportTitle = Me.ReportTitle
    '    'mList.ReportSource = rd
    '    'mList.Zoom(1)
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    Dim dt As DataTable = ds.Tables(0)
    '    Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    Dim fn As String
    '    If Not dt Is Nothing Then
    '        rd = New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '        fn = IO.Path.Combine(nDB.GetSetting(Database.SettingEnum.ReportPath), Me.ReportFile)
    '        rd.Load(fn)
    '        rd.SetDatabaseLogon(gDBUser, gDBPassword) ', gDBDataSource, gDBInitialCatalog)
    '        rd.SetDataSource(CType(dt, DataTable))
    '        If ds.Tables.Count > 0 Then
    '            For Each rdx As CrystalDecisions.CrystalReports.Engine.ReportDocument In rd.Subreports
    '                '1
    '                rdx = rd.OpenSubreport(rd.Name)

    '                Dim dr As DataRow = ms.tMenuDetailTab.Select("ReportFile=" & GSCOM.SQL.SQLFormat(rd.Name))(0)
    '                Dim mdtr As New Database.MenuDetailTabRow(dr)

    '                'rdx.SetDataSource(ds.Tables(rd.Name))
    '                rdx.SetDataSource(ds.Tables(mdtr.TableName))
    '                '2
    '            Next
    '        End If
    '        rd.SummaryInfo.ReportTitle = Me.ReportTitle
    '        mList.ReportSource = rd
    '        mList.Zoom(1)  'rob
    '    End If
    'End Sub

    'Private mMenu As Database.Menu

    'Sub New(ByVal pMenu As Database.Menu, ByVal pParentColumnName As String, ByVal pChildColumnName As String)
    '    mMenu = pMenu
    '    Me.ParentColumnName = pParentColumnName
    '    mChildColumnName = pChildColumnName
    '    Dim mr As New Database.MenuRow
    '    Dim dra() As DataRow = nDB.MenuTable.Select("ID=" & CInt(mMenu).ToString)
    '    If dra.Length > 0 Then
    '        mr.InnerRow = dra(0)
    '        Dim vDisplayColumnsTable As GSCOM.SQL.ZDataTable = nDB.GetDisplayColumnsTable(mMenu) 'vSettings.GetSetting(UI.DataList.Settings.KeyEnum.SelectString, "* ")
    '        mPage = New BrowserDataListTabPage(mr.DataSource, gConnection, "1=0", vDisplayColumnsTable, mr.Sort, mMenu)
    '        mList = CType(mPage.MainList, BrowserDataList)
    '        mList.CloseButton.Enabled = False

    '        InitDataListBase(mPage, mr)
    '        InitZIDataTabPageList(mPage, mr)

    '        AddHandler mPage.ButtonClick, AddressOf Me.ButtonClick

    '    Else
    '        MsgBox("New InfoSetDetailMenu", MsgBoxStyle.Exclamation)
    '    End If

    'End Sub

    'Protected Overridable Sub ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)

    '    Dim dt As DataTable = CType(mPage.MainList.DataSource, DataTable)
    '    Select Case e.ButtonText
    '        Case "New"
    '            Dim pInfoSet As InfoSet
    '            pInfoSet = GetInfoSet(mMenu)
    '            'IMPORTANT: SAVE THE ID OF THE RECORD THAT 
    '            'TRIGGERED THE LOADING OF INFO BECAUSE mID IS 
    '            'RESET TO 0 ON LOADINFO() FUNCTION IF SAME MODULE
    '            Dim vCallingRecordID As Integer = CInt(mID) 'mID is string for GUID only.. e.g. FileBrowser
    '            If pInfoSet Is Nothing Then
    '                pInfoSet = NewInfo(mMenu, dt, 0)
    '            Else
    '                pInfoSet.LoadInfo(0) 'mID IS RESET TO 0 IF SAME MODULE
    '            End If
    '            pInfoSet.mDataset.Tables(0).Rows(0).Item(Me.mChildColumnName) = vCallingRecordID 'INITIALIZE THE COLUMN. SET TO PARENT
    '            Application.DoEvents()
    '            If pInfoSet IsNot Nothing Then
    '                pInfoSet.ShowDialog()
    '            End If
    '        Case Else
    '            MainModule.SelectStandardMenu(mPage, dt, e, mPage)
    '    End Select

    'End Sub

    Property Text As String
        Get
            Return mPage.Text
        End Get
        Set(ByVal value As String)
            mPage.Text = value
        End Set
    End Property


    Public Sub New()
        mPage = New TabPage
        mList = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        mList.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        mPage.Controls.Add(mList)
        mList.Dock = DockStyle.Fill
        mList.BringToFront()
    End Sub
End Class
