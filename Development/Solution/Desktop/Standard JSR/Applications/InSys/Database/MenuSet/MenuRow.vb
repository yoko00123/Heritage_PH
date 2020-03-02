Option Explicit On
Option Strict On

Public Class MenuRow

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
            Return CInt(dr(Tables.tMenu.Field.ID.ToString))
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return dr(Tables.tMenu.Field.Name.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Code() As String
        Get
            Return dr(Tables.tMenu.Field.Code.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property Sort() As String
        Get
            Return dr(Tables.tMenu.Field.Sort.ToString).ToString
        End Get
    End Property

    Public Property DataSource() As String
        Get
            Return dr(Tables.tMenu.Field.DataSource.ToString).ToString
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.DataSource.ToString) = value
        End Set
    End Property

    Public Property ListSource() As String
        Get
            Return dr("ListSource").ToString
        End Get
        Set(ByVal value As String)
            dr("ListSource") = value
        End Set
    End Property

#Region "ListMenu"
    Public Property ListMenu() As String
        Get
            Return dr("ListMenu").ToString
        End Get
        Set(ByVal value As String)
            dr("ListMenu") = value
        End Set
    End Property
    Public Property SaveTrigger() As String
        Get
            Return dr(Tables.tMenu.Field.SaveTrigger.ToString).ToString
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.SaveTrigger.ToString) = value
        End Set
    End Property
    Public Property ListFixedFilter() As String
        Get
            Return dr(Tables.tMenu.Field.ListFixedFilter.ToString).ToString
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.ListFixedFilter.ToString) = value
        End Set
    End Property

    Public Property ID_ListMenu() As Object
        Get
            Return dr(Tables.tMenu.Field.ID_ListMenu.ToString)
        End Get
        Set(ByVal value As Object)
            dr(Tables.tMenu.Field.ID_ListMenu.ToString) = value
        End Set
    End Property
#End Region


#Region "ListRowField"
    Public Property ListRowField() As String
        Get
            Return dr(Tables.tMenu.Field.ListRowField.ToString).ToString
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.ListRowField.ToString) = value
        End Set
    End Property

    'Public Property ID_ListRowField() As Object
    '    Get
    '        Return dr(Tables.tMenu.Field.ID_ListRowField.ToString)
    '    End Get
    '    Set(ByVal value As Object)
    '        dr(Tables.tMenu.Field.ID_ListRowField.ToString) = value
    '    End Set
    'End Property
#End Region

    Public ReadOnly Property BaseDataSource() As String
        Get
            Return dr(Tables.tMenu.Field.BaseDataSource.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property ID_Menu() As Object
        Get
            Return dr(Tables.tMenu.Field.ID_Menu.ToString)
        End Get
    End Property

    Public ReadOnly Property SeqNo() As Integer
        Get
            Return CInt(dr(Tables.tMenu.Field.SeqNo.ToString))
        End Get
    End Property
    Public ReadOnly Property ImageFile() As String
        Get
            Return dr(Tables.tMenu.Field.ImageFile.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property ReportFile() As String
        Get
            Return dr(Tables.tMenu.Field.ReportFile.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property IsActive() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.IsActive.ToString))
        End Get
    End Property

    Public ReadOnly Property IsVisible() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.IsVisible.ToString))
        End Get
    End Property
    Public ReadOnly Property Comment() As String
        Get
            Return dr(Tables.tMenu.Field.Comment.ToString).ToString
        End Get
    End Property
  

    Public ReadOnly Property ID_MenuType() As Integer
        Get
            Return CInt(dr(Tables.tMenu.Field.ID_MenuType.ToString))
        End Get
    End Property
    Public ReadOnly Property AllowNew() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.AllowNew.ToString))
        End Get

    End Property

    Public ReadOnly Property IsSpanView() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.IsSpanView.ToString))
        End Get

    End Property

    Public ReadOnly Property TableName() As Object
        Get
            Return dr(Tables.tMenu.Field.TableName.ToString).ToString
        End Get
    End Property
    Public ReadOnly Property AllowDelete() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.AllowDelete.ToString))
        End Get
    End Property

    Public ReadOnly Property AllowOpen() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.AllowOpen.ToString))
        End Get
    End Property
    Public ReadOnly Property ReportTitle() As String
        Get
            Return dr(Tables.tMenu.Field.ReportTitle).ToString
        End Get
    End Property

    Public ReadOnly Property ReportSubTitle() As String
        Get
            Return dr(Tables.tMenu.Field.ReportSubTitle).ToString
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return dr(Tables.tMenu.Field.Description.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property [ReadOnly]() As Boolean
        Get
            Return CBool(dr(Tables.tMenu.Field.ReadOnly.ToString))
        End Get
    End Property

    Public Property ColorRGB() As String
        Get
            If IsDBNull(dr(Tables.tMenu.Field.ColorRGB.ToString)) Then
                Return "c0c0c0"
            Else
                Return dr(Tables.tMenu.Field.ColorRGB.ToString).ToString
            End If
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.ColorRGB.ToString) = value
        End Set
    End Property

    Public Property DarkColorRGB() As String
        Get
            Return dr(Tables.tMenu.Field.DarkColorRGB.ToString).ToString
        End Get
        Set(ByVal value As String)
            dr(Tables.tMenu.Field.DarkColorRGB.ToString) = value
        End Set
    End Property

    'Public Property Color() As String
    '    Get
    '        Return dr("Color").ToString
    '    End Get
    '    Set(ByVal value As String)
    '        dr("Color") = value
    '    End Set
    'End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal pdr As DataRow)
        dr = pdr
    End Sub
End Class
