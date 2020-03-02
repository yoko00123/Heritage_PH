<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectEmployeeForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectEmployeeForm))
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.btnProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripDropDownButton = New System.Windows.Forms.ToolStripDropDownButton
        Me.IsBasicPayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsAnnualizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Is13MonthToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsAdjustmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnUncheckAll = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStrip.SuspendLayout()
        Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnProcess, Me.ToolStripDropDownButton, Me.btnUncheckAll})
        Me.ToolStrip.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(234, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip"
        '
        'btnProcess
        '
        Me.btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), System.Drawing.Image)
        Me.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(64, 22)
        Me.btnProcess.Text = "&Process"
        '
        'ToolStripDropDownButton
        '
        Me.ToolStripDropDownButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IsBasicPayToolStripMenuItem, Me.IsAnnualizeToolStripMenuItem, Me.Is13MonthToolStripMenuItem, Me.IsAdjustmentToolStripMenuItem})
        Me.ToolStripDropDownButton.Image = CType(resources.GetObject("ToolStripDropDownButton.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton.Name = "ToolStripDropDownButton"
        Me.ToolStripDropDownButton.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripDropDownButton.Text = "&Check All"
        '
        'IsBasicPayToolStripMenuItem
        '
        Me.IsBasicPayToolStripMenuItem.Name = "IsBasicPayToolStripMenuItem"
        Me.IsBasicPayToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.IsBasicPayToolStripMenuItem.Text = "IsBasicPay"
        '
        'IsAnnualizeToolStripMenuItem
        '
        Me.IsAnnualizeToolStripMenuItem.Name = "IsAnnualizeToolStripMenuItem"
        Me.IsAnnualizeToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.IsAnnualizeToolStripMenuItem.Text = "IsAnnualize"
        '
        'Is13MonthToolStripMenuItem
        '
        Me.Is13MonthToolStripMenuItem.Name = "Is13MonthToolStripMenuItem"
        Me.Is13MonthToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.Is13MonthToolStripMenuItem.Text = "Is13Month"
        '
        'IsAdjustmentToolStripMenuItem
        '
        Me.IsAdjustmentToolStripMenuItem.Name = "IsAdjustmentToolStripMenuItem"
        Me.IsAdjustmentToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.IsAdjustmentToolStripMenuItem.Text = "IsAdjustment"
        '
        'btnUncheckAll
        '
        Me.btnUncheckAll.Image = CType(resources.GetObject("btnUncheckAll.Image"), System.Drawing.Image)
        Me.btnUncheckAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUncheckAll.Name = "btnUncheckAll"
        Me.btnUncheckAll.Size = New System.Drawing.Size(81, 22)
        Me.btnUncheckAll.Text = "&Uncheck All"
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.BottomToolStripPanel
        '
        Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.StatusStrip)
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(603, 403)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(603, 450)
        Me.ToolStripContainer.TabIndex = 3
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.ToolStrip)
        '
        'StatusStrip
        '
        Me.StatusStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(603, 22)
        Me.StatusStrip.TabIndex = 0
        '
        'SelectEmployeeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 450)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectEmployeeForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Process Payroll"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents IsBasicPayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsAnnualizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Is13MonthToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsAdjustmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnUncheckAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
End Class
