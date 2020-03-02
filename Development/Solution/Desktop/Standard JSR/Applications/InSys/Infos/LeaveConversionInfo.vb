Option Explicit On
Option Strict On



Friend Class LeaveConversionInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tLeaveConversion(Connection)
    'Private mtLeaveDetail As New Database.Tables.tLeave_Detail(Connection)
    Private mtLeaveConversion_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tLeaveConversion_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.LeaveConversionControl
    Private mImportButton As ToolStripButton
    Private mPost As ToolStripButton
    Private mLoad As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtLeaveConversion_Detail)
        End With
        InitControl(pMenu)

        'gLen.code 20110416
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tLeaveConversion.Field.ID)
        'cdc = mtLeaveConversion_Detail.Columns(Database.Tables.tLeaveConversion_Detail.Field.ID_LeaveConversion)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mtLeaveConversion_Detail = DirectCast(Me.mDataset.Tables("tLeaveConversion_Detail"), GSCOM.SQL.ZDataTable)
        '###

        'mPost = MyBase.AddButton("Apply", gMainForm.imgList.Images("_LeaveConversion.ico"), AddressOf Post)
        'mLoad = MyBase.AddButton("Load Details", gMainForm.imgList.Images("_LeaveConversion.ico"), AddressOf Load)

        AfterNew()
        mGrid = Me.GetDataGridView(mtLeaveConversion_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        Dim sb As New System.Text.StringBuilder
        sb.Append(" dbo.fEmployeeIsUnderUser(ID_Employee," & gUser & ") = 1")
        mtLeaveConversion_Detail.ClearThenFill(sb.ToString)
    End Sub

#End Region

    'gLen.code 20110416
    'Private Sub Post(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to apply this file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        'GSCOM.SQL.ExecuteNonQuery("EXEC pLeaveConversion " & myDT.Get(Database.Tables.tLeaveConversion.Field.ID).ToString & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeaveConversion.Field.StartDate).ToString) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeaveConversion.Field.EndDate).ToString), Connection)
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pLeaveConversion_Apply " & myDT.Get(Database.Tables.tLeaveConversion.Field.ID).ToString, gConnection)
    '        MyBase.LoadInfo(CInt(myDT.Get(Database.Tables.tLeaveConversion.Field.ID)))
    '        MsgBox("Finished Applying the file.", MsgBoxStyle.Information)
    '    End If

    'End Sub

    'Private Sub Load(ByVal sender As Object, ByVal e As EventArgs)
    '    GSCOM.SQL.ExecuteNonQuery("EXEC pLeaveConversion " & myDT.Get(Database.Tables.tLeaveConversion.Field.ID).ToString, Connection)
    '    MyBase.LoadInfo(CInt(myDT.Get(Database.Tables.tLeaveConversion.Field.ID)))
    'End Sub
    '###

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tLeaveConversion)
        End Set
    End Property
#End Region

End Class
