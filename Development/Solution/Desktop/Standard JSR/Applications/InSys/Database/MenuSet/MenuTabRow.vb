Option Explicit On
Option Strict On

Public Class MenuTabRow

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
            Return CInt(dr(Tables.tMenuTab.Field.ID.ToString))
        End Get
    End Property

    Public ReadOnly Property ID_Menu() As Integer
        Get
            Return CInt(dr(Tables.tMenuTab.Field.ID_Menu.ToString))
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return dr(Tables.tMenuTab.Field.Name.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property SeqNo() As Integer
        Get
            Return CInt(dr(Tables.tMenuTab.Field.SeqNo.ToString))
        End Get
    End Property

    Public ReadOnly Property IsActive() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTab.Field.IsActive.ToString))
        End Get
    End Property

    Public ReadOnly Property Comment() As String
        Get
            Return dr(Tables.tMenuTab.Field.Comment.ToString).ToString
        End Get
    End Property

    Public ReadOnly Property ImageFile() As String
        Get
            Return dr(Tables.tMenuTab.Field.ImageFile.ToString).ToString
        End Get
    End Property
   

    Public ReadOnly Property HasTable() As Boolean
        Get
            Return CBool(dr(Tables.tMenuTab.Field.HasTable.ToString))
        End Get

    End Property

    Public ReadOnly Property Description() As String
        Get
            Return dr(Tables.tMenuTab.Field.Description.ToString).ToString
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal pdr As DataRow)
        dr = pdr
    End Sub
End Class
