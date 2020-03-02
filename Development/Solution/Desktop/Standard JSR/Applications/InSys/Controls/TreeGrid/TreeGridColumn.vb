Option Explicit On
Option Strict On

Friend Class TreeGridColumn
    Public Name As String
    Public Width As Integer = 64
    Friend mStringFormat As New StringFormat
    Friend mIndex As Integer
    Public Tree As DataTreeView
    Private mLeft As Integer
    Private mRight As Integer


    Public Property BackColor() As Color
        Get
            Return Me.EdittingControl.BackColor
        End Get
        Set(ByVal value As Color)
            Me.EdittingControl.BackColor = value
        End Set
    End Property


    Public WithEvents EdittingControl As TextBox


    Public Property Left() As Integer
        Get
            Return mLeft
        End Get
        Set(ByVal value As Integer)
            mLeft = value
        End Set
    End Property


    Public ReadOnly Property Right() As Integer
        Get
            Return Left + Width
        End Get
    End Property

    Public ReadOnly Property Index() As Integer
        Get
            Return mIndex
        End Get
    End Property

    Public Property TextAlign() As HorizontalAlignment
        Get
            Return EdittingControl.TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            EdittingControl.TextAlign = value
            Select Case value
                Case HorizontalAlignment.Center
                    mStringFormat.Alignment = StringAlignment.Center
                Case HorizontalAlignment.Left
                    mStringFormat.Alignment = StringAlignment.Near
                Case HorizontalAlignment.Right
                    mStringFormat.Alignment = StringAlignment.Far
            End Select
        End Set
    End Property

    Public Sub New(ByVal pName As String, ByVal pTree As DataTreeView)
        Me.Tree = pTree
        EdittingControl = New TextBox
        Me.Name = pName
        Me.BackColor = Me.Tree.BackColor
        With EdittingControl
            .Hide()
            .BorderStyle = Windows.Forms.BorderStyle.None
        End With
        'EdittingControl.DataBindings.Add("Text", Tree.DataSource, Name)
        Tree.Controls.Add(EdittingControl)
    End Sub

    Private Sub EdittingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles EdittingControl.KeyDown
        Dim t As TextBox
        t = CType(sender, TextBox)
        If e.KeyCode = Keys.Enter Then
            't.DataBindings("Text").WriteValue()



            t.Hide()
            Me.Tree.Focus()
            'If Me.Tree.NextDataboundNode IsNot Nothing Then
            '    Me.Tree.SelectedNode = Me.Tree.NextDataboundNode
            'End If
        End If
    End Sub

    Public ReadOnly Property DataType() As Type
        Get
            Return Me.Tree.DataSource.Columns(Me.Name).DataType
        End Get
    End Property

    Private Sub EdittingControl_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles EdittingControl.Validated
        Dim t As TextBox
        Dim o As Object
        t = CType(sender, TextBox)

        Try
            If Me.DataType Is GetType(Int32) Or Me.DataType Is GetType(Decimal) Then
                o = CDec(t.Text)
            Else
                o = t.Text
            End If
            Me.Tree.CurrentCell.Value = o
        Catch ex As Exception

        End Try
        t.Hide()
    End Sub
End Class
