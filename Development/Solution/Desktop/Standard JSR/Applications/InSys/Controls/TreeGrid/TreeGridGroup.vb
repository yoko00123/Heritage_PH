Friend Class TreeGridGroup
    Public Name As String
    Public Text As String
    Public ImageKey As String
    Public Sort As String

    Private mIndex As Integer

    Public Property Index() As Integer
        Get
            Return mIndex
        End Get
        Set(ByVal value As Integer)
            mIndex = value
        End Set
    End Property


End Class
