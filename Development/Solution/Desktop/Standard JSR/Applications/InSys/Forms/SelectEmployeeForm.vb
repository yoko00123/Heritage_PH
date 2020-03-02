Imports GSCOM.Applications.InSys.Database.Tables

Friend Class SelectEmployeeForm

    Private mID_PayrollPeriod As Int32
    Private mID_PayrollPeriodType As Int32
    Private myDT As tPayrollPeriod

    Private Sub SelectEmployeeForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub SelectEmployeeForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'InitGrid()

    End Sub

    Dim lf As GSCOM.UI.DataList.DataList

    Private Sub InitTab()
        lf = New GSCOM.UI.DataList.DataList
        lf.Init("dbo.fGetEmployeeForPayroll (" & mID_PayrollPeriod.ToString & ")", gConnection, "", Nothing, "", "")
        lf.Text = "Employees for payroll"
        InitGrid()
        'lf.Dock = DockStyle.Fill
        ToolStripContainer.ContentPanel.Controls.Add(lf)



        lf.Dock = DockStyle.Fill


        'tcPayroll.TabPages.Add(CType(lf, TabPage))
        'tcPayroll.Dock = DockStyle.Fill
    End Sub

    Private Sub InitGrid()
        Dim dt As New DataTable
        With lf.MainView
            'ROBBIE : idatalistgrid
            '.AllowUserToAddRows = False
            '.AllowUserToDeleteRows = False
            '.AllowUserToResizeRows = False
            ' .ReadOnly = False
            '.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            '.BackgroundColor = System.Drawing.Color.Gray
            '.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .EvenBackColor = System.Drawing.Color.White
            '.Font = New System.Drawing.Font("Verdana", 7.0!)
            .GridColor = System.Drawing.SystemColors.Control
            '.Location = New System.Drawing.Point(0, 0)
            '.MultiSelect = False
            '.RowTemplate.Height = 18
            '.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .OddBackColor = Color.WhiteSmoke
            '.AutoGenerateColumns = True
        End With
        'For Each dc As DataGridViewColumn In lf.MainView.Columns
        '    Select Case dc.DataPropertyName
        '        Case tPersona.Field.ID.ToString
        '            dc.Visible = False
        '        Case tPersona.Field.Code.ToString
        '            dc.ReadOnly = True
        '        Case tPersona.Field.Name.ToString
        '            dc.ReadOnly = True
        '        Case tPayroll.Field.IsBasicPay.ToString, tPayroll.Field.Is13Month.ToString, tPayroll.Field.IsAnnualize.ToString
        '            If mID_PayrollPeriodType = 1 Then
        '                dc.Visible = True
        '            ElseIf mID_PayrollPeriodType = 2 Then
        '                dc.Visible = False
        '            End If
        '        Case tPayroll.Field.IsPreviousEmployer.ToString
        '            If mID_PayrollPeriodType = 1 Then
        '                dc.Visible = False
        '            ElseIf mID_PayrollPeriodType = 2 Then
        '                dc.Visible = True
        '            End If
        '    End Select
        'Next
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim tran As System.Data.SqlClient.SqlTransaction
        Dim dt As New tPayroll(gConnection)
        Dim dra() As DataRow
        Dim dr As DataRow
        Dim ds As New DataSet
        Dim a As DataTable
        Dim tmpTable As New DataTable

        Try
            Cursor = Cursors.WaitCursor

            ds.Tables.Add(dt)
            ds.EnforceConstraints = False
            ' goto last cell to refresh checked item/s...
            'robbie idalistgrid
            'lf.MainView.CurrentCell = lf.MainView(1, lf.MainView.Rows.GetLastRow(DataGridViewElementStates.Displayed))

            a = CType(lf.DataSource, DataTable)
            dra = a.Select("((IsBasicPay=1) OR (Is13Month=1) OR (IsAnnualize=1) OR (IsAdjustment=1) OR (IsPreviousEmployer=1)) AND (ID <> 9999999)")
            For Each dd As DataRow In dra
                dr = dt.AddRow
                dr(tPayroll.Field.ID_PayrollPeriod.ToString) = mID_PayrollPeriod
                dr(tPayroll.Field.ID_Employee.ToString) = dd(tPayroll.Field.ID.ToString)
                dr(tPayroll.Field.IsBasicPay.ToString) = dd(tPayroll.Field.IsBasicPay.ToString)
                dr(tPayroll.Field.Is13Month.ToString) = dd(tPayroll.Field.Is13Month.ToString)
                dr(tPayroll.Field.IsAnnualize.ToString) = dd(tPayroll.Field.IsAnnualize.ToString)
                dr(tPayroll.Field.IsAdjustment.ToString) = dd(tPayroll.Field.IsAdjustment.ToString)
                dr(tPayroll.Field.IsPreviousEmployer.ToString) = dd(tPayroll.Field.IsPreviousEmployer.ToString)
            Next
            gConnection.Open()
            tran = gConnection.BeginTransaction()
            Try
                dt.Adapter.InsertCommand.Transaction = tran
                dt.Adapter.SelectCommand.Transaction = tran
                dt.Adapter.UpdateCommand.Transaction = tran
                dt.Adapter.DeleteCommand.Transaction = tran
                dt.Update()
                tran.Commit()
            Catch ex As Exception
                Try
                    tran.Rollback()
                Catch ex2 As Exception
                    MsgBox(ex2.Message, MsgBoxStyle.Critical)
                End Try
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            Finally
                gConnection.Close()
            End Try
            Application.DoEvents()

            GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_New " & mID_PayrollPeriod.ToString, gConnection, 120)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_Delete " & mID_PayrollPeriod.ToString, gConnection)
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            gConnection.Close()
            Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub New(ByVal pHeader As DataTable)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = gIcon
        myDT = CType(pHeader, tPayrollPeriod)
        mID_PayrollPeriod = CInt(myDT.Get(tPayrollPeriod.Field.ID))
        mID_PayrollPeriodType = CInt(myDT.Get(tPayrollPeriod.Field.ID_PayrollPeriodType))

        InitTab()

    End Sub

    Private Sub CheckPayrollItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsBasicPayToolStripMenuItem.Click, IsAnnualizeToolStripMenuItem.Click, Is13MonthToolStripMenuItem.Click, IsAdjustmentToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        Try
            Select Case CType(sender, ToolStripMenuItem).Name
                Case IsBasicPayToolStripMenuItem.Name
                    CheckUncheck(IsBasicPayToolStripMenuItem.Text)
                Case IsAnnualizeToolStripMenuItem.Name
                    CheckUncheck(IsAnnualizeToolStripMenuItem.Text)
                Case Is13MonthToolStripMenuItem.Name
                    CheckUncheck(Is13MonthToolStripMenuItem.Text)
                Case IsAdjustmentToolStripMenuItem.Name
                    CheckUncheck(IsAdjustmentToolStripMenuItem.Text)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CheckUncheck(ByVal Column As String, Optional ByVal IsCheck As Boolean = True)
        Cursor = Cursors.WaitCursor
        Try
            If IsCheck Then
                With lf.MainView
                    'For Each dd As DataGridViewRow In .Rows
                    '    dd.Cells(Column).Value = True
                    'Next

                    'robbie idatalistgrid
                    Dim dt As DataTable
                    dt = CType(lf.DataSource, DataTable)
                    For Each dd As DataRow In dt.Rows
                        dd.Item(Column) = True
                        'dd.Item(tPayroll.Field.IsAnnualize.ToString) = False
                        'dd.Item(tPayroll.Field.Is13Month.ToString) = False
                        'dd.Item(tPayroll.Field.IsAdjustment.ToString) = False
                    Next


                End With
            Else
                Dim dt As DataTable
                dt = CType(lf.DataSource, DataTable)
                For Each dd As DataRow In dt.Rows
                    dd.Item(tPayroll.Field.IsBasicPay.ToString) = False
                    dd.Item(tPayroll.Field.IsAnnualize.ToString) = False
                    dd.Item(tPayroll.Field.Is13Month.ToString) = False
                    dd.Item(tPayroll.Field.IsAdjustment.ToString) = False
                Next
            End If
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnUncheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUncheckAll.Click
        CheckUncheck("", False)
    End Sub

End Class