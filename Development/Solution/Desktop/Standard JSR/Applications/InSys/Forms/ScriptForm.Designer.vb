<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptForm
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.RunButton = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ScriptBox = New System.Windows.Forms.TextBox
        Me.InfoMessageBox = New System.Windows.Forms.TextBox
        Me.MainGrid = New GSCOM.UI.GSDataGridView.GSDataGridView
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RunButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'RunButton
        '
        Me.RunButton.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.FormRun
        Me.RunButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RunButton.Name = "RunButton"
        Me.RunButton.Size = New System.Drawing.Size(46, 22)
        Me.RunButton.Text = "Run"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ScriptBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.InfoMessageBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MainGrid)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 541)
        Me.SplitContainer1.SplitterDistance = 173
        Me.SplitContainer1.TabIndex = 2
        '
        'ScriptBox
        '
        Me.ScriptBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScriptBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScriptBox.Location = New System.Drawing.Point(0, 0)
        Me.ScriptBox.Multiline = True
        Me.ScriptBox.Name = "ScriptBox"
        Me.ScriptBox.Size = New System.Drawing.Size(527, 173)
        Me.ScriptBox.TabIndex = 0
        '
        'InfoMessageBox
        '
        Me.InfoMessageBox.Dock = System.Windows.Forms.DockStyle.Right
        Me.InfoMessageBox.Location = New System.Drawing.Point(527, 0)
        Me.InfoMessageBox.Multiline = True
        Me.InfoMessageBox.Name = "InfoMessageBox"
        Me.InfoMessageBox.Size = New System.Drawing.Size(265, 173)
        Me.InfoMessageBox.TabIndex = 1
        '
        'MainGrid
        '
        Me.MainGrid.AllowUserToAddRows = False
        Me.MainGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.MainGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MainGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MainGrid.BackgroundColor = System.Drawing.Color.Gray
        Me.MainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainGrid.EvenBackColor = System.Drawing.Color.White
        Me.MainGrid.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.MainGrid.GridColor = System.Drawing.SystemColors.ControlDark
        Me.MainGrid.Location = New System.Drawing.Point(0, 0)
        Me.MainGrid.MultiSelect = False
        Me.MainGrid.Name = "MainGrid"
        Me.MainGrid.OddBackColor = System.Drawing.Color.AliceBlue
        Me.MainGrid.ReadOnly = True
        Me.MainGrid.RowHeadersWidth = 24
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.MainGrid.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.MainGrid.RowTemplate.Height = 18
        Me.MainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MainGrid.Size = New System.Drawing.Size(792, 364)
        Me.MainGrid.TabIndex = 2
        '
        'ScriptForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ScriptForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScriptForm"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents RunButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MainGrid As GSCOM.UI.GSDataGridView.GSDataGridView
    Friend WithEvents InfoMessageBox As System.Windows.Forms.TextBox
    Friend WithEvents ScriptBox As System.Windows.Forms.TextBox
End Class
