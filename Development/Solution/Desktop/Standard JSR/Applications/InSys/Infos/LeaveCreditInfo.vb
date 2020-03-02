'Option Explicit On
'Option Strict On

'

'Friend Class LeaveCreditInfo
'    Inherits InfoSet

'    Private myDT As New Database.Tables.tLeaveCredit(Connection)
'    Private mtLeaveCredit_Detail As New Database.Tables.tLeaveCredit_Detail(Connection)
'    Private mControl As New nDB.LeaveCreditControl

'    Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(Table)
'            .Add(mtLeaveCredit_Detail)
'        End With
'        Dim pdc As DataColumn
'        Dim cdc As DataColumn
'        Dim rel As DataRelation
'        pdc = myDT.Columns(Database.Tables.tLeaveCredit.Field.ID)
'        cdc = mtLeaveCredit_Detail.Columns(Database.Tables.tLeaveCredit_Detail.Field.ID_LeaveCredit)
'        rel = mDataset.Relations.Add(pdc, cdc)
'        myDT.Columns(Database.Tables.tLeaveCredit.Field.ID_Company).DefaultValue = nDB.GetCompanyID
'        AfterNew()
'        'Dim cbo As DataGridViewComboBoxColumn
'        'cbo = CType(MyBase.GetDataGridView(mtLeaveCredit_Detail).Columns(Database.Tables.tLeaveCredit_Detail.Field.ID_PayrollItem.ToString), DataGridViewComboBoxColumn)
'        'CType(cbo.DataSource, DataTable).DefaultView.RowFilter = "(ID_PayrollItemCategory=3)"
'    End Sub

'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        'mtLeaveCredit_Detail.ClearThenFill("ID_LeaveCredit=" & pID.ToString)
'        MyBase.LoadInfo(pID)
'    End Sub

'    'Protected Overrides Sub SetDefaultValues()
'    '    Dim vID As Integer
'    '    vID = CInt(myDT.Get(Database.Tables.tLeaveCredit.Field.ID))
'    '    mtLeaveCredit_Detail.Columns(Database.Tables.tLeaveCredit_Detail.Field.ID_LeaveCredit).DefaultValue = vID
'    'End Sub

'#Region "Overrides"
'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, Database.Tables.tLeaveCredit)
'        End Set
'    End Property

'    Protected Overrides Property Control() As System.Windows.Forms.Control
'        Get
'            Return mControl
'        End Get
'        Set(ByVal value As System.Windows.Forms.Control)
'            mControl = CType(value, nDB.LeaveCreditControl)
'        End Set
'    End Property

'#End Region


'End Class
