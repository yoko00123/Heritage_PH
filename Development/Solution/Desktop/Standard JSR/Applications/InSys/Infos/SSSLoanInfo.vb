Option Explicit On
Option Strict On



Friend Class SSSLoanInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tSSSLoanRemittance(Connection)
    Private myDT_SSSLoanRemittance_Detail As New Database.Tables.tSSSLoanRemittance_Detail(Connection)
    Private myDT_Company As New Database.Tables.tCompany(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.SSSNETControl
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mLoadButton As ToolStripButton
    Private mLCLButton As ToolStripButton
    Private mLCLButtonBPI As ToolStripButton
    'Private mMCLButton As ToolStripButton
    'Private mBNButton As ToolStripButton
    'Private mNR3001DK As ToolStripButton
    'Private mDBF As ToolStripButton
    Private mReportViewerSSS As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mAddEmployeeButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_SSSLoanRemittance_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tSSSLoanRemittance.Field.ID)
        cdc = myDT_SSSLoanRemittance_Detail.Columns(Database.Tables.tSSSLoanRemittance_Detail.Field.ID_SSSLoanRemittance)
        rel = mDataset.Relations.Add(pdc, cdc)
        mReportViewer = AddReportViewer("SSS Loan Transmittal")
        mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None


        myDT.Columns(Database.Tables.tSSSLoanRemittance.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tSSSLoanRemittance.Field.Year).DefaultValue = nDB.GetServerDate.Year

        mReportViewerSSS = AddReportViewer("SSS Loan LMS Diskette Report")
        mReportViewerSSS.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        mLoadButton = MyBase.AddButton("Load", gMainForm.imgList.Images("ImportFile.png"), AddressOf Load_Employee)
        mLCLButton = MyBase.AddButton("Generate LCL File", gMainForm.imgList.Images("SSS.png"), AddressOf Generate_LoanTextFile)
        mLCLButtonBPI = MyBase.AddButton("Export File (BPI)", gMainForm.imgList.Images("_ApplyFile.png"), AddressOf Generate_LoanTextFileBPI)
        'mMCLButton = MyBase.AddButton("MCL File", gMainForm.imgList.Images("misc.a.ico"), AddressOf Generate_TextFile)
        'mBNButton = MyBase.AddButton("Banknet File", gMainForm.imgList.Images("misc.a.ico"), AddressOf Generate_TextFile)
        'mNR3001DK = MyBase.AddButton("NR3001DK File", gMainForm.imgList.Images("misc.a.ico"), AddressOf NR3001DK_TextFile)
        'mDBF = MyBase.AddButton("DBF File", gMainForm.imgList.Images("misc.a.ico"), AddressOf Generate_DBF_File)

        mAddEmployeeButton = MyBase.AddButton("Add Employee", gMainForm.imgList.Images("_employee.png"), AddressOf AddEmployee)

        Me.ReloadAfterCommit = True
        AfterNew()

        Me.ReArrangeTab(False)

    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mLoadButton.Enabled = Not pID = 0 ' new mode
        mLCLButton.Enabled = Not pID = 0 ' new mode
        'mMCLButton.Enabled = Not pID = 0 ' new mode
        'mBNButton.Enabled = Not pID = 0 ' new mode
        'mNR3001DK.Enabled = Not pID = 0 ' new mode
        'mDBF.Enabled = Not pID = 0 ' new mode

        'myDT_SSSLoanRemittance_Detail.ClearThenFill("ID_SSSLoanRemittance=" & pID.ToString)
        MyBase.LoadInfo(pID)
        Dim s As String
        s = "ID_SSSLoanRemittance=" & pID.ToString
        s &= " AND "
        s &= "ID_Employee IN (SELECT ID FROM dbo.fSessionEmployee(" & nDB.GetUserID.ToString & ", " & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & ")" & ")"
        myDT_SSSLoanRemittance_Detail.ClearThenFill(s)

        'CUSTOMIZED
        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT * FROM vzSSSLoanTransmittal WHERE ID=" & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString, Connection)
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "SSSLoanTransmittal.rpt")
        rd.SetDataSource(dt)
        mReportViewer.ReportSource = rd
        mReportViewer.Zoom(1)


        Dim dtsss As DataTable
        dtsss = GSCOM.SQL.TableQuery("SELECT * FROM vzSSSLoanLMSDiskette WHERE ID_SSSLoanRemittance=" & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString, Connection)
        Dim rdsss As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rdsss.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "SSSLoanLMSDiskette.rpt")
        rdsss.SetDataSource(dtsss)
        mReportViewerSSS.ReportSource = rdsss
        mReportViewerSSS.Zoom(1)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tSSSLoanRemittance)
        End Set
    End Property

#End Region

#Region "Procedures"


    Public Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.HasUnsavedChanges Then
            Dim s As String
            s = "(ID_Company = " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company).ToString & ")"
            s &= " AND ID NOT IN (SELECT ID_Employee FROM tSSSLoanRemittance_Detail WHERE ID_SSSLoanRemittance =" & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString & ")"
            Dim f As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, s, True)
            If f.ShowDialog() = DialogResult.OK And f.GetTable IsNot Nothing Then
                Application.DoEvents()

                For Each dr As DataRow In f.GetTable.Select()
                    GSCOM.SQL.ExecuteNonQuery("EXEC pSSSLoanRemittancePerEmp " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString & "," & nDB.GetUserID & "," & dr("ID").ToString, Connection)
                Next
            Else
                Exit Sub
            End If

            LoadInfo(CInt(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID)))
        Else
            MsgBox("Must Save First")
        End If
    End Sub

    Private Sub Generate_LoanTextFileBPI(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim rp As String = Trim(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.RefNo).ToString)

        If myDT.Get(Database.Tables.tSSSLoanRemittance.Field.RefNo).ToString.Length >= 5 Then
            rp = myDT.Get(Database.Tables.tSSSLoanRemittance.Field.RefNo).ToString.Substring(0, 5)
        End If
        MyDialog.Filter = "Text files (*.txt)|*.txt"
        myDT_Company.ClearThenFill("ID=" & nDB.GetCompanyID.ToString)
        MyDialog.FileName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString.Replace(" ", "").Substring(0, 4) + _
            myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year).ToString.Substring(2) + Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00") + _
            "BPI" + rp + _
            ".txt"

        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_Generate_LoanTextFile(MyDialog.FileName)
        End If
    End Sub

    Private Sub Load_Employee(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Load data?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Loading SSS details... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pSSSLoanRemittance " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString & "," & nDB.GetUserID, Connection)
            LoadInfo(CInt(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID)))
            Application.DoEvents()
            EndProcess("")
        End If
    End Sub
    Private Sub Generate_LoanTextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim CompName As String
        Dim m As String
        Dim y As String


        MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company)))
        CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Code).ToString
        m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
        y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "00")
        y = Right(y, 2)
        MyDialog.FileName = CompName & y & m & ".Txt"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            If CType(sender, ToolStripButton) Is mLCLButton Then
                Save_Generate_LoanTextFile(MyDialog.FileName)
            End If
        End If
    End Sub

    Private Sub Save_Generate_LoanTextFile(ByVal FileName As String)
        'Dim m As String
        'Dim y As String
        Dim fnum As Integer
        Dim s As String
        Dim Year As String
        Dim Month As String
        Dim dt As DataTable
        Dim vNow As Date = nDB.GetServerDate
        Try
            myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company)))
            Year = myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year).ToString
            Month = myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month).ToString
            fnum = FreeFile()
            FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
            dt = GSCOM.SQL.TableQuery("EXEC pSSSLoaNtextFile " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString & "," & nDB.GetUserID, Connection)
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                s = dt.Rows(i).Item("Text").ToString
                s &= vbCrLf
                FileSystem.Print(fnum, s)
            Next

            MsgBox("File has been exported successfully.", MsgBoxStyle.Information)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            FileClose(fnum)
        End Try
    End Sub
    'Private Sub Generate_DBF_File(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim ofd As New System.Windows.Forms.FolderBrowserDialog
    '    ofd.ShowNewFolderButton = True
    '    If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
    '        BeginProcess("Saving SSS details... Please wait.")
    '        Dim a As New GSCOM.External.SSSLoanRemittanceDBF.SSSLoanRemittanceDBF_Export
    '        Dim dtHeader As DataTable
    '        Dim dtEmployee As DataTable
    '        Dim dtTrail As DataTable
    '        Dim CompanyName As String
    '        Dim CompanySSN As String
    '        Dim m As String
    '        Dim y As String

    '        'Kill(GSCOM.Common.AddPathSeparator(ofd.SelectedPath) & "Header.dbf")
    '        'Kill(GSCOM.Common.AddPathSeparator(ofd.SelectedPath) & "Employee.dbf")
    '        'Kill(GSCOM.Common.AddPathSeparator(ofd.SelectedPath) & "Trailer.dbf")

    '        m = IO.Path.Combine(ofd.SelectedPath, "Header.dbf")
    '        If My.Computer.FileSystem.FileExists(m) Then
    '            Kill(m)
    '        End If
    '        m = IO.Path.Combine(ofd.SelectedPath, "Employee.dbf")
    '        If My.Computer.FileSystem.FileExists(m) Then
    '            Kill(m)
    '        End If
    '        m = IO.Path.Combine(ofd.SelectedPath, "Trailer.dbf")
    '        If My.Computer.FileSystem.FileExists(m) Then
    '            Kill(m)
    '        End If

    '        m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
    '        y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
    '        myDT_Company.ClearThenFill("ID=" & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company))
    '        CompanyName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString.ToUpper
    '        CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo).ToString.Replace("-", "")
    '        dtHeader = GSCOM.SQL.TableQuery("SELECT '00' AS RECCD, '" & CompanyName & "' AS ERNME, '" & m & y & "' AS APQTR, '" & CompanySSN & "' AS ERIDN", Connection)
    '        dtEmployee = GSCOM.SQL.TableQuery("SELECT * FROM vzSSSLoanRemittance_DBF_Detail WHERE ID_SSSLoanRemittance = " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString, Connection)
    '        dtTrail = GSCOM.SQL.TableQuery("SELECT * FROM vzSSSLoanRemittance_DBF_Summary WHERE ID_SSSLoanRemittance = " & myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID).ToString, Connection)
    '        a.HeaderTable = dtHeader
    '        a.EmployeeTable = dtEmployee
    '        a.TrailerTable = dtTrail
    '        a.Save(ofd.SelectedPath)

    '        EndProcess("")
    '        MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    'Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim MyDialog As New SaveFileDialog()
    '    Dim m As String
    '    Dim y As String

    '    MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
    '    m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
    '    y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
    '    MyDialog.FileName = "R3" & m & y & "." & Format(nDB.GetServerDate, "MMddHHmm")
    '    MyDialog.FilterIndex = 2
    '    MyDialog.CheckFileExists = False
    '    MyDialog.CheckPathExists = True
    '    If (MyDialog.ShowDialog() = DialogResult.OK) Then
    '        If CType(sender, ToolStripButton) Is mMCLButton Then
    '            Save_MCL_File(MyDialog.FileName)
    '        ElseIf CType(sender, ToolStripButton) Is mBNButton Then
    '            Save_BN_File(MyDialog.FileName)
    '        End If
    '    End If
    'End Sub

    'Private Sub NR3001DK_TextFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim MyDialog As New SaveFileDialog()
    '    Dim m As String
    '    Dim y As String

    '    MyDialog.Filter = "All files (*.*)|*.*"
    '    m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
    '    y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
    '    '        MyDialog.FileName = "NR3001DK"
    '    MyDialog.FileName = "R3" & m & y & "." & Format(nDB.GetServerDate, "MMddHHmm")
    '    MyDialog.FilterIndex = 1
    '    MyDialog.CheckFileExists = False
    '    MyDialog.CheckPathExists = True
    '    If (MyDialog.ShowDialog() = DialogResult.OK) Then
    '        If CType(sender, ToolStripButton) Is mNR3001DK Then
    '            Save_NR3001DK_File(MyDialog.FileName)
    '        End If
    '    End If
    'End Sub

    Private Sub Save_BN_File(ByVal FileName As String)
        Dim m As String
        Dim y As String
        Dim fnum As Integer
        Dim s As String
        Dim CompName As String
        Dim CompanySSN As String
        Dim dr As DataRow
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_SSSLoanRemittance_Detail Is Nothing)) Then
                If myDT_SSSLoanRemittance_Detail.Rows.Count > 0 Then
                    m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company)))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo).ToString
                    fnum = FreeFile()
                    'MsgBox("Please insert diskette to drive A: then press ok to continue")
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00VIZCARRA PHARMA INC           0420060387665045
                    s = "00   1" & m & y & "         " & Format(vNow, "yyyMMdd")
                    s &= y & m
                    s &= Strings.Left(CompName.PadRight(40, " "c), 40)
                    'CompanySSN = "1234567890"
                    s &= Strings.Left(CompanySSN.PadLeft(10, " "c), 10) & "   C"

                    FileSystem.PrintLine(fnum, s)
                    For Each dr In myDT_SSSLoanRemittance_Detail.Rows
                        s = "20   "
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(20, " "c), 20)
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(20, " "c), 20)
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "c), 1)
                        s &= Strings.Left(dr.Item("SSSNo").ToString.PadRight(10, " "c), 10)
                        s &= Strings.Left(Format(dr.Item("TotalSSS"), "0.00").PadLeft(8, " "c), 8)
                        s &= "0.00".PadLeft(8, " "c)
                        s &= Strings.Left(Format(dr.Item("SSSEC"), "0.00").PadLeft(8, " "c), 8)
                        s &= Format(IIf(IsDBNull(dr.Item("DateHired")), vNow, dr.Item("DateHired")), "yyyyMMdd") & "N"
                        FileSystem.PrintLine(fnum, s)
                    Next
                    s = "99   "
                    s &= Strings.Left(Format(myDT_SSSLoanRemittance_Detail.Compute("SUM(TotalSSS)", ""), "0.00").PadLeft(14, " "c), 14)
                    s &= "0.00".PadLeft(10, " "c)
                    s &= Strings.Left(Format(myDT_SSSLoanRemittance_Detail.Compute("SUM(SSSEC)", ""), "0.00").PadLeft(12, " "c), 12)
                    s &= "VP" & m & y & "       "
                    FileSystem.Print(fnum, s)
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

    Private Sub Save_MCL_File(ByVal FileName As String)
        Dim m As String
        Dim y As String
        Dim fnum As Integer
        Dim s As String
        Dim CompName As String
        Dim CompanySSN As String
        Dim dr As DataRow
        Try
            If (Not (myDT_SSSLoanRemittance_Detail Is Nothing)) Then
                If myDT_SSSLoanRemittance_Detail.Rows.Count > 0 Then
                    m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company)))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo).ToString
                    fnum = FreeFile()
                    'MsgBox("Please insert diskette to drive A: then press ok to continue")
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00VIZCARRA PHARMA INC           0420060387665045
                    s = "00"
                    s &= Strings.Left(CompName.PadRight(30, " "c), 30)
                    s &= m & y
                    s &= Strings.Left(CompanySSN, 10)

                    FileSystem.PrintLine(fnum, s)
                    '20ACOSTA         SATURNINA      A3329325573 1128.00    0.00    0.00  0.00  0.00  0.00 10.00  0.00  0.00      N19981216
                    For Each dr In myDT_SSSLoanRemittance_Detail.Rows
                        s = "20"
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(15, " "c), 15)
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(15, " "c), 15)
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "c), 1)
                        s &= Strings.Left(dr.Item("SSSNo").ToString.PadRight(10, " "c), 10)
                        s &= Strings.Left(Format(dr.Item("TotalSSS"), "0.00").PadLeft(8, " "c), 8)
                        s &= "    0.00    0.00  0.00  0.00  0.00"
                        s &= Strings.Left(Format(dr.Item("SSSEC"), "0.00").PadLeft(6, " "c), 6)
                        s &= "  0.00  0.00      N"
                        s &= Format(IIf(IsDBNull(dr.Item("DateHired")), nDB.GetServerDate, dr.Item("DateHired")), "yyyyMMdd")
                        FileSystem.PrintLine(fnum, s)
                    Next
                    '99     1927.00        0.00        0.00      0.00      0.00      0.00     20.00      0.00      0.00
                    s = "99"
                    s &= Strings.Left(Format(myDT_SSSLoanRemittance_Detail.Compute("SUM(TotalSSS)", ""), "0.00").PadLeft(12, " "c), 12)
                    s &= "        0.00        0.00      0.00      0.00      0.00     "
                    s &= Strings.Left(Format(myDT_SSSLoanRemittance_Detail.Compute("SUM(SSSEC)", ""), "0.00").PadLeft(10, " "c), 10)
                    s &= "      0.00      0.00"
                    FileSystem.Print(fnum, s)
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

    Private Sub Save_NR3001DK_File(ByVal FileName As String)
        Dim MonthId As Integer
        Dim m As String
        Dim y As String
        Dim fnum As Integer
        Dim s As String
        Dim CompName As String
        Dim CompanySSN As String
        Dim dr As DataRow
        Dim vNow As Date = nDB.GetServerDate
        Dim r As String
        Try
            If (Not (myDT_SSSLoanRemittance_Detail Is Nothing)) Then
                If myDT_SSSLoanRemittance_Detail.Rows.Count > 0 Then
                    Dim REMIT1st As Decimal = 0
                    Dim REMIT2nd As Decimal = 0
                    Dim REMIT3rd As Decimal = 0
                    Dim EC1st As Decimal = 0
                    Dim EC2nd As Decimal = 0
                    Dim EC3rd As Decimal = 0
                    Dim TotalREMIT1st As Decimal = 0
                    Dim TotalREMIT2nd As Decimal = 0
                    Dim TotalREMIT3rd As Decimal = 0
                    Dim TotalEC1st As Decimal = 0
                    Dim TotalEC2nd As Decimal = 0
                    Dim TotalEC3rd As Decimal = 0

                    MonthId = CInt(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month).ToString)
                    m = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tSSSLoanRemittance.Field.ID_Company)))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo).ToString
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00BISTRO ITALIANO CORPORATION   1220060391551174
                    s = "00" & Strings.Left(CompName.PadRight(30, " "c), 30).ToUpper
                    s &= m & y & Strings.Left(CompanySSN.Replace("-", "").PadLeft(10, " "c), 10)
                    FileSystem.PrintLine(fnum, s)

                    For Each dr In myDT_SSSLoanRemittance_Detail.Rows
                        '20ACUÒA          JAMES RAYMOND  M3372442364    0.00    0.00 1410.00  0.00  0.00  0.00  0.00  0.00 10.00122006N
                        Select Case MonthId
                            Case 1, 4, 7, 10
                                REMIT1st = CDec(dr.Item("TotalSSS"))
                                EC1st = CDec(dr.Item("SSSEC"))
                                TotalREMIT1st += REMIT1st
                                TotalEC1st += EC1st
                            Case 2, 5, 8, 11
                                REMIT2nd = CDec(dr.Item("TotalSSS"))
                                EC2nd = CDec(dr.Item("SSSEC"))
                                TotalREMIT2nd += REMIT2nd
                                TotalEC2nd += EC2nd
                            Case 3, 6, 9, 12
                                REMIT3rd = CDec(dr.Item("TotalSSS"))
                                EC3rd = CDec(dr.Item("SSSEC"))
                                TotalREMIT3rd += REMIT3rd
                                TotalEC3rd += EC3rd
                        End Select
                        r = Strings.Left(dr.Item("Remarks").ToString.PadRight(1, " "c), 1).ToUpper
                        s = "20"
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(15, " "c), 15).ToUpper
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(15, " "c), 15).ToUpper
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "c), 1).ToUpper
                        s &= Strings.Left(dr.Item("SSSNo").ToString.Replace("-", "").PadRight(10, " "c), 10)
                        s &= Strings.Left(Format(REMIT1st, "0.00").PadLeft(8, " "c), 8)
                        s &= Strings.Left(Format(REMIT2nd, "0.00").PadLeft(8, " "c), 8)
                        s &= Strings.Left(Format(REMIT3rd, "0.00").PadLeft(8, " "c), 8)
                        s &= "0.00".PadLeft(6, " "c)
                        s &= "0.00".PadLeft(6, " "c)
                        s &= "0.00".PadLeft(6, " "c)
                        s &= Strings.Left(Format(EC1st, "0.00").PadLeft(6, " "c), 6)
                        s &= Strings.Left(Format(EC2nd, "0.00").PadLeft(6, " "c), 6)
                        s &= Strings.Left(Format(EC3rd, "0.00").PadLeft(6, " "c), 6)
                        s &= "      "
                        s &= r
                        Select Case r
                            Case "1"
                                s &= Format(IIf(IsDBNull(dr.Item("DateHired")), vNow, dr.Item("DateHired")), "MMddyyyy")
                            Case "2"
                                s &= Format(IIf(IsDBNull(dr.Item("DateSeparated")), vNow, dr.Item("DateSeparated")), "MMddyyyy")
                            Case "N"
                                s &= "0"
                        End Select
                        FileSystem.PrintLine(fnum, s)
                        REMIT1st = 0
                        REMIT2nd = 0
                        REMIT3rd = 0
                        EC1st = 0
                        EC2nd = 0
                        EC3rd = 0
                    Next
                    '99        0.00        0.00    40420.00      0.00      0.00      0.00      0.00      0.00    290.00
                    s = "99"
                    s &= Strings.Left(Format(TotalREMIT1st, "0.00").PadLeft(12, " "c), 12)
                    s &= Strings.Left(Format(TotalREMIT2nd, "0.00").PadLeft(12, " "c), 12)
                    s &= Strings.Left(Format(TotalREMIT3rd, "0.00").PadLeft(12, " "c), 12)
                    s &= "0.00".PadLeft(10, " "c)
                    s &= "0.00".PadLeft(10, " "c)
                    s &= "0.00".PadLeft(10, " "c)
                    s &= Strings.Left(Format(TotalEC1st, "0.00").PadLeft(10, " "c), 10)
                    s &= Strings.Left(Format(TotalEC2nd, "0.00").PadLeft(10, " "c), 10)
                    s &= Strings.Left(Format(TotalEC3rd, "0.00").PadLeft(10, " "c), 10)
                    FileSystem.Print(fnum, s)
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

End Class
