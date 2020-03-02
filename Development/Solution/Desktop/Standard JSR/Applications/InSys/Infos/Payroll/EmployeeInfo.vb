Option Explicit On
Option Strict On



Namespace Payroll

    Friend Class EmployeeInfo
        Inherits InfoSet

        Private myDT As New Database.Tables.tEmployee(Connection)

        Private mtPayrollItemSetup As New Database.Tables.tPayrollItemSetup(Connection)
        Private mControl As New InSys.DataControl

        Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
            MyBase.New(c, pListing, pID)
            With mDataset.Tables
                .Add(myDT)
                .Add(mtPayrollItemSetup)
            End With
            '            InitControl(mControl, Database.Menu.System_Generic)

            Dim pdc As DataColumn
            Dim cdc As DataColumn
            Dim rel As DataRelation
            pdc = myDT.Columns(Database.Tables.tEmployee.Field.ID)
            cdc = mtPayrollItemSetup.Columns(Database.Tables.tPayrollItemSetup.Field.ID_Employee)
            rel = mDataset.Relations.Add(pdc, cdc)
            myDT.Columns(Database.Tables.tEmployee.Field.ID_Company).DefaultValue = nDB.GetCompanyID
            AfterNew()
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
            If vNew Then
                Dim dt As DataTable
                s = "SELECT * FROM dbo.fGetEmployeeCode ('" + nDB.GetServerDate.ToShortDateString + "')"
                dt = GSCOM.SQL.TableQuery(s, Connection)
                If dt.Rows.Count > 0 Then
                    myDT.Columns(Database.Tables.tEmployee.Field.Code).DefaultValue = dt.Rows(0).Item(1).ToString
                End If
            End If
            MyBase.LoadInfo(pID)
        End Sub

        Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
            Get
                Return myDT
            End Get
            Set(ByVal value As GSCOM.SQL.ZDataTable)
                myDT = CType(value, Database.Tables.tEmployee)
            End Set
        End Property



#End Region


    End Class

    'Protected Overrides Sub SetDefaultValues()
    '    Dim vID As Integer
    '    vID = CInt(myDT.Get(Database.Tables.tEmployee.Field.ID))
    '    mtEmployeeRestDay.Columns(Database.Tables.tEmployeeRestDay.Field.ID_Employee).DefaultValue = vID
    '    mtPayrollItemSetup.Columns(Database.Tables.tPayrollItemSetup.Field.ID_Employee).DefaultValue = vID
    '    mtEmployeeLeaveCredit.Columns(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee).DefaultValue = vID
    '    mtTrainingActivityEmployee.Columns(Database.Tables.tTrainingActivityEmployee.Field.ID_Employee).DefaultValue = vID
    'End Sub

    'mtEmployeeRestDay.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
    'mtPayrollItemSetup.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
    'mtEmployeeLeaveCredit.ClearThenFill(Database.Tables.tEmployeeLeaveCredit.Field.ID_Employee.ToString & "=" & pID.ToString)
    'mtTrainingActivityEmployee.ClearThenFill(Database.Tables.tTrainingActivityEmployee.Field.ID_Employee.ToString & "=" & pID.ToString)

End Namespace
