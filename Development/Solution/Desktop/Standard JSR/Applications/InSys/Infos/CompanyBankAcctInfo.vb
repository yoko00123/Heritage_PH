Option Explicit On
Option Strict On



Friend Class CompanyBankAcctInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tCompanyBankAcct(Connection)


    Private mControl As New InSys.DataControl

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)

        End With
        InitControl(pMenu)
        Me.ReloadAfterCommit = True
        myDT.Columns(Database.Tables.tCompanyBankAcct.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        AfterNew()
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tCompanyBankAcct)
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

    Private Sub EmployeeInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        GSCOM.SQL.ExecuteNonQuery("pBankAcctSetting_LoadDefaultValues " & myDT.Get(Database.Tables.tCompanyBankAcct.Field.ID).ToString, e.Transaction)
    End Sub

End Class
