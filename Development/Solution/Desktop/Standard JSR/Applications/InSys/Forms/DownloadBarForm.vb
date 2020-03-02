Public Class DownloadBarForm

    Public IsCancelled As Boolean = False
    Public IsFinish As Boolean = False

    Private Sub DownloadBarForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
    End Sub

    Public Delegate Sub dgMainStatus(msg As String)
    Public Sub MainStatus(msg As String)
        If Me.InvokeRequired Then
            Me.Invoke(New dgMainStatus(AddressOf MainStatus), msg)
        Else
            Me.Text = msg
        End If
    End Sub

    Public Delegate Sub dgSubStatus(msg As String, progress As Int32)
    Public Sub SubStatus(msg As String, progress As Int32)
        If Me.InvokeRequired Then
            Me.Invoke(New dgSubStatus(AddressOf SubStatus), msg, progress)
        Else
            Me.lblstatus.Text = msg
            Me.pbar.Value = Convert.ToInt32(IIf(progress > Me.pbar.Maximum, Me.pbar.Maximum, progress))
            Me.txtdetail.AppendText(msg & vbCrLf)
            Me.txtdetail.ScrollToCaret()
        End If
    End Sub

    Public Delegate Sub dgSetMaxProgress(max As Int32)
    Public Sub SetMaxProgress(max As Int32)
        If Me.pbar.InvokeRequired Then
            Me.pbar.Invoke(New dgSetMaxProgress(AddressOf SetMaxProgress), max)
        Else
            Me.pbar.Value = 0
            Me.pbar.Maximum = max
        End If
    End Sub

    Public Delegate Sub dgFinish(isfinish As Boolean)
    Public Sub Finish(isfinish As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(New dgFinish(AddressOf Finish), isfinish)
        Else
            Me.IsFinish = isfinish
            Me.Close()
            GC.Collect()
        End If
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
        If Me.IsFinish = False Then
            Me.TopMost = False
            If MsgBox("Are you sure you want to cancel?", MsgBoxStyle.YesNo, Application.ProductName) = MsgBoxResult.Yes Then
                e.Cancel = False
                'Throw New Exception("Process has been cancelled.")
                IsCancelled = True
            Else
                e.Cancel = True
                Me.TopMost = True
            End If
            MyBase.OnClosing(e)
        End If
    End Sub

    Private Sub btnShowDetails_Click(sender As Object, e As EventArgs) Handles btnShowDetails.Click
        If Convert.ToInt32(btnShowDetails.Tag) = 1 Then
            btnShowDetails.Tag = 0
            btnShowDetails.Text = "&Hide Details"
            Me.Size = New Size(476, 323)
            Me.txtdetail.Visible = True
        Else
            btnShowDetails.Tag = 1
            btnShowDetails.Text = "&Show Details"
            Me.Size = New Size(476, 101)
            Me.txtdetail.Visible = False
        End If
    End Sub

End Class