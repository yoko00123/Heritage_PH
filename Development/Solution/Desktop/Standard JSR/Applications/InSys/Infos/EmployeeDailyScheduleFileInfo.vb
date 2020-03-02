Option Explicit On
Option Strict On



Friend Class EmployeeDailyScheduleFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeDailyScheduleFile(gConnection)
    Private mtEmployeeDailyScheduleFile_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tEmployeeDailyScheduleFile_Detail(gConnection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.LeaveFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With
        InitControl(pMenu)
        mtEmployeeDailyScheduleFile_Detail = DirectCast(Me.mDataset.Tables("tEmployeeDailyScheduleFile_Detail"), GSCOM.SQL.ZDataTable)
        mGenTemplateButton = Me.GetStripButton("Generate Template")
        AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        mImportButton = Me.GetStripButton("Import File")
        AddHandler mImportButton.Click, AddressOf ImportFile
        'mApplyButton = Me.GetStripButton("Apply File")
        'AddHandler mApplyButton.Click, AddressOf TransferSchedule
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtEmployeeDailyScheduleFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtEmployeeDailyScheduleFile_Detail.Rows.Count > 0)
        mGenTemplateButton.Enabled = True
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

    'Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailyScheduleFile_Apply " & myDT.Get(Database.Tables.tEmployeeDailyScheduleFile.Field.ID).ToString, Connection)
    '        MsgBox("Finished applying the file.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tLeaveFile_Detail.Field.EmployeeCode.ToString
        ' s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.Date.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.REG.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.EXT.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.OT.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.ND.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.NDOT.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.TARDY.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.UT.ToString
        s &= ", " & Database.Tables.tEmployeeDailyScheduleFile_Detail.Field.IsRD.ToString
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
            myDT.Set(Database.Tables.tEmployeeDailyScheduleFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtEmployeeDailyScheduleFile_Detail, s)

            'For i = 0 To 0
            '    mtLeaveFile_Detail.Rows(i).Delete()
            'Next

            mtEmployeeDailyScheduleFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtEmployeeDailyScheduleFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeDailyScheduleFile_Detail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.LeaveFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.LeaveFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tEmployeeDailyScheduleFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            a.DataSource = sfd.FileName
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "EmployeeDailyScheduleFile.xls", sfd.FileName, True)
            Dim dt As New DataTable
            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeDailyScheduleFile)
        End Set
    End Property



#End Region

   
End Class
