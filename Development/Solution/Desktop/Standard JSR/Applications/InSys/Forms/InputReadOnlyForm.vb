Public Class InputReadOnlyForm

    Public Sub New(msg As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TextBox1.Text = msg
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clipboard.SetText(Me.TextBox1.Text)
    End Sub

    Public Shared Sub ShowMsg(msg As String)
        Using m As New InputReadOnlyForm(msg)
            m.ShowDialog()
        End Using
    End Sub

End Class