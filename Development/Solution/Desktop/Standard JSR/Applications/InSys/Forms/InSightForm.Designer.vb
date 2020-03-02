<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InSightForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ImageView = New GSCOM.UI.ImageListView.ImageListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.MenuView = New GSCOM.UI.GSDataGridView.GSDataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.MenuButtonView = New GSCOM.UI.GSDataGridView.GSDataGridView()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.MenuView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.MenuButtonView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(784, 442)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ImageView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(776, 416)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Images"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ImageView
        '
        Me.ImageView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImageView.ImageList = Nothing
        Me.ImageView.Location = New System.Drawing.Point(3, 3)
        Me.ImageView.Name = "ImageView"
        Me.ImageView.Size = New System.Drawing.Size(770, 410)
        Me.ImageView.TabIndex = 1
        Me.ImageView.UseCompatibleStateImageBehavior = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.MenuView)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(776, 416)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Menu"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'MenuView
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.MenuView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MenuView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MenuView.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.MenuView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MenuView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuView.EvenBackColor = System.Drawing.Color.White
        Me.MenuView.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.MenuView.GridColor = System.Drawing.SystemColors.ControlDark
        Me.MenuView.Location = New System.Drawing.Point(3, 3)
        Me.MenuView.MultiSelect = False
        Me.MenuView.Name = "MenuView"
        Me.MenuView.OddBackColor = System.Drawing.Color.AliceBlue
        Me.MenuView.RowHeadersWidth = 24
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.MenuView.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.MenuView.RowTemplate.Height = 18
        Me.MenuView.SchemaTable = Nothing
        Me.MenuView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MenuView.Size = New System.Drawing.Size(770, 410)
        Me.MenuView.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.MenuButtonView)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(776, 416)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "MenuButton"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'MenuButtonView
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        Me.MenuButtonView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MenuButtonView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MenuButtonView.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.MenuButtonView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MenuButtonView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuButtonView.EvenBackColor = System.Drawing.Color.White
        Me.MenuButtonView.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.MenuButtonView.GridColor = System.Drawing.SystemColors.ControlDark
        Me.MenuButtonView.Location = New System.Drawing.Point(3, 3)
        Me.MenuButtonView.MultiSelect = False
        Me.MenuButtonView.Name = "MenuButtonView"
        Me.MenuButtonView.OddBackColor = System.Drawing.Color.AliceBlue
        Me.MenuButtonView.RowHeadersWidth = 24
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.MenuButtonView.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.MenuButtonView.RowTemplate.Height = 18
        Me.MenuButtonView.SchemaTable = Nothing
        Me.MenuButtonView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MenuButtonView.Size = New System.Drawing.Size(770, 410)
        Me.MenuButtonView.TabIndex = 2
        '
        'InSightForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 442)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "InSightForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InSightForm"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.MenuView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.MenuButtonView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ImageView As GSCOM.UI.ImageListView.ImageListView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents MenuView As GSCOM.UI.GSDataGridView.GSDataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents MenuButtonView As GSCOM.UI.GSDataGridView.GSDataGridView
End Class
