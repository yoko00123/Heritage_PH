Friend Class BrowserDataListForm
    Inherits UI.DataLookUp.DataLookUpDropDownForm

    Dim WithEvents bdl As BrowserDataList
    Public SelectedID As Integer

    Public Property CallingInfoSet As InfoSet
        Get
            Return bdl.mGrid.CallingInfoSet
        End Get
        Set(ByVal value As InfoSet)
            bdl.mGrid.CallingInfoSet = value
        End Set
    End Property


    Sub New(ByVal pMenu As Database.Menu, ByVal pAllowDuplicateList As Boolean, Optional ByVal pFixedFilter As String = "", Optional ByVal pSelectionMode As Boolean = False, Optional ByVal destT As DataTable = Nothing)
        Me.Size = New Size(600, 400)
        bdl = New BrowserDataList

        MainList = bdl

        bdl.Dock = DockStyle.Fill
        bdl.mGrid.Menu = pMenu
        bdl.mGrid.SchemaTable = New GSCOM.SQL.SchemaTable(gConnection, CStr(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.TableName)))

        If pFixedFilter = "" Then
            If Not pAllowDuplicateList Then
                pFixedFilter &= GetFixedFilter(destT)
            End If
        Else
            If GetFixedFilter(destT) <> "" Then
                If Not pAllowDuplicateList Then
                    pFixedFilter &= " AND "
                    pFixedFilter &= GetFixedFilter(destT)
                End If
            End If
        End If

        Dim pSort As String = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.Sort).ToString



        bdl.mGrid.SelectionMode = pSelectionMode

        bdl.Init(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.DataSource).ToString, gConnection, pFixedFilter, nDB.GetDisplayColumnsTable(pMenu), "", pSort, pMenu)
        Me.Controls.Add(bdl)

    End Sub
    Private Function GetFixedFilter(ByVal DestTB As DataTable) As String
        If DestTB Is Nothing Then Return " (1=1) "
        Dim tm As String = nDB.GetMenuValue(bdl.mGrid.Menu, Database.Tables.tMenu.Field.TableName).ToString
        tm = "ID_" & Strings.Right(tm, tm.Length - 1)

        Dim s As String = ""
        If DestTB.Select.Length > 0 Then
            s &= " ID NOT IN ("
            For Each dr As DataRow In DestTB.Select
                s &= dr(tm).ToString & ","
            Next
            s = Strings.Left(s, s.Length - 1)

            s &= ")"
        End If
        Return s
    End Function

    Public Function GetTable() As DataTable

        Dim bDT As New DataTable
        Dim bStr As String

        If Me.CheckedRowID Is Nothing Then
            Return Nothing
        Else
            bStr = "SELECT * FROM " & nDB.GetMenuValue(bdl.mGrid.Menu, Database.Tables.tMenu.Field.DataSource).ToString & " WHERE ID IN ("

            For Each i As Integer In Me.CheckedRowID
                bStr &= i.ToString & ","
            Next
            bStr = Strings.Left(bStr, bStr.Length - 1)
            bStr &= ")"

            bDT = GSCOM.SQL.TableQuery(bStr, gConnection)

            Return bDT
        End If
    End Function

    Public Function GetDetailTable(ByVal DataSource As String) As DataTable
        Dim tm As String = nDB.GetMenuValue(bdl.mGrid.Menu, Database.Tables.tMenu.Field.TableName).ToString
        Dim bDT As New DataTable
        Dim bStr As String
        If Me.CheckedRowID Is Nothing Then
            Return Nothing
        Else
            bStr = "SELECT * FROM " & DataSource & " WHERE "
            bStr &= "ID_" & Strings.Right(tm, tm.Length - 1) & " IN ("
            For Each i As Integer In Me.CheckedRowID
                bStr &= i.ToString & ","
            Next
            bStr = Strings.Left(bStr, bStr.Length - 1)
            bStr &= ")"
            bDT = GSCOM.SQL.TableQuery(bStr, gConnection)

            Return bDT
        End If
    End Function

    Public Overrides Property CheckedRowID As Integer()
        Get
            Return bdl.mGrid.CheckedRowID
        End Get
        Set(ByVal value As Integer())
            bdl.mGrid.CheckedRowID = value
        End Set
    End Property

    'Private Sub InitializeComponent()
    '    Me.SuspendLayout()
    '    '
    '    'BrowserDataListForm
    '    '
    '    'Me.ClientSize = New System.Drawing.Size(584, 362)
    '    Me.Name = "BrowserDataListForm"
    '    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    '    Me.ResumeLayout(False)

    'End Sub
    Public Property CloseAfterSelect As Boolean = True
    Private Sub bdl_ButtonClick(ByVal sender As Object, ByVal e As Interfaces.ZIDataList.ButtonClickEventArgs) Handles bdl.ButtonClick
        If e.ButtonText = "Open" Then
            Me.SelectedID = e.SelectedID
            If CloseAfterSelect Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
        MyBase.OnButtonClick(Me, e)
    End Sub


    Private Sub BrowserDataListForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SelectedID = 0
        Me.bdl.Focus()
        Me.bdl.Select()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'BrowserDataListForm
        '
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Name = "BrowserDataListForm"
        Me.ShowIcon = False
        Me.ResumeLayout(False)

    End Sub
End Class
