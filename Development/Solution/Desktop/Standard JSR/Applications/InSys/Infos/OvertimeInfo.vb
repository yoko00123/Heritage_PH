Option Explicit On
Option Strict On

Friend Class OvertimeInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tOvertime(Connection)
    Private mControl As New InSys.DataControl


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)
        AfterNew()
        'mControl._ComputedHours.ReadOnly = True

    End Sub
    Private mID As Integer

#Region "Overrides"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------\
        'Dataset.EnforceConstraints = False
        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------/

        'myDT_Payroll.Rows.Clear()
        'myDT_Payroll.SetFilter("ID_PayrollPeriod=" & pID.ToString)
        'myDT_Payroll.Fill()

        'ROBBIE: must be placed before loadinfo for creating new 
        'BILLY'myDT.Columns(Database.Tables.tOvertime.Field.ID_FilingStatus).DefaultValue = 1
        MyBase.LoadInfo(pID)
        mID = pID

        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------\
        'Dataset.EnforceConstraints = True
        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------/



        'CUSTOMIZED
        'NOTE: LoadInfo is only called after commiting if ReloadAfterCommit is true
        'mAddPayrollButton.Enabled = myDT.Rows(0).RowState <> DataRowState.Added

        MyBase.SaveButton.Enabled = Not CBool(myDT.Get(Database.Tables.tOvertime.Field.IsPaid))
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tOvertime)
        End Set
    End Property

#End Region

    Protected Overrides Function CanSave() As Boolean
        Dim s As String
        Dim b As Boolean

        Dim d As String
        If mID = 0 Then
            d = "NULL"
        Else
            d = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.ID))
        End If

        s = "SELECT dbo.fCheckOvertime(" & d & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.ID_Employee)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.WorkDate)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.FollowingDay)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.StartTime)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.EndTime)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOvertime.Field.ID_WorkCredit)) & ")"
        Try
            b = CType(GSCOM.SQL.ExecuteScalar(s, gConnection), Boolean)
        Catch ex As Exception
            b = False
        End Try

        If Not b Then
            MsgBox("Invalid Overtime Filing")
        Else
            Return MyBase.CanSave()
        End If
        Return b
    End Function

End Class
