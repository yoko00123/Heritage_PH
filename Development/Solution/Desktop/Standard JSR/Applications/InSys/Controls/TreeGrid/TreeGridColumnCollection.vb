Friend Class TreeGridColumnCollection
    Inherits Collections.Generic.List(Of TreeGridColumn)
    'Private mCollection As New Collection
    Public Tree As DataTreeView
    Private mCurrentLeft As Integer


    Public Overloads Function Add(ByVal pName As String, ByVal pWidth As Integer, ByVal pTextAlign As HorizontalAlignment) As TreeGridColumn
        Dim c As New TreeGridColumn(pName, Tree)
        c.TextAlign = pTextAlign
        c.Left = mCurrentLeft
        c.Width = pWidth

        mCurrentLeft += pWidth
        Me.Add(c)
        c.mIndex = Me.Count - 1

        If c.mIndex = 0 Then
            c.BackColor = GSCOM.Common.DefaultRequiredFieldBackColor
        Else
            c.BackColor = GSCOM.Common.DefaultReadOnlyFieldBackColor
        End If
        c.BackColor = Tree.BackColor
        Return c
    End Function

    Public Overloads ReadOnly Property Item(ByVal key As String) As TreeGridColumn
        Get
            For Each c As TreeGridColumn In Me
                If c.Name = key Then Return c
            Next
            Return Nothing
        End Get

    End Property

    Public Sub New(ByVal pTree As DataTreeView)
        Me.Tree = pTree
        mCurrentLeft = Tree.TextWidth
    End Sub
End Class
