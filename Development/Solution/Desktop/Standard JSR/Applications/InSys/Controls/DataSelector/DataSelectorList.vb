Option Explicit On
Option Strict On



Friend Class DataSelectorList
    Inherits GSCOM.UI.DataList.DataListBase

    Friend mGrid As DataSelectorWithList

    Public Sub New()
        MyBase.New()
        mGrid = New DataSelectorWithList
        MyBase.InitGrid(mGrid)
        mGrid.Dock = DockStyle.Fill
        mGrid.BringToFront()
        Me.Mode = ViewMode.Selection
        'mGrid.AllowUserToAddRows = False
        'mGrid.AllowUserToDeleteRows = False
        'mGrid.AllowUserToResizeRows = False
        'mGrid.ReadOnly = True
        GroupCountLabel.Visible = True
        GroupCountBox.Visible = True
        SelectionModeButton.Checked = True
        mGrid.Visible = True
        mGrid.BringToFront()
    End Sub

    Public Sub Go()
        mGrid.Populate()
    End Sub

    'Public ReadOnly Property ImageKeys() As Collection
    '    Get
    '        Return mGrid.ImageKeys
    '    End Get
    'End Property


    Public Overrides Property ImageList() As ImageList
        Get
            Return mGrid.ImageList

        End Get
        Set(ByVal value As ImageList)
            mGrid.ImageList = value
        End Set
    End Property


    'Public Overrides Property ImageKey() As String
    '    Get
    '        Return mGrid.ImageKey
    '    End Get
    '    Set(ByVal value As String)
    '        mGrid.ImageKey = value
    '    End Set
    'End Property


    'Public Overrides Property GroupImageKey() As String
    '    Get
    '        Return mGrid.GroupImageKey
    '    End Get
    '    Set(ByVal value As String)
    '        mGrid.GroupImageKey = value
    '    End Set
    'End Property

    '''''''''''''''''''''

    Private Sub GroupCountBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GroupCountBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            mGrid.Populate()
        End If
    End Sub

    'Public Property GroupCount() As Integer
    '    Get
    '        Return mGrid.GroupCount
    '    End Get
    '    Set(ByVal value As Integer)
    '        If mGrid IsNot Nothing Then
    '            mGrid.GroupCount = value
    '            'somecontrols are inaccessible when datalist is closed
    '            'so dont rely on textbox
    '            'had problem on saving settings
    '            GroupCountBox.Text = mGrid.GroupCount.ToString
    '        End If
    '    End Set
    'End Property

    'Private Sub GroupCountBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupCountBox.TextChanged
    '    If IsNumeric(GroupCountBox.Text) Then
    '        GroupCount = CInt(GroupCountBox.Text)
    '    Else
    '        GroupCount = 0
    '    End If
    'End Sub

    'Public Overrides Function GetSelectedIDs() As Integer()
    '    Return  mGrid.GetSelectedIDs
    'End Function


    'Public Sub CheckNodes(ByVal pCheckedTable As DataTable, ByVal pColumnName As String)
    '    mGrid.CheckNodes(pCheckedTable, pColumnName)
    'End Sub



    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'MainFilter
        '
        Me.MainFilter.Size = New System.Drawing.Size(168, 512)
        '
        'DataSelectorList
        '
        Me.Name = "DataSelectorList"
        Me.ResumeLayout(False)

    End Sub
End Class
