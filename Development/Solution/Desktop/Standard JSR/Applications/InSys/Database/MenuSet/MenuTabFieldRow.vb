Option Explicit On
Option Strict On

Public Class MenuTabFieldRow

    Private dr As DataRow

    Public Property InnerRow() As DataRow
        Get
            Return dr
        End Get
        Set(ByVal value As DataRow)
            dr = value
        End Set
    End Property
    Public ReadOnly Property ID() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.ID.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_MenuTab() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.ID_MenuTab.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_SystemControlType() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.ID_SystemControlType.ToString))
        End Get
    End Property

    Public ReadOnly Property Panel() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.Panel.ToString))
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Name.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Sort() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Sort.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property StringFormat() As String
        Get
            Return dr(Tables.tMenuTabField.Field.StringFormat.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Height() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.Height.ToString))
        End Get
    End Property

    Public ReadOnly Property Label() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Label.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property Header() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Header.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property EffectiveLabel() As String
        Get
            Return dr("EffectiveLabel").ToString
        End Get
    End Property
    Public ReadOnly Property ID_Menu() As Object
        Get
            Return dr(Tables.tMenuTabField.Field.ID_Menu.ToString)
        End Get
    End Property

    Public ReadOnly Property SeqNo() As Integer
        Get
            Return CInt(dr(Tables.tMenuTabField.Field.SeqNo.ToString))
        End Get
    End Property

    Public ReadOnly Property IsActive() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.IsActive.ToString))
        End Get
    End Property

    Public ReadOnly Property ShowInBrowser() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.ShowInBrowser.ToString))
        End Get
    End Property
    Public ReadOnly Property Comment() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Comment.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property Expression() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Expression.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ShowInInfo() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.ShowInInfo.ToString))
        End Get

    End Property
  
    Public ReadOnly Property ShowInList() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.ShowInList.ToString))
        End Get
    End Property



    Public ReadOnly Property Description() As String
        Get
            Return dr(Tables.tMenuTabField.Field.Description.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property [ReadOnly]() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.ReadOnly.ToString))
        End Get
    End Property

    Public ReadOnly Property [TableName]() As String
        Get
            Return dr("TableName").ToString
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal pdr As DataRow)
        dr = pdr
    End Sub

    Public ReadOnly Property IsForeignKey() As Boolean
        Get
            Return (Strings.Left(Me.Name, 3) = "ID_")
        End Get
    End Property

    Public ReadOnly Property IsComboBox() As Boolean
        Get
            Return Me.IsForeignKey And IsDBNull(Me.ID_Menu)
        End Get
    End Property

    Public ReadOnly Property ParentLookUp() As String
        Get
            Return dr(Tables.tMenuTabField.Field.ParentLookUp.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ParentLookUpChildColumn() As String
        Get
            Return dr(Tables.tMenuTabField.Field.ParentLookUpChildColumn.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property [ID_SystemAggregateFunction]() As String
        Get
            Return dr(Tables.tMenuTabField.Field.ID_SystemAggregateFunction.ToString).ToString
        End Get
    End Property


    Public ReadOnly Property [SystemAggregateFunction]() As String
        Get
            Return dr("SystemAggregateFunction").ToString
        End Get
    End Property

    Public ReadOnly Property [DefaultValue] As String
        Get
            Return dr(Tables.tMenuTabField.Field.DefaultValue.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property IsRequired As Boolean
        Get
            Return CBool(dr(Tables.tMenuTabField.Field.IsRequired.ToString))
        End Get
    End Property

    Public ReadOnly Property FixedFilter As String
        Get
            Return dr(Tables.tMenuTabField.Field.FixedFilter.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ListColumn As String
        Get
            Return dr(Tables.tMenuTabField.Field.ListColumn.ToString).ToString
        End Get
    End Property
End Class
