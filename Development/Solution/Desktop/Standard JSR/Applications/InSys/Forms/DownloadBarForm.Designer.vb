<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadBarForm
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
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.btnShowDetails = New System.Windows.Forms.Button()
        Me.txtdetail = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.White
        Me.lblstatus.Location = New System.Drawing.Point(2, 7)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(76, 13)
        Me.lblstatus.TabIndex = 0
        Me.lblstatus.Text = "Initializing..."
        '
        'pbar
        '
        Me.pbar.Location = New System.Drawing.Point(0, 25)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(460, 5)
        Me.pbar.TabIndex = 1
        '
        'btnShowDetails
        '
        Me.btnShowDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShowDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnShowDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowDetails.ForeColor = System.Drawing.Color.White
        Me.btnShowDetails.Location = New System.Drawing.Point(355, 35)
        Me.btnShowDetails.Name = "btnShowDetails"
        Me.btnShowDetails.Size = New System.Drawing.Size(100, 23)
        Me.btnShowDetails.TabIndex = 2
        Me.btnShowDetails.Tag = "1"
        Me.btnShowDetails.Text = "&Show Details"
        Me.btnShowDetails.UseVisualStyleBackColor = False
        '
        'txtdetail
        '
        Me.txtdetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.txtdetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtdetail.ForeColor = System.Drawing.Color.White
        Me.txtdetail.Location = New System.Drawing.Point(5, 64)
        Me.txtdetail.Multiline = True
        Me.txtdetail.Name = "txtdetail"
        Me.txtdetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtdetail.Size = New System.Drawing.Size(449, 215)
        Me.txtdetail.TabIndex = 3
        Me.txtdetail.Visible = False
        '
        'DownloadBarForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(460, 62)
        Me.Controls.Add(Me.txtdetail)
        Me.Controls.Add(Me.btnShowDetails)
        Me.Controls.Add(Me.pbar)
        Me.Controls.Add(Me.lblstatus)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DownloadBarForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Downloads"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents pbar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnShowDetails As System.Windows.Forms.Button
    Friend WithEvents txtdetail As System.Windows.Forms.TextBox
End Class
