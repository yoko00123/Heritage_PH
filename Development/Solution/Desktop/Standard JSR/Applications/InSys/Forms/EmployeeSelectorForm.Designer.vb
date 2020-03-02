<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmployeeSelectorForm
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
        Me.mList = New GSCOM.Applications.InSys.BrowserDataList()
        Me.SuspendLayout()
        '
        'mList
        '
        Me.mList.AutoGenerateFilters = True
        Me.mList.BackColor = System.Drawing.SystemColors.Control
        Me.mList.DataSource = Nothing
        Me.mList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mList.FilterBarVisible = False
        Me.mList.FixedFilter = Nothing
        Me.mList.GroupImageKey = Nothing
        Me.mList.ImageKey = Nothing
        Me.mList.ImageList = Nothing
        Me.mList.Location = New System.Drawing.Point(0, 0)
        Me.mList.Mode = GSCOM.UI.DataList.DataListBase.ViewMode.Grid
        Me.mList.Name = "mList"
        Me.mList.OddBackColor = System.Drawing.Color.AliceBlue
        Me.mList.ReportPath = Nothing
        Me.mList.SelectModeVisible = False
        Me.mList.SelectString = Nothing
        Me.mList.Size = New System.Drawing.Size(575, 262)
        Me.mList.TabIndex = 0
        '
        'EmployeeSelectorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 262)
        Me.Controls.Add(Me.mList)
        Me.Name = "EmployeeSelectorForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mList As GSCOM.Applications.InSys.BrowserDataList
End Class
