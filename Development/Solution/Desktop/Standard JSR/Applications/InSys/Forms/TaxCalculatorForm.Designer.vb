<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaxCalculatorForm
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
        Me._TaxableAmount = New System.Windows.Forms.TextBox
        Me.ComputeButton = New System.Windows.Forms.Button
        Me._TaxAmount = New System.Windows.Forms.TextBox
        Me._ID_PayrollFrequency = New System.Windows.Forms.ComboBox
        Me._ID_TaxExemption = New System.Windows.Forms.ComboBox
        Me._FirstGrossAmt = New System.Windows.Forms.TextBox
        Me._SSS = New System.Windows.Forms.TextBox
        Me._HDMF = New System.Windows.Forms.TextBox
        Me._PHIC = New System.Windows.Forms.TextBox
        Me._SecondGrossAmt = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me._ComputeDeductions = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        '_TaxableAmount
        '
        Me._TaxableAmount.Location = New System.Drawing.Point(120, 220)
        Me._TaxableAmount.Name = "_TaxableAmount"
        Me._TaxableAmount.Size = New System.Drawing.Size(159, 20)
        Me._TaxableAmount.TabIndex = 9
        '
        'ComputeButton
        '
        Me.ComputeButton.Location = New System.Drawing.Point(120, 246)
        Me.ComputeButton.Name = "ComputeButton"
        Me.ComputeButton.Size = New System.Drawing.Size(159, 24)
        Me.ComputeButton.TabIndex = 10
        Me.ComputeButton.Text = "Compute"
        Me.ComputeButton.UseVisualStyleBackColor = True
        '
        '_TaxAmount
        '
        Me._TaxAmount.Location = New System.Drawing.Point(120, 278)
        Me._TaxAmount.Name = "_TaxAmount"
        Me._TaxAmount.Size = New System.Drawing.Size(159, 20)
        Me._TaxAmount.TabIndex = 11
        '
        '_ID_PayrollFrequency
        '
        Me._ID_PayrollFrequency.FormattingEnabled = True
        Me._ID_PayrollFrequency.Location = New System.Drawing.Point(121, 66)
        Me._ID_PayrollFrequency.Name = "_ID_PayrollFrequency"
        Me._ID_PayrollFrequency.Size = New System.Drawing.Size(158, 21)
        Me._ID_PayrollFrequency.TabIndex = 3
        '
        '_ID_TaxExemption
        '
        Me._ID_TaxExemption.FormattingEnabled = True
        Me._ID_TaxExemption.Location = New System.Drawing.Point(121, 93)
        Me._ID_TaxExemption.Name = "_ID_TaxExemption"
        Me._ID_TaxExemption.Size = New System.Drawing.Size(158, 21)
        Me._ID_TaxExemption.TabIndex = 4
        '
        '_FirstGrossAmt
        '
        Me._FirstGrossAmt.Location = New System.Drawing.Point(121, 12)
        Me._FirstGrossAmt.Name = "_FirstGrossAmt"
        Me._FirstGrossAmt.Size = New System.Drawing.Size(159, 20)
        Me._FirstGrossAmt.TabIndex = 1
        '
        '_SSS
        '
        Me._SSS.Location = New System.Drawing.Point(121, 142)
        Me._SSS.Name = "_SSS"
        Me._SSS.Size = New System.Drawing.Size(159, 20)
        Me._SSS.TabIndex = 6
        '
        '_HDMF
        '
        Me._HDMF.Location = New System.Drawing.Point(121, 168)
        Me._HDMF.Name = "_HDMF"
        Me._HDMF.Size = New System.Drawing.Size(159, 20)
        Me._HDMF.TabIndex = 7
        '
        '_PHIC
        '
        Me._PHIC.Location = New System.Drawing.Point(121, 194)
        Me._PHIC.Name = "_PHIC"
        Me._PHIC.Size = New System.Drawing.Size(159, 20)
        Me._PHIC.TabIndex = 8
        '
        '_SecondGrossAmt
        '
        Me._SecondGrossAmt.Location = New System.Drawing.Point(121, 38)
        Me._SecondGrossAmt.Name = "_SecondGrossAmt"
        Me._SecondGrossAmt.Size = New System.Drawing.Size(159, 20)
        Me._SecondGrossAmt.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "First Gross Amount"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Second Gross Amount"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Payroll Frequency"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(37, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tax Exemption"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 220)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Taxable Amount"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(86, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "SSS"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(76, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "HDMF"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(81, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "PHIC"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(49, 278)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Tax Amount"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_ComputeDeductions
        '
        Me._ComputeDeductions.AutoSize = True
        Me._ComputeDeductions.Location = New System.Drawing.Point(120, 120)
        Me._ComputeDeductions.Name = "_ComputeDeductions"
        Me._ComputeDeductions.Size = New System.Drawing.Size(15, 14)
        Me._ComputeDeductions.TabIndex = 5
        Me._ComputeDeductions.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Compute Deductions"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TaxCalculatorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(288, 314)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me._ComputeDeductions)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me._SecondGrossAmt)
        Me.Controls.Add(Me._PHIC)
        Me.Controls.Add(Me._HDMF)
        Me.Controls.Add(Me._SSS)
        Me.Controls.Add(Me._FirstGrossAmt)
        Me.Controls.Add(Me._ID_TaxExemption)
        Me.Controls.Add(Me._ID_PayrollFrequency)
        Me.Controls.Add(Me._TaxAmount)
        Me.Controls.Add(Me.ComputeButton)
        Me.Controls.Add(Me._TaxableAmount)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TaxCalculatorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tax Calculator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _TaxableAmount As System.Windows.Forms.TextBox
    Friend WithEvents ComputeButton As System.Windows.Forms.Button
    Friend WithEvents _TaxAmount As System.Windows.Forms.TextBox
    Friend WithEvents _ID_PayrollFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents _ID_TaxExemption As System.Windows.Forms.ComboBox
    Friend WithEvents _FirstGrossAmt As System.Windows.Forms.TextBox
    Friend WithEvents _SSS As System.Windows.Forms.TextBox
    Friend WithEvents _HDMF As System.Windows.Forms.TextBox
    Friend WithEvents _PHIC As System.Windows.Forms.TextBox
    Friend WithEvents _SecondGrossAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents _ComputeDeductions As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
