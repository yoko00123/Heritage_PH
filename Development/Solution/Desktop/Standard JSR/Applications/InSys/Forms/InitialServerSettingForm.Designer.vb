<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitialServerSettingForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InitialServerSettingForm))
        Me.ServerGroupBox = New System.Windows.Forms.GroupBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ServerSaveButton = New System.Windows.Forms.ToolStripButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DBUserNameBox = New System.Windows.Forms.TextBox()
        Me.DBPasswordBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DBBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ServerBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ServerGroupBox.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ServerGroupBox
        '
        Me.ServerGroupBox.Controls.Add(Me.ToolStrip1)
        Me.ServerGroupBox.Controls.Add(Me.Label4)
        Me.ServerGroupBox.Controls.Add(Me.DBUserNameBox)
        Me.ServerGroupBox.Controls.Add(Me.DBPasswordBox)
        Me.ServerGroupBox.Controls.Add(Me.Label3)
        Me.ServerGroupBox.Controls.Add(Me.DBBox)
        Me.ServerGroupBox.Controls.Add(Me.Label2)
        Me.ServerGroupBox.Controls.Add(Me.ServerBox)
        Me.ServerGroupBox.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerGroupBox.ForeColor = System.Drawing.Color.White
        Me.ServerGroupBox.Location = New System.Drawing.Point(5, 115)
        Me.ServerGroupBox.Name = "ServerGroupBox"
        Me.ServerGroupBox.Size = New System.Drawing.Size(454, 100)
        Me.ServerGroupBox.TabIndex = 24
        Me.ServerGroupBox.TabStop = False
        Me.ServerGroupBox.Text = "Server"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerSaveButton, Me.pbar})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 72)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(448, 25)
        Me.ToolStrip1.TabIndex = 25
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ServerSaveButton
        '
        Me.ServerSaveButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ServerSaveButton.ForeColor = System.Drawing.Color.Black
        Me.ServerSaveButton.Image = CType(resources.GetObject("ServerSaveButton.Image"), System.Drawing.Image)
        Me.ServerSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ServerSaveButton.Name = "ServerSaveButton"
        Me.ServerSaveButton.Size = New System.Drawing.Size(51, 22)
        Me.ServerSaveButton.Text = "Save"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(233, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 23)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Password"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DBUserNameBox
        '
        Me.DBUserNameBox.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DBUserNameBox.Location = New System.Drawing.Point(299, 16)
        Me.DBUserNameBox.Name = "DBUserNameBox"
        Me.DBUserNameBox.Size = New System.Drawing.Size(145, 21)
        Me.DBUserNameBox.TabIndex = 21
        Me.DBUserNameBox.UseSystemPasswordChar = True
        '
        'DBPasswordBox
        '
        Me.DBPasswordBox.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DBPasswordBox.Location = New System.Drawing.Point(299, 43)
        Me.DBPasswordBox.Name = "DBPasswordBox"
        Me.DBPasswordBox.Size = New System.Drawing.Size(145, 21)
        Me.DBPasswordBox.TabIndex = 22
        Me.DBPasswordBox.UseSystemPasswordChar = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(217, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 23)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "User name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DBBox
        '
        Me.DBBox.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DBBox.Location = New System.Drawing.Point(74, 45)
        Me.DBBox.Name = "DBBox"
        Me.DBBox.Size = New System.Drawing.Size(140, 21)
        Me.DBBox.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Database"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ServerBox
        '
        Me.ServerBox.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.ServerBox.Location = New System.Drawing.Point(8, 19)
        Me.ServerBox.Name = "ServerBox"
        Me.ServerBox.Size = New System.Drawing.Size(206, 21)
        Me.ServerBox.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 25)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Welcome!"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(86, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(373, 50)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "This is the initial setup, please provide the following credentials in order to r" & _
    "un the system"
        '
        'pbar
        '
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(100, 22)
        Me.pbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbar.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'InitialServerSettingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(467, 220)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ServerGroupBox)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InitialServerSettingForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Server Setting"
        Me.ServerGroupBox.ResumeLayout(False)
        Me.ServerGroupBox.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ServerGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ServerSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DBUserNameBox As System.Windows.Forms.TextBox
    Friend WithEvents DBPasswordBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DBBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ServerBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pbar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
