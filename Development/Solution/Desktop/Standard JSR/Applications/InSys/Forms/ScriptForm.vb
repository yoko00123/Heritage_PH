Public Class ScriptForm

    Private WithEvents a As SqlClient.SqlConnection

  
    Private Sub RunButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunButton.Click
        Try
            'If MsgBox("Are you sure you want to run the script?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            InfoMessageBox.Text = ""
            a = gConnection()
            MainGrid.DataSource = GSCOM.SQL.TableQuery(ScriptBox.Text, a)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

   
    Private Sub ScriptForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.F5
                RunButton.PerformClick()
        End Select
    End Sub

    'Dim WithEvents asd As SqlClient.SqlDataAdapter
    'Private Sub a_InfoMessage(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlInfoMessageEventArgs) Handles a.InfoMessage
    '    InfoMessageBox.Text &= e.Message.PadRight(2, CChar(vbCrLf))


    'End Sub



    Private Sub a_StateChange(ByVal sender As Object, ByVal e As System.Data.StateChangeEventArgs) Handles a.StateChange
        InfoMessageBox.Text &= e.CurrentState.ToString.PadRight(2, CChar(vbCrLf))
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Icon = gIcon
        ' Add any initialization after the InitializeComponent() call.
        MainGrid.MultiSelect = True
    End Sub
End Class
