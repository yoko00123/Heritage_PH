Option Explicit On
Option Strict On



Friend Class TMonthInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tTMonth(Connection)
    'Private mtTMonth_Detail As New Database.Tables.tTMonth_Detail(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.TMonthControl
    Private mGenerateButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)

        With mDataset.Tables
            .Add(myDT)
            '.Add(mtTMonth_Detail)   'gLen.code 20110416
        End With

        InitControl(pMenu)

        'gLen.code 20110416
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tTMonth.Field.ID)
        'cdc = mtTMonth_Detail.Columns(Database.Tables.tTMonth_Detail.Field.ID_TMonth)
        'rel = mDataset.Relations.Add(pdc, cdc)
        'myDT.Columns(Database.Tables.tTMonth.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        '###

        Me.ReloadAfterCommit = True
        mGenerateButton = MyBase.AddButton("Generate", gMainForm.imgList.Images("misc.a.ico"), AddressOf Generate)
        'myDT.Columns(Database.Tables.tTMonth.Field.DaysPerYear).DefaultValue = nDB.GetSetting(dDaysPerYear)
        ' Me.ClearThenFillOnLoadInfo = False 'ROBBIE 20070319
        AfterNew()
    End Sub
#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mGenerateButton.Enabled = (pID > 0)
    End Sub

#End Region

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tTMonth)
        End Set
    End Property

#Region "Customized"
    Private Sub Generate(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Generate?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Generating 13th month details... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pTMonth " & GSCOM.SQL.SQLFormat(CInt(myDT.Get(Database.Tables.tTMonth.Field.ID).ToString)) & " , " & gUser, Connection)

            LoadInfo(CInt(myDT.Get(Database.Tables.tTMonth.Field.ID)))
            Application.DoEvents()
            EndProcess("")
            MsgBox("Finish generating 13th month.", MsgBoxStyle.Information)
        End If
    End Sub
#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
