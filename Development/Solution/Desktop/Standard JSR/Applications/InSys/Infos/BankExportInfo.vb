Option Explicit On
Option Strict Off




Friend Class BankExportInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tBankExport(Connection)
    Private myDT_BankExport_Detail As DataTable 'New Database.Tables.tBankExport_Detail(Connection)
    Private myDT_CompanyBankAcctNo As New Database.Tables.tCompanyBankAcct(Connection)
    Private myDT_CompanyBankAcctID As New Database.Tables.tCompanyBankAcctSetting(Connection)
    Private mControl As New InSys.DataControl
    'Private mLoadButton As ToolStripButton
    Private mBEButton As ToolStripButton
    'Private mBEButton2 As ToolStripButton
    Private mReportViewerSummary As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private mReportViewerDetail As CrystalDecisions.Windows.Forms.CrystalReportViewer

    ' Private mReportViewerTextFile As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Dim arr() As String = New String(14) {}


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(myDT_BankExport_Detail)
        End With
        InitControl(pMenu)
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tBankExport.Field.ID)
        'cdc = myDT_BankExport_Detail.Columns(Database.Tables.tBankExport_Detail.Field.ID_BankExport)
        'rel = mDataset.Relations.Add(pdc, cdc)

        myDT.Columns(Database.Tables.tBankExport.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        ' mLoadButton = MyBase.AddButton("Load", gMainForm.imgList.Images("ImportFile.png"), AddressOf Load_Employee)
        'AddHandler mLoadButton.Click, AddressOf Load_Employee
        mBEButton = MyBase.AddButton("Generate File", gMainForm.imgList.Images("GenerateFile.png"), AddressOf Generate_TextFile)
        'AddHandler mBEButton.Click, AddressOf Generate_TextFile
        'mBEButton2 = MyBase.AddButton("Export File", gMainForm.imgList.Images("GenerateFile.png"), AddressOf GenTemplate)
        'mBEButton2 = Me.GetStripButton("Export File")
        'AddHandler mBEButton2.Click, AddressOf GenTemplate
        mReportViewerSummary = AddReportViewer("Bank Summary")
        mReportViewerSummary.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rd.OpenSubreport()

        mReportViewerDetail = AddReportViewer("Bank Detail")
        mReportViewerDetail.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None


        myDT_BankExport_Detail = Me.mDataset.Tables("tBankExport_Detail")
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mBEButton.Enabled = Not pID = 0 ' new mode
        'mLoadButton.Enabled = (pID <> 0) ' new mode
        MyBase.LoadInfo(pID)
        'CUSTOMIZED

        Dim dt As DataTable
        dt = GSCOM.SQL.TableQuery("SELECT * FROM vzCompanyBankExportSummary WHERE ID_BankExport=" & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, Connection)
        Dim sdt As DataTable
        sdt = GSCOM.SQL.TableQuery("SELECT * FROM vCompanyBankAcctSignatory", Connection)
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "BankAdviceSummary.rpt")
        rd.SetDataSource(dt)
        mReportViewerSummary.ReportSource = rd
        rd.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
        For Each srd As CrystalDecisions.CrystalReports.Engine.ReportDocument In rd.Subreports
            srd = rd.OpenSubreport(srd.Name)
            For Each dsc As CrystalDecisions.Shared.IConnectionInfo In srd.DataSourceConnections
                dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
            Next
            srd.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
            srd.SetDataSource(sdt)
        Next
        mReportViewerSummary.Zoom(1)

        Dim dt1 As DataTable
        dt1 = GSCOM.SQL.TableQuery("SELECT * FROM vzCompanyBankExport WHERE ID_BankExport=" & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, Connection)
        Dim rd1 As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        rd1.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "BankAdviceDetail.rpt")
        rd1.SetDataSource(dt1)
        mReportViewerDetail.ReportSource = rd1
        rd1.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
        For Each srd1 As CrystalDecisions.CrystalReports.Engine.ReportDocument In rd1.Subreports
            srd1 = rd1.OpenSubreport(srd1.Name)
            '
            For Each dsc As CrystalDecisions.Shared.IConnectionInfo In srd1.DataSourceConnections
                dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
            Next
            srd1.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
            srd1.SetDataSource(sdt)
        Next
        mReportViewerDetail.Zoom(1)

    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tBankExport)
        End Set
    End Property


#End Region

#Region "Procedures"

    Private Sub Load_Employee(ByVal sender As Object, ByVal e As EventArgs)
        'If MsgBox("Load data?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
        'BeginProcess("Loading Bank details... Please wait.")
        ''GSCOM.SQL.ExecuteNonQuery("EXEC pHDMFExport " & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, Connection)
        'GSCOM.SQL.ExecuteNonQuery("EXEC pBankExport " & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, Connection)
        'LoadInfo(CInt(myDT.Get(Database.Tables.tBankExport.Field.ID)))
        'Application.DoEvents()
        'EndProcess("")
        'End If
    End Sub

    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim CheckIfPosted As DataTable = GSCOM.SQL.TableQuery("SELECT * from tUserPayrollPeriod where ID_User = " & nDB.GetUserID & " and ID_PayrollPeriod = " & myDT.Get(Database.Tables.tBankExport.Field.ID_CompanyBankAcct).ToString, Connection)
        Dim CheckIfPosted As DataTable = GSCOM.SQL.TableQuery("SELECT * from tUserPayrollPeriod where ID_User = " & nDB.GetUserID & " and ID_PayrollPeriod = " & myDT.Get(Database.Tables.tBankExport.Field.ID_PayrollPeriod).ToString, Connection)

        If CheckIfPosted.Rows.Count <> 0 Then
            Dim MyDialog As New SaveFileDialog()
            'Dim FName_DT As DataTable

            'FName_DT = GSCOM.SQL.TableQuery("SELECT [Value] FROM tCompanyBankAcctSetting WHERE [Name]='FileName' AND ID_CompanyBankAcct=" & myDT.Get(Database.Tables.tBankExport.Field.ID_CompanyBankAcct).ToString, Connection)

            'Dim ExportFileName As String = FName_DT.Rows(0)(0)
            'Dim ExportExtentionName As String = Split(FName_DT.Rows(0)(0).ToString, ".", , CompareMethod.Text).GetValue(Split(FName_DT.Rows(0)(0).ToString, ".", , CompareMethod.Text).Length - 1)

            'MyDialog.Filter = "Default " & "(*." & ExportExtentionName & ")|*." & ExportExtentionName & "|All files (*.*)|*.*"     '|Text files (*.txt)|*.txt"
            MyDialog.Filter = "Text File  s (*.txt)|*.txt|All Files|*.*"
            'MyDialog.FileName = ExportFileName
            MyDialog.FileName = "*.txt"
            MyDialog.FilterIndex = 1
            MyDialog.CheckFileExists = False
            MyDialog.CheckPathExists = True
            If (MyDialog.ShowDialog() = DialogResult.OK) Then
                Save_File(MyDialog.FileName)
            End If
        Else
            MsgBox("Cannot generate bank export file." & vbCrLf & "Please post payroll period first.", vbCritical)
        End If

    End Sub

    Private Sub Save_File(ByVal FileName As String)
        Dim fnum As Integer
        'Dim s As String
        'Dim dr As DataRow
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_BankExport_Detail Is Nothing)) Then
                If myDT_BankExport_Detail.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
                    myDT_CompanyBankAcctNo.ClearThenFill("ID=" & myDT.Get(Database.Tables.tBankExport.Field.ID_CompanyBankAcct))

                    'Dim dt As New nExtern.GenerateUploadFile.GenerateUploadFile
                    FileSystem.Print(fnum, Me.GetText(myDT.Get(Database.Tables.tBankExport.Field.ID), gConnection))

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


    Public Overloads Function GetText(ByVal pID As Integer, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim s As String = ""
        Dim dt As New DataTable

        GSCOM.SQL.FillTable(dt, "EXEC pGenerateTextFile " & pID.ToString, gConnection)
        For Each dr As DataRow In dt.Rows
            s &= dr(0).ToString & vbCrLf
        Next
        's = s.Trim
        Return s
    End Function

#End Region

    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.LeaveFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.LeaveFileAdapter

        sfd.FileName = "BankExport.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "BankExport.xls", sfd.FileName, True)
            a.DataSource = sfd.FileName 'initialize datasource (filename)

            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable

            Dim s As String = ""


            s &= "Select LastName,Firstname,MiddleName,BankAcctNo,NetAmt from vBankExport_Detail where ID_BankExport = " & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString



            dt = GSCOM.SQL.TableQuery(s, Connection)

            UseArray(sfd.FileName, dt)


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

#Region "BankExportInfo_Saved"

    Private Sub BankExportInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        'If e.RowState = DataRowState.Added Then
        '    GSCOM.SQL.ExecuteNonQuery("EXEC pBankExport " & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, e.Transaction)
        'End If

        'GSCOM.SQL.ExecuteNonQuery("EXEC pBankExport " & myDT.Get(Database.Tables.tBankExport.Field.ID).ToString, gConnection)
    End Sub

#End Region


End Class