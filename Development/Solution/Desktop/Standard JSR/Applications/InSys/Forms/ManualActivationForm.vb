Public Class ManualActivationForm

    Public ac As New ManualRegistration

    Private Sub ManualActivationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IO.File.Exists(Application.StartupPath + "\Icon.ico") Then Me.Icon = Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\Icon.ico")
        PictureBox1.BackColor = Color.FromArgb(0, 119, 234)
        PictureBox2.BackColor = Color.FromArgb(0, 119, 234)
        Label3.BackColor = Color.FromArgb(255, 120, 0)
        Label3.ForeColor = Color.White
        Me.BackColor = Color.White
        btnActivate.BackColor = Color.FromArgb(255, 120, 0)
        btnActivate.ForeColor = Color.White
        txtProductKey.Text = ac.GetProductKey().ToUpper
        txtActivationKey.Text = ""
        txtActivationKey.Select()
    End Sub

    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        If ac.Compare(txtProductKey.Text, txtActivationKey.Text) Then
            MsgBox("GIRAFFE Systems has been activated." + vbNewLine + "Thank you for using GIRAFFE Systems.", CType(MsgBoxStyle.Information + vbOKOnly, MsgBoxStyle), "GIRAFFE Systems Security")
            GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = 1 where name = 'ProductRegistered'", gConnection)
            GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = '" & ac.GetMACAddress() & "' where name = 'MacAddress'", gConnection)
            GSCOM.SQL.ExecuteNonQuery("update tsetting set value  = '" + txtActivationKey.Text + "' where name = 'ActivationKey'", gConnection)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Invalid Activation Key.", CType(MsgBoxStyle.Exclamation + vbOKOnly, MsgBoxStyle), "GIRAFFE Systems Security")
            txtActivationKey.Select()
        End If
    End Sub

End Class