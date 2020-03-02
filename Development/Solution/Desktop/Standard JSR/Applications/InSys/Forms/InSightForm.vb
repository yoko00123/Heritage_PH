Public Class InSightForm

    Private Sub InSightForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ImageView.ImageList = gImageList
        Me.MenuView.DataSource = nDB.MenuSet.tMenu
        Me.MenuButtonView.DataSource = nDB.MenuSet.tMenuButton
    End Sub
End Class