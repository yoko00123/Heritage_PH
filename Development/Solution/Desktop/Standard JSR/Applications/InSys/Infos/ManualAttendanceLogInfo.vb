Option Explicit On
Option Strict Off



Friend Class ManualAttendanceLogInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tManualAttendanceLog(Connection)
    Private mManualAttendanceLog_Detail As New Database.Tables.tManualAttendanceLog_Detail(Connection)
    'Private mtEmployeeLeaveCredit As New Database.Tables.tEmployeeLeaveCredit(Connection)
    Private mControl As New InSys.DataControl
    Dim msd, med As TextBox

    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Private dgv As GSDetailDataGridView
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            .Add(mManualAttendanceLog_Detail)
        End With
        InitControl(pMenu)
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        pdc = myDT.Columns(Database.Tables.tManualAttendanceLog.Field.ID)
        cdc = mManualAttendanceLog_Detail.Columns(Database.Tables.tManualAttendanceLog_Detail.Field.ID_ManualAttendanceLog)
        rel = mDataset.Relations.Add(pdc, cdc)
        myDT.Columns(Database.Tables.tManualAttendanceLog.Field.ID_EditedByUser).DefaultValue = gUser
        mGenTemplateButton = MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_generateFile.png"), AddressOf GenTemplate)
        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("_importFile.png"), AddressOf ImportFile)
        '  MyBase.AddButton("Execute", gMainForm.imgList.Images("ApplyFile.png"), AddressOf Apply)

        Me.ReloadAfterCommit = True
        AfterNew()
        

        mGrid = Me.GetDataGridView(mManualAttendanceLog_Detail)
        mGrid.Columns("ID_AttendanceLogType").HeaderCell.Value = "Attendance Log Type"
        mGrid.Columns("ID_EmployeeAttendanceLogCreditDate").HeaderCell.Value = "Employee AttendanceLog CreditDate"
    End Sub



#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tManualAttendanceLog)
        End Set
    End Property

#End Region

    Private Sub ManualAttendanceLogInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        'GSCOM.SQL.ExecuteNonQuery("EXEC pManualAttendanceLog " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tManualAttendanceLog.Field.ID)), e.Transaction)
    End Sub
    Private Sub Apply(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to execute file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pManualAttendanceLog_Apply " & myDT.Get(Database.Tables.tManualAttendanceLog.Field.ID).ToString, Connection)
            MsgBox("Done.", MsgBoxStyle.Information)
        End If
    End Sub


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.OBFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter

        sfd.FileName = "ManualAttendanceLogFile.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "ManualAttendanceLogFile.xls", sfd.FileName, True)
            a.DataSource = sfd.FileName 'initialize datasource (filename)
        
            MsgBox("Done", MsgBoxStyle.Information)
        End If
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
        Dim dtemp As New Database.Tables.tEmployee(Connection)
        Dim dtpay As New Database.Tables.tPayrollItem(Connection)
        Dim nr As DataRow

        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            dt = SQL.GetExcelTable(FileName, "Sheet1")
            dtemp.ClearThenFill("")
            dtpay.ClearThenFill("")
            mManualAttendanceLog_Detail.Rows.Clear()
            Dim total As Decimal = 0
            For Each dr As DataRow In dt.Rows
                nr = mManualAttendanceLog_Detail.NewRow()

                'gLen.code 20110416
                'Dim s As String
                For Each dc As DataColumn In mManualAttendanceLog_Detail.Columns
                    If dc.ColumnName.ToString = "Date" Or dc.ColumnName.ToString = "Time" Then
                        'If dc.ColumnName.ToString = "EmployeeCode" Or dc.ColumnName.ToString = "Employee" Then
                        nr(dc.ColumnName.ToString) = dr.Item(dc.ColumnName.ToString)
                        'Else
                        's = dr(dc.ColumnName.ToString).ToString
                        'If s = "" Then
                        's = "0.00"
                        ' End If
                        'nr(dc.ColumnName.ToString) = s
                        'End If
                    End If
                Next

                
                

                mManualAttendanceLog_Detail.Rows.Add(nr)

            Next

            Dim dg As DataGridView
            dg = Me.GetDataGridView(mManualAttendanceLog_Detail)
            dg.Refresh()

            For Each d As DataGridViewRow In dg.Rows
                dg.Update()
            Next

            Me.EndProcess("Finish downloading file [" & FileName & "].")
        Catch ex As OleDb.OleDbException
            Me.EndProcess("Error occur while importing data. This is due to file connection, please check if sheet name is Sheet1.", False)
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tManualAttendanceLog_Detail.Field.ID.ToString
        s &= ", " & Database.Tables.tManualAttendanceLog_Detail.Field.Date.ToString
        s &= ", " & Database.Tables.tManualAttendanceLog_Detail.Field.Time.ToString
        s &= ", " & Database.Tables.tManualAttendanceLog_Detail.Field.ID_AttendanceLogType.ToString
        s &= ", " & Database.Tables.tManualAttendanceLog_Detail.Field.ID_EmployeeAttendanceLogCreditDate.ToString
        's &= ", " & Database.Tables.tOBFile_Detail.Field.Comment.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule6.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule7.ToString
        's &= ", " & Database.Tables.tScheduleFile_Detail.Field.Comment.ToString
        Return s
    End Function

End Class
