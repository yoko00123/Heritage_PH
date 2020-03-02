Option Explicit On
Option Strict On



Friend Class ImportedLoanInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tImportedLoan(Connection)
    Private mtImportedLoan_Detail As New Database.Tables.tImportedLoan_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.ImportedLoanControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    ' Private mPostButton As ToolStripButton
    ' Private mVoidButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtImportedLoan_Detail)
        End With
        'Dim pdc As DataColumn
        '  Dim cdc As DataColumn
        '  Dim rel As DataRelation
        InitControl(pMenu)
        ' pdc = myDT.Columns(Database.Tables.tImportedLoan.Field.ID)
        ' cdc = mtImportedLoan_Detail.Columns(Database.Tables.tImportedLoan_Detail.Field.ID_ImportedLoan)
        ' rel = mDataset.Relations.Add(pdc, cdc)
        myDT.Columns(Database.Tables.tImportedLoan.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        'mGenTemplateButton = Me.GetStripButton("Generate Template")
        'AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        'mImportButton = GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        'AddHandler mImportButton.Click, AddressOf ImportFile

        'mPostButton = GetStripButton("Post Loan File") 'MyBase.AddButton("Post Loan File", gMainForm.imgList.Images("ApplyFile.png"), AddressOf TransferLoan)
        'AddHandler mPostButton.Click, AddressOf TransferLoan

        'mVoidButton = GetStripButton("Void") 'MyBase.AddButton("Void", gMainForm.imgList.Images("Cancel.png"), AddressOf Void)
        'AddHandler mVoidButton.Click, AddressOf Void
        AfterNew()
    End Sub

    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.OBFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tImportedLoan.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Loan.xls", sfd.FileName, True)

            a.DataSource = sfd.FileName
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable

            'Dim s As String
            's &= "SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ")"

            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") Where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")
            'UseArray(sfd.FileName, dt)



            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tImportedLoan)
        End Set
    End Property



#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        ' mImportButton.Enabled = Not (mtImportedLoan_Detail.Rows.Count > 0)
        'mPostButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedLoan.Field.IsPosted)) AndAlso (mtImportedLoan_Detail.Rows.Count > 0)
        'mVoidButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedLoan.Field.IsVoided)) AndAlso (mtImportedLoan_Detail.Rows.Count > 0) AndAlso CBool(myDT.Get(Database.Tables.tImportedLoan.Field.IsPosted))
    End Sub

#End Region

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            Me.mImportButton.Enabled = False
            ' Me.mPostButton.Enabled = False
            ' Me.mVoidButton.Enabled = False
        End If
    End Sub

    'Private Sub TransferLoan(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to post loan file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Loan " & myDT.Get(Database.Tables.tImportedLoan.Field.ID).ToString, Connection)
    '        Me.mPostButton.Enabled = False
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tImportedLoan.Field.ID)))
    '        MsgBox("Finish posting loan.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    'Private Sub Void(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to void loan file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Loan_Void " & myDT.Get(Database.Tables.tImportedLoan.Field.ID).ToString, Connection)
    '        Me.mVoidButton.Enabled = False
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tImportedLoan.Field.ID)))
    '        MsgBox("Finish voiding loan.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtImportedLoan_Detail)
            For Each dr As DataRow In mtImportedLoan_Detail.Rows
                If dr.RowState = DataRowState.Unchanged Then
                    dr.SetAdded()   'SetAdded is for Unchangeds only
                End If
            Next
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tImportedLoan.Field.Name, s)
            Me.EndProcess("")
        Catch ex As Exception
            Me.EndProcess("Error occur while importing data.", False)
        End Try
    End Sub

    Protected Overrides Function CanSave() As Boolean
        Return MyBase.CanSave()
    End Function

End Class

'Me.ReloadAfterCommit = True

'#Region "SetDefaultValues"

'    Protected Overrides Sub SetDefaultValues()
'        Dim vID As Integer
'        vID = CInt(myDT.Get(Database.Tables.tImportedLoan.Field.ID))
'        mtImportedLoan_Detail.Columns(Database.Tables.tImportedLoan_Detail.Field.ID_ImportedLoan).DefaultValue = vID
'    End Sub

'#End Region

'Private mGrid As New GSDevExpressGridControl
'Private WithEvents mGridView As DevExpress.XtraGrid.Views.Grid.GridView

'Private Sub InitGrid()
'    mGrid.DataSource = mtImportedLoan_Detail
'End Sub

'Private Sub mGridView_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGridView.DataSourceChanged
'    mGrid.GridView.Columns.ColumnByFieldName(Database.Tables.tImportedLoan_Detail.Field.ID_ImportedLoan.ToString).Visible = False
'End Sub

'Me.NoGridTables = mtImportedLoan_Detail.TableName

'InitGrid()
'Me.AddControl(mGrid, "Details")
'mGridView = mGrid.GridView
