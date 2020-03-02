Option Explicit On
Option Strict On



Friend Class EmployeeLeaveCreditFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeLeaveCreditFile(Connection)
    Private mtEmployeeLeaveCreditFile_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tEmployeeLeaveCreditFile_Detail(Connection) 'Andrew 20110517
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.EmployeeLeaveCreditFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtEmployeeLeaveCreditFile_Detail) 'Andrew 20110517
        End With
        InitControl(pMenu)
        'Andrew 20110517
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'InitControl(pMenu)
        'pdc = myDT.Columns(Database.Tables.tEmployeeLeaveCreditFile.Field.ID)
        'cdc = mtEmployeeLeaveCreditFile_Detail.Columns(Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.ID_EmployeeLeaveCreditFile)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mtEmployeeLeaveCreditFile_Detail = DirectCast(mDataset.Tables("tEmployeeLeaveCreditFile_Detail"), GSCOM.SQL.ZDataTable)
        'myDT.Columns(Database.Tables.tEmployeeLeaveCreditFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        'mGenTemplateButton = MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf GenTemplate)
        'mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferSchedule)
        mImportButton = Me.GetStripButton("Import File")
        mApplyButton = Me.GetStripButton("Apply File")
        AddHandler Me.GetStripButton("Generate Template").Click, AddressOf GenTemplate
        AddHandler mImportButton.Click, AddressOf ImportFile
        '============================================================================
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtEmployeeLeaveCreditFile_Detail)
    End Sub


#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtEmployeeLeaveCreditFile_Detail.Rows.Count > 0)
        mApplyButton.Enabled = (pID > 0)
    End Sub

#End Region

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            Me.mImportButton.Enabled = False
            Me.mApplyButton.Enabled = False
        End If
    End Sub

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeLeaveCreditFile_Apply " & myDT.Get(Database.Tables.tEmployeeLeaveCreditFile.Field.ID).ToString, Connection)
            'Me.mApplyButton.Enabled = False
            'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.VL.ToString
        s &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.SL.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule1.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule2.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule3.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule4.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule5.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule6.ToString
        's &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Schedule7.ToString
        s &= ", " & Database.Tables.tEmployeeLeaveCreditFile_Detail.Field.Comment.ToString
        Return s
    End Function

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tEmployeeLeaveCreditFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtEmployeeLeaveCreditFile_Detail, s)

            mtEmployeeLeaveCreditFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtEmployeeLeaveCreditFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeLeaveCreditFile_Detail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileAdapter

        sfd.FileName = "EmployeeLeaveCredit.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "EmployeeLeaveCredit.xls", sfd.FileName, True)
            'a.DataSource = sfd.FileName 'initialize datasource (filename)
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

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeLeaveCreditFile)
        End Set
    End Property


#End Region

    'Private Sub mGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles mGrid.CellFormatting
    '    Select Case mGrid.Columns(e.ColumnIndex).DataPropertyName.ToLower
    '        Case Database.Tables.tScheduleFile_Detail.Field.Schedule1.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule2.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule3.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule4.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule5.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule6.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule7.ToString.ToLower
    '    End Select
    'End Sub
End Class
