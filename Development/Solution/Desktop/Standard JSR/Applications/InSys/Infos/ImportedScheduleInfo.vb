'Option Explicit On
'Option Strict On

'

'Friend Class ImportedScheduleInfo
'    Inherits InfoSet

'    Private myDT As New Database.Tables.tImportedSchedule(Connection)
'    Private myDT_ImportedSchedule_Detail As New Database.Tables.tImportedSchedule_Detail(Connection)
'    Private myDT_ImportedSchedule_Employee As New Database.Tables.tImportedSchedule_Employee(Connection)
'    Private mControl As New nDB.ImportedScheduleControl

'    Private mImportButton As ToolStripButton
'    Private mPostButton As ToolStripButton
'    Private mVoidButton As ToolStripButton
'    Private mGrid As New GSDevExpressGridControl
'    Private WithEvents mGridView As DevExpress.XtraGrid.Views.Grid.GridView

'    Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(myDT)
'            .Add(myDT_ImportedSchedule_Detail)
'            .Add(myDT_ImportedSchedule_Employee)
'        End With

'        Dim pdc1 As DataColumn
'        Dim cdc1 As DataColumn
'        Dim rel1 As DataRelation
'        Dim pdc2 As DataColumn
'        Dim cdc2 As DataColumn
'        Dim rel2 As DataRelation
'        pdc1 = myDT.Columns(Database.Tables.tImportedSchedule.Field.ID)
'        cdc1 = myDT_ImportedSchedule_Detail.Columns(Database.Tables.tImportedSchedule_Detail.Field.ID_ImportedSchedule)
'        rel1 = Dataset.Relations.Add(pdc1, cdc1)

'        pdc2 = myDT.Columns(Database.Tables.tImportedSchedule.Field.ID)
'        cdc2 = myDT_ImportedSchedule_Employee.Columns(Database.Tables.tImportedSchedule_Employee.Field.ID_ImportedSchedule)
'        rel2 = Dataset.Relations.Add(pdc2, cdc2)

'        myDT.Columns(Database.Tables.tImportedSchedule.Field.ID_Company).DefaultValue = nDB.GetCompanyID


'        InitGrid()
'        Me.AddControl(mGrid, "Details")
'        mGridView = mGrid.GridView

'        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ImportFile)
'        mPostButton = MyBase.AddButton("Post File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferSchedule)
'        mVoidButton = MyBase.AddButton("Void", gMainForm.imgList.Images("misc.a.ico"), AddressOf Void)

'        Me.NoGridTables = myDT_ImportedSchedule_Detail.TableName
'        Me.ReloadAfterCommit = True
'        AfterNew()
'    End Sub

'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, Database.Tables.tImportedSchedule)
'        End Set
'    End Property

'#Region "LoadInfo"

'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        'myDT_ImportedSchedule_Detail.ClearThenFill(Database.Tables.tImportedSchedule_Detail.Field.ID_ImportedSchedule.ToString & "=" & pID.ToString)
'        'myDT_ImportedSchedule_Employee.ClearThenFill(Database.Tables.tImportedSchedule_Employee.Field.ID_ImportedSchedule.ToString & "=" & pID.ToString)
'        MyBase.LoadInfo(pID)
'        mImportButton.Enabled = Not (myDT_ImportedSchedule_Detail.Rows.Count > 0)
'        mPostButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedSchedule.Field.IsPosted)) AndAlso (myDT_ImportedSchedule_Detail.Rows.Count > 0)
'        mVoidButton.Enabled = Not CBool(myDT.Get(Database.Tables.tImportedSchedule.Field.IsVoided)) AndAlso (myDT_ImportedSchedule_Detail.Rows.Count > 0) AndAlso CBool(myDT.Get(Database.Tables.tImportedSchedule.Field.IsPosted))
'    End Sub

'#End Region

'    '#Region "SetDefaultValues"

'    '    Protected Overrides Sub SetDefaultValues()
'    '        Dim vID As Integer
'    '        vID = CInt(myDT.Get(Database.Tables.tImportedSchedule.Field.ID))
'    '        myDT_ImportedSchedule_Detail.Columns(Database.Tables.tImportedSchedule_Detail.Field.ID_ImportedSchedule).DefaultValue = vID
'    '        myDT_ImportedSchedule_Employee.Columns(Database.Tables.tImportedSchedule_Employee.Field.ID_ImportedSchedule).DefaultValue = vID
'    '    End Sub

'    '#End Region

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

'    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
'        If MsgBox("Do you want to post the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'            GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Schedule " & myDT.Get(Database.Tables.tImportedSchedule.Field.ID).ToString, Connection)
'            Me.mPostButton.Enabled = False
'            LoadInfo(CInt(myDT.Get(Database.Tables.tImportedSchedule.Field.ID)))
'            MsgBox("Finished posting.", MsgBoxStyle.Information)
'        End If
'    End Sub

'    Private Sub Void(ByVal sender As Object, ByVal e As EventArgs)
'        If MsgBox("Do you want to void the file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
'            GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Schedule_Void " & myDT.Get(Database.Tables.tImportedSchedule.Field.ID).ToString, Connection)
'            Me.mVoidButton.Enabled = False
'            LoadInfo(CInt(myDT.Get(Database.Tables.tImportedSchedule.Field.ID)))
'            MsgBox("Finished voiding.", MsgBoxStyle.Information)
'        End If
'    End Sub

'    Private Sub TransferExcelData(ByVal FileName As String)
'        Dim dt As New DataTable
'        Dim o As Object = Nothing
'        Dim i As Integer
'        Dim bFooterFound As Boolean = False
'        Try
'            Me.BeginProcess("Transferring from excel file, please wait...")
'            'myDT_ImportedSchedule_Detail.Clear()
'            GSCOM.SQL.GetExcelTable(FileName, "SCHEDULE TEMPLATE", myDT_ImportedSchedule_Detail)
'            GSCOM.SQL.GetExcelTable(FileName, "EMPLOYEE LIST WITH CODES", myDT_ImportedSchedule_Employee)

'            'o = myDT_ImportedSchedule_Detail.Get(1, Database.Tables.tImportedSchedule_Detail.Field.WEDTOT)
'            o = myDT_ImportedSchedule_Detail.Get(3, Database.Tables.tImportedSchedule_Detail.Field.MONTIME)
'            myDT.Set(Database.Tables.tImportedSchedule.Field.StartDate, CDate(o))

'            For i = 0 To 4
'                myDT_ImportedSchedule_Detail.Rows(i).Delete()
'            Next
'            For i = 5 To myDT_ImportedSchedule_Detail.Rows.Count - 1
'                If myDT_ImportedSchedule_Detail.Get(i, Database.Tables.tImportedSchedule_Detail.Field.EMPNAME).ToString = "FOOTERROW" Then
'                    bFooterFound = True
'                End If
'                If bFooterFound Then
'                    myDT_ImportedSchedule_Detail.Rows(i).Delete()
'                End If
'            Next

'            myDT_ImportedSchedule_Detail.AcceptChanges()
'            For Each dr As DataRow In myDT_ImportedSchedule_Detail.Select()
'                ' SetAdded is for Unchangeds only
'                If dr.RowState = DataRowState.Unchanged Then
'                    dr.SetAdded()
'                End If
'            Next

'            For i = 0 To 3
'                myDT_ImportedSchedule_Employee.Rows(i).Delete()
'            Next
'            bFooterFound = False
'            For i = 4 To myDT_ImportedSchedule_Employee.Rows.Count - 1
'                If myDT_ImportedSchedule_Employee.Get(i, Database.Tables.tImportedSchedule_Employee.Field.ACCESSNO).ToString = "FOOTERROW" Then
'                    bFooterFound = True
'                End If
'                If bFooterFound Then
'                    myDT_ImportedSchedule_Employee.Rows(i).Delete()
'                End If
'            Next
'            myDT_ImportedSchedule_Employee.AcceptChanges()
'            For Each dr As DataRow In myDT_ImportedSchedule_Employee.Select()
'                'SetAdded is for Unchangeds only
'                If dr.RowState = DataRowState.Unchanged Then
'                    dr.SetAdded()
'                End If
'            Next

'            'mGrid.GridView.BestFitColumns()
'            Me.EndProcess("")
'        Catch ex As Exception
'            Me.EndProcess("Error occur while importing data.", False)
'        End Try
'    End Sub

'    Protected Overrides Function CanSave() As Boolean
'        'myDT_ImportedSchedule_Detail.AcceptChanges()
'        Return MyBase.CanSave()
'    End Function

'    Private Sub InitGrid()
'        mGrid.DataSource = myDT_ImportedSchedule_Detail
'        'mGrid.GridView.Columns.ColumnByFieldName(Database.Tables.tImportedSchedule_Detail.Field.ID_ImportedSchedule.ToString).Visible = False
'    End Sub

'    Private Sub mGridView_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGridView.DataSourceChanged
'        mGrid.GridView.Columns.ColumnByFieldName(Database.Tables.tImportedSchedule_Detail.Field.ID_ImportedSchedule.ToString).Visible = False
'    End Sub

'End Class
