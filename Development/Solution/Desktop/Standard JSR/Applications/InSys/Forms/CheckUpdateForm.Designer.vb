<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckUpdateForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CheckUpdateForm))
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnStartUpdate = New System.Windows.Forms.LinkLabel()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblBuild = New System.Windows.Forms.Label()
        Me.btnCheckUlit = New System.Windows.Forms.Button()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.btnCheckUlit)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.lblBuild)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.lblVersion)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.lblProduct)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.btnStartUpdate)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.Label3)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.Label2)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.Label1)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.pbar)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.lblStatus)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(417, 151)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(417, 151)
        Me.ToolStripContainer1.TabIndex = 0
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        Me.ToolStripContainer1.TopToolStripPanelVisible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(7, 11)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(61, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Initializing..."
        '
        'pbar
        '
        Me.pbar.Location = New System.Drawing.Point(3, 29)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(408, 10)
        Me.pbar.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Product:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Version:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Build:"
        '
        'btnStartUpdate
        '
        Me.btnStartUpdate.AutoSize = True
        Me.btnStartUpdate.Location = New System.Drawing.Point(338, 120)
        Me.btnStartUpdate.Name = "btnStartUpdate"
        Me.btnStartUpdate.Size = New System.Drawing.Size(67, 13)
        Me.btnStartUpdate.TabIndex = 5
        Me.btnStartUpdate.TabStop = True
        Me.btnStartUpdate.Text = "Start Update"
        Me.btnStartUpdate.Visible = False
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Location = New System.Drawing.Point(65, 63)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(47, 13)
        Me.lblProduct.TabIndex = 6
        Me.lblProduct.Text = "Product:"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(65, 91)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(47, 13)
        Me.lblVersion.TabIndex = 7
        Me.lblVersion.Text = "Product:"
        '
        'lblBuild
        '
        Me.lblBuild.AutoSize = True
        Me.lblBuild.Location = New System.Drawing.Point(65, 120)
        Me.lblBuild.Name = "lblBuild"
        Me.lblBuild.Size = New System.Drawing.Size(47, 13)
        Me.lblBuild.TabIndex = 8
        Me.lblBuild.Text = "Product:"
        '
        'btnCheckUlit
        '
        Me.btnCheckUlit.BackgroundImage = CType(resources.GetObject("btnCheckUlit.BackgroundImage"), System.Drawing.Image)
        Me.btnCheckUlit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCheckUlit.Location = New System.Drawing.Point(388, 4)
        Me.btnCheckUlit.Name = "btnCheckUlit"
        Me.btnCheckUlit.Size = New System.Drawing.Size(23, 23)
        Me.btnCheckUlit.TabIndex = 9
        Me.btnCheckUlit.UseVisualStyleBackColor = True
        '
        'CheckUpdateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 151)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CheckUpdateForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Check for Update"
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.ContentPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents pbar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStartUpdate As System.Windows.Forms.LinkLabel
    Friend WithEvents lblBuild As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents btnCheckUlit As System.Windows.Forms.Button
End Class
