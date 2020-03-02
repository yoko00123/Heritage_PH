Option Explicit On
Option Strict On

Friend Class TreeGridNode
    Inherits TreeNode

    Public Cells As New TreeGridCellCollection

    Private mRow As DataRow
    Public Property Row() As DataRow
        Get
            Return mRow
        End Get
        Set(ByVal value As DataRow)
            mRow = value
        End Set
    End Property

    'Public Width As Integer = 64
    'Friend mStringFormat As New StringFormat
    'Private mTextAlign As HorizontalAlignment

    'Friend mIndex As Integer

    'Public ReadOnly Property Index() As Integer
    '    Get
    '        Return mIndex
    '    End Get
    'End Property

    Public ReadOnly Property Databound() As Boolean
        Get
            Return (mrow IsNot Nothing)
        End Get
    End Property

    Public ReadOnly Property IsLastSibling() As Boolean
        Get
            Return Me.Parent IsNot Nothing AndAlso Me Is Me.Parent.LastNode
        End Get
    End Property

End Class
