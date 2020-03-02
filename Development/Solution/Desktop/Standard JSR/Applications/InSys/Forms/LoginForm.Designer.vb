<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.MainStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MainProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ServerSettingButton = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.ServerGroupBox = New System.Windows.Forms.GroupBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ServerSaveButton = New System.Windows.Forms.ToolStripButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DBUserNameBox = New System.Windows.Forms.TextBox()
        Me.DBPasswordBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ServerBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkRemember = New System.Windows.Forms.CheckBox()
        Me.chkSelect = New System.Windows.Forms.CheckBox()
        Me._ID_Company = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.hlp1 = New System.Windows.Forms.HelpProvider()
        Me.DBBox = New System.Windows.Forms.ComboBox()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ServerGroupBox.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.LogoPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(478, 360)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Gray
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainStatus, Me.MainProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(478, 23)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'MainStatus
        '
        Me.MainStatus.BackColor = System.Drawing.Color.Transparent
        Me.MainStatus.Name = "MainStatus"
        Me.MainStatus.Size = New System.Drawing.Size(221, 18)
        Me.MainStatus.Spring = True
        '
        'MainProgressBar
        '
        Me.MainProgressBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MainProgressBar.Name = "MainProgressBar"
        Me.MainProgressBar.Size = New System.Drawing.Size(240, 17)
        Me.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ServerSettingButton)
        Me.Panel2.Controls.Add(Me.StatusStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 459)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(478, 23)
        Me.Panel2.TabIndex = 11
        '
        'ServerSettingButton
        '
        Me.ServerSettingButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ServerSettingButton.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.servers
        Me.ServerSettingButton.Location = New System.Drawing.Point(0, 0)
        Me.ServerSettingButton.Name = "ServerSettingButton"
        Me.ServerSettingButton.Size = New System.Drawing.Size(26, 23)
        Me.ServerSettingButton.TabIndex = 13
        Me.ServerSettingButton.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.Color.White
        Me.Cancel.Location = New System.Drawing.Point(360, 430)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(106, 23)
        Me.Cancel.TabIndex = 21
        Me.Cancel.Text = "&Cancel"
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK.ForeColor = System.Drawing.Color.White
        Me.OK.Location = New System.Drawing.Point(248, 430)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(106, 23)
        Me.OK.TabIndex = 20
        Me.OK.Text = "&OK"
        '
        'ServerGroupBox
        '
        Me.ServerGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerGroupBox.Controls.Add(Me.DBBox)
        Me.ServerGroupBox.Controls.Add(Me.ToolStrip1)
        Me.ServerGroupBox.Controls.Add(Me.Label4)
        Me.ServerGroupBox.Controls.Add(Me.DBUserNameBox)
        Me.ServerGroupBox.Controls.Add(Me.DBPasswordBox)
        Me.ServerGroupBox.Controls.Add(Me.Label3)
        Me.ServerGroupBox.Controls.Add(Me.Label2)
        Me.ServerGroupBox.Controls.Add(Me.ServerBox)
        Me.ServerGroupBox.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerGroupBox.ForeColor = System.Drawing.Color.White
        Me.ServerGroupBox.Location = New System.Drawing.Point(12, 324)
        Me.ServerGroupBox.Name = "ServerGroupBox"
        Me.ServerGroupBox.Size = New System.Drawing.Size(454, 100)
        Me.ServerGroupBox.TabIndex = 23
        Me.ServerGroupBox.TabStop = False
        Me.ServerGroupBox.Text = "Server"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerSaveButton})
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
        Me.Label3.Location = New System.Drawing.Point(232, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 23)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "User name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.ServerBox.Size = New System.Drawing.Size(218, 21)
        Me.ServerBox.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(183, 141)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(283, 177)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRemember)
        Me.Panel1.Controls.Add(Me.chkSelect)
        Me.Panel1.Controls.Add(Me._ID_Company)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtUserName)
        Me.Panel1.Controls.Add(Me.UsernameLabel)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.PasswordLabel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(277, 157)
        Me.Panel1.TabIndex = 8
        '
        'chkRemember
        '
        Me.chkRemember.AutoSize = True
        Me.chkRemember.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRemember.Location = New System.Drawing.Point(165, 135)
        Me.chkRemember.Name = "chkRemember"
        Me.chkRemember.Size = New System.Drawing.Size(105, 18)
        Me.chkRemember.TabIndex = 23
        Me.chkRemember.Text = "&Remember Me"
        Me.chkRemember.UseVisualStyleBackColor = True
        '
        'chkSelect
        '
        Me.chkSelect.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelect.Location = New System.Drawing.Point(7, 47)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(263, 18)
        Me.chkSelect.TabIndex = 21
        Me.chkSelect.Text = "&Select company after validation"
        Me.chkSelect.UseVisualStyleBackColor = True
        '
        '_ID_Company
        '
        Me._ID_Company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._ID_Company.Enabled = False
        Me._ID_Company.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me._ID_Company.Location = New System.Drawing.Point(7, 23)
        Me._ID_Company.Name = "_ID_Company"
        Me._ID_Company.Size = New System.Drawing.Size(263, 21)
        Me._ID_Company.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(263, 23)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "&Company"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserName
        '
        Me.txtUserName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.txtUserName.Location = New System.Drawing.Point(75, 85)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(195, 21)
        Me.txtUserName.TabIndex = 1
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.Location = New System.Drawing.Point(10, 85)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(260, 21)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&User name"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.txtPassword.Location = New System.Drawing.Point(75, 111)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(195, 21)
        Me.txtPassword.TabIndex = 2
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.Location = New System.Drawing.Point(10, 111)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(260, 21)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Password"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DBBox
        '
        Me.DBBox.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DBBox.FormattingEnabled = True
        Me.DBBox.Location = New System.Drawing.Point(74, 43)
        Me.DBBox.Name = "DBBox"
        Me.DBBox.Size = New System.Drawing.Size(152, 21)
        Me.DBBox.TabIndex = 25
        '
        'LoginForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(478, 482)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.ServerGroupBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ServerGroupBox.ResumeLayout(False)
        Me.ServerGroupBox.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents MainProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents MainStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ServerSettingButton As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents ServerGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ServerSaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DBUserNameBox As System.Windows.Forms.TextBox
    Friend WithEvents DBPasswordBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ServerBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRemember As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelect As System.Windows.Forms.CheckBox
    Friend WithEvents _ID_Company As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents hlp1 As System.Windows.Forms.HelpProvider
    Friend WithEvents DBBox As System.Windows.Forms.ComboBox

End Class
