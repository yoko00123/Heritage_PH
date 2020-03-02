

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InfoForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PrintButton = New System.Windows.Forms.ToolStripButton()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.NewButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.RefreshButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HeaderButton = New System.Windows.Forms.ToolStripButton()
        Me.TranslucentButton = New System.Windows.Forms.ToolStripButton()
        Me.PlotValuesButton = New System.Windows.Forms.ToolStripButton()
        Me.EmailButton = New System.Windows.Forms.ToolStripButton()
        Me.spcMain = New System.Windows.Forms.SplitContainer()
        Me.mStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.mStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ImageBox = New System.Windows.Forms.PictureBox()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.InfoFormContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateCopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateXMLFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportXMLFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropoertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BasicTab = New GSCOM.Applications.InSys.DataControl()
        Me.tcMain = New GSCOM.Applications.InSys.DataControl()
        Me.tsMain.SuspendLayout()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        Me.mStatusStrip.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InfoFormContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintButton
        '
        Me.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(36, 22)
        Me.PrintButton.Text = "&Print"
        '
        'tsMain
        '
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewButton, Me.SaveButton, Me.PrintButton, Me.RefreshButton, Me.ToolStripSeparator1, Me.HeaderButton, Me.TranslucentButton, Me.PlotValuesButton, Me.EmailButton})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(784, 25)
        Me.tsMain.TabIndex = 0
        Me.tsMain.Text = "ToolStrip1"
        '
        'NewButton
        '
        Me.NewButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(35, 22)
        Me.NewButton.Text = "&New"
        '
        'SaveButton
        '
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(35, 22)
        Me.SaveButton.Text = "&Save"
        '
        'RefreshButton
        '
        Me.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(50, 22)
        Me.RefreshButton.Text = "&Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'HeaderButton
        '
        Me.HeaderButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.HeaderButton.Checked = True
        Me.HeaderButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HeaderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HeaderButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HeaderButton.Name = "HeaderButton"
        Me.HeaderButton.Size = New System.Drawing.Size(23, 22)
        Me.HeaderButton.Text = "Header"
        '
        'TranslucentButton
        '
        Me.TranslucentButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TranslucentButton.CheckOnClick = True
        Me.TranslucentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TranslucentButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TranslucentButton.Name = "TranslucentButton"
        Me.TranslucentButton.Size = New System.Drawing.Size(23, 22)
        Me.TranslucentButton.Text = "Translucent"
        Me.TranslucentButton.Visible = False
        '
        'PlotValuesButton
        '
        Me.PlotValuesButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PlotValuesButton.Name = "PlotValuesButton"
        Me.PlotValuesButton.Size = New System.Drawing.Size(41, 22)
        Me.PlotValuesButton.Text = "Detail"
        Me.PlotValuesButton.Visible = False
        '
        'EmailButton
        '
        Me.EmailButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.EmailButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.EmailButton.Name = "EmailButton"
        Me.EmailButton.Size = New System.Drawing.Size(40, 22)
        Me.EmailButton.Text = "Email"
        Me.EmailButton.Visible = False
        '
        'spcMain
        '
        Me.spcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spcMain.Location = New System.Drawing.Point(0, 89)
        Me.spcMain.Margin = New System.Windows.Forms.Padding(2)
        Me.spcMain.Name = "spcMain"
        '
        'spcMain.Panel1
        '
        Me.spcMain.Panel1.Controls.Add(Me.BasicTab)
        Me.spcMain.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!)
        '
        'spcMain.Panel2
        '
        Me.spcMain.Panel2.Controls.Add(Me.tcMain)
        Me.spcMain.Size = New System.Drawing.Size(784, 451)
        Me.spcMain.SplitterDistance = 267
        Me.spcMain.SplitterWidth = 1
        Me.spcMain.TabIndex = 8
        '
        'mStatusStrip
        '
        Me.mStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mStatusLabel})
        Me.mStatusStrip.Location = New System.Drawing.Point(0, 540)
        Me.mStatusStrip.Name = "mStatusStrip"
        Me.mStatusStrip.Size = New System.Drawing.Size(784, 22)
        Me.mStatusStrip.TabIndex = 9
        Me.mStatusStrip.Text = "StatusStrip1"
        '
        'mStatusLabel
        '
        Me.mStatusLabel.Name = "mStatusLabel"
        Me.mStatusLabel.Size = New System.Drawing.Size(10, 17)
        Me.mStatusLabel.Text = " "
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.ImageBox)
        Me.Panel1.Controls.Add(Me.NameLabel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 64)
        Me.Panel1.TabIndex = 10
        '
        'ImageBox
        '
        Me.ImageBox.BackColor = System.Drawing.Color.Transparent
        Me.ImageBox.Location = New System.Drawing.Point(8, 8)
        Me.ImageBox.Name = "ImageBox"
        Me.ImageBox.Size = New System.Drawing.Size(48, 48)
        Me.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImageBox.TabIndex = 0
        Me.ImageBox.TabStop = False
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.BackColor = System.Drawing.Color.Transparent
        Me.NameLabel.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold)
        Me.NameLabel.Location = New System.Drawing.Point(62, 33)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(72, 23)
        Me.NameLabel.TabIndex = 1
        Me.NameLabel.Text = "Name"
        Me.NameLabel.UseMnemonic = False
        '
        'InfoFormContextMenu
        '
        Me.InfoFormContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateCopyToolStripMenuItem, Me.GenerateXMLFileToolStripMenuItem, Me.ImportXMLFileToolStripMenuItem, Me.PropoertiesToolStripMenuItem})
        Me.InfoFormContextMenu.Name = "InfoFormContextMenu"
        Me.InfoFormContextMenu.Size = New System.Drawing.Size(159, 92)
        '
        'CreateCopyToolStripMenuItem
        '
        Me.CreateCopyToolStripMenuItem.Name = "CreateCopyToolStripMenuItem"
        Me.CreateCopyToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.CreateCopyToolStripMenuItem.Text = "&Create Copy"
        '
        'GenerateXMLFileToolStripMenuItem
        '
        Me.GenerateXMLFileToolStripMenuItem.Name = "GenerateXMLFileToolStripMenuItem"
        Me.GenerateXMLFileToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.GenerateXMLFileToolStripMenuItem.Text = "Export XML File"
        '
        'ImportXMLFileToolStripMenuItem
        '
        Me.ImportXMLFileToolStripMenuItem.Name = "ImportXMLFileToolStripMenuItem"
        Me.ImportXMLFileToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ImportXMLFileToolStripMenuItem.Text = "Import XML File"
        '
        'PropoertiesToolStripMenuItem
        '
        Me.PropoertiesToolStripMenuItem.Name = "PropoertiesToolStripMenuItem"
        Me.PropoertiesToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PropoertiesToolStripMenuItem.Text = "P&roperties"
        '
        'BasicTab
        '
        Me.BasicTab.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.BasicTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BasicTab.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BasicTab.Location = New System.Drawing.Point(0, 0)
        Me.BasicTab.Margin = New System.Windows.Forms.Padding(2)
        Me.BasicTab.Multiline = True
        Me.BasicTab.Name = "BasicTab"
        Me.BasicTab.SelectedIndex = 0
        Me.BasicTab.Size = New System.Drawing.Size(267, 451)
        Me.BasicTab.TabIndex = 1
        '
        'tcMain
        '
        Me.tcMain.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Margin = New System.Windows.Forms.Padding(2)
        Me.tcMain.Multiline = True
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(516, 451)
        Me.tcMain.TabIndex = 0
        '
        'InfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.ContextMenuStrip = Me.InfoFormContextMenu
        Me.Controls.Add(Me.spcMain)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.mStatusStrip)
        Me.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.MinimizeBox = False
        Me.Name = "InfoForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.mStatusStrip.ResumeLayout(False)
        Me.mStatusStrip.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InfoFormContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PrintButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Private WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents TranslucentButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents tcMain As InSys.DataControl
    Friend WithEvents HeaderButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RefreshButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewButton As System.Windows.Forms.ToolStripButton
    Private WithEvents mStatusStrip As System.Windows.Forms.StatusStrip
    Private WithEvents mStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents NameLabel As System.Windows.Forms.Label
    Public WithEvents ImageBox As System.Windows.Forms.PictureBox
    Friend WithEvents BasicTab As GSCOM.Applications.InSys.DataControl
    Friend WithEvents PlotValuesButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EmailButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents InfoFormContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PropoertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateCopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateXMLFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportXMLFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
