Option Explicit On
Option Strict On



Friend Class ZInfo
    Inherits InfoSet

    Private myDT As GSCOM.SQL.ZDataTable
    'Private mControl As New InSys.DataControl

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        Dim s As String
        s = nDB.GetMenuValue(CType(pMenu, Database.Menu), Database.Tables.tMenu.Field.TableName).ToString
        myDT = New GSCOM.SQL.ZDataTable(c, s)
        With mDataset.Tables
            .Add(Table)
        End With
        InitControl(pMenu)
        'HARDCODE

        AfterNew()
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, GSCOM.SQL.ZDataTable)
        End Set
    End Property
End Class
