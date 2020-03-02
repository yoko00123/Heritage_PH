Option Explicit On
Option Strict Off



Friend Class SSSLoanExportInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tSSSLoanExport(Connection)
    Private myDT_SSSLoanExport_Detail As New Database.Tables.tSSSLoanExport_Detail(Connection)
    Private myDT_Company As New Database.Tables.tCompany(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.SSSLoanExportControl
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mLoadButton As ToolStripButton
    Private mMCLButton As ToolStripButton
    Private mBNButton As ToolStripButton
    Private mNR3001DK As ToolStripButton
    Private mDBF As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_SSSLoanExport_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tSSSLoanExport.Field.ID)
        cdc = myDT_SSSLoanExport_Detail.Columns(Database.Tables.tSSSLoanExport_Detail.Field.ID_SSSLoanExport)
        rel = mDataset.Relations.Add(pdc, cdc)
        mReportViewer = AddReportViewer("Report Summary")
        mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        myDT.Columns(Database.Tables.tSSSLoanExport.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tSSSLoanExport.Field.Year).DefaultValue = nDB.GetServerDate.Year

        mLoadButton = MyBase.AddButton("Load", gMainForm.imgList.Images("ImportFile.png"), AddressOf Load_Employee)
        mMCLButton = MyBase.AddButton("MCL File", gMainForm.imgList.Images("SSS.png"), AddressOf Generate_TextFile)
        mBNButton = MyBase.AddButton("Banknet File", gMainForm.imgList.Images("SSS.png"), AddressOf Generate_TextFile)
        mNR3001DK = MyBase.AddButton("NR3001DK File", gMainForm.imgList.Images("SSS.png"), AddressOf NR3001DK_TextFile)
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mLoadButton.Enabled = Not pID = 0 ' new mode
        mMCLButton.Enabled = Not pID = 0 ' new mode
        mBNButton.Enabled = Not pID = 0 ' new mode
        mNR3001DK.Enabled = Not pID = 0 ' new mode
        mDBF.Enabled = Not pID = 0 ' new mode

        'myDT_SSSLoanExport_Detail.ClearThenFill("ID_SSSLoanExport=" & pID.ToString)
        MyBase.LoadInfo(pID)
        'CUSTOMIZED
        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT * FROM vzSSSLoanExportSummary WHERE ID=" & myDT.Get(Database.Tables.tSSSLoanExport.Field.ID).ToString, Connection)
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "SSS NET Summary.rpt")
        rd.SetDataSource(dt)
        mReportViewer.ReportSource = rd
        mReportViewer.Zoom(1)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tSSSLoanExport)
        End Set
    End Property

#End Region

#Region "Procedures"

    Private Sub Load_Employee(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Load data?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Loading SSS details... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pSSSLoanExport " & myDT.Get(Database.Tables.tSSSLoanExport.Field.ID).ToString, Connection)
            LoadInfo(CInt(myDT.Get(Database.Tables.tSSSLoanExport.Field.ID)))
            Application.DoEvents()
            EndProcess("")
        End If
    End Sub



    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim m As String
        Dim y As String

        MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        m = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Month), "00")
        y = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.Year), "0000")
        MyDialog.FileName = "R3" & m & y & "." & Format(nDB.GetServerDate, "MMddHHmm")
        MyDialog.FilterIndex = 2
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            If CType(sender, ToolStripButton) Is mMCLButton Then
                Save_MCL_File(MyDialog.FileName)
            ElseIf CType(sender, ToolStripButton) Is mBNButton Then
                Save_BN_File(MyDialog.FileName)
            End If
        End If
    End Sub

    Private Sub NR3001DK_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        MyDialog.Filter = "All files (*.*)|*.*"
        MyDialog.FileName = "NR3001DK"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            If CType(sender, ToolStripButton) Is mNR3001DK Then
                Save_NR3001DK_File(MyDialog.FileName)
            End If
        End If
    End Sub

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
            If (Not (myDT_SSSLoanExport_Detail Is Nothing)) Then
                If myDT_SSSLoanExport_Detail.Rows.Count > 0 Then
                    m = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Company))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name)
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo)
                    fnum = FreeFile()
                    'MsgBox("Please insert diskette to drive A: then press ok to continue")
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00VIZCARRA PHARMA INC           0420060387665045
                    s = "00   1" & m & y & "         " & Format(vNow, "yyyMMdd")
                    s &= y & m
                    s &= Strings.Left(CompName.PadRight(40, " "), 40)
                    'CompanySSN = "1234567890"
                    s &= Strings.Left(CompanySSN.PadLeft(10, " "), 10) & "   C"

                    FileSystem.PrintLine(fnum, s)
                    For Each dr In myDT_SSSLoanExport_Detail.Rows
                        s = "20   "
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(20, " "), 20)
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(20, " "), 20)
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "), 1)
                        s &= Strings.Left(dr.Item("SSSNo").ToString.PadRight(10, " "), 10)
                        s &= Strings.Left(Format(dr.Item("TotalSSS"), "0.00").PadLeft(8, " "), 8)
                        s &= "0.00".PadLeft(8, " ")
                        s &= Strings.Left(Format(dr.Item("SSSEC"), "0.00").PadLeft(8, " "), 8)
                        s &= Format(IIf(IsDBNull(dr.Item("DateHired")), vNow, dr.Item("DateHired")), "yyyyMMdd") & "N"
                        FileSystem.PrintLine(fnum, s)
                    Next
                    s = "99   "
                    s &= Strings.Left(Format(myDT_SSSLoanExport_Detail.Compute("SUM(TotalSSS)", ""), "0.00").PadLeft(14, " "), 14)
                    s &= "0.00".PadLeft(10, " ")
                    s &= Strings.Left(Format(myDT_SSSLoanExport_Detail.Compute("SUM(SSSEC)", ""), "0.00").PadLeft(12, " "), 12)
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
            If (Not (myDT_SSSLoanExport_Detail Is Nothing)) Then
                If myDT_SSSLoanExport_Detail.Rows.Count > 0 Then
                    m = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Company))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name)
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo)
                    fnum = FreeFile()
                    'MsgBox("Please insert diskette to drive A: then press ok to continue")
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00VIZCARRA PHARMA INC           0420060387665045
                    s = "00"
                    s &= Strings.Left(CompName.PadRight(30, " "), 30)
                    s &= m & y
                    s &= Strings.Left(CompanySSN, 10)

                    FileSystem.PrintLine(fnum, s)
                    '20ACOSTA         SATURNINA      A3329325573 1128.00    0.00    0.00  0.00  0.00  0.00 10.00  0.00  0.00      N19981216
                    For Each dr In myDT_SSSLoanExport_Detail.Rows
                        s = "20"
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(15, " "), 15)
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(15, " "), 15)
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "), 1)
                        s &= Strings.Left(dr.Item("SSSNo").ToString.PadRight(10, " "), 10)
                        s &= Strings.Left(Format(dr.Item("TotalSSS"), "0.00").PadLeft(8, " "), 8)
                        s &= "    0.00    0.00  0.00  0.00  0.00"
                        s &= Strings.Left(Format(dr.Item("SSSEC"), "0.00").PadLeft(6, " "), 6)
                        s &= "  0.00  0.00      N"
                        s &= Format(IIf(IsDBNull(dr.Item("DateHired")), nDB.GetServerDate, dr.Item("DateHired")), "yyyyMMdd")
                        FileSystem.PrintLine(fnum, s)
                    Next
                    '99     1927.00        0.00        0.00      0.00      0.00      0.00     20.00      0.00      0.00
                    s = "99"
                    s &= Strings.Left(Format(myDT_SSSLoanExport_Detail.Compute("SUM(TotalSSS)", ""), "0.00").PadLeft(12, " "), 12)
                    s &= "        0.00        0.00      0.00      0.00      0.00     "
                    s &= Strings.Left(Format(myDT_SSSLoanExport_Detail.Compute("SUM(SSSEC)", ""), "0.00").PadLeft(10, " "), 10)
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
        Try
            If (Not (myDT_SSSLoanExport_Detail Is Nothing)) Then
                If myDT_SSSLoanExport_Detail.Rows.Count > 0 Then
                    Dim REMIT1st As Decimal = 0.0
                    Dim REMIT2nd As Decimal = 0.0
                    Dim REMIT3rd As Decimal = 0.0
                    Dim EC1st As Decimal = 0.0
                    Dim EC2nd As Decimal = 0.0
                    Dim EC3rd As Decimal = 0.0
                    Dim TotalREMIT1st As Decimal = 0.0
                    Dim TotalREMIT2nd As Decimal = 0.0
                    Dim TotalREMIT3rd As Decimal = 0.0
                    Dim TotalEC1st As Decimal = 0.0
                    Dim TotalEC2nd As Decimal = 0.0
                    Dim TotalEC3rd As Decimal = 0.0

                    MonthId = myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Month)
                    m = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Month), "00")
                    y = Format(myDT.Get(Database.Tables.tSSSLoanExport.Field.Year), "0000")
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & myDT.Get(Database.Tables.tSSSLoanExport.Field.ID_Company))
                    CompName = myDT_Company.Get(Database.Tables.tCompany.Field.Name)
                    CompanySSN = myDT_Company.Get(Database.Tables.tCompany.Field.SSSNo)
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    '00BISTRO ITALIANO CORPORATION   1220060391551174
                    s = "00" & Strings.Left(CompName.PadRight(30, " "), 30).ToUpper
                    s &= m & y & Strings.Left(CompanySSN.Replace("-", "").PadLeft(10, " "), 10)
                    FileSystem.PrintLine(fnum, s)

                    For Each dr In myDT_SSSLoanExport_Detail.Rows
                        '20ACUÒA          JAMES RAYMOND  M3372442364    0.00    0.00 1410.00  0.00  0.00  0.00  0.00  0.00 10.00122006N
                        Select Case MonthId
                            Case 1, 4, 7, 10
                                REMIT1st = dr.Item("TotalSSS")
                                EC1st = dr.Item("SSSEC")
                                TotalREMIT1st += REMIT1st
                                TotalEC1st += EC1st
                            Case 2, 5, 8, 11
                                REMIT2nd = dr.Item("TotalSSS")
                                EC2nd = dr.Item("SSSEC")
                                TotalREMIT2nd += REMIT2nd
                                TotalEC2nd += EC2nd
                            Case 3, 6, 9, 12
                                REMIT3rd = dr.Item("TotalSSS")
                                EC3rd = dr.Item("SSSEC")
                                TotalREMIT3rd += REMIT3rd
                                TotalEC3rd += EC3rd
                        End Select

                        s = "20"
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(15, " "), 15).ToUpper
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(15, " "), 15).ToUpper
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "), 1).ToUpper
                        s &= Strings.Left(dr.Item("SSSNo").ToString.Replace("-", "").PadRight(10, " "), 10)
                        s &= Strings.Left(Format(REMIT1st, "0.00").PadLeft(8, " "), 8)
                        s &= Strings.Left(Format(REMIT2nd, "0.00").PadLeft(8, " "), 8)
                        s &= Strings.Left(Format(REMIT3rd, "0.00").PadLeft(8, " "), 8)
                        s &= "0.00".PadLeft(6, " ")
                        s &= "0.00".PadLeft(6, " ")
                        s &= "0.00".PadLeft(6, " ")
                        s &= Strings.Left(Format(EC1st, "0.00").PadLeft(6, " "), 6)
                        s &= Strings.Left(Format(EC2nd, "0.00").PadLeft(6, " "), 6)
                        s &= Strings.Left(Format(EC3rd, "0.00").PadLeft(6, " "), 6)
                        s &= Format(IIf(IsDBNull(dr.Item("DateHired")), vNow, dr.Item("DateHired")), "MMyyyy") & "N"
                        FileSystem.PrintLine(fnum, s)
                        REMIT1st = 0.0
                        REMIT2nd = 0.0
                        REMIT3rd = 0.0
                        EC1st = 0.0
                        EC2nd = 0.0
                        EC3rd = 0.0
                    Next
                    '99        0.00        0.00    40420.00      0.00      0.00      0.00      0.00      0.00    290.00
                    s = "99"
                    s &= Strings.Left(Format(TotalREMIT1st, "0.00").PadLeft(12, " "), 12)
                    s &= Strings.Left(Format(TotalREMIT2nd, "0.00").PadLeft(12, " "), 12)
                    s &= Strings.Left(Format(TotalREMIT3rd, "0.00").PadLeft(12, " "), 12)
                    s &= "0.00".PadLeft(10, " ")
                    s &= "0.00".PadLeft(10, " ")
                    s &= "0.00".PadLeft(10, " ")
                    s &= Strings.Left(Format(TotalEC1st, "0.00").PadLeft(10, " "), 10)
                    s &= Strings.Left(Format(TotalEC2nd, "0.00").PadLeft(10, " "), 10)
                    s &= Strings.Left(Format(TotalEC3rd, "0.00").PadLeft(10, " "), 10)
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
