Friend Class TextDataList
    Inherits DataListControl

    Friend WithEvents mMainView As New System.Windows.Forms.TextBox

    Private mSaveButton As ToolStripButton

    Public Sub New()
        MyBase.New()
        mMainView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        mMainView.Dock = System.Windows.Forms.DockStyle.Fill
        mMainView.Location = New System.Drawing.Point(0, 0)
        mMainView.Name = "MainView"
        mMainView.Size = New System.Drawing.Size(332, 20)
        mMainView.TabIndex = 8
        mMainView.Multiline = True
        mMainView.ReadOnly = True
        mMainView.Font = New Font("Courier New", 8)
        mMainView.ScrollBars = ScrollBars.Both
        mMainView.WordWrap = False
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(mMainView)
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(MyBase.MainFilter)
        MyCaption.BackColor = Color.SteelBlue
        mSaveButton = New ToolStripButton("Save", Nothing, AddressOf Save)
        MyBase.ToolStrip1.Items.Add(mSaveButton)
    End Sub

    Private Sub Save(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If (Not (DataSource Is Nothing)) Then
                If DataSource.Rows.Count > 0 Then
                    Dim MyDialog As New SaveFileDialog()
                    MyDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                    MyDialog.FileName = ""
                    MyDialog.FilterIndex = 1
                    MyDialog.CheckFileExists = False
                    MyDialog.CheckPathExists = True
                    If (MyDialog.ShowDialog() = DialogResult.OK) Then
                        SaveFile(MyDialog.FileName)
                    End If
                Else
                    MsgBox("No record found.", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub SaveFile(ByVal FileName As String)
        Dim fnum As Integer
        Try
            fnum = FreeFile()
            FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
            FileSystem.Print(fnum, mMainView.Text)
            MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
        Finally
            FileClose(fnum)
        End Try
    End Sub

    Public Overrides Property DataSource() As DataTable 'Implements GSCOM.Interfaces.ZIDataList.DataSource
        Get
            DataSource = mDataSource
        End Get
        Set(ByVal Value As DataTable)
            Try
                mDataSource = Value
                If mInited Then
                    If Not mDataSource Is Nothing Then
                        Dim s As String = ""
                        For Each dr As DataRow In mDataSource.Rows
                            s &= dr.Item("Text").ToString & vbCrLf
                        Next
                        mMainView.Text = s.TrimEnd
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Set
    End Property

End Class