Public Class MessageBox2

    Private defaultResult As DialogResult = Windows.Forms.DialogResult.None

    Private Sub MessageBox2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If defaultResult = Windows.Forms.DialogResult.None And chbApplyToAll.Checked = True Then
            defaultResult = Me.DialogResult
        End If
    End Sub

    Private Sub MessageBox2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If defaultResult <> Windows.Forms.DialogResult.None Then
            Me.DialogResult = defaultResult
            Me.Close()
        End If
    End Sub

    Private Sub lnkAbort_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAbort.LinkClicked
        Me.DialogResult = Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub lnkRetry_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRetry.LinkClicked
        Me.DialogResult = Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub lnkIgnore_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkIgnore.LinkClicked
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub
End Class