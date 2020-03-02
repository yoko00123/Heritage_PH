Option Explicit On
Option Strict On



Friend Class ServiceChargeInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tServiceCharge(Connection)
    'gLen.code 20110416
    'Private mtServiceCharge_Detail As New Database.Tables.tServiceCharge_Detail(Connection)
    'Private mtServiceChargePaymentSchedule As New Database.Tables.tServiceChargePaymentSchedule(Connection)
    Private mtServiceCharge_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tServiceCharge_Detail(Connection)
    Private mtServiceChargePaymentSchedule As GSCOM.SQL.ZDataTable 'New Database.Tables.tServiceChargePaymentSchedule(Connection)
    '###
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.ServiceChargeControl
    Private mLoadButton As ToolStripButton
    ' Private mReportViewerSummary As CrystalDecisions.Windows.Forms.CrystalReportViewer
    ' Private mReportViewerDetail As CrystalDecisions.Windows.Forms.CrystalReportViewer

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            'gLen.code 20110416
            '.Add(mtServiceCharge_Detail)
            '.Add(mtServiceChargePaymentSchedule)
            '###
        End With
        InitControl(pMenu)

        'gLen.code 20110416
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tServiceCharge.Field.ID)
        'cdc = mtServiceCharge_Detail.Columns(Database.Tables.tServiceCharge_Detail.Field.ID_ServiceCharge)
        'rel = mDataset.Relations.Add(pdc, cdc)
        'cdc = mtServiceChargePaymentSchedule.Columns(Database.Tables.tServiceChargePaymentSchedule.Field.ID_ServiceCharge)
        'rel = mDataset.Relations.Add(pdc, cdc)
        'mtServiceCharge_Detail = DirectCast(Me.mDataset.Tables("tServiceCharge_Detail"), GSCOM.SQL.ZDataTable)
        'mtServiceChargePaymentSchedule = DirectCast(Me.mDataset.Tables("tServiceChargePaymentSchedule"), GSCOM.SQL.ZDataTable)
        '###

        myDT.Columns(Database.Tables.tServiceCharge.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        'mLoadButton = MyBase.AddButton("Load", gMainForm.imgList.Images("ImportFile.png"), AddressOf Load_Employee)

        'mReportViewerDetail = AddReportViewer("Service Charge Report")
        'mReportViewerDetail.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        '  mLoadButton.Enabled = (pID <> 0) ' new mode
        MyBase.LoadInfo(pID)

        'Dim sb As New System.Text.StringBuilder
        'sb.Append(" dbo.fEmployeeIsUnderUser(ID_Employee," & gUser & ") = 1")

        'mtServiceCharge_Detail.ClearThenFill(sb.ToString)

        'Dim dt1 As DataTable
        'dt1 = GSCOM.SQL.TableQuery("SELECT * FROM vzServiceCharge WHERE ID_ServiceCharge=" & myDT.Get(Database.Tables.tServiceCharge.Field.ID).ToString, Connection)
        'Dim rd1 As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rd1.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "ServiceChargeReport.rpt")
        'rd1.SetDataSource(dt1)
        'mReportViewerDetail.ReportSource = rd1
        'mReportViewerDetail.Zoom(1)

    End Sub


#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tServiceCharge)
        End Set
    End Property



    'Protected Overrides Function CanSave() As Boolean
    '    If mtServiceChargePaymentSchedule Is Nothing Then Return True
    '    Dim dra As DataRow()
    '    Dim Total As Decimal
    '    Dim Valid As Boolean = False

    '    dra = mtServiceChargePaymentSchedule.Select()

    '    For Each dr As DataRow In dra
    '        Total = Total + CDec(dr.Item(Database.Tables.tServiceChargePaymentSchedule.Field.Percentage.ToString))
    '    Next

    '    If Total = 100 Then
    '        Valid = True
    '    End If
    '    Return Valid
    'End Function

#End Region

    '#Region "Procedures"

    'Private Sub Load_Employee(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Load data?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        BeginProcess("Loading Service Charge Details... Please wait.")
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pServiceCharge " & myDT.Get(Database.Tables.tServiceCharge.Field.ID).ToString & ", " & gUser, Connection)
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tServiceCharge.Field.ID)))
    '        Application.DoEvents()
    '        EndProcess("")
    '        MsgBox("Finished Loading Service Charge.", MsgBoxStyle.Information)

    '    End If
    'End Sub

    '#End Region

End Class
