Option Explicit On
Option Strict Off



Friend Class RF1Info
    Inherits InfoSet

    Private myDT As New Database.Tables.tRF1(Connection)
    Private myDT_RF1_Detail As New Database.Tables.tRF1_Detail(Connection)
    Private myDT_Company As New Database.Tables.tCompany(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.RF1Control
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mRF1Button As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_RF1_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tRF1.Field.ID)
        cdc = myDT_RF1_Detail.Columns(Database.Tables.tRF1_Detail.Field.ID_RF1)
        rel = mDataset.Relations.Add(pdc, cdc)
        mReportViewer = AddReportViewer("Report Summary")
        mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

        myDT.Columns(Database.Tables.tRF1.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tRF1.Field.Year).DefaultValue = nDB.GetServerDate.Year

        mRF1Button = MyBase.AddButton("Generate RF1 File", gMainForm.imgList.Images("PhilHealth.png"), AddressOf Generate_TextFile)
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mRF1Button.Enabled = Not pID = 0 ' new mode
        'myDT_RF1_Detail.ClearThenFill("ID_RF1=" & pID.ToString)
        MyBase.LoadInfo(pID)
        'CUSTOMIZED
        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT * FROM vzRF1 WHERE ID_RF1=" & myDT.Get(Database.Tables.tRF1.Field.ID).ToString, Connection)
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "PHICRF1Quarterly.rpt")
        rd.SetDataSource(dt)
        mReportViewer.ReportSource = rd
        mReportViewer.Zoom(1)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tRF1)
        End Set
    End Property



    'Protected Overrides Sub SetDefaultValues()
    '    Dim vID As Integer
    '    vID = CInt(myDT.Get(Database.Tables.tRF1.Field.ID))
    '    myDT_RF1_Detail.Columns(Database.Tables.tRF1_Detail.Field.ID_RF1).DefaultValue = vID
    'End Sub

#End Region

#Region "Procedures"

    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim y As String

        MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        y = Format(myDT.Get(Database.Tables.tRF1.Field.Year), "0000")
        MyDialog.FileName = "PH" & y & myDT.Get(Database.Tables.tRF1.Field.ID_Quarter).ToString & ".txt"
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_PHIC_File(MyDialog.FileName)
        End If
    End Sub

    Private Sub Save_PHIC_File(ByVal FileName As String)
        'Dim m As String
        'Dim y As String
        Dim fnum As Integer
        Dim s As String
        Dim dr As DataRow
        'must save the current datetime because successive calls to getserverdate yields different values
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_RF1_Detail Is Nothing)) Then
                If myDT_RF1_Detail.Rows.Count > 0 Then
                    ' get company info
                    myDT_Company.ClearThenFill("ID=" & myDT.Get(Database.Tables.tRF1.Field.ID_Company))
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    FileSystem.PrintLine(fnum, "REMITTANCE REPORT")
                    FileSystem.PrintLine(fnum, myDT_Company.Get(Database.Tables.tCompany.Field.Name).ToString.ToUpper)
                    FileSystem.PrintLine(fnum, myDT_Company.Get(Database.Tables.tCompany.Field.Address).ToString.ToUpper)

                    s = Strings.Left(myDT_Company.Get(Database.Tables.tCompany.Field.PhilHealthNo).ToString.PadRight(12, " "), 12).ToUpper
                    s &= myDT.Get(Database.Tables.tRF1.Field.ID_Quarter).ToString
                    s &= myDT.Get(Database.Tables.tRF1.Field.Year).ToString & "R"
                    FileSystem.PrintLine(fnum, s)
                    FileSystem.PrintLine(fnum, "MEMBERS")

                    '09/05/05    KIBAGAMI                      GENJURO                        00785000005000005000000000000000000000000000          '
                    For Each dr In myDT_RF1_Detail.Rows
                        s = Strings.Left(dr.Item("RefNo").ToString.PadRight(12, " "), 12).ToUpper
                        s &= Strings.Left(dr.Item("LastName").ToString.PadRight(30, " "), 30).ToUpper
                        s &= Strings.Left(dr.Item("FirstName").ToString.PadRight(30, " "), 30).ToUpper
                        s &= Strings.Left(dr.Item("MiddleName").ToString.PadRight(1, " "), 1).ToUpper
                        s &= Strings.Left(Replace(Format(dr.Item("MonthlyRate"), "0.00"), ".", "").PadLeft(8, " "), 8)

                        s &= Strings.Left(Replace(Format(dr.Item("PS1Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        s &= Strings.Left(Replace(Format(dr.Item("ES1Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        s &= Strings.Left(Replace(Format(dr.Item("PS2Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        s &= Strings.Left(Replace(Format(dr.Item("ES2Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        s &= Strings.Left(Replace(Format(dr.Item("PS3Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        s &= Strings.Left(Replace(Format(dr.Item("ES3Amt"), "0.00"), ".", "").PadLeft(6, "0"), 6)
                        's &= Strings.Left("concat employee status here".PadLeft(10, " "), 10)
                        s &= Strings.Left("          ".PadLeft(10, " "), 10)

                        FileSystem.PrintLine(fnum, s)
                    Next
                    ' summary
                    FileSystem.PrintLine(fnum, "M5-SUMMARY")
                    s = "1"
                    s &= Strings.Left(Replace(Format(myDT_RF1_Detail.Compute("SUM(PS1Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES1Amt)", ""), "0.00"), ".", "").PadLeft(8, "0"), 8)
                    s &= Strings.Left(myDT.Get(Database.Tables.tRF1.Field.M1RefNo).ToString.PadLeft(15, " "), 15)
                    s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1.Field.M1DatePaid)), vNow, myDT.Get(Database.Tables.tRF1.Field.M1DatePaid)), "MMddyyyy")
                    s &= Strings.Left(Format(myDT_RF1_Detail.Compute("COUNT(ID)", "PS1Amt > 0.00"), "0").ToString.PadLeft(3, "0"), 3)
                    FileSystem.PrintLine(fnum, s)
                    s = "2"
                    s &= Strings.Left(Replace(Format(myDT_RF1_Detail.Compute("SUM(PS2Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES2Amt)", ""), "0.00"), ".", "").PadLeft(8, "0"), 8)
                    s &= Strings.Left(myDT.Get(Database.Tables.tRF1.Field.M2RefNo).ToString.PadLeft(15, " "), 15)
                    s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1.Field.M2DatePaid)), vNow, myDT.Get(Database.Tables.tRF1.Field.M2DatePaid)), "MMddyyyy")
                    s &= Strings.Left(Format(myDT_RF1_Detail.Compute("COUNT(ID)", "PS2Amt > 0.00"), "0").ToString.PadLeft(3, "0"), 3)
                    FileSystem.PrintLine(fnum, s)
                    s = "3"
                    s &= Strings.Left(Replace(Format(myDT_RF1_Detail.Compute("SUM(PS3Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES3Amt)", ""), "0.00"), ".", "").PadLeft(8, "0"), 8)
                    s &= Strings.Left(myDT.Get(Database.Tables.tRF1.Field.M3RefNo).ToString.PadLeft(15, " "), 15)
                    s &= Format(IIf(IsDBNull(myDT.Get(Database.Tables.tRF1.Field.M3DatePaid)), vNow, myDT.Get(Database.Tables.tRF1.Field.M3DatePaid)), "MMddyyyy")
                    s &= Strings.Left(Format(myDT_RF1_Detail.Compute("COUNT(ID)", "PS3Amt > 0.00"), "0").ToString.PadLeft(3, "0"), 3)
                    FileSystem.PrintLine(fnum, s)

                    s = "GRAND TOTAL"
                    s &= Strings.Left(Replace(Format(myDT_RF1_Detail.Compute("SUM(PS1Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES1Amt)", "") + _
                        myDT_RF1_Detail.Compute("SUM(PS2Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES2Amt)", "") + _
                        myDT_RF1_Detail.Compute("SUM(PS3Amt)", "") + myDT_RF1_Detail.Compute("SUM(ES3Amt)", ""), "0.00"), ".", "").PadLeft(10, "0"), 10)
                    FileSystem.PrintLine(fnum, s)
                    s = Strings.Left(myDT.Get(Database.Tables.tRF1.Field.CertifiedBy).ToString.PadRight(40, " "), 40) & _
                        Strings.Left(myDT.Get(Database.Tables.tRF1.Field.CertifiedByPosition).ToString.PadRight(20, " "), 20)
                    FileSystem.PrintLine(fnum, s)
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

#Region "RF1Info_Saved"

    Private Sub RF1Info_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        ' If e.RowState = DataRowState.Added Then
        GSCOM.SQL.ExecuteNonQuery("EXEC pRF1 " & myDT.Get(Database.Tables.tRF1.Field.ID).ToString, e.Transaction)
        ' End If
    End Sub

#End Region

End Class
