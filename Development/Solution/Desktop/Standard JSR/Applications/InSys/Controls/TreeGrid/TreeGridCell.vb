Friend Class TreeGridCell

    Public Column As TreeGridColumn
    Public Node As TreeGridNode
    Private mReadOnly As Boolean
    Friend mSelected As Boolean

    Private ReadOnly Property Tree() As DataTreeView
        Get
            Return DirectCast(Node.TreeView, DataTreeView)
        End Get
    End Property

    Public ReadOnly Property Bounds() As Rectangle
        Get
            If Node.IsLastSibling Then
                Return New Rectangle(Node.Bounds.Left + Column.Left, Node.Bounds.Top, Column.Width, Node.Bounds.Height - 1)
            Else
                Return New Rectangle(Node.Bounds.Left + Column.Left, Node.Bounds.Top, Column.Width, Node.Bounds.Height)

            End If
        End Get
    End Property

    Public ReadOnly Property Width() As Integer
        Get
            Return Column.Width
        End Get
    End Property

    Public ReadOnly Property Selected() As Boolean
        Get
            Return mSelected
        End Get
    End Property


    Public ReadOnly Property ColumnIndex() As Integer
        Get
            Return Column.Index
        End Get
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return mReadOnly
        End Get
        Set(ByVal value As Boolean)
            mReadOnly = value
        End Set
    End Property

    Public Property Value() As Object
        Get
            If Node IsNot Nothing AndAlso Node.Row IsNot Nothing Then
                Select Case Node.Row.RowState
                    Case DataRowState.Added, DataRowState.Modified, DataRowState.Unchanged
                        Return Node.Row(Column.Name)
                    Case Else
                        Return Nothing
                End Select
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Object)
            Node.Row(Column.Name) = value
        End Set
    End Property

    Public Sub Draw(ByVal g As Graphics, ByVal P As Pen, ByVal pBrush As Brush)
        Dim b As Brush
        b = CType(pBrush.Clone, Brush)

        Dim dx As Integer
        g.DrawRectangle(P, Me.Bounds) 'draw the outline

        'If Me.Column.Index = 0 Or Me.Node IsNot Me.Node.TreeView.SelectedNode Then
        '    g.FillRectangle(New SolidBrush(Me.Column.BackColor), New Rectangle(Me.Bounds.X + 1, Me.Bounds.Y + 1, Me.Bounds.Width - 1, Me.Bounds.Height - 1)) 'fill rect with color
        '    b = New SolidBrush(Color.FromKnownColor(KnownColor.WindowText))

        'End If



        If Me.Value IsNot Nothing Then
            Select Case Column.TextAlign
                Case HorizontalAlignment.Left
                    dx = Me.Bounds.Left
                Case HorizontalAlignment.Center
                    dx = Me.Bounds.Left + (Me.Bounds.Width \ 2)
                Case HorizontalAlignment.Right
                    dx = Me.Bounds.Right '- 1
            End Select
            g.DrawString(FormattedValue, Tree.Font, b, dx, Me.Bounds.Top + 2, Column.mStringFormat)  'draw the text
        End If
        b.Dispose()
    End Sub

    Private ReadOnly Property FormattedValue() As String
        Get
            If IsNumeric(Me.Value) Then
                If CDec(Me.Value) = 0 Then
                    Return "-"
                End If
            End If
            Return Me.Value.ToString
        End Get
    End Property


    'Private mEditBox As TextBox
    Private mEditing As Boolean

    Public ReadOnly Property Editing() As Boolean
        Get
            Return mEditing
        End Get
    End Property

    Public Sub BeginEdit(Optional ByVal key As String = "")
        'If Not mEditing Then
        mEditing = True
        With Me.Column.EdittingControl
            .Bounds = New Rectangle(Me.Bounds.Left + 1, Me.Bounds.Top + 2, Me.Bounds.Width - 2, Me.Bounds.Height)
            Me.Column.EdittingControl.Show()
            If key <> "" Then
                .Text = key
                .SelectionLength = 0
                .SelectionStart = .Text.Length
            Else
                .Text = Me.Value.ToString
                '.SelectionLength = 0
                '.SelectionStart = .Text.Length
            End If
            .Focus()
        End With
        'AddHandler mEditBox.KeyDown, AddressOf mEditBox_KeyDown
        'End If
    End Sub

    'Public Sub EndEdit()
    '    'RemoveHandler mEditBox.KeyDown, AddressOf mEditBox_KeyDown
    '    'Me.Column.EdittingControl.Hide()
    '    Me.Column.EdittingControl.DataBindings("Text").WriteValue()
    '    Me.Column.EdittingControl.Hide()
    '    Tree.Focus()
    '    If Tree.NextDataboundNode IsNot Nothing Then
    '        Tree.SelectedNode = Tree.NextDataboundNode
    '    End If
    '    mEditing = False

    'End Sub

    'Private Sub mEditBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        Me.EndEdit()
    '    End If
    'End Sub

End Class
