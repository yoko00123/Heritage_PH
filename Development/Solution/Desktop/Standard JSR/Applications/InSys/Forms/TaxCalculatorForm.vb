Friend Class TaxCalculatorForm

    Private Class tTaxCalculator
        Inherits DataTable

        Public Sub New()
            MyBase.New("tTaxCalculator")
            With Me.Columns
                With .Add("ID_PayrollFrequency", GetType(System.Int32))
                    .AllowDBNull = False
                End With
                With .Add("ID_TaxExemption", GetType(System.Int32))
                    .AllowDBNull = False

                End With
                With .Add("FirstGrossAmt", GetType(System.Decimal))
                    .AllowDBNull = False
                    .DefaultValue = 0
                End With
                With .Add("SecondGrossAmt", GetType(System.Decimal))
                    .AllowDBNull = False
                    .DefaultValue = 0
                End With
                With .Add("ComputeDeductions", GetType(System.Boolean))
                    .AllowDBNull = False
                    .DefaultValue = True
                End With
                .Add("SSS", GetType(System.Decimal))
                .Add("HDMF", GetType(System.Decimal))
                .Add("PHIC", GetType(System.Decimal))
                With .Add("TaxableAmount", GetType(System.Decimal))
                    .AllowDBNull = False
                    .DefaultValue = 0
                End With
                .Add("TaxAmount", GetType(System.Decimal))
            End With

        End Sub
    End Class

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputeButton.Click
        Dim s As String
        Dim d As Decimal
        Dim sum As Decimal
        Dim Checked As Boolean
        Checked = Checkvalues(Me._FirstGrossAmt.Text, Me._SecondGrossAmt.Text)
        If Checked Then
            sum = (CDec(Me._FirstGrossAmt.Text) + CDec(Me._SecondGrossAmt.Text))
            If _ComputeDeductions.Checked Then

                '--------SSS
                s = "SELECT dbo.fSSS(" & CDec(sum) & ")"
                Me._SSS.Text = GSCOM.SQL.ExecuteScalar(s, gConnection).ToString

                '--------

                '-------- HDMF
                s = "SELECT dbo.fhdmf(" & CDec(sum) & ")"
                Me._HDMF.Text = GSCOM.SQL.ExecuteScalar(s, gConnection).ToString
                '-------- 

                '-------------- PHIC
                s = "SELECT dbo.fphic(" & CDec(sum) & ")"
                Me._PHIC.Text = GSCOM.SQL.ExecuteScalar(s, gConnection).ToString
                '-------- 
            Else
                With dt.Rows(0)
                    .Item("SSS") = 0
                    .Item("HDMF") = 0
                    .Item("PHIC") = 0
                End With

            End If

            sum = CDec(CDec(sum) - (CDec(Me._PHIC.Text) + CDec(Me._HDMF.Text) + CDec(Me._SSS.Text)))
            Me._TaxableAmount.Text = CStr(sum)
            d = CDec(Me._TaxableAmount.Text)
            s = "SELECT dbo.fTax(" & GSCOM.SQL.SQLFormat(Me._ID_PayrollFrequency.SelectedValue) & "," & GSCOM.SQL.SQLFormat(Me._ID_TaxExemption.SelectedValue) & "," & GSCOM.SQL.SQLFormat(d) & ")"
            Me._TaxAmount.Text = GSCOM.SQL.ExecuteScalar(s, gConnection).ToString
        Else
            MsgBox("Provide values for all of the required field.")
        End If
    End Sub
    Dim dt As New tTaxCalculator
    Private Sub TaxCalculatorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim ds As New DataSet
        ds.EnforceConstraints = False
        ds.Tables.Add(dt)

        GSCOM.UI.BindControls(Me, dt)
        MainModule.InitControl(Me)
        dt.Rows.Add()

        'With Me._ID_PayrollFrequency
        '    .DataSource = Database.GetLookUp("tPayrollFrequency")
        '    .ValueMember = "ID"
        '    .DisplayMember = "Name"
        'End With

        'With Me._ID_TaxExemption
        '    .DataSource = Database.GetLookUp("tTaxExemption")
        '    .ValueMember = "ID"
        '    .DisplayMember = "Name"
        'End With
        Me.Width = 294

    End Sub

    Public Function Checkvalues(ByVal fTax As String, ByVal sTax As String) As Boolean
        Dim checktax As Decimal
        Try
            checktax = CDec(fTax)
            checktax = CDec(sTax)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ShowInTaskbar = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class