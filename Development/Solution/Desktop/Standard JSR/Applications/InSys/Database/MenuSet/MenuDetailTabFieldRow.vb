Option Explicit On
Option Strict On

Public Class MenuDetailTabFieldRow

    Private dr As DataRow

    Public Property InnerRow() As DataRow
        Get
            Return dr
        End Get
        Set(ByVal value As DataRow)
            dr = value
        End Set
    End Property
    Public ReadOnly Property ParentLookUpListColumn() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ParentLookUpListColumn.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property ID() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTabField.Field.ID.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_MenuDetailTab() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTabField.Field.ID_MenuDetailTab.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_SystemControlType() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTabField.Field.ID_SystemControlType.ToString))
        End Get
    End Property

    Public ReadOnly Property Width() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTabField.Field.Width.ToString))
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Name.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Label() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Label.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property Header() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Header.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property EffectiveLabel() As String
        Get
            Return dr("EffectiveLabel").ToString
        End Get
    End Property

    Public ReadOnly Property ListColumn() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ListColumn.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ListText() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ListText.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ID_Menu() As Object
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ID_Menu.ToString)
        End Get
    End Property

    Public ReadOnly Property SeqNo() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTabField.Field.SeqNo.ToString))
        End Get
    End Property

    Public ReadOnly Property IsActive() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.IsActive.ToString))
        End Get
    End Property

    Public ReadOnly Property IsColumn() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.IsColumn.ToString))
        End Get
    End Property

    Public ReadOnly Property IsGroup() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.IsGroup.ToString))
        End Get
    End Property
    Public ReadOnly Property CopyFromList() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.CopyFromList.ToString))
        End Get
    End Property
    Public ReadOnly Property ShowInBrowser() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.ShowInBrowser.ToString))
        End Get
    End Property
    Public ReadOnly Property ShowInInfo() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.ShowInInfo.ToString))
        End Get
    End Property
    Public ReadOnly Property Comment() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Comment.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Sort() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Sort.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Text() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Text.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Formula() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Formula.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Description.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property [ReadOnly]() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.ReadOnly.ToString))
        End Get
    End Property

    Public Sub New()

    End Sub
    Public ReadOnly Property ImageFile() As String
        Get
            Return dr(Tables.tMenu.Field.ImageFile.ToString).ToString
        End Get
    End Property

    Public Sub New(ByVal pdr As DataRow)
        dr = pdr
    End Sub

    Public ReadOnly Property Expression() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Expression.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ParentLookUp() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ParentLookUp.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ParentLookUpChildColumn() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.ParentLookUpChildColumn.ToString).ToString
        End Get
    End Property

    ''''''''''''''''''

    Public ReadOnly Property [DefaultValue] As Object
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Defaultvalue.ToString)
        End Get
    End Property

    Public ReadOnly Property IsRequired As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.IsRequired.ToString))
        End Get
    End Property

    Public ReadOnly Property IsFrozen As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTabField.Field.IsFrozen.ToString))
        End Get
    End Property

    Public ReadOnly Property FixedFilter As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.FixedFilter.ToString).ToString
        End Get
    End Property
End Class


