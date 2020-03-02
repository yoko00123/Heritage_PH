Option Explicit On
Option Strict On

Friend Class TreeGrid
    Inherits DataTreeView

     Public DetailInfo As DetailClass
    Public Const TreeGridPrefix As String = "mTreeGrid"

    Private Sub tv_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MyBase.AfterSelect
        Dim n As TreeGridNode
        n = TryCast(e.Node, TreeGridNode)
        If n IsNot Nothing AndAlso n.Row IsNot Nothing Then
            If DetailInfo IsNot Nothing Then
                DetailInfo.UpdateDetail(n.Row)
            End If
        End If
    End Sub

    Public Property OriginalListSource As String
#Region "Move"
    Private Sub MoveNext(ByVal myCurrencyManager As CurrencyManager)
        If myCurrencyManager.Position = myCurrencyManager.Count - 1 Then
            MessageBox.Show("You're at end of the records")
        Else
            myCurrencyManager.Position += 1
        End If
    End Sub
    Private Sub MoveFirst(ByVal myCurrencyManager As CurrencyManager)
        myCurrencyManager.Position = 0
    End Sub
    Private Sub MovePrevious(ByVal myCurrencyManager As CurrencyManager)
        If myCurrencyManager.Position = 0 Then
            MessageBox.Show("You're at the beginning of the records.")
        Else
            myCurrencyManager.Position -= 1
        End If
    End Sub
    Private Sub MoveLast(ByVal myCurrencyManager As CurrencyManager)
        myCurrencyManager.Position = myCurrencyManager.Count - 1
    End Sub
#End Region
    Public mInfoMenuSet As Database.MenuSetClass
    Dim mmMenuDetailTabRow As Database.MenuDetailTabRow
    Friend Property mMenuDetailTabRow As Database.MenuDetailTabRow
        Get
            Return mmMenuDetailTabRow
        End Get
        Set(ByVal value As Database.MenuDetailTabRow)
            mmMenuDetailTabRow = value
        End Set
    End Property
    Friend Property CopyValues As Boolean = True

    Friend Overridable Sub LoadTree(ByVal pFilter As String, ByVal pLoadListSource As Boolean, ByVal pListSource As String)
        Me.DataSourceFilter = pFilter
        If pLoadListSource Then
            LoadListSource(pFilter, pListSource)
        End If
        If pLoadListSource And CopyValues Then
            GoCopyValues()
        End If
        Me.Populate()
    End Sub

    Friend Function GetRowFilterString(ByVal dr As DataRow) As String
        Dim a, n As String
        Dim s As String = ""
        Dim dra2 As DataRow()
        If mInfoMenuSet IsNot Nothing And mMenuDetailTabRow IsNot Nothing Then
            s = "("
            dra2 = mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(mMenuDetailTabRow.ID) & " AND ListKey=1")
            If dra2.Length > 0 Then
                Dim q As String = ""
                For Each dr2 As DataRow In dra2
                    n = dr2("Name").ToString
                    a = dr2("ListColumn").ToString
                    If a = "" Then a = n
                    'q &= "b." & n & "=" & "a." & a & " AND "
                    q &= n & "=" & GSCOM.SQL.SQLFormat(dr(a)) & " AND "

                Next
                q = Strings.Left(q, q.Length - 5) ' " AND "
                If s = "(" Then
                    s &= q
                Else
                    s &= " AND " & q
                End If
            End If
            s &= ")"
        End If

        Return s
    End Function

    Private Sub LoadListSource(ByVal pFilter As String, ByVal pListSource As String)
        Dim a, n As String
        Dim s As String
        Dim dra2 As DataRow()
        n = "v" & Strings.Right(mMenuDetailTabRow.TableName, mMenuDetailTabRow.TableName.Length - 1)
        s = "SELECT * FROM "
        If pListSource <> "" Then
            s &= pListSource
        Else
            s &= mMenuDetailTabRow.ListSource
        End If
        's &= mMenuDetailTabRow.ListSource & " a where not exists (SELECT * FROM " & n & " b where "
        s &= " a where not exists (SELECT * FROM " & n & " b where "
        s &= pFilter
        dra2 = mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(mMenuDetailTabRow.ID) & " AND ListKey=1")
        If dra2.Length > 0 Then
            Dim q As String = ""
            For Each dr2 As DataRow In dra2
                n = dr2("Name").ToString
                a = dr2("ListColumn").ToString
                If a = "" Then a = n
                q &= "b." & n & "=" & "a." & a & " AND "
            Next
            q = Strings.Left(q, q.Length - 5) ' " AND "
            s &= " AND " & q
        End If
        s &= ")"
        Me.ListSource = GSCOM.SQL.TableQuery(s, gConnection)

    End Sub

    Friend Sub GoCopyValuesCore(ByVal dr2 As DataRow)
        Dim a, n As String
        Dim drNew As DataRow
        drNew = CType(Me.DataSource, DataTable).NewRow
        'For Each dr3 As DataRow In mtMenuDetailTabField.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(dr("ID")) & " AND (CopyFromList=1 OR (IsGroup=1 AND Name<>'ID') OR (ListKey=1))")
        For Each dr3 As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & mMenuDetailTabRow.ID & " AND (CopyFromList=1) AND (Name<>'ID')")
            n = dr3("Name").ToString
            a = dr3("ListColumn").ToString
            If a = "" Then a = n
            drNew(n) = dr2(a)
        Next
        CType(Me.DataSource, DataTable).Rows.Add(drNew)
    End Sub
    Private Sub GoCopyValues()
        If ListSource IsNot Nothing And DataSource IsNot Nothing Then
            For Each dr2 As DataRow In Me.ListSource.Select
                GoCopyValuesCore(dr2)
            Next
        End If

    End Sub
 
End Class
