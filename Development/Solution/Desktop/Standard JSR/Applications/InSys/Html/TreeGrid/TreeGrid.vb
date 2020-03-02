Option Explicit On
Option Strict On

Imports GSCOM.Html

Friend Class TreeGrid
    Inherits HtmlInfoDetail

    Private Const TreeGridPrefix As String = "mTreeGrid"

    Dim tv As DataTreeView
    Public CheckBoxes As Boolean
    Public Name As String

    Public Sub New(ByVal pParent As HtmlContent)
        MyBase.New(pParent)
        tv = New DataTreeView
        tv.TabHeaderColor = Me.TabHeaderColor
    End Sub

    Public Function PrintDetail(ByVal pMenuDetailTab As DataRow) As String
        Dim sb As New System.Text.StringBuilder
        Dim dt As GSCOM.SQL.ZDataTable
        Dim s As String
        'Dim vFieldRows As DataRow()
        s = pMenuDetailTab("TableName").ToString
        dt = CType(mParent.mDataSet.Tables(s), SQL.ZDataTable)
        If dt.Select.Length > 0 Then
            sb.AppendLine(Me.GetPreLineFeeds)
            Dim dra As DataRow()
            s = pMenuDetailTab("ListSource").ToString
            Me.DataSource = dt
            Me.Name = TreeGridPrefix & pMenuDetailTab("ID").ToString
            s = "ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(pMenuDetailTab("ID"))
            dra = mParent.mMenuSet.tMenuDetailTabField.Select(s & "AND IsGroup=1")
            For Each dr As DataRow In dra
                Me.Groups.Add(dr("Name").ToString, dr("Text").ToString, dr("Sort").ToString, dr("ImageFile").ToString)
            Next
            dra = mParent.mMenuSet.tMenuDetailTabField.Select(s & " AND (IsColumn=1)")
            For Each dr As DataRow In dra
                Me.Columns.Add(dr("Name").ToString, CInt(dr("Width")), "right")
            Next
            Me.CheckBoxes = CBool(pMenuDetailTab("CheckBoxes"))
            sb.AppendLine(Go(pMenuDetailTab))
        End If
        Return sb.ToString
    End Function

    Private Enum eRowSelectSource As Integer
        None = 0
        Tree = 1
        Grid = 2
    End Enum

    Private mRowSelectSource As eRowSelectSource
    Private Function Go(ByVal pMenuDetailTab As DataRow) As String
        Dim sb As New System.Text.StringBuilder

        Return tv.Go(pMenuDetailTab)

        Return sb.ToString
    End Function


    Public ReadOnly Property Groups() As TreeGridGroupCollection
        Get
            Return tv.Groups
        End Get
    End Property

    Public ReadOnly Property Columns() As TreeGridColumnCollection
        Get
            Return tv.Columns
        End Get
    End Property

    Public Property ListSource() As DataTable
        Get
            Return tv.ListSource
        End Get
        Set(ByVal value As DataTable)
            tv.ListSource = value
        End Set
    End Property

    Public ReadOnly Property AllNodes() As TreeGridNodeCollection
        Get
            Return tv.AllNodes
        End Get
    End Property

    Private ReadOnly Property Tree() As DataTreeView
        Get
            Return tv
        End Get
    End Property
    
#Region "FromDataSelector"
     
    Dim mCtr As Integer

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Private mDataSource As DataTable
    Public Property DataSource() As DataTable
        Get
            Return tv.DataSource
        End Get
        Set(ByVal value As DataTable)
            tv.DataSource = value
        End Set
    End Property

    Event LoadListClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
 
    Public DetailInfo As DetailClass

End Class
