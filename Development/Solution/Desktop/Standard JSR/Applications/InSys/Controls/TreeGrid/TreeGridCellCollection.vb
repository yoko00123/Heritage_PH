Friend Class TreeGridCellCollection
    Inherits Collections.Generic.List(Of TreeGridCell)
    'Private mCollection As New Collection

    'Public Overloads Function Add(ByVal pName As String, ByVal pWidth As Integer, ByVal pTextAlign As HorizontalAlignment) As TreeGridCell
    '    Dim c As New TreeGridCell
    '    c.Name = pName
    '    c.Width = pWidth
    '    'c.BackColor = pBackColor
    '    c.TextAlign = pTextAlign
    '    Me.Add(c)
    '    c.mIndex = Me.Count - 1
    '    Return c
    'End Function

    'Public Overloads ReadOnly Property Item(ByVal key As String) As TreeGridCell
    '    Get
    '        For Each c As TreeGridCell In Me
    '            If c.Name = key Then Return c
    '        Next
    '        Return Nothing
    '    End Get

    'End Property

End Class
