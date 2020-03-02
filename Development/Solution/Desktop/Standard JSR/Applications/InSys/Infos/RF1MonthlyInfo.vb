Option Explicit On
Option Strict Off



Friend Class RF1MonthlyInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tRF1Monthly(Connection)
    Private myDT_RF1Monthly_Detail As New Database.Tables.tRF1Monthly_Detail(Connection)
    Private myDT_Company As New Database.Tables.tCompany(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.RF1MonthlyControl
    'Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mRF1MonthlyButton As ToolStripButton
    'Private mRF1MonthlyDataButton As ToolStripButton
    Private mRF1BPIWriter As ToolStripButton
    Private mAddEmployeeButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_RF1Monthly_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tRF1Monthly.Field.ID)
        cdc = myDT_RF1Monthly_Detail.Columns(Database.Tables.tRF1Monthly_Detail.Field.ID_RF1Monthly)
        rel = mDataset.Relations.Add(pdc, cdc)
        'mReportViewer = AddReportViewer("Report Summary")
        'mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        myDT.Columns(Database.Tables.tRF1Monthly.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tRF1Monthly.Field.Year).DefaultValue = nDB.GetServerDate.Year

        mRF1MonthlyButton = MyBase.AddButton("Export File (BPI)", gMainForm.imgList.Images("_ApplyFile.png"), AddressOf Generate_TextFile)
        'mRF1MonthlyDataButton = MyBase.AddButton("Generate Excel File", gMainForm.imgList.Images("excel.png"), AddressOf GenerateTemplate)
        'mRF1BPIWriter = MyBase.AddButton("Generate Excel File (BPI)", gMainForm.imgList.Images("excel.png"), AddressOf GenerateTemplate)

        mAddEmployeeButton = MyBase.AddButton("Add Employee", gMainForm.imgList.Images("_employee.png"), AddressOf AddEmployee)

        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'mRF1MonthlyDataButton.Enabled = Not pID = 0
        'mRF1MonthlyButton.Enabled = Not pID = 0 ' new mode
        'myDT_RF1Monthly_Detail.ClearThenFill("ID_RF1Monthly=" & pID.ToString)
        MyBase.LoadInfo(pID)
        'CUSTOMIZED
        'Dim dt As DataTable
        'dt = GSCOM.SQL.TableQuery("SELECT * FROM vzRF1Monthly WHERE ID_RF1Monthly=" & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "PHICRF-1Monthly.rpt")
        'rd.SetDataSource(dt)
        'mReportViewer.ReportSource = rd
        ' mReportViewer.Zoom(1)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tRF1Monthly)
        End Set
    End Property



#End Region

#Region "Procedures"

    Public Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.HasUnsavedChanges Then
            Dim s As String
            s = "(ID_Company = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Company).ToString & ")"
            s &= " AND ID NOT IN (SELECT ID_Employee FROM tRF1Monthly_Detail WHERE ID_RF1Monthly =" & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString & ")"
            Dim f As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, s, True)
            If f.ShowDialog() = DialogResult.OK And f.GetTable IsNot Nothing Then
                Application.DoEvents()

                For Each dr As DataRow In f.GetTable.Select()
                    GSCOM.SQL.ExecuteNonQuery("EXEC pRF1MonthlyPerEmp " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString & "," & dr("ID").ToString, Connection)
                Next
            Else
                Exit Sub
            End If

            LoadInfo(CInt(myDT.Get(Database.Tables.tRF1Monthly.Field.ID)))
        Else
            MsgBox("Must Save First")
        End If
    End Sub
    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim y As String

        MyDialog.Filter = "Text files (*.txt)|*.txt"
        y = Format(CDate("1/1/" + myDT.Get(Database.Tables.tRF1Monthly.Field.Year).ToString), "yy")
        myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Company)))
        MyDialog.FileName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString.Trim() & "_" & MonthName(myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Month), True).ToUpper & y & "_R.txt"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_PHIC_File(MyDialog.FileName)
        End If
    End Sub

    Private Sub Save_PHIC_File(ByVal FileName As String)
        Dim fnum As Integer
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_RF1Monthly_Detail Is Nothing)) Then
                If myDT_RF1Monthly_Detail.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    Dim dt As New DataTable
                    dt = GSCOM.SQL.TableQuery("dbo.pRF1_ExportToFile " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, gConnection)
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

#Region "RF1MonthlyInfo_Saved"



    'Private Sub RF1MonthlyInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
    '    ' If e.RowState = DataRowState.Added Then
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pRF1Monthly " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, e.Transaction)
    '    '  End If
    'End Sub

#End Region


    'Private Sub GenerateTemplate(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim sfd As New SaveFileDialog
    '    Dim file As String
    '    file = GSCOM.SQL.ExecuteScalar("SELECT dbo.fPHICRemittanceFileName(" & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString & ")", Connection).ToString
    '    sfd.FileName = file & ".xls"
    '    sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
    '    sfd.FilterIndex = 0

    '    Dim dt As New DataTable
    '    Dim sqlString As String



    '    If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
    '        If CType(sender, ToolStripButton) Is mRF1MonthlyDataButton Then
    '            sqlString = "Select UPPER(LastName)LastName,UPPER(Suffix)Suffix,UPPER(FirstName)FirstName,UPPER(ISNULL(MiddleName,'.'))MiddleName,RefNo,MonthlyRate from tRF1Monthly_Detail Where ID_RF1Monthly = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString & "Order By LastName"
    '            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "PHILHEALTH EXCEL FORMAT_2012.XLS", sfd.FileName, True)
    '            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
    '            dt = GSCOM.SQL.TableQuery(sqlString, Connection)
    '            UseArray(sfd.FileName, dt)
    '            MsgBox("Done", MsgBoxStyle.Information)
    '        ElseIf CType(sender, ToolStripButton) Is mRF1BPIWriter Then
    '            sqlString = "Select UPPER(LastName)LastName,UPPER(Suffix)Suffix,UPPER(FirstName)FirstName,UPPER(ISNULL(MiddleName,'.'))MiddleName,RefNo,NULL,NULL,MonthlyRate from tRF1Monthly_Detail Where ID_RF1Monthly = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString & "Order By LastName"
    '            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "PHILHEALTH EXCEL FORMAT_2013.xls", sfd.FileName, True)
    '            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
    '            dt = GSCOM.SQL.TableQuery(sqlString, Connection)
    '            UseBPIWriter(sfd.FileName, dt)
    '            MsgBox("Done", MsgBoxStyle.Information)
    '        End If

    '    End If
    'End Sub

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
        Dim CompName, Address, Incharge, Position, RefNo, TelNo, SSSNo, TinNo, PhicNo As String
        Dim yy, mm As Integer
        Dim total As Decimal
        Dim DatePaid As Date

        CompName = GSCOM.SQL.ExecuteScalar("Select UPPER(REPLACE(REPLACE(REPLACE(Company,'.',''),',',''),'-',''))Company From vRF1Monthly Where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Address = GSCOM.SQL.ExecuteScalar("select UPPER(REPLACE(REPLACE(REPLACE(ISNULL([Address],''),'.',''),',',''),'-',''))Address From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Incharge = GSCOM.SQL.ExecuteScalar("Select UPPER(ISNULL(CertifiedBy,''))CertifiedBy From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Position = GSCOM.SQL.ExecuteScalar("Select UPPER(ISNULL(CertifiedByPosition,''))CertifiedByPosition From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        yy = GSCOM.SQL.ExecuteScalar("Select Year From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        mm = GSCOM.SQL.ExecuteScalar("Select ID_Month From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        RefNo = GSCOM.SQL.ExecuteScalar("Select ISNULL(MRefNo,'') From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        total = GSCOM.SQL.ExecuteScalar("Select SUM(PSAmt)+SUM(ESAmt) from tRF1Monthly_detail Where ID_RF1Monthly = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        DatePaid = GSCOM.SQL.ExecuteScalar("Select ISNULL(MDatePaid,'') From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        TelNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(TelNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        SSSNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(SSSNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        TinNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(TIN,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        PhicNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(PhilhealthNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)





        oSheet = oBook.Worksheets(1)
        oSheet.range("B1").Value = CompName
        oSheet.range("B2").Value = Address
        oSheet.range("B5").Value = Incharge
        oSheet.range("B6").Value = Position

        oSheet.range("E4").Value = yy
        oSheet.range("E5").Value = mm

        oSheet.range("AM1").Value = RefNo
        oSheet.range("AM2").Value = total
        oSheet.range("AM3").Value = DatePaid

        oSheet.range("B3").Value = PhicNo
        oSheet.range("B4").Value = TelNo
        oSheet.range("F2").Value = SSSNo
        oSheet.range("F4").Value = TinNo

        oSheet.Range("B8").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray
        oBook.Save()
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub

    Private Sub UseBPIWriter(ByVal pFileName As String, ByVal vDT As DataTable)
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
        Dim CompName, Address, Incharge, Position, RefNo, TelNo, SSSNo, TinNo, PhicNo As String
        Dim yy, mm As Integer
        Dim total As Decimal
        Dim DatePaid As Date

        CompName = GSCOM.SQL.ExecuteScalar("Select UPPER(REPLACE(REPLACE(REPLACE(Company,'.',''),',',''),'-',''))Company From vRF1Monthly Where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Address = GSCOM.SQL.ExecuteScalar("select UPPER(REPLACE(REPLACE(REPLACE(ISNULL([Address],''),'.',''),',',''),'-',''))Address From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Incharge = GSCOM.SQL.ExecuteScalar("Select UPPER(ISNULL(CertifiedBy,''))CertifiedBy From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        Position = GSCOM.SQL.ExecuteScalar("Select UPPER(ISNULL(CertifiedByPosition,''))CertifiedByPosition From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        yy = GSCOM.SQL.ExecuteScalar("Select Year From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        mm = GSCOM.SQL.ExecuteScalar("Select ID_Month From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        RefNo = GSCOM.SQL.ExecuteScalar("Select ISNULL(MRefNo,'') From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        total = GSCOM.SQL.ExecuteScalar("Select SUM(PSAmt)+SUM(ESAmt) from tRF1Monthly_detail Where ID_RF1Monthly = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        DatePaid = GSCOM.SQL.ExecuteScalar("Select ISNULL(MDatePaid,'') From tRF1Monthly where ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        TelNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(TelNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        SSSNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(SSSNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        TinNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(TIN,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)
        PhicNo = GSCOM.SQL.ExecuteScalar("select ISNULL(REPLACE(PhilhealthNo,'-',''),'') From tRF1Monthly rf INNER JOIN tCompany c On c.ID =  rf.ID_Company where rf.ID = " & myDT.Get(Database.Tables.tRF1Monthly.Field.ID).ToString, Connection)





        oSheet = oBook.Worksheets(1)

        oSheet.range("B1").Value = CompName
        oSheet.range("B2").Value = Address
        oSheet.range("B5").Value = Incharge
        oSheet.range("B6").Value = Position

        oSheet.range("E4").Value = yy
        oSheet.range("E5").Value = mm

        oSheet.range("AX1").Value = RefNo
        oSheet.range("AX2").Value = total
        oSheet.range("AX3").Value = DatePaid

        oSheet.range("B3").Value = PhicNo
        oSheet.range("B4").Value = TelNo
        oSheet.range("G1").Value = SSSNo
        oSheet.range("G2").Value = TinNo

        oSheet.Range("B9").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray
        oBook.Save()
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub
#Region "Comment_EMIL"
    '' get company info
    ''myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Company)))
    '                fnum = FreeFile()
    '                FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
    '                FileSystem.PrintLine(fnum, "REMITTANCE REPORT")
    '                FileSystem.PrintLine(fnum, Strings.Left(myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString.PadRight(60, " "c), 60).ToUpper)
    '                FileSystem.PrintLine(fnum, Strings.Left(myDT_Company.Get(Database.Tables.tCompany.Field.Address).ToString.PadRight(100, " "c), 100).ToUpper)
    '                s = Strings.Left(myDT_Company.Get(Database.Tables.tCompany.Field.PhilHealthNo).ToString.PadRight(12, " "c), 12).ToUpper
    '                Select Case myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Month)
    '                    Case 1 To 3
    '                        s &= "1"
    '                    Case 4 To 6
    '                        s &= "2"
    '                    Case 7 To 9
    '                        s &= "3"
    '                    Case 10 To 12
    '                        s &= "4"
    '                End Select
    '                s &= myDT.Get(Database.Tables.tRF1Monthly.Field.Year).ToString & "R"
    '                FileSystem.PrintLine(fnum, s)
    '                FileSystem.PrintLine(fnum, "MEMBERS")

    ''09/05/05    KIBAGAMI                      GENJURO                        00785000005000005000000000000000000000000000          '
    '                For Each dr In myDT_RF1Monthly_Detail.Rows

    '                    s = Strings.Left(dr.Item("RefNo").ToString.Replace("-", "").PadRight(12, " "c), 12).ToUpper
    '                    s &= Strings.Left(dr.Item("LastName").ToString.PadRight(30, " "c), 30).ToUpper
    '                    s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(30, " "c), 30).ToUpper
    '                    s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "c), 1).ToUpper
    '                    s &= Strings.Left(Replace(Format(dr.Item("MonthlyRate"), "0.00"), ".", "").PadLeft(8, "0"c), 8)
    '                    Select Case myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Month)
    '                        Case 1, 4, 7, 10
    '                            s &= Strings.Left(Replace(Format(dr.Item("PSAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    '                            s &= Strings.Left(Replace(Format(dr.Item("ESAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    '                            s &= "".PadLeft(24, "0"c)
    '                        Case 2, 5, 8, 11
    '                            s &= "".PadLeft(12, "0"c)
    '                            s &= Strings.Left(Replace(Format(dr.Item("PSAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    '                            s &= Strings.Left(Replace(Format(dr.Item("ESAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    '                            s &= "".PadLeft(12, "0"c)
    '                        Case 3, 6, 9, 12
    '                            s &= "".PadLeft(24, "0"c)
    '                            s &= Strings.Left(Replace(Format(dr.Item("PSAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    '                            s &= Strings.Left(Replace(Format(dr.Item("ESAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)

    '                    End Select
    ''s &= Strings.Left(Replace(Format(dr.Item("PSAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    ''s &= Strings.Left(Replace(Format(dr.Item("ESAmt"), "0.00"), ".", "").PadLeft(6, "0"c), 6)
    ''s &= Strings.Left(Replace(Format(dr.Item("PS2Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
    ''s &= Strings.Left(Replace(Format(dr.Item("ES2Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
    ''s &= Strings.Left(Replace(Format(dr.Item("PS3Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
    ''s &= Strings.Left(Replace(Format(dr.Item("ES3Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
    ''s &= Strings.Left("concat employee status here".PadLeft(10, " "), 10)
    '                        s &= Strings.Left("          ".PadLeft(10, " "c), 10)

    '                        FileSystem.PrintLine(fnum, s)
    '                Next
    '' summary
    '                FileSystem.PrintLine(fnum, "M5-SUMMARY")
    '                Select Case myDT.Get(Database.Tables.tRF1Monthly.Field.ID_Month)
    '                    Case 1, 4, 7, 10
    '                        s = "1"
    '                        s &= Strings.Left(Replace(Format(CDec(myDT_RF1Monthly_Detail.Compute("SUM(PSAmt)", "")) + CDec(myDT_RF1Monthly_Detail.Compute("SUM(ESAmt)", "")), "0.00"), ".", "").PadLeft(15, "0"c), 15)
    '                        s &= Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.MRefNo).ToString.PadLeft(15, " "c), 15)
    '                        s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), vNow, myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), "MMddyyyy")
    '                        s &= Strings.Left(Format(myDT_RF1Monthly_Detail.Compute("COUNT(ID)", "PSAmt > 0.00"), "0").ToString.PadLeft(8, " "c), 8)
    '                        FileSystem.PrintLine(fnum, s)
    '                        FileSystem.PrintLine(fnum, "2" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                        FileSystem.PrintLine(fnum, "3" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                    Case 2, 5, 8, 11
    '                        FileSystem.PrintLine(fnum, "1" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                        s = "2"
    '                        s &= Strings.Left(Replace(Format(CDec(myDT_RF1Monthly_Detail.Compute("SUM(PSAmt)", "")) + CDec(myDT_RF1Monthly_Detail.Compute("SUM(ESAmt)", "")), "0.00"), ".", "").PadLeft(15, "0"c), 15)
    '                        s &= Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.MRefNo).ToString.PadLeft(15, " "c), 15)
    '                        s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), vNow, myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), "MMddyyyy")
    '                        s &= Strings.Left(Format(myDT_RF1Monthly_Detail.Compute("COUNT(ID)", "PSAmt > 0.00"), "0").ToString.PadLeft(8, " "c), 8)
    '                        FileSystem.PrintLine(fnum, s)
    '                        FileSystem.PrintLine(fnum, "3" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                    Case 3, 6, 9, 12
    '                        FileSystem.PrintLine(fnum, "1" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                        FileSystem.PrintLine(fnum, "2" + "".PadLeft(15, "0"c) + "".PadRight(15, " "c) + "".PadRight(8, " "c) + "0".PadLeft(8, " "c))
    '                        s = "3"
    '                        s &= Strings.Left(Replace(Format(CDec(myDT_RF1Monthly_Detail.Compute("SUM(PSAmt)", "")) + CDec(myDT_RF1Monthly_Detail.Compute("SUM(ESAmt)", "")), "0.00"), ".", "").PadLeft(15, "0"c), 15)
    '                        s &= Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.MRefNo).ToString.PadLeft(15, " "c), 15)
    '                        s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), vNow, myDT.Get(Database.Tables.tRF1Monthly.Field.MDatePaid)), "MMddyyyy")
    '                        s &= Strings.Left(Format(myDT_RF1Monthly_Detail.Compute("COUNT(ID)", "PSAmt > 0.00"), "0").ToString.PadLeft(8, " "c), 8)
    '                        FileSystem.PrintLine(fnum, s)
    '                End Select

    ''s = "2"
    ''s &= Strings.Left(Replace(Format(myDT_RF1Monthly_Detail.Compute("SUM(PS2Amt)", "") + myDT_RF1Monthly_Detail.Compute("SUM(ES2Amt)", ""), "0.00"), ".", "").PadLeft(8, "0"), 8)
    ''s &= Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.M2RefNo).ToString.PadLeft(15, " "), 15)
    ''s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1Monthly.Field.M2DatePaid)), vNow, myDT.Get(Database.Tables.tRF1Monthly.Field.M2DatePaid)), "MMddyyyy")
    ''s &= Strings.Left(Format(myDT_RF1Monthly_Detail.Compute("COUNT(ID)", "PS2Amt > 0.00"), "0").ToString.PadLeft(3, "0"), 3)
    ''FileSystem.PrintLine(fnum, s)
    ''s = "3"
    ''s &= Strings.Left(Replace(Format(myDT_RF1Monthly_Detail.Compute("SUM(PS3Amt)", "") + myDT_RF1Monthly_Detail.Compute("SUM(ES3Amt)", ""), "0.00"), ".", "").PadLeft(8, "0"), 8)
    ''s &= Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.M3RefNo).ToString.PadLeft(15, " "), 15)
    ''s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1Monthly.Field.M3DatePaid)), vNow, myDT.Get(Database.Tables.tRF1Monthly.Field.M3DatePaid)), "MMddyyyy")
    ''s &= Strings.Left(Format(myDT_RF1Monthly_Detail.Compute("COUNT(ID)", "PS3Amt > 0.00"), "0").ToString.PadLeft(3, "0"), 3)
    ''FileSystem.PrintLine(fnum, s)

    '                s = "GRAND TOTAL"
    '                s &= Strings.Left(Replace(Format(CDec(myDT_RF1Monthly_Detail.Compute("SUM(PSAmt)", "")) + CDec(myDT_RF1Monthly_Detail.Compute("SUM(ESAmt)", "")), "0.00"), ".", "").PadLeft(15, "0"c), 15) '+ _
    ''myDT_RF1Monthly_Detail.Compute("SUM(PS2Amt)", "") + myDT_RF1Monthly_Detail.Compute("SUM(ES2Amt)", "") + _
    ''myDT_RF1Monthly_Detail.Compute("SUM(PS3Amt)", "") + myDT_RF1Monthly_Detail.Compute("SUM(ES3Amt)", ""), "0.00"), ".", "").PadLeft(10, "0"), 10)
    '                FileSystem.PrintLine(fnum, s)
    '                s = Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.CertifiedBy).ToString.PadRight(40, " "c), 40) & _
    '                    Strings.Left(myDT.Get(Database.Tables.tRF1Monthly.Field.CertifiedByPosition).ToString.PadRight(20, " "c), 20)
    '                FileSystem.PrintLine(fnum, s)
#End Region
End Class
