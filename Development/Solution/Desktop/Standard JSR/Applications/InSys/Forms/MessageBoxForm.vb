Imports System.Windows.Forms

Imports nDB = GSCOM.Applications.InSys.Database



Public Class MessageBoxForm
    Friend isPaused As Boolean

    Friend Enum MgsResult
        All
        Specific
        [Resume]
        [Cancel]
    End Enum

    Friend Result As MgsResult = MgsResult.Cancel

    Private Sub MessageBoxForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If isPaused = False Then
        '    lnkResume.Visible = False
        '    lnkCancel.Location = lnkResume.Location
        'End If
        'PictureBox1.Image = nDB.ImageList.Images("Person.A.png")
    End Sub

    Private Sub LinkAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAll.LinkClicked
        'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Result = MgsResult.All
        Me.Close()
    End Sub

    Private Sub lnkSpecific_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSpecific.LinkClicked
        'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Result = MgsResult.Specific
        Me.Close()
    End Sub

    Private Sub lnkResume_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkResume.LinkClicked
        'Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Result = MgsResult.Resume
        Me.Close()
    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        'Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Result = MgsResult.Cancel
        Me.Close()
    End Sub
End Class
