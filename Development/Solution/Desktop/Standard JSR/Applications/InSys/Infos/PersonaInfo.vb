Option Explicit On
Option Strict On



Friend Class PersonaInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tPersona(Connection)
    'Private mtAddress As New Database.Tables.tAddress(Connection)
    'Private mtPersonaEmploymentRequirement As New Database.Tables.tPersonaEmploymentRequirement(Connection)
    Private mControl As New InSys.DataControl
    Private WithEvents _SSSNo As MaskedTextBox
    'Private mEmploymentRequirementTable As DataTable = nDB.GetLookUp("(SELECT TOP 100 PERCENT ID, Name, CASE WHEN IsRequired = 1 THEN 'A. Required' ELSE 'B. Optional' END Necessity  FROM " & nDB.GetMenuDataSourceValue(Database.Menu.Maintenance_HumanResource_EmploymentRequirement) & " ORDER BY SeqNo) A")
    'Private mTVEmploymentRequirement As New GSCOM.UI.DataSelector.DataSelector

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtAddress)
            '.Add(mtPersonaEmploymentRequirement)
        End With
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tPersona.Field.ID)
        'cdc = mtAddress.Columns(Database.Tables.tAddress.Field.ID_Owner)
        'rel = mDataset.Relations.Add(pdc, cdc) 'address 
        'cdc = mtPersonaEmploymentRequirement.Columns(Database.Tables.tPersonaEmploymentRequirement.Field.ID_Persona)
        'rel = mDataset.Relations.Add(pdc, cdc) 'employment requirement
        ' mtAddress.Columns(Database.Tables.tAddress.Field.ID_Table).DefaultValue = 1 'CONSTANT PERSONA
        'For Each dr As DataRow In mEmploymentRequirementTable.Select("ID IS NULL")
        '    dr.Delete() 'this is null value added by GetLookUp Function
        'Next
        'With mTVEmploymentRequirement
        '    .ImageList = gImageList
        '    .ImageKey = nDB.GetMenuValue(Database.Menu.Maintenance_HumanResource_EmploymentRequirement, Database.Tables.tMenu.Field.ImageFile).ToString
        '    .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
        '    .GroupCount = 1
        '    .DataSource = mEmploymentRequirementTable
        '    .Go()
        'End With
        'Me.AddControl(mTVEmploymentRequirement, "Employment Requirement")
        'Me.NoGridTables = mtPersonaEmploymentRequirement.TableName
        myDT.Columns(Database.Tables.tPersona.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        InitControl(pMenu)
        AfterNew()
        '_SSSNo = CType(Me.GetControl("_SSSNO"), MaskedTextBox)
        '_SSSNo.Mask = "00-0000000-0"
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        'mtAddress.ClearThenFill("(ID_Owner=" & pID.ToString & ") AND (ID_Table=" & (1).ToString & ")") '1 : CONSTANT PERSONA
        'mTVEmploymentRequirement.CheckNodes(mtPersonaEmploymentRequirement, Database.Tables.tPersonaEmploymentRequirement.Field.ID_EmploymentRequirement.ToString)
    End Sub

    'Protected Overrides Function CanSave() As Boolean
    '    Dim b As Boolean = True
    '    'With mControl
    '    '    b = b And GSCOM.UI.ValidMaskedTextBox(._SSSno)
    '    '    b = b And GSCOM.UI.ValidMaskedTextBox(._HDMFNo)
    '    '    b = b And GSCOM.UI.ValidMaskedTextBox(._PhilHealthNo)
    '    'End With
    '    If b Then
    '        mTVEmploymentRequirement.EndEdit(mtPersonaEmploymentRequirement, Database.Tables.tPersonaEmploymentRequirement.Field.ID_EmploymentRequirement.ToString)
    '        Dim a As Boolean = True
    '        For Each dr As DataRow In mEmploymentRequirementTable.Select("Necessity='A. Required'")
    '            If (mtPersonaEmploymentRequirement.Select(Database.Tables.tPersonaEmploymentRequirement.Field.ID_EmploymentRequirement.ToString & "=" & GSCOM.SQL.SQLFormat(dr("ID"))).Length = 0) Then
    '                a = False
    '                Exit For
    '            End If
    '        Next
    '        myDT.Set(Database.Tables.tPersona.Field.HasCompleteEmploymentRequirements, a)
    '    End If
    '    Return b
    'End Function

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tPersona)
        End Set
    End Property



#End Region



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class