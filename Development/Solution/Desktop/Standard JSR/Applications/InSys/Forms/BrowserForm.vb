Friend Class BrowserForm

    Private Sub PrintButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintButton.Click
        Me.MainBrowser.ShowPrintPreviewDialog()
    End Sub

    Private Sub BrowserForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
    End Sub

    Private Sub BrowserForm_PrevewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown, tsMain.PreviewKeyDown, MainBrowser.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

End Class