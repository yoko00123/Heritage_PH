Option Explicit On
Option Strict On



Friend Class SystemApplicationInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tSystemApplication(Connection)
    Private myDT_SystemApplicationMenu As New Database.Tables.tSystemApplicationMenu(Connection)
    Private mControl As New InSys.DataControl

    Private WithEvents mTVMenu As New MenuTreeView
    Private WithEvents mTVMenuPreview As New MenuTreeView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            .Add(myDT_SystemApplicationMenu)
        End With
        InitControl(pMenu)
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        pdc = myDT.Columns(Database.Tables.tSystemApplication.Field.ID)
        cdc = myDT_SystemApplicationMenu.Columns(Database.Tables.tSystemApplicationMenu.Field.ID_SystemApplication)
        rel = mDataset.Relations.Add(pdc, cdc)

        With mTVMenu
            .DataSource = GSCOM.SQL.TableQuery("SELECT * FROM tMenu", gConnection)
            .DataDestination = myDT_SystemApplicationMenu
            .ValueMember = Database.Tables.tSystemApplicationMenu.Field.ID_Menu.ToString
        End With
        Me.AddControl(mTVMenu, "Menu")

        mTVMenuPreview.CheckBoxes = False
        mTVMenuPreview.ShowAllNode = False
        Me.AddControl(mTVMenuPreview, "Menu Preview")

        Me.NoGridTables = myDT_SystemApplicationMenu.TableName
        AfterNew()
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'myDT_SystemApplicationMenu.ClearThenFill(Database.Tables.tSystemApplicationMenu.Field.ID_SystemApplication.ToString & "=" & pID.ToString)
        MyBase.LoadInfo(pID)
        mTVMenu.CheckNodes()
        mTVMenuPreview.DataSource = GSCOM.SQL.TableQuery(UpdateMenuPreview(pID), gConnection)
    End Sub

    Private Function UpdateMenuPreview(ByVal pID As Integer) As String
        Dim s As String
        s = "SELECT * FROM tMenu M INNER JOIN tSystemApplicationMenu SAM ON M.ID = SAM.ID_Menu INNER JOIN tSystemApplication SA ON SA.ID = SAM.ID_SystemApplication"
        s &= " WHERE (M.IsActive=1) AND (SA.ID=" & pID & ")"
        Return s
    End Function

#Region "Overrides"

    Protected Overrides Function CanSave() As Boolean
        mTVMenu.EndEdit()
        Return MyBase.CanSave()
    End Function


    'Protected Overrides Sub SetDefaultValues()
    '    Dim vID As Integer
    '    vID = CInt(myDT.Get(Database.Tables.tSystemApplication.Field.ID))
    '    myDT_SystemApplicationMenu.Columns(Database.Tables.tSystemApplicationMenu.Field.ID_SystemApplication).DefaultValue = vID
    'End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tSystemApplication)
        End Set
    End Property



#End Region

    Private Sub MenuInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        If Not Me.ReloadAfterCommit Then 'dont double
            Dim pID As Integer
            pID = CInt(myDT.Get(Database.Tables.tSystemApplication.Field.ID))
            mTVMenuPreview.DataSource = GSCOM.SQL.TableQuery(UpdateMenuPreview(pID), e.Transaction)
        End If
    End Sub
End Class
