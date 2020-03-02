Option Explicit On
Option Strict On



Friend Class AttendanceViewInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tAttendanceView(Connection)
    Private mtAttendance As New Database.Tables.tAttendance(Connection)
    'Private WithEvents mtOvertime As New Database.Tables.tOvertime(Connection)
    'Private mControl As New nDB.AttendanceViewControl
    Private mControl As New InSys.DataControl
    Private grdOvertime As DataGridView
    Private mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtAttendance)
            '       .Add(mtOvertime)
        End With
        InitControl(pMenu)
        Me.ReloadAfterCommit = True

        'Me.AddButton("Export", gImageList.Images("misc.a.ico"), AddressOf Export)
        AfterNew()
        mGrid = Me.GetDataGridView(mtAttendance)
        With mGrid
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            .ReadOnly = True
        End With

        _StartDate = CType(Me.GetControl("_StartDate"), TextBox)
        _EndDate = CType(Me.GetControl("_EndDate"), TextBox)

        AddHandler _StartDate.Leave, AddressOf GenerateEndDate

        '  grdOvertime = Me.GetDataGridView(mtOvertime)
        If CBool(nDB.GetSetting(Database.SettingEnum.UseHoursAndMinutesFormat)) Then
            With grdOvertime
                AddHandler .CellFormatting, AddressOf grdOvertime_CellFormatting
                AddHandler .CellParsing, AddressOf grdOvertime_CellParsing
            End With
        End If
    End Sub

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID) 'must be after loadinfo coz of date range, not pID
        Dim sb As New System.Text.StringBuilder
        sb.Append("([Date] BETWEEN '" & myDT.Get(Database.Tables.tAttendanceView.Field.StartDate).ToString & "' AND '" & myDT.Get(0, Database.Tables.tAttendanceView.Field.EndDate).ToString & "')")
        If myDT.Get(Database.Tables.tAttendanceView.Field.ID_Section) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("([ID_Section] = " & myDT.Get(Database.Tables.tAttendanceView.Field.ID_Section).ToString & ")")
        End If
        sb.Append(" AND ")
        sb.Append("[ID_Employee] IN (SELECT ID FROM " & nDB.GetMenuValue(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, Database.Tables.tMenu.Field.DataSource).ToString & ")")
        If myDT.Get(Database.Tables.tAttendanceView.Field.ID_Employee) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_Employee = " & myDT.Get(Database.Tables.tAttendanceView.Field.ID_Employee).ToString & ")")
        End If
        '  mtOvertime.ClearThenFill(sb.ToString)
        If myDT.Get(Database.Tables.tAttendanceView.Field.IsComplete) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(IsComplete = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tAttendanceView.Field.IsComplete)) & ")")
        End If
        mtAttendance.ClearThenFill(sb.ToString)
    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tAttendanceView)
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

#End Region


    'Private Sub mtOvertime_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles mtOvertime.ColumnChanged
    '    If e.Column.ColumnName = Database.Tables.tOvertime.Field.ConsideredHours.ToString Then
    '        If CInt(e.ProposedValue) > 0 Then
    '            e.Row.Item(Database.Tables.tOvertime.Field.ID_FilingStatus) = 2 'approved
    '        Else
    '            e.Row.Item(Database.Tables.tOvertime.Field.ID_FilingStatus) = 1 'filed
    '        End If
    '        grdOvertime.Refresh()
    '    End If
    'End Sub

    Private Sub grdOvertime_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) 'Handles dgv.CellFormatting
        Dim dgv As DataGridView
        dgv = TryCast(sender, DataGridView)
        Try
            If e IsNot Nothing Then
                Select Case e.ColumnIndex
                    Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
                    , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
                    , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
                        If e.Value.ToString <> "-" Then
                            e.Value = DecimalToHoursAndMinutes(CDec(e.Value))
                        End If
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdOvertime_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs) 'Handles dgv.CellParsing
        Dim dgv As DataGridView
        dgv = TryCast(sender, DataGridView)
        Try
            Select Case e.ColumnIndex
                Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = HoursAndMinutesToDecimal(e.Value.ToString)
                        e.ParsingApplied = True
                    End If
            End Select
        Catch ex As Exception
            '   e.Value = o
        End Try
    End Sub

#Region "Date"

    Private WithEvents _StartDate As TextBox
    Private WithEvents _EndDate As TextBox


    Private Sub GenerateEndDate(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim t As TextBox
        t = CType(sender, TextBox)
        FormatNow(t)
    End Sub

    Private Sub FormatNow(ByVal t As TextBox)
        If IsDate(t.Text) Then
            _EndDate.Text = t.Text
        End If
    End Sub



#End Region

End Class

'Private Sub Export(ByVal sender As Object, ByVal e As EventArgs)
'    Export()
'End Sub

'#Region "Export"
'    Private Sub Export()
'        Dim vFileName As String
'        Dim vEmployeeName As String = ""
'        Dim vDepartment As String = ""
'        Dim sa() As String = Nothing
'        Dim vAttendanceTable As New GSCOM.Applications.InSys.Database.Templates.AttendanceTable
'        Dim dr As DataRow
'        Dim ofd As New SaveFileDialog
'        Dim a As New GSCOM.Applications.InSys.Database.Templates.AttendanceAdapter
'        ofd.FileName = "AttendanceTemplate.xls"
'        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
'            vFileName = ofd.FileName  'GSCOM.Common.AddPathSeparator(ofd.SelectedPath) & "AttendanceTemplate.xls" 'set the filename
'            GSCOM.Common.FileFromObject(GSCOM.Applications.InSys.Database.GetAttendanceTemplate, vFileName) 'create the file
'            a.DataSource = vFileName 'initialize datasource (filename)
'            For Each drx As DataRow In mtAttendance.Select
'                dr = vAttendanceTable.NewRow
'                dr("EmployeeCode") = drx("EmployeeCode")
'                dr("Department") = drx("Department")
'                dr("EmployeeName") = drx("Employee")
'                dr("Date") = drx("Date")
'                dr("TimeIn") = drx("TimeIn")
'                dr("TimeOut") = drx("TimeOut")

'                dr("ActualHours") = drx("ComputedREGHours")
'                dr("Tardy") = drx("ComputedTardyMinutes")
'                dr("OT") = drx("ComputedOTHours")
'                dr("TotalHours") = drx("ComputedREGHours")
'                vAttendanceTable.Rows.Add(dr)
'            Next
'            a.Update(vAttendanceTable.Select("", "Department ASC, EmployeeCode ASC"))
'            MsgBox("Done", MsgBoxStyle.Information)
'        End If
'    End Sub

'#End Region
