'Option Explicit On
'Option Strict On

'

'Friend Class ImportedAttendanceInfo
'    Inherits InfoSet

'    Private myDT As New Database.Tables.tImportedAttendance(Connection)
'    Private myDT_ImportedAttendance_Detail As New Database.Tables.tImportedAttendance_Detail(Connection)
'    Private mControl As New InSys.DataControl 'Private mControl As New nDB.ImportedAttendanceControl

'    Private mImportButton As ToolStripButton
'    Private mPostButton As ToolStripButton
'    Private mVoidButton As ToolStripButton
'    Private mGrid As New GSDevExpressGridControl
'    Private WithEvents mGridView As DevExpress.XtraGrid.Views.Grid.GridView

'    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(myDT)
'            .Add(myDT_ImportedAttendance_Detail)
'        End With

'        Dim pdc As DataColumn
'        Dim cdc As DataColumn
'        Dim rel As DataRelation
'        InitControl(pMenu)
'        pdc = myDT.Columns(Database.Tables.tImportedAttendance.Field.ID)
'        cdc = myDT_ImportedAttendance_Detail.Columns(Database.Tables.tImportedAttendance_Detail.Field.ID_ImportedAttendance)
'        rel = mDataset.Relations.Add(pdc, cdc)

'        myDT.Columns(Database.Tables.tImportedAttendance.Field.ID_Company).DefaultValue = nDB.GetCompanyID


'        InitGrid()
'        Me.AddControl(mGrid, "Details")
'        mGridView = mGrid.GridView

'        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ImportFile)
'        mPostButton = MyBase.AddButton("Post Attendance File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferAttendance)
'        mVoidButton = MyBase.AddButton("Void", gMainForm.imgList.Images("misc.a.ico"), AddressOf Void)

'        Me.NoGridTables = myDT_ImportedAttendance_Detail.TableName
'        Me.ReloadAfterCommit = True
'        AfterNew()
'    End Sub

'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, Database.Tables.tImportedAttendance)
'        End Set
'    End Property



'#Region "LoadInfo"

'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        'myDT_ImportedAttendance_Detail.ClearThenFill(Database.Tables.tImportedAttendance_Detail.Field.ID_ImportedAttendance.ToString & "=" & pID.ToString)
'        MyBase.LoadInfo(pID)
'        mImportButton.Enabled = Not (myDT_ImportedAttendance_Detail.Rows.Count > 0)
'        mPostButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedAttendance.Field.IsPosted)) AndAlso (myDT_ImportedAttendance_Detail.Rows.Count > 0)
'        mVoidButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedAttendance.Field.IsVoided)) AndAlso (myDT_ImportedAttendance_Detail.Rows.Count > 0) AndAlso CBool(myDT.Get(Database.Tables.tImportedAttendance.Field.IsPosted))
'    End Sub

'#End Region

'#Region "SetDefaultValues"

'    'Protected Overrides Sub SetDefaultValues()
'    '    Dim vID As Integer
'    '    vID = CInt(myDT.Get(Database.Tables.tImportedAttendance.Field.ID))
'    '    myDT_ImportedAttendance_Detail.Columns(Database.Tables.tImportedAttendance_Detail.Field.ID_ImportedAttendance).DefaultValue = vID
'    'End Sub

'#End Region

'    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
'        Dim MyDialog As New OpenFileDialog()
'        'Dim y As String

'        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
'        'y = Format(myDT.Get(Database.Tables.tRF1.Field.Year), "0000")
'        'MyDialog.FileName = "PH" & y & myDT.Get(Database.Tables.tRF1.Field.ID_Quarter).ToString & ".txt"
'        MyDialog.FilterIndex = 0
'        MyDialog.CheckFileExists = False
'        MyDialog.CheckPathExists = True
'        If (MyDialog.ShowDialog() = DialogResult.OK) Then
'            TransferExcelData(MyDialog.FileName)
'            Me.mImportButton.Enabled = False
'            Me.mPostButton.Enabled = False
'            Me.mVoidButton.Enabled = False
'        End If
'    End Sub

'    Private Sub TransferAttendance(ByVal sender As Object, ByVal e As EventArgs)
'        If MsgBox("Do you want to post attendance file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
'            GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Attendance " & myDT.Get(Database.Tables.tImportedAttendance.Field.ID).ToString, Connection)
'            Me.mPostButton.Enabled = False
'            LoadInfo(CInt(myDT.Get(Database.Tables.tImportedAttendance.Field.ID)))
'            MsgBox("Finish posting attendance.", MsgBoxStyle.Information)
'        End If
'    End Sub

'    Private Sub Void(ByVal sender As Object, ByVal e As EventArgs)
'        If MsgBox("Do you want to void attendance file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
'            GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Attendance_Void " & myDT.Get(Database.Tables.tImportedAttendance.Field.ID).ToString, Connection)
'            Me.mVoidButton.Enabled = False
'            LoadInfo(CInt(myDT.Get(Database.Tables.tImportedAttendance.Field.ID)))
'            MsgBox("Finish voiding attendance.", MsgBoxStyle.Information)
'        End If
'    End Sub

'    Private Sub TransferExcelData(ByVal FileName As String)
'        Dim dt As New DataTable
'        Dim cn As System.Data.OleDb.OleDbConnection
'        Dim cmd As System.Data.OleDb.OleDbDataAdapter


'        Try
'            Me.BeginProcess("Transferring from excel file, please wait...")

'            cn = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;" & _
'                        "data source=" & FileName & ";Extended Properties=Excel 8.0;")
'            cmd = New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", cn)

'            cn.Open()
'            'myDT_ImportedAttendance_Detail.Clear()
'            cmd.Fill(myDT_ImportedAttendance_Detail)

'            For Each dr As DataRow In myDT_ImportedAttendance_Detail.Rows
'                'SetAdded is for Unchangeds only
'                If dr.RowState = DataRowState.Unchanged Then
'                    dr.SetAdded()
'                End If
'            Next

'            cn.Close()
'            mGrid.GridView.BestFitColumns()
'            Me.EndProcess("")
'        Catch ex As Exception
'            Me.EndProcess("Error occur while importing data.", False)
'        End Try
'    End Sub

'    Protected Overrides Function CanSave() As Boolean
'        'myDT_ImportedAttendance_Detail.AcceptChanges()
'        Return MyBase.CanSave()
'    End Function

'    Private Sub InitGrid()
'        mGrid.DataSource = myDT_ImportedAttendance_Detail
'        'mGrid.GridView.Columns.ColumnByFieldName(Database.Tables.tImportedAttendance_Detail.Field.ID_ImportedAttendance.ToString).Visible = False
'    End Sub

'    Private Sub mGridView_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGridView.DataSourceChanged
'        mGrid.GridView.Columns.ColumnByFieldName(Database.Tables.tImportedAttendance_Detail.Field.ID_ImportedAttendance.ToString).Visible = False
'    End Sub

'End Class
