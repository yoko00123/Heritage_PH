Option Explicit On
Option Strict On



Friend Class OBFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tOBFile(Connection)
    Private mtOBFile_Detail As New Database.Tables.tOBFile_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.OBFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView



    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtOBFile_Detail)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tOBFile.Field.ID)
        cdc = mtOBFile_Detail.Columns(Database.Tables.tOBFile_Detail.Field.ID_OBFile)
        rel = mDataset.Relations.Add(pdc, cdc)
        mGenTemplateButton = MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_generateFile.png"), AddressOf GenTemplate)
        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("_importFile.png"), AddressOf ImportFile)
        mApplyButton = MyBase.AddButton("Execute", gMainForm.imgList.Images("ApplyFile.png"), AddressOf TransferSchedule)
        myDT.Columns(Database.Tables.tOBFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtOBFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mGenTemplateButton.Enabled = True
        mImportButton.Enabled = Not (mtOBFile_Detail.Rows.Count > 0)
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
        If MsgBox("Do you want to execute file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pOBFile_Apply " & myDT.Get(Database.Tables.tOBFile.Field.ID).ToString, Connection)
            'Me.mApplyButton.Enabled = False
            'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
            MsgBox("Done.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tOBFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tOBFile_Detail.Field.Date.ToString
        s &= ", " & Database.Tables.tOBFile_Detail.Field.StartTime.ToString
        s &= ", " & Database.Tables.tOBFile_Detail.Field.EndTime.ToString
        s &= ", " & Database.Tables.tOBFile_Detail.Field.Reason.ToString
        s &= ", " & Database.Tables.tOBFile_Detail.Field.Comment.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule6.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule7.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Comment.ToString
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
            myDT.Set(Database.Tables.tOBFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtOBFile_Detail, s)

            'For i = 0 To 0
            '    mtLeaveFile_Detail.Rows(i).Delete()
            'Next

            mtOBFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtOBFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtOBFile_Detail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.OBFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tOBFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "OBFile.xls", sfd.FileName, True)
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
            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)
            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") where IsActive = 1")
            Dim DT As DataTable = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, DT, "A", "B", "Employee Code")
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub




#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tOBFile)
        End Set
    End Property

#End Region
End Class

