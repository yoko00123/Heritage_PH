Option Explicit On
Option Strict On



Friend Class EntitlementFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEntitlementFile(Connection)
    Private mtEntitlementFileDetail As New Database.Tables.tEntitlementFile_Detail(Connection)
    Private mControl As New InSys.DataControl
    Private mImportButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            .Add(mtEntitlementFileDetail)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tEntitlementFile.Field.ID)

        cdc = Me.mtEntitlementFileDetail.Columns(Database.Tables.tEntitlementFile_Detail.Field.ID_EntitlementFile)
        rel = mDataset.Relations.Add(pdc, cdc)
        myDT.Columns(Database.Tables.tEntitlementFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID


        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferSchedule)

        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtEntitlementFileDetail)
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEntitlementFile)
        End Set
    End Property

    'Protected Overrides Property Control() As Control
    '    Get
    '        Return mControl
    '    End Get
    '    Set(ByVal value As Control)
    '        mControl = CType(value, InSys.DataControl)
    '    End Set
    'End Property

    '#Region "LoadInfo"

    'Public Overrides Sub LoadInfo(ByVal pID As Integer)
    '    MyBase.LoadInfo(pID)
    '    mImportButton.Enabled = Not (mtEntitlementFile_Detail.Rows.Count > 0)
    '    mApplyButton.Enabled = (pID > 0)
    'End Sub

    '#End Region

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            Me.mImportButton.Enabled = False
        End If
    End Sub


    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim o As Object = Nothing
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tEntitlementFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Entitlement", mtEntitlementFileDetail, s)
            'o = mtEntitlementFileDetail.Get(0, Database.Tables.tEntitlementFile_Detail.Field.EmployeeCode)
            'If IsDate(o) Then
            'myDT.Set(Database.Tables.tScheduleFile.Field.StartDate, CDate(o))
            'For i = 0 To 0
            'mtEntitlementFileDetail.Rows(i).Delete()
            ' Next
            mtEntitlementFileDetail.AcceptChanges()
            For Each dr As DataRow In mtEntitlementFileDetail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEntitlementFileDetail
            Me.EndProcess()
            'Else
            'EndProcess("No starting date was specified in the file", False)
            'End If
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pEntitlementFile_Apply " & myDT.Get(Database.Tables.tEntitlementFile.Field.ID).ToString, Connection)
            'Me.mApplyButton.Enabled = False
            'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub
    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tEntitlementFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.EntitlementType.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.Date.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.ORNo.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.ORDate.ToString
        s &= ", " & Database.Tables.tEntitlementFile_Detail.Field.Amount.ToString
        Return s
    End Function
End Class
