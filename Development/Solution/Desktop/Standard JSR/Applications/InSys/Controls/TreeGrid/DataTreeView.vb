Option Explicit On
Option Strict On
Imports System.Collections.Generic

Friend Class DataTreeView
    Inherits TreeView

    Public Columns As TreeGridColumnCollection
    Public GridColor As Color = Color.Gray
    Public Groups As New TreeGridGroupCollection
    Public TextWidth As Integer
    Public DataSourceFilter As String

    Public AllNodes As New TreeGridNodeCollection

    Private mCurrentCell As TreeGridCell

    Public Property CurrentCell() As TreeGridCell
        Get
            Return mCurrentCell
        End Get
        Set(ByVal value As TreeGridCell)
            mCurrentCell = value
            For Each n As TreeGridNode In Me.AllNodes
                For Each c As TreeGridCell In n.Cells
                    c.mSelected = False
                Next
            Next
            If mCurrentCell IsNot Nothing Then
                mCurrentCell.mSelected = True
            End If
        End Set
    End Property

    Private mDataSource As DataTable
    Public mDataIDs As New Collections.Specialized.StringCollection


    Public Property DataSource() As DataTable
        Get
            Return mDataSource
        End Get
        Set(ByVal value As DataTable)
            mDataSource = value
            If Me.DataSource Is Nothing Then
                'If Me.BindingContext(mDataSource) IsNot Nothing Then
                '    myCurrencyManager = TryCast(Me.BindingContext(mDataSource), CurrencyManager)
                '    '    If myCurrencyManager IsNot Nothing Then
                '    myCurrencyManager.Position = 0
                '    'End If
                'End If
            End If

        End Set
    End Property

    Private mListSource As DataTable
    Public Property ListSource() As DataTable
        Get
            Return mListSource
        End Get
        Set(ByVal value As DataTable)
            mListSource = value
        End Set
    End Property


    Sub CleanUp(ByVal pIDs As Collections.Specialized.StringCollection, ByVal pColumnName As String)
        If Me.DataSource IsNot Nothing Then
            For Each dr As DataRow In Me.DataSource.Select
                If dr.RowState = DataRowState.Added Then
                    If pIDs Is Nothing Then
                        Me.DataSource.Rows.Remove(dr)
                    Else
                        If Not pIDs.Contains(CStr(dr(pColumnName))) Then
                            Me.DataSource.Rows.Remove(dr)
                        End If
                    End If
                End If
            Next

        End If
    End Sub

    Public Shadows Sub ExpandAll()
        MyBase.ExpandAll()
        If Me.Nodes.Count > 0 Then
            Me.Nodes(0).EnsureVisible()
        End If
    End Sub

    Public Sub RetainChecked()
        If Me.CheckBoxes Then
            For Each n As TreeGridNode In Me.AllNodes
                If n.Databound Then
                    If (Not n.Checked) Then
                        n.Row.Delete()
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub RetainIDs()
        If Me.CheckBoxes Then
            mDataIDs.Clear()
            For Each n As TreeGridNode In Me.AllNodes
                If n.Databound Then
                    If n.Checked Then
                        mDataIDs.Add(n.Name)
                    End If
                End If
            Next
        End If
    End Sub

    Protected Overrides Sub OnDrawNode(ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs)
        If Me.DrawMode = TreeViewDrawMode.OwnerDrawText Then

            Dim bBrush As New SolidBrush(Color.Black)
            Dim P As New Pen(GridColor)
            Try

                Dim TextRect As Rectangle
                Dim vSelected As Boolean
                Dim n As TreeGridNode
                n = DirectCast(e.Node, TreeGridNode)
                TextRect = New Rectangle(e.Node.Bounds.Left, e.Bounds.Top + 2, TextWidth, e.Bounds.Height)
                vSelected = CBool(e.State And TreeNodeStates.Selected)
                If vSelected And Me.Focused Then bBrush.Color = Color.White
                e.Graphics.DrawString(e.Node.Text, Me.Font, bBrush, TextRect.Left, TextRect.Top)  'draw the text
                'If IsNumeric(e.Node.Name) Then
                If DirectCast(e.Node, TreeGridNode).Row IsNot Nothing Then
                    Dim dy As Integer = n.Bounds.Height
                    If n.IsLastSibling Then dy -= 1
                    If Me.Columns.Count > 0 Then
                        e.Graphics.DrawRectangle(P, New Rectangle(e.Node.Bounds.Left, n.Bounds.Top, TextRect.Width, dy)) 'draw the outline
                    End If

                    For Each c As TreeGridCell In n.Cells
                        c.Draw(e.Graphics, P, bBrush)
                    Next
                End If
            Catch ex As Exception
                MsgBox(ex.Message & " [Draw]")
            Finally
                bBrush.Dispose()
                P.Dispose()
            End Try
        Else
            MyBase.OnDrawNode(e)
        End If

    End Sub

    Private Sub AddNode(ByVal nc As TreeNodeCollection, ByVal n As TreeGridNode)
        'Dim col As TreeGridColumn
        'For i As Integer = 0 To Me.Columns.Count - 1
        '    col = Me.Columns(i)
        '    cell.Column = col
        '    cell.Node = n
        '    n.Cells.Add(cell)

        'Next
        Dim cell As New TreeGridCell
        For Each col As TreeGridColumn In Me.Columns
            cell = New TreeGridCell
            cell.Column = col
            cell.Node = n
            n.Cells.Add(cell)
        Next

        nc.Add(n)
        AllNodes.Add(n)
    End Sub

    'Public Shadows Property SelectedNode() As TreeGridNode
    '    Get
    '        Return CType(MyBase.SelectedNode, TreeGridNode)
    '    End Get
    '    Set(ByVal value As TreeGridNode)
    '        MyBase.SelectedNode = value
    '    End Set
    'End Property

    'Public Shadows Property PrevNode() As TreeGridNode
    '    Get
    '        Return CType(MyBase.PrevvisibleNode, TreeGridNode)
    '    End Get
    '    Set(ByVal value As TreeGridNode)
    '        MyBase.SelectedNode = value
    '    End Set
    'End Property

    'Public Shadows Property Nodes(ByVal index As Integer) As TreeGridNode
    '    Get
    '        Return DirectCast(MyBase.Nodes(index), TreeGridNode)
    '    End Get
    '    Set(ByVal value As TreeGridNode)
    '        MyBase.Nodes(index) = value
    '    End Set
    'End Property

    'Public Shadows Property Nodes() As TreeGridNode
    '    Get
    '        Return DirectCast(MyBase.Nodes(index), TreeGridNode)
    '    End Get
    '    Set(ByVal value As TreeGridNode)
    '        MyBase.Nodes(index) = value
    '    End Set
    'End Property



    Public ReadOnly Property PrevDataboundNode() As TreeGridNode
        Get
            If MyBase.Nodes.Count > 0 Then
                Dim n As TreeGridNode
                If Me.SelectedNode Is Nothing Then
                    n = DirectCast(Me.Nodes(0), TreeGridNode)
                    If n.Row IsNot Nothing Then
                        Return n
                    End If
                Else
                    n = DirectCast(Me.SelectedNode, TreeGridNode)
                End If
                Do While n.PrevVisibleNode IsNot Nothing
                    n = DirectCast(n.PrevVisibleNode, TreeGridNode)
                    If n.Row IsNot Nothing Then
                        Return n
                    End If
                Loop
                Return Nothing
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property NextDataboundNode() As TreeGridNode
        Get
            If MyBase.Nodes.Count > 0 Then
                Dim n As TreeGridNode
                If Me.SelectedNode Is Nothing Then
                    n = DirectCast(Me.Nodes(0), TreeGridNode)
                    If n.Row IsNot Nothing Then
                        Return n
                    End If
                Else
                    n = DirectCast(Me.SelectedNode, TreeGridNode)
                End If
                Do While n.NextVisibleNode IsNot Nothing
                    n = DirectCast(n.NextVisibleNode, TreeGridNode)
                    If n.Row IsNot Nothing Then
                        Return n
                    End If
                Loop
                Return Nothing
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        TextWidth = 200
        'columns are dependent on textwidth
        Columns = New TreeGridColumnCollection(Me)


        'Me.Graphics = Me.CreateGraphics
        Me.HideSelection = False

        Me.DrawMode = TreeViewDrawMode.OwnerDrawText

        'Me.DrawMode = TreeViewDrawMode.Normal
        Me.ItemHeight = 18
        Me.FullRowSelect = True
        Me.ShowLines = False
    End Sub
    'Private Sub DeleteNode(ByVal nc As TreeNodeCollection)
    '    Try
    '        For Each n As TreeNode In nc
    '            DeleteNode(n.Nodes)
    '            nc.Remove(n)
    '        Next
    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try
    'End Sub
    Public Sub Populate()
        'Me.Visible = False
        Try
            For Each n As TreeGridNode In Me.AllNodes
                n.Row = Nothing
            Next
            If Me.DrawMode = TreeViewDrawMode.OwnerDrawText Then
                Me.DrawMode = TreeViewDrawMode.Normal 'TREEVIEW HAS ERROR!! DAMN M$!
                Me.Nodes.Clear() '.RemoveByKey("g")
                Me.DrawMode = TreeViewDrawMode.OwnerDrawText 'TREEVIEW HAS ERROR!! DAMN M$!
            Else
                Me.Nodes.Clear() '.RemoveByKey("g")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)


        End Try
        If DataSource IsNot Nothing Then
            Dim n As New TreeGridNode
            With n
                .Name = "g"
                .Text = "All"
                .ImageKey = "folder.png"
            End With
            Me.AddNode(Me.Nodes, n)
            n.SelectedImageKey = n.ImageKey
            PopulateTreeView(n.Nodes, DataSource, True, "", "", Nothing)
            Me.Nodes(0).ExpandAll()
            Me.Nodes(0).EnsureVisible()
        End If
        'Me.Visible = True
    End Sub

    Private mRowFilter As String

    Public Property RowFilter() As String
        Get
            Return mRowFilter
        End Get
        Set(ByVal value As String)
            mRowFilter = value
        End Set
    End Property




    'Public Sub KeepChecked(ByVal pColumn As String)
    '    Me.mDataIDs.Clear()
    '    For Each dr2 As DataRowView In Me.DataSource.DefaultView

    '        Me.mDataIDs.Add(dr2(pColumn).ToString)
    '    Next
    'End Sub

#Region "PopulateTreeView"

    Private Sub PopulateTreeView(ByVal nc As TreeNodeCollection, ByVal dt As DataTable, ByVal pExpandAll As Boolean, ByVal pFilter As String, ByVal pGroupID As Object, ByVal pGroupRow As TreeGridGroup)
        Dim n As TreeGridNode
        Dim a As New List(Of String)
        Dim drx As DataRow
        Dim s As String = ""
        Dim b As String = ""
        Dim pGroupName As String
        Dim dr As TreeGridGroup
        Dim vSort As String
        'Dim vAllIsChecked As Boolean = True
        Try


            If pFilter = RowFilter Then
                nc.Clear()
            End If
            If pGroupRow Is Nothing Then
                If Groups.Count = 0 Then Exit Sub
                dr = Groups(0)
            Else
                dr = Groups(pGroupRow.Index + 1)
            End If
            pGroupName = dr.Name
            vSort = dr.Sort & IIf(dr.Sort = "", "", ",").ToString & dr.Text
            a = GSCOM.Common.GetDistinctObjects(Of String)(mDataSource.Select(pFilter, vSort), pGroupName)
            For Each ctr As Object In a
                If IsDBNull(ctr) Then
                    b = pGroupName & " IS NULL"
                Else
                    b = pGroupName & "=" & GSCOM.SQL.SQLFormat(ctr)
                End If
                drx = mDataSource.Select(b, vSort)(0)
                n = New TreeGridNode()
                With n
                    If IsDBNull(ctr) Then
                        .Text = "(Unspecified)"
                    Else
                        .Text = drx(dr.Text).ToString
                    End If
                    .ImageKey = dr.ImageKey
                    .SelectedImageKey = .ImageKey
                    If dr.Index = Groups.Count - 1 Then
                        .Name = drx(pGroupName).ToString
                        .Row = drx

                        If Me.CheckBoxes Then
                            'Select .Row.RowState
                            '    Case DataRowState.Unchanged ', DataRowState.Modified
                            '        .Checked = True
                            '    Case Else
                            '        'If .Row.
                            '        '    .Checked = True
                            'End Selectn
                            'If Me.ListSource IsNot Nothing Then
                            '    If Me.ListSource.Select("ID=" & .Name).Length = 0 Then
                            '        .Checked = True
                            '    End If
                            'End If

                            'If  .Row.RowState = DataRowState.Unchanged Then
                            If mDataIDs.Contains(n.Name) Or
                                .Row.RowState = DataRowState.Unchanged Then
                                n.Checked = True
                            End If

                        End If
                    Else
                        .Name = "g" & pGroupName & drx(pGroupName).ToString
                    End If
                End With
                Me.AddNode(nc, n)
                s = ""
                If pFilter <> "" Then
                    s = pFilter & " AND "
                End If
                s &= b
                If dr.Index < Groups.Count - 1 Then
                    PopulateTreeView(n.Nodes, dt, pExpandAll, s, drx(pGroupName), dr)
                End If
                'vAllIsChecked = vAllIsChecked And n.Checked
                'pn.Checked = vAllIsChecked
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    Protected Overrides Sub OnAfterSelect(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        MyBase.OnAfterSelect(e)
        Dim dr As DataRow
        dr = CType(e.Node, TreeGridNode).Row
        Dim myCurrencyManager As CurrencyManager
        myCurrencyManager = CType(Me.BindingContext(mDataSource), CurrencyManager)
        If dr IsNot Nothing Then
            Dim drv As DataRowView
            For iRow As Integer = 0 To mDataSource.DefaultView.Count - 1
                drv = mDataSource.DefaultView(iRow)
                If drv.Row Is dr Then
                    myCurrencyManager.Position = iRow
                End If
            Next
            'Else
            '    myCurrencyManager.Position = -1
        End If
        '''''''''''''''''''''''''''''''''''''
        Dim n As TreeGridNode
        n = DirectCast(Me.SelectedNode, TreeGridNode)
        If n.Databound AndAlso n.Cells.Count > 0 Then
            Me.CurrentCell = n.Cells(0)
        Else
            Me.CurrentCell = Nothing
        End If


    End Sub

    Private mEditing As Boolean

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        '        If mEditing Then
        '            Select Case e.KeyCode
        '                Case Keys.Up, Keys.Down
        '                    GoTo L1
        '                Case Else
        '                    e.Handled = True

        '            End Select
        '        Else
        'L1:
        '            MyBase.OnKeyDown(e)
        '        End If
        Select Case e.KeyCode
            Case Keys.F2
                If Me.CurrentCell IsNot Nothing Then
                    Me.CurrentCell.BeginEdit()
                    e.Handled = True
                End If
                'Case Keys.Up, Keys.Down
                'Case Keys.Space
                '    e.Handled = True
            Case Else

        End Select
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        Select Case e.KeyChar
            Case Chr(Keys.Enter)

            Case Else
                If Me.CurrentCell IsNot Nothing Then
                    Me.CurrentCell.BeginEdit(e.KeyChar)
                    e.Handled = True
                Else
                    MyBase.OnKeyPress(e)
                End If
        End Select
    End Sub

    Private myCurrencyManager As CurrencyManager

    Protected Overrides Sub OnAfterCheck(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        MyBase.OnAfterCheck(e)
        If e.Action = TreeViewAction.ByKeyboard Or e.Action = TreeViewAction.ByMouse Then
            e.Node.TreeView.SelectedNode = e.Node
            Application.DoEvents()
            GSCOM.UI.CheckAllNodeCheckBox(e.Node.Nodes, e.Node.Checked)
        End If

    End Sub

    'Protected Overrides Sub OnNodeMouseClick(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    '    MyBase.OnNodeMouseClick(e)
    '    GSCOM.UI.CheckAllNodeCheckBox(e.Node.Nodes, e.Node.Checked)
    'End Sub
    'Protected Overrides Sub OnNodeMouseDoubleClick(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    '    MyBase.OnNodeMouseDoubleClick(e)
    '    GSCOM.UI.CheckAllNodeCheckBox(e.Node.Nodes, e.Node.Checked)
    'End Sub

    'Private Sub DataTreeView_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    '    Dim n As TreeNode
    '    n = Me.SelectedNode
    '    If n IsNot Nothing Then
    '        Application.DoEvents()
    '        GSCOM.UI.CheckAllNodeCheckBox(n.Nodes, n.Checked)

    '    End If
    'End Sub

    'Private Sub DataTreeView_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles Me.BeforeCheck
    '    If e.Action = TreeViewAction.ByKeyboard Then

    '    End If
    'End Sub

    'Private Sub LoadTree(ByVal dt As DataTable, ByVal pFilter As String, ByVal pLoadList As Boolean)
    '    'ROBBIE 20091013
    '    Dim s As String
    '    Dim tg As DataTreeView
    '    Dim a, n As String
    '    Dim vList As DataTable
    '    Dim drNew As DataRow
    '    Dim dr As DataRow
    '    Dim dra2 As DataRow()
    '    tg = Me
    '    If tg IsNot Nothing Then

    '        tg.DataSourceFilter = pFilter

    '        s = "SELECT * FROM "
    '        n = dr("TableName").ToString
    '        n = "v" & Strings.Right(n, n.Length - 1)
    '        s &= dr("ListSource").ToString & " a where not exists (SELECT * FROM " & n & " b where "
    '        s &= pFilter

    '        dra2 = mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND ListKey=1")
    '        If dra2.Length > 0 Then
    '            Dim q As String = ""
    '            For Each dr2 As DataRow In dra2
    '                n = dr2("Name").ToString
    '                a = dr2("ListColumn").ToString
    '                If a = "" Then a = n
    '                q &= "b." & n & "=" & "a." & a & " AND "
    '            Next
    '            q = Strings.Left(q, q.Length - 5) ' " AND "
    '            s &= " AND " & q
    '        End If
    '        s &= ")"
    '        If pLoadList Then

    '            vList = GSCOM.SQL.TableQuery(s, gConnection)
    '            'tg.DataSource = Me.Dataset.Tables(dr("TableName").ToString)

    '            For Each dr2 As DataRow In vList.Select
    '                drNew = tg.DataSource.NewRow
    '                'For Each dr3 As DataRow In mtMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND (CopyFromList=1 OR (IsGroup=1 AND Name<>'ID') OR (ListKey=1))")
    '                For Each dr3 As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND (CopyFromList=1) AND (Name<>'ID')")
    '                    n = dr3("Name").ToString
    '                    a = dr3("ListColumn").ToString
    '                    If a = "" Then a = n
    '                    drNew(n) = dr2(a)
    '                Next

    '                'For Each dr3 As DataRow In mtMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND Name<>'ID'")
    '                '    n = dr3("Name").ToString
    '                '    drNew(n) = dr2(n)
    '                'Next
    '                'For Each dr3 As DataRow In mtMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND IsGroup=1")
    '                '    n = dr3("Text").ToString
    '                '    drNew(n) = dr2(n)
    '                'Next



    '                tg.DataSource.Rows.Add(drNew)
    '            Next
    '        End If

    '        tg.Populate()
    '    End If



    'End Sub

End Class


