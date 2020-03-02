Option Explicit On
Option Strict Off



Friend Class EmployeeInfo
    Inherits InfoSet

    Public MonthsPerYear As Decimal = 0
    Public DaysPerYear As Decimal = 0
    Public HoursPerDay As Decimal = 0
    Dim IsNew As Boolean = False
    Dim gpID As Integer
    Private myDT As New Database.Tables.tEmployee(Connection)
    Private mtPersona As New Database.Tables.tPersona(Connection)
    Private mtEmployeeRestDay As New Database.Tables.tEmployeeRestDay(Connection)
    'Private mtEmployeeRestDay As GSCOM.SQL.ZDataTable
    Private mtCompanyRestDay As GSCOM.SQL.ZDataTable

    Private mControl As New InSys.DataControl
    Private mWeekDayTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name FROM vWeekDay_List ORDER BY ID", Connection)
    Private mTVWeekDay As New InSys.WeekDaySelector
    Private mViewPersona As ToolStripButton
    Private WithEvents MainTab As System.Windows.Forms.TabControl

#Region "Controls"
    Private WithEvents _ID_Parameter As ComboBox
    Private _MonthlyRate As TextBox
    Private _DailyRate As TextBox
    Private _HourlyRate As TextBox
    Private _DMonthlyRate As TextBox
    Private _DDailyRate As TextBox
    Private _DHourlyRate As TextBox
    Private _MonthlySMW As TextBox
    Private _DailySMW As TextBox
#End Region

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        IsNew = pID = 0

        With mDataset.Tables
            .Add(myDT)
            .Add(mtEmployeeRestDay)
        End With

        'mViewPersona = MyBase.AddButton("View Employee Data", gMainForm.imgList.Images("_persona.png"), AddressOf ViewEmployeeData)

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        pdc = myDT.Columns(Database.Tables.tEmployee.Field.ID)
        cdc = mtEmployeeRestDay.Columns(Database.Tables.tEmployeeRestDay.Field.ID_Employee)
        rel = mDataset.Relations.Add(pdc, cdc)

        'mtEmployeeRestDay = Me.mDataset.Tables("tEmployeeRestDay")

        InitControl(pMenu)
        Me.NoGridTables &= "," & mtEmployeeRestDay.TableName
        'mtEmployeeRestDay = DirectCast(Me.mDataset.Tables("tEmployeeRestDay"), GSCOM.SQL.ZDataTable)

        Me.ReloadAfterCommit = True

        _ID_Parameter = CType(Me.GetControl("_ID_Parameter"), System.Windows.Forms.ComboBox)
        _MonthlyRate = CType(Me.GetControl("_MonthlyRate"), System.Windows.Forms.TextBox)
        _DailyRate = CType(Me.GetControl("_DailyRate"), System.Windows.Forms.TextBox)
        _HourlyRate = CType(Me.GetControl("_HourlyRate"), System.Windows.Forms.TextBox)
        _DMonthlyRate = CType(Me.GetControl("_DMonthlyRate"), System.Windows.Forms.TextBox)
        _DDailyRate = CType(Me.GetControl("_DDailyRate"), System.Windows.Forms.TextBox)
        _DHourlyRate = CType(Me.GetControl("_DHourlyRate"), System.Windows.Forms.TextBox)
        _MonthlySMW = CType(Me.GetControl("_MonthlySMW"), System.Windows.Forms.TextBox)
        _DailySMW = CType(Me.GetControl("_DailySMW"), System.Windows.Forms.TextBox)

        If Me.GetTabPage("Salary") IsNot Nothing Then
            MainTab = CType(Me.GetTabPage("Salary").Parent, System.Windows.Forms.TabControl)
        End If
        'Dim b As Boolean
        'b = CBool(GSCOM.SQL.ExecuteScalar("SELECT CanViewEmployeeSalary FROM tUserGroup ug INNER JOIN tUser u on ug.id=u.ID_UserGroup WHERE u.ID=" & nDB.GetUserID.ToString, Connection))
        'If Not b Then
        '    MainTab.TabPages.Remove(Me.GetTabPage("Salary"))
        'End If
        'Dim s As String
        's = "SELECT * FROM tCompanyRestDay WHERE ID_Company=" & myDT.Rows(0)("ID_Company").ToString

        'mtCompanyRestDay = CType(GSCOM.SQL.TableQuery(s, gConnection), SQL.ZDataTable)

        If MainTab IsNot Nothing Then
            AddHandler _MonthlyRate.Validated, AddressOf _MonthlyRate_Validated
            AddHandler _DailyRate.Validated, AddressOf _DailyRate_Validated
            AddHandler _HourlyRate.Validated, AddressOf _HourlyRate_Validated

            If IsNothing(_DMonthlyRate) Then
            Else
                AddHandler _DMonthlyRate.Validated, AddressOf _DMonthlyRate_Validated
                AddHandler _DDailyRate.Validated, AddressOf _DDailyRate_Validated
                AddHandler _DHourlyRate.Validated, AddressOf _DHourlyRate_Validated
            End If
            If IsNothing(_DailySMW) Or IsNothing(_MonthlySMW) Then
            Else
                AddHandler _DailySMW.Validated, AddressOf _DailySMW_Validated
                AddHandler _MonthlySMW.Validated, AddressOf _MonthlySMW_Validated
            End If

        End If
        AddHandler Me.GetStripButton("View Employee Data").Click, AddressOf ViewEmployeeData
        With mTVWeekDay
            .DataSource = mWeekDayTable
            '.GetSelectedIDs()

            .Go()
        End With
        'Me.AddControl(mTVWeekDay, 
        Dim tp As TabPage = Me.AddControl(mTVWeekDay, "Rest Days")
        Dim ts As ToolStrip
        ts = CType(tp.Tag, ToolStrip)
        'With ts.Items.Add("Load List")

        '    .ImageKey = "_weekday.png"
        '    .Alignment = ToolStripItemAlignment.Right
        '    AddHandler .Click, AddressOf LoadListClick

        'End With

        AfterNew()
    End Sub

    Private Sub ViewEmployeeData(ByVal sender As Object, ByVal e As EventArgs)
        Dim oPersona As InfoSet
        oPersona = GetInfoSet(Database.Menu.INSYSPEOPLE_EmployeeInfo)
        Dim id As Integer
        id = CInt(myDT.Get(Database.Tables.tEmployee.Field.ID_Persona).ToString)

        If oPersona Is Nothing Then
            oPersona = New PersonaInfo(Database.Menu.INSYSPEOPLE_EmployeeInfo, Connection, mtPersona, id)
            'oPersona = New EmployeeDailyScheduleInfo(Database.Menu.EMPLOYEETIMEMANAGEMENTSYSTEM_EmployeeDailySchedule, Connection, mtEmployeeDailySchedule, pID)
            AddInfoSet(oPersona, Database.Menu.INSYSPEOPLE_EmployeeInfo)
            'ROBBIE 20070517 ---------------\
            'If Not CBool(GSCOM.Applications.InSys.nDB.GetMenuValue(Database.Menu.EMPLOYEETIMEMANAGEMENTSYSTEM_EmployeeDailySchedule, Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)) Then
            ' oPersona.MakeReadOnly()
            'End If
            'ROBBIE 20070517 ---------------/
            'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
            'oPersona.GetStripButton("Compute Header").Enabled = False
            'oPersona.GetStripButton("Compute Hours").Enabled = False
            'End If
        Else
            oPersona.LoadInfo(id)
            'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
            '   oPersona.GetStripButton("Compute Header").Enabled = False
            '  oPersona.GetStripButton("Compute Hours").Enabled = False
            'End If
        End If
        Application.DoEvents()
        oPersona.SaveButton.Enabled = mControl.Enabled
        oPersona.Size = Me.Size
        oPersona.ShowDialog()
        'LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))

    End Sub

    Private Sub LoadListClick()
        mTVWeekDay.ExpandAll()

    End Sub

    Protected Overrides Sub Finalize()
        mControl = Nothing
        MyBase.Finalize()
    End Sub

#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        Dim vNew As Boolean
        Dim s As String
        vNew = pID = 0
        IsNew = vNew

        If vNew Then
            Dim dt As DataTable
            s = "SELECT * FROM dbo.fGetEmployeeCode ('" + nDB.GetServerDate.ToShortDateString + "')"
            dt = GSCOM.SQL.TableQuery(s, Connection)
            If dt.Rows.Count > 0 Then
                myDT.Columns(Database.Tables.tEmployee.Field.Code).DefaultValue = dt.Rows(0).Item(1).ToString
            End If
        End If
        MyBase.LoadInfo(pID)
        If vNew Then

            Dim dt As DataTable
            s = "SELECT ID_WeekDay FROM tCompanyRestday WHERE ID_Company=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployee.Field.ID_Company))
            dt = GSCOM.SQL.TableQuery(s, Connection)
            Dim drx As DataRow
            For Each dr As DataRow In dt.Rows
                drx = mtEmployeeRestDay.NewRow
                drx(Database.Tables.tEmployeeRestDay.Field.ID_WeekDay.ToString) = dr(Database.Tables.tCompanyRestDay.Field.ID_WeekDay.ToString)
                mtEmployeeRestDay.Rows.Add(drx)

            Next

        Else
            MyBase.LoadInfo(pID)
            Dim o As Object
            o = myDT.Rows(0).Item("MonthsPerYear")
            MonthsPerYear = CDec(IIf(IsDBNull(o), 0, o))

            o = myDT.Rows(0).Item("DaysPerYear")
            DaysPerYear = CDec(IIf(IsDBNull(o), 0, o))

            o = myDT.Rows(0).Item("HoursPerDay")
            HoursPerDay = CDec(IIf(IsDBNull(o), 0, o))

        End If

        mTVWeekDay.CheckNodes(mtEmployeeRestDay, Database.Tables.tEmployeeRestDay.Field.ID_WeekDay.ToString)
        mTVWeekDay.ExpandAll()
        mTVWeekDay.CheckNodes(mtEmployeeRestDay, Database.Tables.tEmployeeRestDay.Field.ID_WeekDay.ToString)
        'mTVWeekDay.RetainChecked()
    End Sub

    Protected Overrides Function CanSave() As Boolean
        mTVWeekDay.EndEdit(mtEmployeeRestDay, Database.Tables.tCompanyRestDay.Field.ID_WeekDay.ToString)

        Dim emp As String
        Dim emps As Boolean
        emp = "Select dbo.fEmployeeValidInsert(" & nDB.GetCompanyID & ")"

        If nDB.GetCompanyID.ToString = "" Then
            emp = "Select dbo.fEmployeeValidInsert(NULL)"
        End If

        Try
            emps = CType(GSCOM.SQL.ExecuteScalar(emp, gConnection), Boolean)
        Catch ex As Exception
            emps = False
        End Try

        If Not emps And Not IsNew Then
            MsgBox("You have reached the maximum number of employees that can be inputted in the system.")

        Else
            Return MyBase.CanSave()
        End If

        ' Return MyBase.CanSave()
    End Function


    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployee)
        End Set
    End Property



#End Region


    Private Sub EmployeeInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved


        Dim s As String
        s = Strings.Right(myDT.TableName, myDT.TableName.Length - 1)
        s = "SELECT ID FROM vzValid" & s & " WHERE ID=" & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString
        If GSCOM.SQL.TableQuery(s, e.Transaction).Rows.Count = 0 Then
            Throw New InfoSet.InconsistentDataException()
        Else
            If e.RowState = DataRowState.Added Then
                GSCOM.SQL.ExecuteNonQuery("pEmployeeLeaveCredit " & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString, e.Transaction)
                'MyBase.ReloadAfterCommit = True
            End If
        End If
    End Sub

    '------------


#Region "Rate"
    'ROBBIE: USE VALIDATE EVENT TO AVOID MULTIPLE TRIGERRING 
    ' ESPECIALLY WHEN TEXT IS LOADED PROGRAMATICALLY
    Private Sub _MonthlyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _MonthlyRate.Validated
        Dim d As Decimal
        If Ok() Then
            d = (GSCOM.Common.GetTextBoxDecimalValue(_MonthlyRate) * MonthsPerYear / DaysPerYear)
            _DailyRate.Text = d.ToString
            _HourlyRate.Text = (d / HoursPerDay).ToString
        End If
    End Sub

    Private Sub _DailyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _DailyRate.Validated
        If Ok() Then
            _MonthlyRate.Text = (GSCOM.Common.GetTextBoxDecimalValue(_DailyRate) * DaysPerYear / MonthsPerYear).ToString
            _HourlyRate.Text = (GSCOM.Common.GetTextBoxDecimalValue(_DailyRate) / HoursPerDay).ToString
        End If
    End Sub

    Private Sub _HourlyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _HourlyRate.Validated
        If Ok() Then
            Dim d As Decimal
            d = (GSCOM.Common.GetTextBoxDecimalValue(_HourlyRate) * HoursPerDay)
            _DailyRate.Text = d.ToString
            _MonthlyRate.Text = (d * DaysPerYear / MonthsPerYear).ToString
        End If
    End Sub

    Private Sub _DMonthlyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles _DMonthlyRate.Validated
        If Ok() Then
            Dim d As Decimal
            d = (GSCOM.Common.GetTextBoxDecimalValue(_DMonthlyRate) * MonthsPerYear / DaysPerYear)
            _DDailyRate.Text = d.ToString
            _DHourlyRate.Text = (d / HoursPerDay).ToString
        End If
    End Sub

    Private Sub _DDailyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _DDailyRate.Validated
        If Ok() Then
            _DMonthlyRate.Text = (GSCOM.Common.GetTextBoxDecimalValue(_DDailyRate) * DaysPerYear / MonthsPerYear).ToString
            _DHourlyRate.Text = (GSCOM.Common.GetTextBoxDecimalValue(_DDailyRate) / HoursPerDay).ToString
        End If
    End Sub

    Private Sub _DHourlyRate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _DHourlyRate.Validated
        If Ok() Then
            Dim d As Decimal
            d = (GSCOM.Common.GetTextBoxDecimalValue(_DHourlyRate) * HoursPerDay)
            _DDailyRate.Text = d.ToString
            _DMonthlyRate.Text = (d * DaysPerYear / MonthsPerYear).ToString
        End If
    End Sub

    Private Sub _DailySMW_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _DailySMW.Validated
        If Ok() Then
            _MonthlySMW.Text = (GSCOM.Common.GetTextBoxDecimalValue(_DailySMW) * DaysPerYear / MonthsPerYear).ToString
        End If
    End Sub

    Private Sub _MonthlySMW_Validated(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _MonthlySMW.Validated
        If Ok() Then
            Dim d As Decimal
            d = (GSCOM.Common.GetTextBoxDecimalValue(_MonthlySMW) * MonthsPerYear / DaysPerYear)
            _DailySMW.Text = d.ToString
        End If
    End Sub

    Private Function Ok() As Boolean
        Dim b As Boolean
        b = (MonthsPerYear <> 0) And (DaysPerYear <> 0) And (HoursPerDay <> 0)
        Return b
    End Function

#End Region

    Private Sub _ID_Parameter_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ID_Parameter.SelectedValueChanged
        If IsNew Then
            Dim dta As DataTable
            Dim s As String
            s = "SELECT MonthsPerYear,DaysPerYear,HoursPerDay FROM tParameter WHERE ID =" & GSCOM.SQL.SQLFormat(_ID_Parameter.SelectedValue)
            dta = GSCOM.SQL.TableQuery(s, Connection)
            If dta.Select.Length > 0 Then
                MonthsPerYear = CDec(dta.Rows(0).Item("MonthsPerYear"))
                DaysPerYear = CDec(dta.Rows(0).Item("DaysPerYear"))
                HoursPerDay = CDec(dta.Rows(0).Item("HoursPerDay"))
            End If
        End If
    End Sub
End Class
