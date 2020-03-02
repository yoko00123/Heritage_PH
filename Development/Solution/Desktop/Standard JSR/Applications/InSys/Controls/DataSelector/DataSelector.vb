Option Explicit On
Option Strict On
Imports System.Collections.Generic

Public Class DataSelector

    Private mRowFilter As String

    Public Sub ExpandAll()
        MainTree.ExpandAll()
    End Sub

    Public Sub CollapseAll()
        MainTree.CollapseAll()
    End Sub

    Public Sub RetainChecked()
        If MainTree.CheckBoxes Then
            'For Each n As TreeNode In MainTree.TopNode.
            '    'If n.TreeView Then
            '    If (Not n.Checked) Then
            '        n.Row.Delete()
            '    End If
            '    'End If
            'Next

            For Each n1 As TreeNode In MainTree.Nodes
                'n1.Expand()
                For Each n2 As TreeNode In n1.Nodes
                    If (n2.Checked) Then

                        'n2.Expand()
                    Else
                        n2.Collapse()

                    End If
                Next

            Next
        End If
    End Sub

    Public Function CheckedNodes() As Specialized.StringCollection
        Dim sa As New Specialized.StringCollection
        UI.Common.CheckedNodes(MainTree.Nodes, sa)
        Return sa
    End Function

    Public Function UnCheckedNodes() As Specialized.StringCollection
        Dim sa As New Specialized.StringCollection
        UI.Common.UnCheckedNodes(MainTree.Nodes, sa)
        Return sa
    End Function

    Public Property RowFilter() As String
        Get
            'return mrowfilter
            Return Me.DataSource.DefaultView.RowFilter
        End Get
        Set(ByVal value As String)
            'mRowFilter = value
            Me.DataSource.DefaultView.RowFilter = value
            Me.Go()
        End Set
    End Property

    Private mDataSource As DataTable
    Public Property DataSource() As DataTable
        Get
            Return mDataSource
        End Get
        Set(ByVal value As DataTable)
            mDataSource = value
        End Set
    End Property

    Private mGroupCount As Integer
    Public Property GroupCount() As Integer
        Get
            Return mGroupCount
        End Get
        Set(ByVal value As Integer)
            mGroupCount = value
            'cGroupCount.Text = value.ToString
        End Set
    End Property

    Public Property ImageList() As ImageList
        Get
            Return MainTree.ImageList
        End Get
        Set(ByVal value As ImageList)
            MainTree.ImageList = value
        End Set
    End Property

    Private mImageKey As String
    Public Property ImageKey() As String
        Get
            Return mImageKey
        End Get
        Set(ByVal value As String)
            mImageKey = value
        End Set
    End Property

    Private mGroupImageKey As String
    Public Property GroupImageKey() As String
        Get
            Return mGroupImageKey
        End Get
        Set(ByVal value As String)
            mGroupImageKey = value
        End Set
    End Property

    Public Sub Go()
        If DataSource Is Nothing Then
            MainTree.Nodes.Clear()
        Else
            'If IsNumeric(cGroupCount.Text) Then
            '    mGroupCount = CInt(cGroupCount.Text)
            'Else
            '    GroupCount = 1
            'End If
            Dim i As Integer
            i = mGroupCount
            MainTree.Nodes.Clear()
            Dim n As TreeNode
            n = MainTree.Nodes.Add("All")
            With n
                .ImageKey = mGroupImageKey
                .SelectedImageKey = .ImageKey
            End With
            PopulateTreeView(n.Nodes, DataSource, True, RowFilter, i)
            MainTree.Nodes(0).ExpandAll()

        End If
    End Sub


    Public ImageKeys As New Collection

    Public Sub EndEdit(ByVal pCheckedTable As DataTable, ByVal pColumnName As String)
        Dim u As Specialized.StringCollection = Me.UnCheckedNodes
        Dim dra As DataRow() = pCheckedTable.Select("", "", DataViewRowState.CurrentRows)
        Dim s As String
        For Each drx As DataRow In dra
            s = drx.Item(pColumnName).ToString
            If u.Contains(s) Then
                drx.Delete()
            End If
        Next
        dra = pCheckedTable.Select("", "", DataViewRowState.CurrentRows)
        Dim sc As Specialized.StringCollection = GSCOM.Common.GetDistinctStrings(dra, pColumnName)
        Dim c As Specialized.StringCollection = Me.CheckedNodes
        Dim dr As DataRow
        For Each a As String In c
            If (Not sc.Contains(a)) Then
                dr = pCheckedTable.NewRow
                dr.Item(pColumnName) = a
                pCheckedTable.Rows.Add(dr)
            End If
        Next
    End Sub


#Region "PopulateTreeView"
    Private Sub PopulateTreeView(ByVal nc As TreeNodeCollection, ByVal dt As DataTable, ByVal pExpandAll As Boolean, ByVal pFilter As String, ByRef pGroupCount As Integer)
        Dim n As TreeNode
        Dim dra As DataRow()
        Dim s As String
        Dim dc As String
        Dim al As New List(Of String)
        Dim oa() As Object
        Dim ogc As Integer
        Dim i As Integer
        Dim n2 As TreeNode
        Try
            If pFilter = RowFilter Then
                nc.Clear()
            End If
            ogc = pGroupCount
            dra = dt.Select(pFilter, DataSource.DefaultView.Sort)
            i = (dt.Columns.Count - 1 - mGroupCount) + pGroupCount
            If pGroupCount = 0 Then
                'dra = dt.Select(s)
                For Each fff As DataRow In dra
                    n2 = nc.Add(Go2(fff, i + 1))
                    'n2 = nc.Add(fff("Name").ToString)
                    n2.ImageKey = mImageKey
                    n2.SelectedImageKey = mImageKey
                    n2.Name = fff("ID").ToString
                Next
            Else
                If i >= 0 Then
                    dc = dt.Columns(i).ColumnName
                    al = GSCOM.Common.GetDistinctObjects(Of String)(dra, dc)
                    Try
                        al.Sort() 'sorting fails sometimes maybe bcoz of dbnull 'al.Sort(  )
                    Catch ex As Exception
                    End Try
                    oa = al.ToArray()
                    For Each o As Object In oa
                        s = o.ToString
                        If s = "" Then
                            n = nc.Add("(Unknown)")
                        Else
                            n = nc.Add(s)
                        End If
                        With n
                            '.Name = dr.Item("Id").ToString
                            '.Tag = dr 'Color.FromName(dr.Item("Color").ToString)


                            '.ImageKey = mGroupImageKey
                            If ImageKeys.Contains(dc) Then
                                .ImageKey = ImageKeys(dc).ToString
                            Else
                                .ImageKey = mGroupImageKey
                            End If

                            .SelectedImageKey = .ImageKey


                        End With
                        s = ""
                        If pFilter <> "" Then
                            s = pFilter & " AND "
                        End If
                        If o Is DBNull.Value Then
                            s &= "(" & dc & " IS NULL)"
                        Else
                            s &= "(" & dc & "=" & GSCOM.SQL.SQLFormat(o) & ")"
                        End If
                        pGroupCount -= 1

                        ''20061118
                        'If pExpandAll Then
                        '    'If pFilter = "" Then
                        '    n.ExpandAll()
                        '    'End If
                        'End If

                        If pGroupCount >= 0 Then
                            PopulateTreeView(n.Nodes, dt, pExpandAll, s, pGroupCount)
                        Else
                        End If
                        pGroupCount = ogc
                        If pExpandAll Then
                            'If pFilter = "" Then
                            n.ExpandAll()
                            'End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region


    Private Function Go2(ByVal pRow As DataRow, ByVal pColumnCount As Integer) As String
        Dim s As String = ""
        Dim a As String
        Dim d As String = " | "
        For i As Integer = 0 To pColumnCount - 1
            If pRow.Table.Columns(i).ColumnName = "ID" Then
                'a = Format(pRow(i), "000000")
                'a = pRow(i), "000000")
                a = ""
            Else
                a = pRow(i).ToString
                s &= a & d
            End If
            's &= a & d 'moved
        Next
        If s <> "" Then
            s = Strings.Left(s, s.Length - d.Length)
        End If
        Return s
    End Function

    'Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Go()
    'End Sub

    Private Sub MainTree_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MainTree.AfterCheck
        If mAutoCheckAllNodeCheckBox Then
            GSCOM.UI.CheckAllNodeCheckBox(e.Node.Nodes, e.Node.Checked)
        End If
    End Sub

    Private mAutoCheckAllNodeCheckBox As Boolean = True

    Public Sub CheckNodes(ByVal pCheckedTable As DataTable, ByVal pColumnName As String)
        Dim b As Boolean = mAutoCheckAllNodeCheckBox
        mAutoCheckAllNodeCheckBox = False 'do not trigger autocheck
        UI.Common.CheckNodes(MainTree.Nodes, pCheckedTable, pColumnName)
        mAutoCheckAllNodeCheckBox = b
    End Sub

    Public Function GetSelectedIDs() As Integer()
        Dim sa As New Specialized.StringCollection
        Dim ia() As Integer = Nothing
        UI.Common.CheckedNodes(MainTree.Nodes, sa)
        If sa IsNot Nothing Then
            For Each s As String In sa
                'If s <> "" Then  'ROBBIE 20061214 DO NOT CHECK IF HAS NO NAME. UI.Common.CheckedNodes Should handle this
                If ia Is Nothing Then
                    ReDim ia(0)
                Else
                    Array.Resize(ia, ia.Length + 1)
                End If
                ia(ia.Length - 1) = CInt(s)
                'End If
            Next
        End If
        Return ia
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MainTree.ShowLines = False
    End Sub
End Class

