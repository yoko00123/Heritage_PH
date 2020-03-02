Friend Class GSDetailDataGridView
    Inherits GSCOM.UI.GSDataGridView.GSDataGridView
    Public DetailInfo As DetailClass
    Public DetailInfo2 As DetailClass 'EMIL 20130305--2nd Detail Tab
    Private mRowIndex As Integer = -100

    Public Sub New()
        MyBase.New()
        With Me
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .AllowUserToResizeRows = False
            .AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            .ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .Dock = System.Windows.Forms.DockStyle.Fill
            .EvenBackColor = System.Drawing.Color.White
            .Font = New System.Drawing.Font("Verdana", 7.0!)
            .GridColor = Color.Silver
            .Location = New System.Drawing.Point(0, 0)
            .MultiSelect = False
            '.RowTemplate.Height = 22
            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .Size = New System.Drawing.Size(332, 334)
            .OddBackColor = Color.WhiteSmoke
            'ROBBIE 20060911 ---------------------------------\
            .AutoGenerateColumns = False
            'ROBBIE 20060911 ---------------------------------/
            .Dock = DockStyle.Fill
        End With
    End Sub

    Private Sub GSDetailDataGridView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellClick

    End Sub

    Private Sub GSDetailDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        If e.ColumnIndex >= 0 Then
            Try
                If e.DesiredType IsNot GetType(System.Boolean) Then
                    If Not (TypeOf Me.Columns(e.ColumnIndex) Is DataGridViewCheckBoxColumn) Then 'MUST CHECK e.DESIREDTYPE IS NOT RELIABLE, THE COLUMN HAS TEXT VALUE WHEN INVISIBLE
                        If IsNumeric(e.Value) Then
                            If Me.Columns(e.ColumnIndex).ValueType IsNot GetType(String) Then
                                If CDec(e.Value) = 0 Then
                                    e.Value = "-"
                                End If
                            End If
                        End If
                    End If
                End If
                
            Catch ex As Exception
                Console.WriteLine(e.Value.ToString) 'ROBBIE 20070802
            End Try

            Try
                If IsNothing(Me.DataSource) Then Exit Sub '??????????????????????? 20110106
                Dim dc As DataColumn = Nothing
                If TypeOf (Me.Columns(e.ColumnIndex)) Is GSCOM.UI.DataLookUp.DataGridViewLookUpColumn Then
                    Dim s As String
                    s = CType(Me.Columns(e.ColumnIndex), GSCOM.UI.DataLookUp.DataGridViewLookUpColumn).ColumnName
                    dc = CType(Me.DataSource, DataTable).Columns(s)
                Else
                    If Me.DataSource IsNot Nothing Then
                        dc = CType(Me.DataSource, DataTable).Columns(Me.Columns(e.ColumnIndex).DataPropertyName)
                    End If
                End If
                If dc IsNot Nothing Then
                    If dc.DataType IsNot GetType(Boolean) Then
                        If Not dc.AllowDBNull Then
                            'If e.RowIndex Mod 2 = 1 Then
                            e.CellStyle.BackColor = GSCOM.Common.DefaultRequiredFieldBackColor
                            'Else
                            '    e.CellStyle.BackColor = GSCOM.Common.DefaultRequiredFieldOddBackColor
                            'End If
                        End If
                    End If
                    If dc.ReadOnly Or Me.Columns(e.ColumnIndex).ReadOnly Then
                        'If e.RowIndex Mod 2 = 1 Then
                        e.CellStyle.BackColor = GSCOM.Common.DefaultReadOnlyFieldBackColor
                        'Else
                        '    e.CellStyle.BackColor = GSCOM.Common.DefaultReadOnlyFieldOddBackColor
                        'End If
                    End If
                End If

                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim dgic As DataGridViewColumn = dgv.Columns(e.ColumnIndex)
                If TypeOf dgic Is DataGridViewImageColumn Then
                    If dgic.Name = "Open" Then
                        Dim grdCell As DataGridViewImageCell = CType(dgv.Item(e.ColumnIndex, e.RowIndex), DataGridViewImageCell)
                        If grdCell.Value Is grdCell.DefaultNewRowValue Then
                            e.Value = Nothing
                        End If
                    End If
                End If


            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GSDetailDataGridView_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Me.ColumnHeaderMouseClick

    End Sub

 
    Private Sub GSDetailDataGridView_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.CurrentCellChanged
        'If Me.CurrentCell IsNot Nothing Then
        '    If Me.CurrentCell.RowIndex <> mRowIndex Then
        '        Dim drv As DataRowView
        '        drv = CType(Me.CurrentRow.DataBoundItem, DataRowView)
        '        If drv IsNot Nothing AndAlso drv.Row IsNot Nothing Then
        '            If DetailInfo IsNot Nothing Then
        '                DetailInfo.UpdateDetail(drv.Row)
        '            End If
        '        End If
        '        mRowIndex = Me.CurrentCell.RowIndex
        '    End If
        'End If
        If Me.CurrentCell IsNot Nothing Then
            If DetailInfo IsNot Nothing Then
                Dim drv As DataRowView
                drv = TryCast(Me.CurrentRow.DataBoundItem, DataRowView)
                Dim dr As DataRow = Nothing
                If drv IsNot Nothing Then dr = drv.Row
                'EMIL 20130305---2 Detail Tab
                If Not DetailInfo Is Nothing Then DetailInfo.UpdateDetail(dr)
                If Not DetailInfo2 Is Nothing Then DetailInfo2.UpdateDetail(dr)
                'EMIL 20130305---2 Detail Tab
            End If
            mRowIndex = Me.CurrentCell.RowIndex
        End If
    End Sub

    Private Sub GSDetailDataGridView_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles Me.RowsRemoved
        'mRowIndex = -1 'ex. rowindex 0 is deleted, rowindex 1 will become rowindex 0. update detail in GSDetailDataGridView_CurrentCellChanged will not run
    End Sub

    Private Sub row()
        Throw New NotImplementedException
    End Sub

End Class
