Option Explicit On
Option Strict On



Friend Class OBInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tOB(Connection)
    Private mtOB_Detail As New Database.Tables.tOB_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.OBFileControl

    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtOB_Detail)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tOB.Field.ID)
        cdc = mtOB_Detail.Columns(Database.Tables.tOB_Detail.Field.ID_OB)
        rel = mDataset.Relations.Add(pdc, cdc)

        '  mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ApplyFile)

        Me.ReloadAfterCommit = True
        AfterNew()



    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)

    End Sub

#End Region



    Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
        'If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    GSCOM.SQL.ExecuteNonQuery("EXEC pOB_Apply " & myDT.Get(Database.Tables.tOB.Field.ID).ToString, Connection)
        '    'Me.mApplyButton.Enabled = False
        '    'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
        '    MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        'End If
    End Sub


#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tOB)
        End Set
    End Property


#End Region




    Private Sub OBInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        GSCOM.SQL.ExecuteNonQuery("EXEC pOB " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tOB.Field.ID)), e.Transaction)

    End Sub
End Class

