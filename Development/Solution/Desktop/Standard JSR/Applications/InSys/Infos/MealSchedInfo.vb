Option Explicit On
Option Strict On



Friend Class MealSchedInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tMealSched(Connection)
    Private mControl As New InSys.DataControl
    'Private mControl As New nDB.MealSchedControl

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With

        InitControl(pMenu)
        AfterNew()
    End Sub

    Protected Overrides Sub Finalize()
        mControl = Nothing
        MyBase.Finalize()
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tMealSched)
        End Set
    End Property

#End Region


End Class
