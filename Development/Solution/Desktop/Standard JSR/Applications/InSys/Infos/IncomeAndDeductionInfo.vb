Option Explicit On
Option Strict Off



Friend Class IncomeAndDeductionInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tIncomeAndDeduction(Connection)
    Private mtIncomeAndDeduction_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tIncomeAndDeduction_Detail(Connection)   'gLen.code 201104116
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.IncomeAndDeductionControl
    Private mImportButton As ToolStripButton
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mGenerateTemplateButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtIncomeAndDeduction_Detail)   'gLen.code 201104116
        End With
        InitControl(pMenu)

        'gLen.code 201104116
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tIncomeAndDeduction.Field.ID)
        'cdc = mtIncomeAndDeduction_Detail.Columns(Database.Tables.tIncomeAndDeduction_Detail.Field.ID_IncomeAndDeduction)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mtIncomeAndDeduction_Detail = DirectCast(Me.mDataset.Tables("tIncomeAndDeduction_Detail"), GSCOM.SQL.ZDataTable)
        '###

        mGenerateTemplateButton = Me.GetStripButton("Generate Template") 'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("Generatetemplate.png"), AddressOf GenTemplate)
        AddHandler mGenerateTemplateButton.Click, AddressOf GenTemplate

        mImportButton = Me.GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        AddHandler mImportButton.Click, AddressOf ImportFile
        myDT.Columns(Database.Tables.tIncomeAndDeduction.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        'gLen.code 20110416
        'MyBase.AddButton("Convert Hours to Amount", gMainForm.imgList.Images("misc.a.ico"), AddressOf ConvertHoursToAmount) 
        'mtIncomeAndDeduction_Detail.Columns(Database.Tables.tIncomeAndDeduction_Detail.Field.Amount).DefaultValue = 0.0 
        '###

        'mReportViewer = AddReportViewer("Report")
        'mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.ReloadAfterCommit = True
        AfterNew()
        'ComputeTotal()   'gLen.code 20110416
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtIncomeAndDeduction_Detail.Rows.Count > 0)

        'Dim dt As DataTable
        'dt = GSCOM.SQL.TableQuery("SELECT * FROM vIncomeAndDeduction_Detail WHERE ID_IncomeAndDeduction=" & myDT.Get(Database.Tables.tIncomeAndDeduction.Field.ID).ToString, Connection)

        'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "IncomeAndDeduction.rpt")
        'rd.SetDataSource(dt)
        'mReportViewer.ReportSource = rd
        'mReportViewer.Zoom(1)

        'ComputeTotal()   'gLen.code 20110416
    End Sub


#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tIncomeAndDeduction)
        End Set
    End Property



#End Region

    'gLen.code 20110416
    'Private Sub IncomeAndDeductionInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
    '    If myDT.Get(Database.Tables.tIncomeAndDeduction.Field.ID_PayrollItem) IsNot DBNull.Value Then
    '        Dim s As String
    '        s = "EXEC pIncomeAndDeduction " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tIncomeAndDeduction.Field.ID))
    '        GSCOM.SQL.ExecuteNonQuery(s, e.Transaction)
    '    End If
    'End Sub
    '###
#Region "ImportFile"

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        ofd.FilterIndex = 0
        ofd.CheckFileExists = True
        ofd.CheckPathExists = True
        If (ofd.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(ofd.FileName)
            Me.mImportButton.Enabled = False
        End If
    End Sub

#End Region

#Region "ConvertHoursToAmount"
    Private Sub ConvertHoursToAmount(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to convert hours to amount?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pConvertHoursToAmount " & myDT.Get(Database.Tables.tIncomeAndDeduction.Field.ID).ToString, Connection)
            MsgBox("Finished converting hours to amount.", MsgBoxStyle.Information)
            Me.LoadInfo(CInt(myDT.Get(Database.Tables.tIncomeAndDeduction.Field.ID)))
        End If
    End Sub

#End Region

    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.LeaveFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.LeaveFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tIncomeAndDeduction.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "IncomeAndDeduction.xls", sfd.FileName, True)
            a.DataSource = sfd.FileName 'initialize datasource (filename)

            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable

            'Dim s As String


            's &= "SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ")"



            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") Where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")

            'UseArray(sfd.FileName, dt)

            'UseArray(sfd.FileName, dt)


            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.open(pFileName)

        Dim DataArray(vDT.Rows.Count - 1, vDT.Columns.Count - 1) As Object
        Dim r, c As Integer
        r = 0
        For Each drx As DataRow In vDT.Rows
            c = 0
            For Each col As DataColumn In vDT.Columns
                DataArray(r, c) = drx.Item(c)
                c += 1
            Next
            r += 1
        Next


        oSheet = oBook.Worksheets(1)

        oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray


        oBook.Save()

        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub

#Region "TransferExcelData"
    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim dtemp As New Database.Tables.tEmployee(Connection)
        Dim dtpay As New Database.Tables.tPayrollItem(Connection)
        Dim nr As DataRow
        Dim r1 As DataRow()
        Dim r2 As DataRow()
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            dt = SQL.GetExcelTable(FileName, "Sheet1")
            dtemp.ClearThenFill("")
            dtpay.ClearThenFill("")
            mtIncomeAndDeduction_Detail.Rows.Clear()
            Dim total As Decimal = 0
            For Each dr As DataRow In dt.Rows
                nr = mtIncomeAndDeduction_Detail.NewRow()

                r1 = dtemp.Select("Code = " & GSCOM.SQL.SQLFormat(dr("EmployeeCode")))
                If r1.Length > 0 Then
                    nr(Database.Tables.tIncomeAndDeduction_Detail.Field.ID_Employee.ToString) = r1(0).Item(Database.Tables.tEmployee.Field.ID)
                    nr("EmployeeCode") = dr.Item("EmployeeCode")
                Else
                    Continue For
                End If

                r2 = dtpay.Select("Code = " & GSCOM.SQL.SQLFormat(dr("PayrollItem")))
                If r2.Length > 0 Then
                    nr(Database.Tables.tIncomeAndDeduction_Detail.Field.ID_PayrollItem.ToString) = r2(0).Item(Database.Tables.tPayrollItem.Field.ID)
                Else
                    Continue For
                End If

                Dim s As String
                s = dr("Amount").ToString
                If s = "" Then
                    s = "0"
                End If


                s = dr("Hours").ToString
                If s = "" Then
                    s = "0"
                End If

                's = dr("TaxAmount").ToString
                'If s = "" Then
                '    s = "0"
                'End If

                nr(Database.Tables.tIncomeAndDeduction_Detail.Field.Amount.ToString) = dr("Amount")
                nr(Database.Tables.tIncomeAndDeduction_Detail.Field.Hours.ToString) = dr("Hours")
                'nr(Database.Tables.tIncomeAndDeduction_Detail.Field.TaxAmount.ToString) = dr("TaxAmount")
                nr(Database.Tables.tIncomeAndDeduction_Detail.Field.Date.ToString) = dr("Date")
                nr(Database.Tables.tIncomeAndDeduction_Detail.Field.Comment.ToString) = dr("Comment")

                mtIncomeAndDeduction_Detail.Rows.Add(nr)
                total += CDec(dr("Amount"))
            Next
            'ComputeTotal()   'gLen.code 20110416

            Dim dg As DataGridView
            dg = Me.GetDataGridView(mtIncomeAndDeduction_Detail)
            dg.Refresh()

            For Each d As DataGridViewRow In dg.Rows
                dg.Update()
            Next

            Me.EndProcess("Finish downloading file [" & FileName & "].")
        Catch ex As OleDb.OleDbException
            Me.EndProcess("Error occur while importing data. This is due to file connection, please check if sheet name is Sheet1.", False)
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub




#End Region

    'gLen.code 20110416
    'Private Sub ComputeTotal()
    '    Application.DoEvents()
    '    Dim t As Decimal
    '    If mtIncomeAndDeduction_Detail IsNot Nothing Then
    '        For Each drv As DataRowView In mtIncomeAndDeduction_Detail.DefaultView
    '            t += CDec(drv("Amount"))
    '        Next
    '        mForm.SetDetailStatusLabelText("Detail", "Total Amount: " & t.ToString)
    '    End If
    'End Sub
    '###

End Class
