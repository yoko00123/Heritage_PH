'Option Explicit On
'Option Strict On

'

'Friend Class TrainingInfo
'    Inherits InfoSet


'    Private myDT As New Database.Tables.tTraining(Connection)
'    'Private myDT_TrainingDesignation As New Database.Tables.tTrainingDesignation(Connection)
'    'Private myDT_TrainingParticipant As New Database.Tables.tTrainingParticipant(Connection)
'    'Private mDesignationTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name,JobClass,Company,CompanyGroup FROM " & nDB.GetMenuDataSourceValue(Database.Menu.Maintenance_Company_Designation), Connection)
'    Private mControl As New InSys.DataControl 'Private mControl As New nDB.TrainingControl
'    'Private mTVDesignation As New DesignationSelector
'    Private mLoadButton As ToolStripButton

'    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(Table)
'            '.Add(myDT_TrainingDesignation)
'            '.Add(myDT_TrainingParticipant)
'        End With

'        Dim pdc As DataColumn
'        'Dim cdc As DataColumn
'        'Dim rel As DataRelation

'        InitControl(pMenu)
'        pdc = myDT.Columns(Database.Tables.tTraining.Field.ID)
'        'cdc = myDT_TrainingDesignation.Columns(Database.Tables.tTrainingDesignation.Field.ID_Training)
'        'rel = mDataset.Relations.Add(pdc, cdc)
'        'cdc = myDT_TrainingParticipant.Columns(Database.Tables.tTrainingParticipant.Field.ID_Training)
'        'rel = mDataset.Relations.Add(pdc, cdc)

'        'For Each dr As DataRow In mDesignationTable.Select("ID IS NULL")
'        '    dr.Delete() 'this is null value added by GetLookUp Function
'        'Next
'        'With mTVDesignation
'        '    .DataSource = mDesignationTable
'        '    .Go()
'        'End With
'        'Me.AddControl(mTVDesignation, "Designation")

'        'Me.NoGridTables = myDT_TrainingDesignation.TableName

'        mLoadButton = MyBase.AddButton("Load Participants", gMainForm.imgList.Images("misc.a.ico"), AddressOf LoadParticipants)
'        Me.ReloadAfterCommit = True
'        AfterNew()
'    End Sub

'    Protected Overrides Function CanSave() As Boolean
'        '      mTVDesignation.EndEdit(myDT_TrainingDesignation, "ID_Designation")
'        Return MyBase.CanSave()
'    End Function


'#Region "LoadInfo"
'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        'myDT_TrainingDesignation.ClearThenFill("ID_Training=" & pID.ToString)
'        MyBase.LoadInfo(pID)
'        'mTVDesignation.CheckNodes(myDT_TrainingDesignation, Database.Tables.tUserGroupDesignation.Field.ID_Designation.ToString)
'    End Sub

'#End Region

'    '#Region "SetDefaultValues"
'    '    Protected Overrides Sub SetDefaultValues()
'    '        Dim vID As Integer
'    '        vID = CInt(myDT.Get(Database.Tables.tTraining.Field.ID))
'    '        myDT_TrainingDesignation.Columns(Database.Tables.tTrainingDesignation.Field.ID_Training).DefaultValue = vID
'    '    End Sub

'    '#End Region



'#Region "Overrides"
'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, Database.Tables.tTraining)
'        End Set
'    End Property



'#End Region

'    Private Sub LoadParticipants(ByVal sender As Object, ByVal e As EventArgs)
'        If MsgBox("Load Participants?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'            GSCOM.SQL.ExecuteNonQuery("EXEC pTrainingParticipants " & myDT.Get(Database.Tables.tTraining.Field.ID).ToString, Connection)
'            ' Me.mLoadButton.Enabled = False

'        End If
'    End Sub



'End Class
