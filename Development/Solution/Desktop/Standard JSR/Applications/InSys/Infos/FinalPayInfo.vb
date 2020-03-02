Option Explicit On
Option Strict Off



Friend Class FinalPayInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tFinalPay(Connection)
    Private mControl As New InSys.DataControl
    Private mtFinalPay_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tFinalPay_Detail(Connection)
    Private mtFinalPay_LeaveConversion As GSCOM.SQL.ZDataTable 'New Database.Tables.tFinalPay_LeaveConversion(Connection)
    Private mtFinalPay_Annualization As GSCOM.SQL.ZDataTable 'New Database.Tables.tFinalPay_Annualization(Connection)
    Private mtFinalPayLoan As GSCOM.SQL.ZDataTable 'New Database.Tables.tFinalPayLoan(Connection)

    Private mReportViewerSummary As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mReportViewerDetail As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mGeneratePayrollButton As ToolStripButton
    Private mGenerate13thMonthButton As ToolStripButton
    Private mGenerateLeaveConversionButton As ToolStripButton
    Private mGenerateLoanPaymentButton As ToolStripButton
    Private mApplyConvertedLeaves As ToolStripButton
    Private mApplyLoanPayment As ToolStripButton
    Private mAnnualizeButton As ToolStripButton
    Private mReportViewer2316 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mHideZero As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With
        InitControl(pMenu)

        mtFinalPay_Detail = DirectCast(Me.mDataset.Tables("tFinalPay_Detail"), GSCOM.SQL.ZDataTable)
        mtFinalPay_LeaveConversion = DirectCast(Me.mDataset.Tables("tFinalPay_LeaveConversion"), GSCOM.SQL.ZDataTable)
        mtFinalPayLoan = DirectCast(Me.mDataset.Tables("tFinalPayLoan"), GSCOM.SQL.ZDataTable)
        mtFinalPay_Annualization = DirectCast(Me.mDataset.Tables("tFinalPay_Annualization"), GSCOM.SQL.ZDataTable)

        Me.ReloadAfterCommit = True


        mReportViewerDetail = AddReportViewer("Final Pay Report")
        mReportViewerDetail.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None


        mReportViewer2316 = AddReportViewer("2316 Report")
        mReportViewer2316.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        mHideZero = MyBase.AddButton("Hide Zero Amount", gMainForm.imgList.Images("GeneratePayroll.png"), AddressOf HideZero)
        mHideZero.CheckOnClick = True
        
        AfterNew()
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tFinalPay)
        End Set
    End Property


    'Private Sub FinalPayInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
    ' If e.RowState = DataRowState.Modified Then
    'GSCOM.SQL.ExecuteNonQuery("pFinalPay_Edit " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, e.Transaction)
    ' End If
    'GSCOM.SQL.ExecuteNonQuery("pUpdateFinalPay " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, e.Transaction)

    'End Sub
    Public Overrides Sub LoadInfo(ByVal pID As Integer)

        MyBase.LoadInfo(pID)
        mHideZero.Enabled = (pID > 0)
        Dim dt1 As DataTable
        Dim dt2 As DataTable
        Dim dt3 As DataTable
        Dim s1 As String
        Dim s2 As String
        Dim s3 As String
        s1 = "SELECT * FROM dbo.fFinalPay_Header(" & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString & ")"
        s2 = "SELECT * FROM dbo.fFinalPayIncome(" & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString & ")"
        s3 = "SELECT * FROM dbo.fFinalPayDeduction(" & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString & ")"
        dt1 = GSCOM.SQL.TableQuery(s1, Connection)
        dt2 = GSCOM.SQL.TableQuery(s2, Connection)
        dt3 = GSCOM.SQL.TableQuery(s3, Connection)
        Dim rd1 As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd1.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "FinalPay.rpt")
        Dim i As Integer

        For i = 0 To rd1.Subreports.Count - 1
            If rd1.Subreports.Item(i).Name = "FinalPayIncome" Then
                rd1.OpenSubreport(rd1.Subreports.Item(i).Name).SetDataSource(dt2)
            Else
                rd1.OpenSubreport(rd1.Subreports.Item(i).Name).SetDataSource(dt3)
            End If
            rd1.OpenSubreport(rd1.Subreports.Item(i).Name).SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
        Next
        rd1.SetDataSource(dt1)
        mReportViewerDetail.ReportSource = rd1
        mReportViewerDetail.Zoom(1)

        Dim dt2316 As DataTable
        dt2316 = GSCOM.SQL.TableQuery("SELECT * FROM dbo.fzFinalPay_2316(" & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString & ")", Connection)
        Dim rd2316 As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd2316.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "2316Printable_FinalPay.rpt")
        rd2316.SetDataSource(dt2316)
        mReportViewer2316.ReportSource = rd2316
        mReportViewer2316.Zoom(1)

    End Sub

#Region "Hide Zero"
    Private Sub HideZero(ByVal sender As Object, ByVal e As EventArgs)
        If mHideZero.Checked Then
            mtFinalPay_Detail.DefaultView.RowFilter = "Total <> 0"
        Else
            mtFinalPay_Detail.DefaultView.RowFilter = ""
        End If
    End Sub
#End Region

#Region "Old Code Procedures"
    'Private Sub GeneratePayroll(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_ComputeUnpaidSalary " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)

    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Finish generating payroll.", MsgBoxStyle.Information)

    'End Sub

    'Private Sub Generate13thMonth(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_Compute13thMonth " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Finish generating 13th Month.", MsgBoxStyle.Information)
    'End Sub

    'Private Sub GenerateLeaveConversion(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_ComputeLeaveConversion " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Finish generating Leave Conversion.", MsgBoxStyle.Information)

    'End Sub

    'Private Sub GenerateLoanPayment(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_ComputeLoanPayment " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Finish generating Loan Payment.", MsgBoxStyle.Information)

    'End Sub
    'Private Sub ApplyConvertedLeave(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_ApplyConvertedLeaves " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Leave Converted.", MsgBoxStyle.Information)

    'End Sub
    'Private Sub ApplyLoanPayment(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_ApplyLoanPayment " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    '    MsgBox("Loan Payment Applied.", MsgBoxStyle.Information)

    'End Sub

    'Private Sub Annualize(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pFinalPay_Annualization " & myDT.Get(Database.Tables.tFinalPay.Field.ID).ToString, Connection)
    '    LoadInfo(CInt(myDT.Get(Database.Tables.tFinalPay.Field.ID)))
    '    Application.DoEvents()
    'End Sub
#End Region
End Class