Option Explicit On
Option Strict Off



Friend Class OTFileInfo
    Inherits InfoSet
    ''BILLY INC
    Private myDT As New Database.Tables.tOverTimeFile(Connection)
    Private mtOverTimeFile_Detail As New Database.Tables.tOverTimeFile_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.OBFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtOverTimeFile_Detail)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tOverTimeFile.Field.ID)
        cdc = mtOverTimeFile_Detail.Columns(Database.Tables.tOverTimeFile_Detail.Field.ID_OverTimeFile)
        rel = mDataset.Relations.Add(pdc, cdc)

        mGenTemplateButton = Me.GetStripButton("Generate Template") 'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf GenTemplate)
        mImportButton = Me.GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        'BILLY'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferSchedule)
        'mApplyButton = GetStripButton("Apply File")
        AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        AddHandler mImportButton.Click, AddressOf ImportFile
        'AddHandler mApplyButton.Click, AddressOf TransferSchedule
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtOverTimeFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtOverTimeFile_Detail.Rows.Count > 0)
        Me.GetStripButton("Generate Template").Enabled = True
        'mApplyButton.Enabled = (pID > 0)
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
            'Me.mApplyButton.Enabled = False
        End If
    End Sub

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
        'BILLY
        'If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    GSCOM.SQL.ExecuteNonQuery("EXEC pOTFile_Apply " & myDT.Get(Database.Tables.tOverTimeFile.Field.ID).ToString, Connection)
        '    'Me.mApplyButton.Enabled = False
        '    'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
        '    MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tOverTimeFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.WorkDate.ToString

        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.StartTime.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.EndTime.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.IsBasic.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.FollowingDay.ToString
        's &= ", " & Database.Tables.tOverTimeFile_Detail.Field.DepartmentCode.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.ForOffSet.ToString
        s &= ", " & Database.Tables.tOverTimeFile_Detail.Field.Reason.ToString
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
            myDT.Set(Database.Tables.tOverTimeFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtOverTimeFile_Detail, s)

            'For i = 0 To 0
            '    mtLeaveFile_Detail.Rows(i).Delete()
            'Next

            mtOverTimeFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtOverTimeFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtOverTimeFile_Detail
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

        sfd.FileName = myDT.Get(Database.Tables.tOverTimeFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "OTFile.xls", sfd.FileName, True)

            a.DataSource = sfd.FileName
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable

            'Dim s As String
            's &= "SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ")"

            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")
            'UseArray(sfd.FileName, dt)



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


        oSheet = oBook.Worksheets(2)

        oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray


        oBook.Save()

        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub
#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tOverTimeFile)
        End Set
    End Property

#End Region

End Class

