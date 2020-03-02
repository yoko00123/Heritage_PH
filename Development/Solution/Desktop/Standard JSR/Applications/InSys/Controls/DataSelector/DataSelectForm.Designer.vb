Option Explicit On
Option Strict On


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataSelectForm
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
        Me.MainList = New GSCOM.Applications.InSys.DataSelectorList()
        Me.SuspendLayout()
        '
        'MainList
        '
        Me.MainList.AutoGenerateFilters = True
        Me.MainList.BackColor = System.Drawing.SystemColors.Control
        Me.MainList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MainList.DataSource = Nothing
        Me.MainList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainList.FilterBarVisible = False
        Me.MainList.FixedFilter = Nothing
        Me.MainList.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.MainList.GroupImageKey = Nothing
        Me.MainList.ImageKey = Nothing
        Me.MainList.ImageList = Nothing
        Me.MainList.Location = New System.Drawing.Point(0, 0)
        Me.MainList.Mode = GSCOM.UI.DataList.DataListBase.ViewMode.Grid
        Me.MainList.Name = "MainList"
        Me.MainList.OddBackColor = System.Drawing.Color.Gainsboro
        Me.MainList.ReportPath = Nothing
        Me.MainList.SelectModeVisible = False
        Me.MainList.SelectString = Nothing
        Me.MainList.Size = New System.Drawing.Size(634, 452)
        Me.MainList.TabIndex = 1
        '
        'DataSelectForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 452)
        Me.Controls.Add(Me.MainList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DataSelectForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainList As DataSelectorList
End Class
