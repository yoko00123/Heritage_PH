Option Explicit On
Option Strict On



<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataSelector
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.MainTree = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'MainTree
        '
        Me.MainTree.CheckBoxes = True
        Me.MainTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTree.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainTree.Location = New System.Drawing.Point(0, 0)
        Me.MainTree.Name = "MainTree"
        Me.MainTree.Size = New System.Drawing.Size(598, 405)
        Me.MainTree.TabIndex = 2
        '
        'DataSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainTree)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "DataSelector"
        Me.Size = New System.Drawing.Size(598, 405)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainTree As System.Windows.Forms.TreeView

End Class
