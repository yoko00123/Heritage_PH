Option Explicit On
Option Strict Off




Friend Class HDMFLoanExportInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tHDMFExport(Connection)
    Private myDT_HDMFExport_Detail As New Database.Tables.tHDMFExport_Detail(Connection)
    Private myDT_Company As New Database.Tables.tCompany(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.HDMFExportControl
    'Private mLoadButton As ToolStripButton
    Private mExportToFileButton As ToolStripButton
    Private mExportToXLLoanFileButton As ToolStripButton
    Private mAddEmployeeButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_HDMFExport_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tHDMFExport.Field.ID)
        cdc = myDT_HDMFExport_Detail.Columns(Database.Tables.tHDMFExport_Detail.Field.ID_HDMFExport)
        rel = mDataset.Relations.Add(pdc, cdc)

        myDT.Columns(Database.Tables.tHDMFExport.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tHDMFExport.Field.Year).DefaultValue = nDB.GetServerDate.Year

        'Me.GetStripButton("Export Loan File")
        'AddHandler Me.GetStripButton("Export Loan File").Click, AddressOf ExportLoanExcelToFile
        'Me.GetStripButton("Export Remittance File")
        'AddHandler Me.GetStripButton("Export Remittance File").Click, AddressOf ExportRemittanceToFile
        'mLoadButton = MyBase.AddButton("Load", gMainForm.imgList.Images("importFile.png"), AddressOf Load_Employee)
        'mExportToFileButton = MyBase.AddButton("Export Remittance (BPI)", gMainForm.imgList.Images("_ApplyFile.png"), AddressOf Generate_TextFile)
        'mExportToFileButton = MyBase.AddButton("Export Remittance Excel File", gMainForm.imgList.Images("excel.png"), AddressOf ExportRemittanceToFile)
        mExportToXLLoanFileButton = MyBase.AddButton("Export Loan File (BPI)", gMainForm.imgList.Images("_ApplyFile.png"), AddressOf Generate_TextFile) 'Text Format
        mExportToXLLoanFileButton = MyBase.AddButton("Export Loan Excel File", gMainForm.imgList.Images("excel.png"), AddressOf ExportLoanExcelToFile) 'Excel Format

        mAddEmployeeButton = MyBase.AddButton("Add Employee", gMainForm.imgList.Images("_employee.png"), AddressOf AddEmployee)

        Me.ReloadAfterCommit = True

        'mAddEmployeeButton = Me.GetStripButton("Add Employee")
        'AddHandler mAddEmployeeButton.Click, AddressOf AddEmployee
        Me.ReArrangeTab(False)

        AfterNew()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'mExportToFileButton.Enabled = (pID <> 0) ' new mode
        ' mLoadButton.Enabled = (pID <> 0) ' new mode
        'myDT_HDMFExport_Detail.ClearThenFill(Database.Tables.tHDMFExport_Detail.Field.ID_HDMFExport.ToString & "=" & pID.ToString)
        MyBase.LoadInfo(pID)
        'CUSTOMIZED
        'Dim dt As DataTable
        'dt = GSCOM.SQL.TableQuery("SELECT * FROM vzRF1 WHERE ID_RF1=" & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, Connection)
        'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "RF-1.rpt")
        'rd.SetDataSource(dt)
        'mReportViewer.ReportSource = rd
        'mReportViewer.Zoom(1)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tHDMFExport)
        End Set
    End Property



#End Region

#Region "Procedures"

    Public Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.HasUnsavedChanges Then
            Dim s As String
            s = "(ID_Company = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString & ")"
            s &= " AND ID NOT IN (SELECT ID_Employee FROM tHDMFLoanExport WHERE ID_HDMFExport =" & myDT.Get(Database.Tables.tHDMFExport.Field.ID).ToString & ")"
            Dim f As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, s, True)
            If f.ShowDialog() = DialogResult.OK And f.GetTable IsNot Nothing Then
                Application.DoEvents()

                For Each dr As DataRow In f.GetTable.Select()
                    GSCOM.SQL.ExecuteNonQuery("EXEC pHDMFLoanExportPerEmp " & myDT.Get(Database.Tables.tHDMFExport.Field.ID).ToString & "," & nDB.GetUserID & "," & dr("ID").ToString, Connection)
                Next
            Else
                Exit Sub
            End If

            LoadInfo(CInt(myDT.Get(Database.Tables.tHDMFExport.Field.ID)))
        Else
            MsgBox("Must Save First")
        End If
    End Sub

    Private Sub Load_Employee(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Load data?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Loading HDMF details... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pHDMFExport " & myDT.Get(Database.Tables.tHDMFExport.Field.ID).ToString, Connection)
            LoadInfo(CInt(myDT.Get(Database.Tables.tHDMFExport.Field.ID)))
            Application.DoEvents()
            EndProcess("")
        End If
    End Sub

    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()

        MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        MyDialog.FileName = myDT.Get(Database.Tables.tHDMFExport.Field.Year).ToString + Format(myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month), "00") + Format(myDT.Get(Database.Tables.tHDMFExport.Field.ID), "000") + ".txt"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_File(MyDialog.FileName, IIf(sender.ToString = "Export Loan File (BPI)", True, False))
        End If
    End Sub

    Private Sub Save_File(ByVal FileName As String, ByVal IsLoan As Boolean)
        Dim fnum As Integer
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_HDMFExport_Detail Is Nothing)) Then
                If myDT_HDMFExport_Detail.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    myDT_Company.ClearThenFill("ID=" & nDB.GetCompanyID.ToString)
                    Dim dt As New DataTable
                    If IsLoan Then
                        dt = GSCOM.SQL.TableQuery("dbo.pHMDFLoan_ExportToFile " & myDT.Get(Database.Tables.tHDMFExport.Field.Year) & "," & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month), Connection)
                    Else
                        dt = GSCOM.SQL.TableQuery("dbo.pHDMF_EportToFile " & myDT.Get(Database.Tables.tHDMFExport.Field.ID).ToString, gConnection)
                    End If
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            FileSystem.PrintLine(fnum, dr.Item("TEXTFILE"))
                        Next
                    End If

                    MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
                Else
                    MsgBox("No record found.", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            FileClose(fnum)
        End Try
    End Sub

#End Region

#Region "Comments"
    's = "                         "
    's &= " "
    's &= Strings.Left(Format(myDT_HDMFExport_Detail.Compute("SUM(NetAmt)", ""), "0.00").PadLeft(25, " "), 25)
    's &= " "
    's &= Strings.Left(Format(vnow, "yyyyMMdd").PadLeft(8, ""), 8)

    'FileSystem.PrintLine(fnum, s)

    'For Each dr In myDT_HDMFExport_Detail.Rows
    '    s = Strings.Left(Replace(Replace(dr.Item(Database.Tables.tBankExport_Detail.Field.BankAcctNo).ToString, "-", ""), " ", "").PadLeft(25, " "), 25).ToUpper
    '    s &= " "
    '    s &= Strings.Left(Format(dr.Item(Database.Tables.tBankExport_Detail.Field.NetAmt), "0.00").PadLeft(25, " "), 25)
    '    FileSystem.PrintLine(fnum, s)
    'Next
    'Select Case Me.myDT.Get(Database.Tables.tBankExport.Field.ID_Bank)
    '    Case Database.BankEnum.EastWestBank
    '        Dim dt As New nExtern.EastWest.PayrollUploadFile
    '        dt.Table = myDT_HDMFExport_Detail
    '        FileSystem.Print(fnum, dt.GetText)
    '    Case Database.BankEnum.EquitablePCI
    '        Dim dt As New nExtern.EquitablePCI.PayrollUploadFile
    '        dt.AcctNoColumn = Database.Tables.tBankExport_Detail.Field.BankAcctNo.ToString
    '        dt.CreditColumn = Database.Tables.tBankExport_Detail.Field.NetAmt.ToString
    '        dt.DebitColumn = Database.Tables.tBankExport_Detail.Field.NetAmt.ToString
    '        dt.Table = myDT_HDMFExport_Detail
    '        FileSystem.Print(fnum, dt.GetText)
    '    Case Database.BankEnum.UnitedCoconutPlantersBank
    '        Dim dt As New External.UCPB.PayrollUploadFile
    '        dt.hAccountNo = "221-100 1384"
    '        dt.hBankCode = "0001"
    '        dt.hPayCode = "1"
    '        dt.hTransDate = vNow
    '        dt.Table = myDT_HDMFExport_Detail
    '        FileSystem.Print(fnum, dt.GetText)
    'End Select
#End Region

#Region "Unused"
    Private Sub ExportToFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim a As New GSCOM.External.HDMF.HDMF_Export
        'Dim dt As DataTable
        'dt = GSCOM.SQL.TableQuery("SELECT * FROM vzHDMF_Export WHERE " & Database.Tables.tHDMFExport_Detail.Field.ID_HDMFExport.ToString & " = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID).ToString & " ORDER BY LNAME", Connection)
        'a.Table = dt
        'a.Save(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "BAQC.dbf")

        'If System.IO.File.Exists(a.FileName) Then
        '    MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
        'End If
        'MsgBox("Not available. This module is not updated.")

        MessageBox.Show(nDB.GetCompanyID)
        MessageBox.Show(CInt(myDT.Get(Database.Tables.tHDMFExport.Field.ID)).ToString)
        Dim MyDialog As New SaveFileDialog()
        'Dim y As String

        MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        MyDialog.FileName = "sample.txt"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Dim dt As New DataTable
            'Dim sd As Object
            'Dim ed As Object
            dt = GSCOM.SQL.TableQuery("dbo.pSHMDFLoan " & myDT.Get(Database.Tables.tHDMFExport.Field.Year) & "," & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month), Connection)
            If dt.Rows.Count > 0 Then

            Else
                MsgBox("No record found.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub ExportLoanExcelToFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.ScheduleTable
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.ScheduleAdapter
        sfd.FileName = "HDMF LOAN-ASCII.XLS"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "HDMLoanFExport.xls", sfd.FileName, True)
            'a.DataSource = sfd.FileName 'initialize datasource (filename)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            'Dim sd As Object
            'Dim ed As Object
            dt = GSCOM.SQL.TableQuery("dbo.pSHMDFLoan " & myDT.Get(Database.Tables.tHDMFExport.Field.Year) & "," & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month), Connection)
            If dt.Rows.Count > 0 Then
                UseArray(sfd.FileName, dt)
                MsgBox("Done", MsgBoxStyle.Information)
            Else
                MsgBox("No record found.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        'Start a new workbook in Excel.
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.open(pFileName)
        'Create an array with 3 columns and 100 rows.
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

        'Add headers to the worksheet on row 1.
        oSheet = oBook.Worksheets(1)
        'oSheet.Range("A1").Value = "Order ID"
        'oSheet.Range("B1").Value = "Amount"
        'oSheet.Range("C1").Value = "Tax"

        'Transfer the array to the worksheet starting at cell A2.
        oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

        'oSheet.Range("A2").Value = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate).ToString
        'oSheet.Range("A3").Value = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate).ToString




        'Save the workbook and quit Excel.
        oBook.Save()
        'oBook.SaveAs(sSampleFolder & "Book2.xls")
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub
#End Region

#Region "ExportRemittanceToFile"
    Private Sub ExportRemittanceToFile(ByVal sender As Object, ByVal e As EventArgs)
        ' Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.ScheduleTable
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.ScheduleAdapter
        sfd.FileName = "M1-1.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        Try
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "M1-1.xls", sfd.FileName, True)
                ' a.DataSource = sfd.FileName 'initialize datasource (filename)
                IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
                Dim dt As New DataTable


                Dim sqlString As String
                sqlString = ("Select DISTINCT HDMFNo,REPLACE(CONVERT(VARCHAR(6),BirthDate,101),'/','')+SUBSTRING(REPLACE(CONVERT(VARCHAR(10),BirthDate,101),'/',''),7,2),NULL,LastName,FirstName,MiddleName,HDMFEE,HDMFER,Total from vzHDMF_M11_2 Where Year =  " & myDT.Get(Database.Tables.tHDMFExport.Field.Year) & "AND ID_Month = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month) & "AND ID_Company = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company) & "Order BY LastName")
                '  ("SELECT  EmpNo,Employee	,RegHRS,Tardy,UT,Absent,RegOT,SunOT,SunOTX,SplOT,SplOTX,SplSunOT,SplSunOTX,RegHolOT,RegHolOTX,RHOlSunOT,RHOlSunOTX,RegNP,RegOTNP,SunNP,SunOTNP,SplNP,SplOTNP,SplSunNP,SplSunOTNP,RegHolNP,	RHolSunOTNP,	RHolSunNP,	RegHolOTNP ,vl, sl FROM (SELECT * FROM fAUB(" & GSCOM.SQL.SQLFormat(sd) & "," & GSCOM.SQL.SQLFormat(ed) & ") A WHERE " & mFilter & ") A") 'Replace(mFilter, "'", "''") & ") A"
                dt = GSCOM.SQL.TableQuery(sqlString, Connection)
                UseArray2(sfd.FileName, dt)
                MsgBox("Done", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UseArray2(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        'Start a new workbook in Excel.
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.open(pFileName)
        'Create an array with 3 columns and 100 rows.
        Dim DataArray(vDT.Rows.Count - 1, vDT.Columns.Count - 1) As Object
        Dim r, c As Integer
        Dim ee, er As Decimal

        r = 0
        ee = 0
        er = 0
        For Each drx As DataRow In vDT.Rows
            c = 0
            For Each col As DataColumn In vDT.Columns
                DataArray(r, c) = drx.Item(c)
                c += 1
            Next
            r += 1
            ee += CDec(drx.Item("HDMFEE"))
            er += CDec(drx.Item("HDMFER"))

        Next
        r = vDT.Rows.Count + 17

        'Add headers to the worksheet on row 1.
        oSheet = oBook.Worksheets(1)
        'oSheet.Range("A1").Value = "Order ID"
        'oSheet.Range("B1").Value = "Amount"
        'oSheet.Range("C1").Value = "Tax"



        Dim s As String
        s = "SELECT Name FROm tMonth WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Month).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("H5").Value = s
        s = "SELECT SSSno FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("F7").Value = s

        s = "SELECT Name FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("B6").Value = s

        s = "SELECT Address FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("B10").Value = s

        s = "SELECT TIN FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("F12").Value = s

        s = "SELECT ZipCode FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("G12").Value = s

        s = "SELECT TelNo FROm tCompany WHERE ID = " & myDT.Get(Database.Tables.tHDMFExport.Field.ID_Company).ToString
        s = (GSCOM.SQL.ExecuteScalar(s, gConnection).ToString())
        oSheet.Range("H12").Value = s


        oSheet.Range("I5").Value = myDT.Get(Database.Tables.tHDMFExport.Field.Year).ToString
        'Transfer the array to the worksheet starting at cell A2.
        oSheet.Range("A17").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

        oSheet.Range("G" & r.ToString).Value = ee.ToString
        oSheet.Range("H" & r.ToString).Value = er.ToString
        oSheet.Range("I" & r.ToString).Value = (ee + er).ToString




        'Save the workbook and quit Excel.
        oBook.Save()
        'oBook.SaveAs(sSampleFolder & "Book2.xls")
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub
#End Region

End Class
