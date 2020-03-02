<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageBoxForm
    Inherits Form


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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lnkCancel = New System.Windows.Forms.LinkLabel()
        Me.lnkResume = New System.Windows.Forms.LinkLabel()
        Me.lnkSpecific = New System.Windows.Forms.LinkLabel()
        Me.lnkAll = New System.Windows.Forms.LinkLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(13, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Sending Option"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(84, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 32)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "e-Payslip"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(13, 82)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(409, 1)
        Me.Panel1.TabIndex = 7
        '
        'lnkCancel
        '
        Me.lnkCancel.AutoSize = True
        Me.lnkCancel.Location = New System.Drawing.Point(87, 229)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(46, 13)
        Me.lnkCancel.TabIndex = 17
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        '
        'lnkResume
        '
        Me.lnkResume.AutoSize = True
        Me.lnkResume.Location = New System.Drawing.Point(87, 196)
        Me.lnkResume.Name = "lnkResume"
        Me.lnkResume.Size = New System.Drawing.Size(53, 13)
        Me.lnkResume.TabIndex = 16
        Me.lnkResume.TabStop = True
        Me.lnkResume.Text = "Resume"
        '
        'lnkSpecific
        '
        Me.lnkSpecific.AutoSize = True
        Me.lnkSpecific.Location = New System.Drawing.Point(87, 163)
        Me.lnkSpecific.Name = "lnkSpecific"
        Me.lnkSpecific.Size = New System.Drawing.Size(97, 13)
        Me.lnkSpecific.TabIndex = 15
        Me.lnkSpecific.TabStop = True
        Me.lnkSpecific.Text = "Send to specific"
        '
        'lnkAll
        '
        Me.lnkAll.AutoSize = True
        Me.lnkAll.Location = New System.Drawing.Point(87, 130)
        Me.lnkAll.Name = "lnkAll"
        Me.lnkAll.Size = New System.Drawing.Size(54, 13)
        Me.lnkAll.TabIndex = 14
        Me.lnkAll.TabStop = True
        Me.lnkAll.Text = "Send All"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources._report
        Me.PictureBox1.Location = New System.Drawing.Point(14, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'MessageBoxForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(434, 278)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkResume)
        Me.Controls.Add(Me.lnkSpecific)
        Me.Controls.Add(Me.lnkAll)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MessageBoxForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InSys"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkResume As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkSpecific As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAll As System.Windows.Forms.LinkLabel

End Class
