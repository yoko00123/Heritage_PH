Friend Class TreeGridGroupCollection
    Inherits Collections.Generic.List(Of TreeGridGroup)

    Private mIndex As Integer

    Public Overloads Function Add(ByVal pName As String, ByVal pText As String, ByVal pSort As String, ByVal pImageKey As String) As TreeGridGroup
        Dim c As New TreeGridGroup
        c.Name = pName
        c.Text = pText
        c.Sort = pSort
        c.ImageKey = pImageKey
        c.Index = mIndex
        Me.Add(c)
        mIndex += 1
        Return c
    End Function

    Public Overloads ReadOnly Property Item(ByVal key As String) As TreeGridGroup
        Get
            For Each c As TreeGridGroup In Me
                If c.Text = key Then Return c
            Next
            Return Nothing
        End Get

    End Property

End Class
