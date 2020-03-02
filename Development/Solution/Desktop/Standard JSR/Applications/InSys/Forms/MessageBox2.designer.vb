<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageBox2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.chbApplyToAll = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lnkIgnore = New System.Windows.Forms.LinkLabel()
        Me.lnkRetry = New System.Windows.Forms.LinkLabel()
        Me.lnkAbort = New System.Windows.Forms.LinkLabel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(14, 94)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(50, 13)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Label1"
        '
        'chbApplyToAll
        '
        Me.chbApplyToAll.AutoSize = True
        Me.chbApplyToAll.Location = New System.Drawing.Point(17, 225)
        Me.chbApplyToAll.Name = "chbApplyToAll"
        Me.chbApplyToAll.Size = New System.Drawing.Size(77, 17)
        Me.chbApplyToAll.TabIndex = 2
        Me.chbApplyToAll.Text = "Apply to all"
        Me.chbApplyToAll.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources._report
        Me.PictureBox1.Location = New System.Drawing.Point(2, 18)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(72, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 32)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "e-Payslip"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(16, 89)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(340, 1)
        Me.Panel1.TabIndex = 8
        '
        'lnkIgnore
        '
        Me.lnkIgnore.AutoSize = True
        Me.lnkIgnore.Location = New System.Drawing.Point(17, 194)
        Me.lnkIgnore.Name = "lnkIgnore"
        Me.lnkIgnore.Size = New System.Drawing.Size(37, 13)
        Me.lnkIgnore.TabIndex = 19
        Me.lnkIgnore.TabStop = True
        Me.lnkIgnore.Text = "Ignore"
        '
        'lnkRetry
        '
        Me.lnkRetry.AutoSize = True
        Me.lnkRetry.Location = New System.Drawing.Point(17, 163)
        Me.lnkRetry.Name = "lnkRetry"
        Me.lnkRetry.Size = New System.Drawing.Size(32, 13)
        Me.lnkRetry.TabIndex = 18
        Me.lnkRetry.TabStop = True
        Me.lnkRetry.Text = "Retry"
        '
        'lnkAbort
        '
        Me.lnkAbort.AutoSize = True
        Me.lnkAbort.Location = New System.Drawing.Point(17, 132)
        Me.lnkAbort.Name = "lnkAbort"
        Me.lnkAbort.Size = New System.Drawing.Size(32, 13)
        Me.lnkAbort.TabIndex = 17
        Me.lnkAbort.TabStop = True
        Me.lnkAbort.Text = "Abort"
        '
        'MessageBox2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(374, 260)
        Me.ControlBox = False
        Me.Controls.Add(Me.lnkIgnore)
        Me.Controls.Add(Me.lnkRetry)
        Me.Controls.Add(Me.lnkAbort)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.chbApplyToAll)
        Me.Controls.Add(Me.lblMessage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MessageBox2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GSCOM.Applications.InSys"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents chbApplyToAll As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lnkIgnore As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRetry As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAbort As System.Windows.Forms.LinkLabel
End Class
