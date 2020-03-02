'Option Explicit On
'Option Strict On

'
'Namespace TimeKeeping
'    Friend Class EmployeeInfo
'        Inherits InfoSet

'        Private myDT As New Database.Tables.tEmployee(Connection)

'        Private mtEmployeeRestDay As New Database.Tables.tEmployeeRestDay(Connection)
'        ' Private mtPayrollItemSetup As New Database.Tables.tPayrollItemSetup(Connection)
'        'Private mvLeaveCredit As DataTable
'        'Private mtEmployeeMovement As New Database.Tables.tEmployeeMovement(Connection)
'        'Private mtEmployeeLogDevice As New Database.Tables.tEmployeeLogDevice(Connection)
'        'Private mtTrainingActivityEmployee As New Database.Tables.tTrainingActivityEmployee(Connection)
'        'Private mControl As New nDB.EmployeeControl
'        Private mControl As New nDB.TimeKeeping.EmployeeControl
'        Private mWeekDayTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name FROM vWeekDay_List", Connection)
'        Private mTVWeekDay As New WeekDaySelector
'        Private dgv As GSDetailDataGridView
'        Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'            MyBase.New(c, pListing, pID)
'            With mDataset.Tables
'                .Add(myDT)
'                .Add(mtEmployeeRestDay)
'                '.Add(mtPayrollItemSetup)
'                '.Add(mtEmployeeMovement)
'                '.Add(mtEmployeeLogDevice)
'                '.Add(mtTrainingActivityEmployee)
'            End With
'            Dim pdc As DataColumn
'            Dim cdc As DataColumn
'            Dim rel As DataRelation
'            pdc = myDT.Columns(Database.Tables.tEmployee.Field.ID)
'            cdc = mtEmployeeRestDay.Columns(Database.Tables.tEmployeeRestDay.Field.ID_Employee)
'            rel = mDataset.Relations.Add(pdc, cdc)
'            'cdc = mtPayrollItemSetup.Columns(Database.Tables.tPayrollItemSetup.Field.ID_Employee)
'            'rel = mDataset.Relations.Add(pdc, cdc)
'            'cdc = mtEmployeeMovement.Columns(Database.Tables.tEmployeeMovement.Field.ID_Employee)
'            'rel = mDataset.Relations.Add(pdc, cdc)
'            'cdc = mtEmployeeLogDevice.Columns(Database.Tables.tEmployeeLogDevice.Field.ID_Employee)
'            'rel = mDataset.Relations.Add(pdc, cdc)
'            'cdc = mtTrainingActivityEmployee.Columns(Database.Tables.tTrainingActivityEmployee.Field.ID_Employee)
'            'rel = mDataset.Relations.Add(pdc, cdc)

'            With mTVWeekDay
'                .DataSource = mWeekDayTable
'                .Go()
'            End With
'            Me.AddControl(mTVWeekDay, "RestDay")


'            'Me.NoGridTables = mtTrainingActivityEmployee.TableName
'            Me.NoGridTables &= "," & mtEmployeeRestDay.TableName

'            myDT.Columns(Database.Tables.tEmployee.Field.ID_Company).DefaultValue = nDB.GetCompanyID
'            'If TypeOf mControl Is nDB.External.EmployeeControl Then
'            '    myDT.Columns(Database.Tables.tEmployee.Field.ID_Branch).DefaultValue = 1
'            'End If

'            ' Dim s As String
'            's = "[" & Database.Tables.tEmployeeMovement.Field.EffectivityDate.ToString & "] DESC"
'            's &= ", [" & Database.Tables.tEmployeeMovement.Field.ID.ToString & "] DESC"
'            'mtEmployeeMovement.DefaultView.Sort = s

'            Me.ReloadAfterCommit = True

'            ' Dim b As Boolean
'            ' b = CBool(GSCOM.SQL.ExecuteScalar("SELECT CanViewEmployeeSalary FROM tUserGroup ug INNER JOIN tUser u on ug.id=u.ID_UserGroup WHERE u.ID=" & nDB.GetUserID.ToString, Connection))
'            ' If Not b Then
'            '' mControl.RemoveTabPage("Salary")
'            ' Me.NoGridTables &= "," & mtEmployeeMovement.TableName
'            ' End If


'            dgv = Me.AddGrid("Leave Credit")
'            With dgv
'                .AutoGenerateColumns = True
'                .ReadOnly = True
'                .AllowUserToAddRows = False
'                .AllowUserToDeleteRows = False
'            End With


'            AfterNew()
'            'Console.WriteLine(mtEmployeeMovement.Adapter.DeleteCommand.CommandText)
'        End Sub

'        Protected Overrides Sub Finalize()
'            mControl = Nothing
'            MyBase.Finalize()
'        End Sub

'#Region "Overrides"

'        Public Overrides Sub LoadInfo(ByVal pID As Integer)
'            Dim vNew As Boolean
'            Dim s As String
'            vNew = pID = 0
'            If vNew Then
'                Dim dt As DataTable
'                s = "SELECT * FROM dbo.fGetEmployeeCode ('" + nDB.GetServerDate.ToShortDateString + "')"
'                dt = GSCOM.SQL.TableQuery(s, Connection)
'                If dt.Rows.Count > 0 Then
'                    myDT.Columns(Database.Tables.tEmployee.Field.Code).DefaultValue = dt.Rows(0).Item(1).ToString
'                End If
'            End If
'            MyBase.LoadInfo(pID)
'            If vNew Then
'                Dim dt As DataTable
'                s = "SELECT ID_WeekDay FROM tCompanyRestday WHERE ID_Company=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployee.Field.ID_Company))
'                dt = GSCOM.SQL.TableQuery(s, Connection)
'                Dim drx As DataRow
'                For Each dr As DataRow In dt.Rows
'                    drx = mtEmployeeRestDay.NewRow
'                    drx(Database.Tables.tEmployeeRestDay.Field.ID_WeekDay.ToString) = dr(Database.Tables.tCompanyRestDay.Field.ID_WeekDay.ToString)
'                    mtEmployeeRestDay.Rows.Add(drx)
'                Next
'            Else
'                mControl.DaysPerYear = CInt(myDT.Rows(0).Item("DaysPerYear"))
'                mControl.HoursPerDay = CInt(myDT.Rows(0).Item("HoursPerDay"))
'            End If

'            'moved from above
'            mTVWeekDay.CheckNodes(mtEmployeeRestDay, Database.Tables.tEmployeeRestDay.Field.ID_WeekDay.ToString)
'            dgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,LeavePayrollItem, Alloted, Used, Balance FROM vzEmployeeLeaveCredit WHERE ID_Employee=" & pID.ToString, gConnection)
'        End Sub

'        Protected Overrides Function CanSave() As Boolean
'            mTVWeekDay.EndEdit(mtEmployeeRestDay, Database.Tables.tCompanyRestDay.Field.ID_WeekDay.ToString)
'            Return MyBase.CanSave()
'        End Function


'        Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'            Get
'                Return myDT
'            End Get
'            Set(ByVal value As GSCOM.SQL.ZDataTable)
'                myDT = CType(value, Database.Tables.tEmployee)
'            End Set
'        End Property



'#End Region

'        Private Sub EmployeeInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
'            Dim s As String
'            s = Strings.Right(myDT.TableName, myDT.TableName.Length - 1)
'            s = "SELECT ID FROM vzValid" & s & " WHERE ID=" & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString
'            If GSCOM.SQL.TableQuery(s, e.Transaction).Rows.Count = 0 Then
'                Throw New InfoSet.InconsistentDataException()
'            Else
'                If e.RowState = DataRowState.Added Then
'                    GSCOM.SQL.ExecuteNonQuery("pEmployeeLeaveCredit " & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString, e.Transaction)
'                    'MyBase.ReloadAfterCommit = True
'                End If
'            End If
'        End Sub
'    End Class

'    'Protected Overrides Sub SetDefaultValues()
'    '    Dim vID As Integer
'    '    vID = CInt(myDT.Get(Database.Tables.tEmployee.Field.ID))
'    '    mtEmployeeRestDay.Columns(Database.Tables.tEmployeeRestDay.Field.ID_Employee).DefaultValue = vID
'    '    mtPayrollItemSetup.Columns(Database.Tables.tPayrollItemSetup.Field.ID_Employee).DefaultValue = vID
'    '    mvLeaveCredit.Columns(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee).DefaultValue = vID
'    '    mtTrainingActivityEmployee.Columns(Database.Tables.tTrainingActivityEmployee.Field.ID_Employee).DefaultValue = vID
'    'End Sub

'    'mtEmployeeRestDay.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
'    'mtPayrollItemSetup.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
'    'mvLeaveCredit.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
'    'mtTrainingActivityEmployee.ClearThenFill(Database.Tables.tTrainingActivityEmployee.Field.ID_Employee.ToString & "=" & pID.ToString)
'End Namespace
