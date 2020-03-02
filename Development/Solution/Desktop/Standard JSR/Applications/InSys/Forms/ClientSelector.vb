Public Class ClientSelector
    Private Client As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If IO.File.Exists(Application.StartupPath + "\Icon.ico") Then Me.Icon = Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\Icon.ico")
        LoadClients()
    End Sub


    Private Sub LoadClients()
        Dim Section As String = ""
        For Each line As String In IO.File.ReadLines(Application.StartupPath + "\Setting.ini")
            If Not (line.StartsWith("[") And line.EndsWith("]")) Then
                Select Case Section
                    Case "[Client]"
                        ComboBox1.Items.Add(line)
                End Select
            Else
                Section = line
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

End Class