

Public Class PrintPreviewForm
    Public Property Title As String
        Get
            Return TitleBox.Text
        End Get
        Set(ByVal value As String)
            TitleBox.Text = value
        End Set
    End Property

    Public Property SubTitle As String
        Get
            Return SubTitleBox.Text
        End Get
        Set(ByVal value As String)
            SubTitleBox.Text = value
        End Set
    End Property

  
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class