Option Explicit On
Option Strict Off



Friend Class EmployeeDailyScheduleInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tEmployeeDailySchedule(Connection)
    Private mtEmployeeDailySchedule_Detail As New Database.Tables.tEmployeeDailySchedule_Detail(Connection)

    Private mtEmployeeAttendanceLog As New Database.Tables.tEmployeeAttendanceLog(Connection)
    Private mtAttendance As New Database.Tables.tAttendance(Connection)
    Private mtOvertime As New Database.Tables.tOvertime(Connection)

    Private mtCOS As New Database.Tables.tEmployeeChangeOfSchedule(Connection)
    Private mtCOSDetail As New Database.Tables.tEmployeeChangeOfSchedule_Detail(Connection)

    Private mtOB As New Database.Tables.tOB(Connection)
    Private mtOB_Detail As New Database.Tables.tOB_Detail(Connection)

    Private mtEmployeeMissedLog As New Database.Tables.tEmployeeMissedLog(Connection)
    Private mtEmployeeMissedLogDetail As New Database.Tables.tEmployeeMissedLog_Detail(Connection)

    '-----------
    Private mtLeave As New Database.Tables.tLeave(Connection)
    Private mtLeaveDetail As New Database.Tables.tLeave_Detail(Connection)
    '-----------
    Private mControl As New InSys.DataControl
    'Private mGeneratePayrollButton As ToolStripButton
    'Private mRecomputeLogsButton As ToolStripButton
    Private mComputeSummary As ToolStripButton
    Private mcomputeHours As ToolStripButton
    Private mEmployeeAttendanceLog As DataGridView
    Private mEmployeeDailySchedule_Detail As DataGridView
    Private mAttendanceGrid As DataGridView
    Private mOTGrid As DataGridView
    Private dgv As GSDetailDataGridView
    Private COSdgv As GSDetailDataGridView
    ' Private MissedLogdgv As GSDetailDataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtEmployeeAttendanceLog)
            .Add(mtAttendance)
            .Add(mtOvertime)
            .Add(mtEmployeeDailySchedule_Detail)
            '     .Add(mtScheduleAdjustment)
            .Add(mtLeaveDetail)
        End With
        InitControl(pMenu)

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        pdc = myDT.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID)

        cdc = mtEmployeeDailySchedule_Detail.Columns(Database.Tables.tEmployeeDailySchedule_Detail.Field.ID_EmployeeDailySchedule)
        rel = mDataset.Relations.Add(pdc, cdc)


        mtEmployeeAttendanceLog.DefaultView.Sort = "[DateTime]"
        mtAttendance.DefaultView.Sort = "[ComputedTimeIn]"
        mtEmployeeAttendanceLog.Columns(Database.Tables.tEmployeeAttendanceLog.Field.ID_EditedByUser).DefaultValue = gUser


        mComputeSummary = Me.GetStripButton("Compute Summary")
        mcomputeHours = Me.GetStripButton("Compute Hours")



        Me.ReloadAfterCommit = True

        dgv = Me.AddGrid("OB Detail")
        With dgv
            .AutoGenerateColumns = True
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
        End With


        COSdgv = Me.AddGrid("COS Detail")
        With COSdgv
            .AutoGenerateColumns = True
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
        End With


        'MissedLogdgv = Me.AddGrid("MissedLog Detail")
        'With MissedLogdgv
        '    .AutoGenerateColumns = True
        '    .ReadOnly = True
        '    .AllowUserToAddRows = False
        '    .AllowUserToDeleteRows = False
        'End With

        'Me.NoTransactionTables = mtEmployeeDailySchedule.TableName
        AfterNew()


        Me.ReArrangeTab(True)

        With CType(MyBase.GetDataGridView(mtOvertime), GSDetailDataGridView)
            .ReadOnly = True
            .AllowUserToAddRows = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'AddHandler .ItemActivate, AddressOf dgv_ItemActivate
        End With



        With CType(MyBase.GetDataGridView(mtLeaveDetail), GSDetailDataGridView)
            .ReadOnly = True
            .AllowUserToAddRows = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'AddHandler .ItemActivate, AddressOf dgv_ItemActivate
        End With


        'mGrid = Me.GetDataGridView(mtEmployeeDailySchedule)
        mEmployeeAttendanceLog = Me.GetDataGridView(mtEmployeeAttendanceLog)
        mEmployeeDailySchedule_Detail = Me.GetDataGridView(mtEmployeeDailySchedule_Detail)
        mEmployeeAttendanceLog.Columns("ID_Employee").Visible = False
        'mOTGrid = Me.GetDataGridView(mtOvertime)
        'mOTGrid.Columns("Employee").Visible = False
        'mOTGrid.Columns("Date").Visible = False

        mEmployeeAttendanceLog.ReadOnly = CBool(nDB.GetUserID <> 1)
        mEmployeeDailySchedule_Detail.ReadOnly = CBool(nDB.GetUserID <> 1)
        mEmployeeAttendanceLog.AllowUserToAddRows = CBool(nDB.GetUserID = 1)
        mEmployeeAttendanceLog.AllowUserToDeleteRows = CBool(nDB.GetUserID = 1)
        mEmployeeDailySchedule_Detail.AllowUserToAddRows = CBool(nDB.GetUserID = 1)
        mEmployeeDailySchedule_Detail.AllowUserToDeleteRows = CBool(nDB.GetUserID = 1)

        mAttendanceGrid = Me.GetDataGridView(mtAttendance)
        mAttendanceGrid.Columns("ID_Employee").Visible = False
        mAttendanceGrid.Columns("Date").Visible = False
        mAttendanceGrid.Columns("DailySchedule").Visible = False


        'mLogGrid = Me.GetDataGridView(mtEmployeeAttendanceLog)
        'With mLogGrid
        '    .AllowUserToDeleteRows = False
        'End With

        For Each cc As DataGridViewColumn In mEmployeeAttendanceLog.Columns
            If cc.Name.StartsWith("ID_") Then
                cc.HeaderText = Strings.Right(cc.Name, cc.Name.Length - 3)
            End If
        Next
        For Each cc As DataGridViewColumn In mAttendanceGrid.Columns
            If cc.Name.StartsWith("ID_") Then
                cc.HeaderText = Strings.Right(cc.Name, cc.Name.Length - 3)
            End If
        Next
        For Each cc As DataGridViewColumn In mEmployeeDailySchedule_Detail.Columns
            If cc.Name.StartsWith("ID_") Then
                cc.HeaderText = Strings.Right(cc.Name, cc.Name.Length - 3)
            End If
        Next

        Dim b As New DataGridViewButtonColumn
        'b.HeaderText = "Copy"

        If CBool(nDB.GetSetting(Database.SettingEnum.UseHoursAndMinutesFormat)) Then
            'With mOTGrid
            '    AddHandler .CellFormatting, AddressOf mOTGrid_CellFormatting
            '    AddHandler .CellParsing, AddressOf mOTGrid_CellParsing
            'End With
        End If

    End Sub

    'Private Sub RecomputeLogs(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Recompute Logs?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pRecomputeLogs " & myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID).ToString, Connection)
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
    '        Application.DoEvents()
    '        MsgBox("Finished recomputing logs.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    'Private Sub ComputeHours(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Compute Hours?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_ComputeHours " & myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID).ToString, Connection)
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
    '        Application.DoEvents()
    '        MsgBox("Finished computing hours.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    'Private Sub ComputeHeader(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Compute Header?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_ComputeHeader " & myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID).ToString, Connection)
    '        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
    '        Application.DoEvents()
    '        MsgBox("Finished computing header.", MsgBoxStyle.Information)
    '    End If
    'End Sub

    Private Sub CreateScheduleAdjustmentTable(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Create schedule adjustment table?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_CreateScheduleAdjustmentTable " & myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID).ToString, Connection)
            LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
            Application.DoEvents()
            MsgBox("Finished creating schedule adjustment table", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ClearOtherTables()
        mtEmployeeAttendanceLog.Clear()
        mtAttendance.Clear()
        'mtOvertime.Clear()
    End Sub

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        Dim vID_Employee As String
        Dim vDate As String
        Dim s As String
        Dim o As Object
        Dim d As Object
        Dim a As Object


        Try
            'If Me.IsPosted Then
            '    mComputeSummary.Enabled = False
            '    mcomputeHours.Enabled = False
            '    Me.SaveButton.Enabled = False
            'Else
            '    mComputeSummary.Enabled = True
            '    mcomputeHours.Enabled = True
            '    Me.SaveButton.Enabled = True
            'End If

            If myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Posted) Then
                mComputeSummary.Enabled = False
                mcomputeHours.Enabled = False
                Me.SaveButton.Enabled = False
            Else
                mComputeSummary.Enabled = True
                mcomputeHours.Enabled = True
                Me.SaveButton.Enabled = True
            End If

            o = myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee)
            mtAttendance.Columns(Database.Tables.tAttendance.Field.ID_Employee).DefaultValue = o
            '  mtOvertime.Columns(Database.Tables.tOvertime.Field.ID_Employee).DefaultValue = o
            mtEmployeeAttendanceLog.Columns(Database.Tables.tEmployeeAttendanceLog.Field.ID_Employee).DefaultValue = o


            d = myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Date)
            mtAttendance.Columns(Database.Tables.tAttendance.Field.Date).DefaultValue = d
            mtOvertime.Columns(Database.Tables.tOvertime.Field.Date).DefaultValue = d
            mtEmployeeAttendanceLog.Columns(Database.Tables.tEmployeeAttendanceLog.Field.WorkDate).DefaultValue = d
            mtEmployeeAttendanceLog.Columns(Database.Tables.tEmployeeAttendanceLog.Field.DateTime).DefaultValue = d


            '----------------------------------\
            a = myDT.Get("AccessNo")
            mtEmployeeAttendanceLog.Columns(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo).DefaultValue = a
            '----------------------------------/

            vID_Employee = GSCOM.SQL.SQLFormat(o)
            vDate = GSCOM.SQL.SQLFormat(d)
            s = "([ID_Employee]=" & vID_Employee & ")"
            s &= " AND"
            s &= "([WorkDate]=" & vDate & ")"
            mtEmployeeAttendanceLog.ClearThenFill(s)
            mtAttendance.ClearThenFill(s)
            s = "(ID_Employee=" & vID_Employee & ")"
            s &= " AND "
            s &= "(DATE= " & vDate & ")"

            mtLeaveDetail.ClearThenFill(s)


            '------------------------------------New
            If CBool(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Posted)) Then
                Me.SaveButton.Enabled = False
                Me.GetStripButton("Compute Hours").Enabled = False
                Me.GetStripButton("Compute Summary").Enabled = False
            End If

            mtOvertime.ClearThenFill(s)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        dgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,Date,StartTime,EndTime FROM dbo.vOB_Detail WHERE Date =  " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Date)) & " AND ID_Employee = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee)), gConnection)

        COSdgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,SchedDate AS Date,OldSched,NewSched,CASE WHEN ID_ForRDSD = 1 THEN 'Rest Day'  WHEN ID_ForRDSD = 2 THEN 'Straight Duty' ELSE NULL END ForRDSD FROM dbo.vEmployeeChangeOfSchedule_Detail WHERE SchedDate = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Date)) & " AND ID_Employee = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee)), gConnection)

        ' MissedLogdgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,LogDate,LogTime,AttendanceLogType,Comment FROM dbo.vEmployeeMissedLog_Detail WHERE LogDate = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.Date)) & " AND ID_Employee = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee)), gConnection)

    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeDailySchedule)
        End Set
    End Property


#End Region



#Region "Format"
    Private Sub mOTGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        Dim dgv As DataGridView
        dgv = TryCast(sender, DataGridView)
        Try
            Select Case e.ColumnIndex
                Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = DecimalToHoursAndMinutes(CDec(e.Value))
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub mOTGrid_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs)
    '    Dim dgv As DataGridView
    '    dgv = TryCast(sender, DataGridView)
    '    Try
    '        Select Case e.ColumnIndex
    '            Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
    '            , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
    '            , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
    '                If e.Value.ToString <> "-" Then
    '                    e.Value = HoursAndMinutesToDecimal(e.Value.ToString)
    '                    e.ParsingApplied = True
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        'err
    '    End Try
    'End Sub
#End Region
End Class

