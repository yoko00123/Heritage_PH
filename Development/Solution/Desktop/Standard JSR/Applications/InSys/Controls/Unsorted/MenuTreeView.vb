Option Explicit On
Option Strict On

Friend Class MenuTreeView
    Inherits TreeView

    Public mDataSource As DataTable
    Public DataDestination As DataTable
    Public ValueMember As String
    Public ShowAllNode As Boolean = True

    Public Property DataSource() As DataTable
        Get
            Return mDataSource
        End Get
        Set(ByVal value As DataTable)
            mDataSource = value
            Me.Nodes.Clear()
            Dim tn As TreeNode
            If ShowAllNode Then
                tn = Me.Nodes.Add("All")
                tn.ImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile)
                tn.SelectedImageKey = tn.ImageKey
                GSCOM.UI.Common.PopulateTreeView(tn.Nodes, DataSource, DBNull.Value, True, True)
            Else
                GSCOM.UI.Common.PopulateTreeView(Me.Nodes, DataSource, DBNull.Value, True, True)
            End If
        End Set
    End Property

    'Public Sub Go()
    '    DataSource = DataSource
    'End Sub

    Public Sub New()
        Me.CheckBoxes = True
        Me.ImageList = gImageList
        Me.ItemHeight = Me.ImageList.ImageSize.Height + 2
        Me.ShowLines = False
    End Sub

    Public Sub CheckNodes()
        GSCOM.UI.Common.CheckNodes(Me.Nodes, DataDestination, ValueMember)
    End Sub

    Public Function CheckedNodes() As Specialized.StringCollection
        Dim sa As New Specialized.StringCollection
        GSCOM.UI.Common.CheckedNodes(Me.Nodes, sa)
        Return sa
    End Function

    Public Function UnCheckedNodes() As Specialized.StringCollection
        Dim sa As New Specialized.StringCollection
        GSCOM.UI.Common.UnCheckedNodes(Me.Nodes, sa)
        Return sa
    End Function

    Public Sub EndEdit()
        'Dim dr As DataRow
        'Dim sa As New Specialized.StringCollection
        'For Each dr In DataDestination.Select("", "", DataViewRowState.CurrentRows)
        '    dr.Delete()
        'Next
        'GSCOM.UI.Common.CheckedNodes(Me.Nodes, sa)
        'If sa IsNot Nothing Then
        '    For Each s As String In sa
        '        dr = DataDestination.NewRow
        '        dr.Item(ValueMember) = s
        '        DataDestination.Rows.Add(dr)
        '    Next
        'End If
        Dim u As Specialized.StringCollection = Me.UnCheckedNodes
        Dim dra As DataRow() = DataDestination.Select("", "", DataViewRowState.CurrentRows)
        Dim s As String
        For Each drx As DataRow In dra
            s = drx.Item(ValueMember).ToString
            If u.Contains(s) Then
                drx.Delete()
            End If
        Next
        dra = DataDestination.Select("", "", DataViewRowState.CurrentRows)
        Dim sc As Specialized.StringCollection = GSCOM.Common.GetDistinctStrings(dra, ValueMember)
        Dim c As Specialized.StringCollection = Me.CheckedNodes
        Dim dr As DataRow
        For Each a As String In c
            If (Not sc.Contains(a)) Then
                dr = DataDestination.NewRow
                dr.Item(ValueMember) = a
                DataDestination.Rows.Add(dr)
            End If
        Next
    End Sub

    Private Sub MenuTreeView_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterCheck
        GSCOM.UI.CheckAllNodeCheckBox(e.Node.Nodes, e.Node.Checked)
    End Sub

End Class