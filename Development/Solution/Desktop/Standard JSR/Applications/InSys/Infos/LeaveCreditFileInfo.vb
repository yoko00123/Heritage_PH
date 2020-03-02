Option Explicit On
Option Strict On



Friend Class LeaveCreditInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tLeaveCreditFile(Connection)
    Private mtLeaveCreditFileDetail As New Database.Tables.tLeaveCreditFile_Detail(Connection)
    Private mImportButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private mControl As New InSys.DataControl
    'Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            .Add(mtLeaveCreditFileDetail)
        End With
        InitControl(pMenu)

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        pdc = myDT.Columns(Database.Tables.tLeaveCreditFile.Field.ID)
        cdc = mtLeaveCreditFileDetail.Columns(Database.Tables.tLeaveCreditFile_Detail.Field.ID_LeaveCreditFile)
        rel = mDataset.Relations.Add(pdc, cdc)

        'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("GenerateTemplate.png"), AddressOf GenTemplate)
        'mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("applyfile.png"), AddressOf TransferSchedule)
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            'Me.mImportButton.Enabled = False
            'Me.mApplyButton.Enabled = False
        End If
    End Sub
    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            'mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tLeaveCreditFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtLeaveCreditFileDetail, s)

            mtLeaveCreditFileDetail.AcceptChanges()
            For Each dr As DataRow In mtLeaveCreditFileDetail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            'mGrid.DataSource = mtLeaveCreditFileDetail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try

    End Sub

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pLeaveCreditFile_Apply " & myDT.Get(Database.Tables.tLeaveCreditFile.Field.ID).ToString, Connection)
            'Me.mApplyButton.Enabled = False
            'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tLeaveCreditFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tLeaveCreditFile_Detail.Field.LeavePayrollItemCode.ToString
        s &= ", " & Database.Tables.tLeaveCreditFile_Detail.Field.Value.ToString
        Return s
    End Function


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.OBFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter

        sfd.FileName = "LeaveCreditFile.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "LeaveCreditFile.xls", sfd.FileName, True)
            a.DataSource = sfd.FileName 'initialize datasource (filename)
            'IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            'Dim dt As New DataTable
            'dt = GSCOM.SQL.TableQuery("SELECT * FROM vEmployee e WHERE e.IsActive=1 ORDER BY Name", Connection)
            'For Each drx As DataRow In dt.Select
            '    dr = vScheduleTable.NewRow
            '    dr = vScheduleTable.NewRow
            '    dr("EmployeeCode") = drx("Code")
            '    dr("Employee") = drx("Name")
            '    vScheduleTable.Rows.Add(dr)
            'Next
            'a.Update(vScheduleTable.Select(""))
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tLeaveCreditFile)
        End Set
    End Property


End Class

