Friend Class TreeViewDataList
    Inherits DataListControl

    Friend WithEvents mMainView As New DataTreeView


    Public Sub New()
        MyBase.New()
        mMainView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        mMainView.Dock = System.Windows.Forms.DockStyle.Fill
        mMainView.Location = New System.Drawing.Point(0, 0)
        mMainView.Name = "MainView"
        mMainView.Size = New System.Drawing.Size(332, 20)
        mMainView.TabIndex = 8
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(mMainView)
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(MyBase.MainFilter)
        MyCaption.BackColor = Color.SteelBlue

        mMainView.ImageList = gImageList
        mMainView.Groups.Add("ID", "Item", "Item", "_item.png")

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
                        'Dim s As String = ""
                        'For Each dr As DataRow In mDataSource.Rows
                        '    s &= dr.Item("Text").ToString & vbCrLf
                        'Next
                        'mMainView.Text = s.TrimEnd
                        mMainView.DataSource = mDataSource
                        mMainView.Populate()
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Set
    End Property
End Class
