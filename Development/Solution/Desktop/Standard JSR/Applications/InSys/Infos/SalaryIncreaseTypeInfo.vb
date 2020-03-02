Option Explicit On
Option Strict On



Friend Class SalaryIncreaseTypeInfo
    Inherits InfoSet



    Private myDT As New Database.Tables.tSalaryIncreaseType(Connection)
    Private mControl As New InSys.DataControl  'Private mControl As New nDB.SalaryIncreaseTypeControl

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)
        'myDT.Columns(Database.Tables.tAlertType.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        AfterNew()
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tSalaryIncreaseType)
        End Set
    End Property





End Class
