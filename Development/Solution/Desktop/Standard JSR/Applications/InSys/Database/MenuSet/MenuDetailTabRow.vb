Option Explicit On
Option Strict On

Public Class MenuDetailTabRow

    Private dr As DataRow
    Public Sub New()

    End Sub
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
            Return CInt(dr(Tables.tMenuDetailTab.Field.ID.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_DetailMenu() As Object
        Get
            Return dr(Tables.tMenuDetailTab.Field.ID_DetailMenu.ToString)
        End Get
    End Property
    Public ReadOnly Property DetailTabFilter() As Object
        Get
            Return dr(Tables.tMenuDetailTab.Field.DetailTabFilter.ToString)
        End Get
    End Property
    Public ReadOnly Property AllowDuplicateList() As Object
        Get
            Return dr(Tables.tMenuDetailTab.Field.AllowDuplicateList.ToString)
        End Get
    End Property
    Public ReadOnly Property ID_ListMenu() As Object
        Get
            Return dr(Tables.tMenuDetailTab.Field.ID_ListMenu.ToString)
        End Get
    End Property
    Public ReadOnly Property Label() As Object
        Get
            Return dr(Tables.tMenuDetailTab.Field.Label.ToString)
        End Get
    End Property

    Public ReadOnly Property ID_Menu() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTab.Field.ID_Menu.ToString))
        End Get
    End Property


    Public ReadOnly Property Name() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.Name.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ReportFile() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ReportFile.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ParentColumn() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ParentColumn.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ParentTableName() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ParentTableName.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property TableName() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.TableName.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ListSource() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ListSource.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property ListMenuDetailSource() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ListMenuDetailSource.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property ListMenuFixedFilter() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ListMenuFixedFilter.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ChildColumn() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ChildColumn.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property Code() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.Code.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property Sort() As String
        Get
            Return dr(Tables.tMenuDetailTabField.Field.Sort.ToString).ToString
        End Get
    End Property


    Public ReadOnly Property DataSource() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.DataSource.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property SeqNo() As Integer
        Get
            Return CInt(dr(Tables.tMenuDetailTab.Field.SeqNo.ToString))
        End Get
    End Property

    Public ReadOnly Property IsActive() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTab.Field.IsActive.ToString))
        End Get
    End Property

    Public ReadOnly Property Comment() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.Comment.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ImageFile() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.ImageFile.ToString).ToString
        End Get
    End Property


    Public ReadOnly Property CheckBoxes() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTab.Field.CheckBoxes.ToString))
        End Get

    End Property

    Public ReadOnly Property Sortable() As Boolean
        Get
            Return CBool(dr(Tables.tMenuDetailTab.Field.Sortable.ToString))
        End Get

    End Property

    Public ReadOnly Property Description() As String
        Get
            Return dr(Tables.tMenuDetailTab.Field.Description.ToString).ToString
        End Get
    End Property

    Public Sub New(ByVal pdr As DataRow)
        dr = pdr
    End Sub

    Public ReadOnly Property ID_MenuDetailTabType() As MenuDetailTabTypeEnum
        Get
            Return CType(dr(Tables.tMenuDetailTab.Field.ID_MenuDetailTabType.ToString), MenuDetailTabTypeEnum)
        End Get
    End Property

    'EMIL 20130212
    Public ReadOnly Property AllowNewRow() As Boolean
        Get
            If TypeOf dr(Tables.tMenuDetailTab.Field.AllowNewRow.ToString) Is Boolean Then
                Return CBool(CObj(dr(Tables.tMenuDetailTab.Field.AllowNewRow.ToString)))
            Else
                Return False
            End If
        End Get
    End Property
    'EMIL 20130213
    Public ReadOnly Property AllowDeleteRow() As Boolean
        Get
            If TypeOf dr(Tables.tMenuDetailTab.Field.AllowDeleteRow.ToString) Is Boolean Then
                Return CBool(CObj(dr(Tables.tMenuDetailTab.Field.AllowDeleteRow.ToString)))
            Else
                Return False
            End If
        End Get
    End Property

End Class
