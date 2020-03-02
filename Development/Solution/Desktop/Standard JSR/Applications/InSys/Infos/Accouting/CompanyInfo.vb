'Option Explicit On
'Option Strict On

'

'Namespace Accounting

'    Friend Class CompanyInfo
'        Inherits InfoSet

'        Private myDT As New Database.Tables.tCompany(Connection)
'        Private mtAccount As New Database.Tables.tAccount(Connection)
'        Private mtPayrollItemAccount As New Database.Tables.tPayrollItemAccount(Connection)
'        Private mControl As New Control  'ndb.CompanyControl

'        Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'            MyBase.New(c, pListing, pID)
'            With mDataset.Tables
'                .Add(Table)
'                .Add(mtAccount)
'                .Add(mtPayrollItemAccount)
'            End With
'            AfterNew()
'        End Sub

'        Public Overrides Sub LoadInfo(ByVal Id As Integer)
'            MyBase.LoadInfo(Id)
'            mtAccount.ClearThenFill("")
'            mtPayrollItemAccount.ClearThenFill("")
'        End Sub

'#Region "Overrides"
'        Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'            Get
'                Return myDT
'            End Get
'            Set(ByVal value As GSCOM.SQL.ZDataTable)
'                myDT = CType(value, Database.Tables.tCompany)
'            End Set
'        End Property

'        'Protected Overrides Property Control() As Control
'        '    Get
'        '        Return mControl
'        '    End Get
'        '    Set(ByVal value As Control)
'        '        mControl = CType(value, Control) ' ndb.CompanyControl)
'        '    End Set
'        'End Property

'#End Region


'    End Class

'End Namespace
