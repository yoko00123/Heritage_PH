Option Explicit On
Option Strict On



Friend Class ColumnsDialog
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal pMenuID As Integer, ByVal pConnection As Data.SqlClient.SqlConnection, ByVal pDataSource As DataTable)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mMenuID = pMenuID
        mConnection = pConnection
        mDataSource = pDataSource

        Me.NewButton.Image = GSCOM.UI.Common.NewButtonImage
        Me.SaveButton.Image = GSCOM.UI.Common.SaveButtonImage
        Me.RefreshButton.Image = GSCOM.UI.Common.RequeryButtonImage
        Me.PrintButton.Image = GSCOM.UI.Common.PrintButtonImage
        Me.HeaderButton.Image = GSCOM.UI.Common.HeaderButtonImage
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MyPreView As GSCOM.UI.GSListView.GSListView
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents NewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RefreshButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HeaderButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TranslucentButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupCountBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents mTreeGrid As DataTreeView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.MyPreView = New GSCOM.UI.GSListView.GSListView()
        Me.mTreeGrid = New GSCOM.Applications.InSys.DataTreeView()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.NewButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintButton = New System.Windows.Forms.ToolStripButton()
        Me.RefreshButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HeaderButton = New System.Windows.Forms.ToolStripButton()
        Me.TranslucentButton = New System.Windows.Forms.ToolStripButton()
        Me.GroupCountBox = New System.Windows.Forms.ToolStripTextBox()
        Me.tsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MyPreView
        '
        Me.MyPreView.Allowsort = False
        Me.MyPreView.BackColor = System.Drawing.Color.White
        Me.MyPreView.CheckBoxes = True
        Me.MyPreView.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyPreView.EvenBackColor = System.Drawing.Color.White
        Me.MyPreView.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.MyPreView.FullRowSelect = True
        Me.MyPreView.GridLines = True
        Me.MyPreView.HighlightBackColor = System.Drawing.Color.LightSteelBlue
        Me.MyPreView.Location = New System.Drawing.Point(0, 410)
        Me.MyPreView.MultiSelect = False
        Me.MyPreView.Name = "MyPreView"
        Me.MyPreView.OddBackColor = System.Drawing.Color.AliceBlue
        Me.MyPreView.Size = New System.Drawing.Size(784, 52)
        Me.MyPreView.SortColumnIndex = -1
        Me.MyPreView.SortHighlightEnabled = True
        Me.MyPreView.TabIndex = 9
        Me.MyPreView.UseCompatibleStateImageBehavior = False
        Me.MyPreView.View = System.Windows.Forms.View.Details
        '
        'mTreeGrid
        '
        Me.mTreeGrid.BackColor = System.Drawing.Color.White
        Me.mTreeGrid.CheckBoxes = True
        Me.mTreeGrid.CurrentCell = Nothing
        Me.mTreeGrid.DataSource = Nothing
        Me.mTreeGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mTreeGrid.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.mTreeGrid.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.mTreeGrid.FullRowSelect = True
        Me.mTreeGrid.HideSelection = False
        Me.mTreeGrid.ItemHeight = 18
        Me.mTreeGrid.ListSource = Nothing
        Me.mTreeGrid.Location = New System.Drawing.Point(0, 25)
        Me.mTreeGrid.Name = "mTreeGrid"
        Me.mTreeGrid.RowFilter = Nothing
        Me.mTreeGrid.ShowLines = False
        Me.mTreeGrid.Size = New System.Drawing.Size(784, 385)
        Me.mTreeGrid.TabIndex = 15
        '
        'tsMain
        '
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewButton, Me.SaveButton, Me.PrintButton, Me.RefreshButton, Me.ToolStripSeparator1, Me.HeaderButton, Me.TranslucentButton, Me.GroupCountBox})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(784, 25)
        Me.tsMain.TabIndex = 17
        Me.tsMain.Text = "ToolStrip1"
        '
        'NewButton
        '
        Me.NewButton.Enabled = False
        Me.NewButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(35, 22)
        Me.NewButton.Text = "&New"
        '
        'SaveButton
        '
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(35, 22)
        Me.SaveButton.Text = "&Save"
        '
        'PrintButton
        '
        Me.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(36, 22)
        Me.PrintButton.Text = "&Print"
        Me.PrintButton.Visible = False
        '
        'RefreshButton
        '
        Me.RefreshButton.Enabled = False
        Me.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(50, 22)
        Me.RefreshButton.Text = "&Refresh"
        Me.RefreshButton.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'HeaderButton
        '
        Me.HeaderButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.HeaderButton.Checked = True
        Me.HeaderButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HeaderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HeaderButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HeaderButton.Name = "HeaderButton"
        Me.HeaderButton.Size = New System.Drawing.Size(23, 22)
        Me.HeaderButton.Text = "Header"
        Me.HeaderButton.Visible = False
        '
        'TranslucentButton
        '
        Me.TranslucentButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TranslucentButton.CheckOnClick = True
        Me.TranslucentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TranslucentButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TranslucentButton.Name = "TranslucentButton"
        Me.TranslucentButton.Size = New System.Drawing.Size(23, 22)
        Me.TranslucentButton.Text = "Translucent"
        Me.TranslucentButton.Visible = False
        '
        'GroupCountBox
        '
        Me.GroupCountBox.Name = "GroupCountBox"
        Me.GroupCountBox.Size = New System.Drawing.Size(100, 25)
        Me.GroupCountBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ColumnsDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(784, 462)
        Me.Controls.Add(Me.mTreeGrid)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.MyPreView)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColumnsDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Column Selection"
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Variables"
    Private m_SelectString As String
    Private m_Inited As Boolean
    Private mConnection As SqlClient.SqlConnection
    Private mMenuID As Integer
    Private mDataSource As DataTable

    Public CanClose As Boolean
    'Public Event OKClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Public Event CloseClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event ActivatedFirstTime(ByVal sender As Object, ByVal e As System.EventArgs)

#End Region

#Region "Events"
    'Private Sub ColumnsDialog_CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    '    DeleteRows()
    '    CanClose = True

    'End Sub

    'Private Sub ColumnsDialog_OKClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'm_SelectString = GetSelectString()
    '    Try
    '        GetSelectString()

    '        CanClose = True

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '    End Try
    '    'Me.Hide()
    'End Sub
    Private mManualChecking As Boolean
    Private Sub MyListView_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles mTreeGrid.AfterCheck
        Dim tgn As TreeGridNode
        tgn = DirectCast(e.Node, TreeGridNode)
        UpdatePreview(tgn)
    End Sub

    Private Sub UpdatePreview(ByVal tgn As TreeGridNode)
         If tgn.Row IsNot Nothing Then
            Dim vKey As String = tgn.Row("Name").ToString
            Dim str As String = tgn.Text
            Select Case tgn.Checked
                Case True
                    If Not MyPreView.Columns.Contains(MyPreView.Columns(vKey)) Then
                        If str = "ImageFile" Then
                            MyPreView.Columns.Insert(1, vKey, str, 0, HorizontalAlignment.Center, 0)
                        Else
                            MyPreView.Columns.Add(vKey, str, 0, HorizontalAlignment.Center, 0)
                        End If
                    End If
                Case False
                     For Each c As ColumnHeader In MyPreView.Columns
                        If c.Text = str Then
                            If Not tgn.Checked Then
                                If vKey <> "ID" Then
                                    MyPreView.Columns.Remove(MyPreView.Columns(vKey))
                                End If
                            End If
                        End If
                    Next
            End Select
            MyPreView.AutoSizeColumns()
        End If
    End Sub

    Private Sub CheckButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As TreeGridNode
        For Each i In mTreeGrid.AllNodes
            i.Checked = True
        Next i
    End Sub

    Private Sub UncheckButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As TreeGridNode
        For Each i In mTreeGrid.AllNodes
            i.Checked = False
        Next i
    End Sub

#End Region

    Private Sub Init2(ByVal pMenu As Integer, ByVal c As Data.SqlClient.SqlConnection, ByVal pDataSource As DataTable)
        Dim vListSource As New GSCOM.SQL.ZDataTable(c, "tMenuTabField")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("MenuTabMenuID=" & pMenu.ToString)
        sb.AppendLine("AND dbo.fMenuTabFieldIsActive(ID)=1")
        sb.AppendLine("AND ShowInList=1")
        sb.AppendLine("AND dbo.fCanViewSalaryAuthenticatedFields(ID, " + nDB.GetUserID.ToString + ") = 1") 'EMIL 20130516

        vListSource.ClearThenFill(sb.ToString)
        mTreeGrid.ListSource = vListSource
        mTreeGrid.DataSource = pDataSource
        Dim dt As DataTable = CType(mTreeGrid.DataSource, DataTable)

        For Each dr As DataRow In dt.Select
            If dr.RowState = DataRowState.Added Then
                dt.Rows.Remove(dr)
            End If
        Next
        dt.AcceptChanges() '?????????????
        Dim drNew As DataRow
        For Each dr2 As DataRow In mTreeGrid.ListSource.Select()
            'If dt.Select("ID_MenuTabField=" & dr2("ID").ToString).Length = 0 Then
            If dt.Select("[Name]=" & GSCOM.SQL.SQLFormat(dr2("Name"))).Length = 0 Then
                drNew = dt.NewRow
                drNew("ID_User") = nDB.GetUserID
                drNew("ID_MenuTabField") = dr2("ID")
                drNew("Name") = dr2("Name")
                drNew("EffectiveLabel") = dr2("EffectiveLabel")
                drNew("ID_MenuTab") = dr2("ID_MenuTab")
                drNew("MenuTab") = dr2("MenuTab")
                drNew("MenuTabSeqNo") = dr2("MenuTabSeqNo")
                drNew("MenuTabFieldSeqNo") = dr2("SeqNo")
                drNew("SeqNo") = dr2("SeqNo")
                drNew("Panel") = dr2("Panel")
                dt.Rows.Add(drNew)
            End If
        Next
        ' dt.Rows.Remove(dt.Select("Name='ID'")(0))

        mTreeGrid.Populate()
        MyPreView.Columns.Clear()
        For Each n As TreeGridNode In mTreeGrid.AllNodes
            If n.Row IsNot Nothing Then
                If n.Row.Item("Name").ToString = "ID" Then
                    n.Row = Nothing
                    n.Remove()
                End If
            End If
        Next
        Dim vKey As String
        For Each dr As DataRow In dt.Select("", "SeqNo")
            If dr.RowState <> DataRowState.Added Then
                vKey = dr("Name").ToString
                If vKey = "ID" Then
                    MyPreView.Columns.Add(vKey, vKey)
                Else
                    MyPreView.Columns.Add(vKey, dr("EffectiveLabel").ToString)
                End If
            End If
        Next
        MyPreView.AutoSizeColumns()

        GroupCountBox.Text = CStr(dt.Select("GroupSeqNo<>0").Length)


    End Sub
#Region "Methods"
    Public Sub Init(ByVal pMenu As Integer, ByVal c As Data.SqlClient.SqlConnection, ByVal pDataSource As DataTable)

        '  mTreeGrid.LoadListButton.Visible = False
        mTreeGrid.ImageList = GSCOM.Applications.InSys.gImageList
        mTreeGrid.Groups.Add("ID_MenuTab", "MenuTab", "MenuTabSeqNo,ID_MenuTab", "_menutab.png")
        'use menutabfieldseqno to retain default sort, not the one thats user defined (umtf.seqno)
        mTreeGrid.Groups.Add("ID", "EffectiveLabel", "MenuTabSeqNo,ID_MenuTab,Panel,MenuTabFieldSeqNo,ID", "_menutabfield.png")
        ' mTreeGrid.DataSource = pDataSource
        MyPreView.AllowColumnReorder = True

        'scm = New SqlClient.SqlCommand("SELECT TOP 0 * FROM " & dtb.TableName, c)
        'sda = New SqlClient.SqlDataAdapter(scm)
        'sda.Fill(dtb)
        'mTreeGrid.Columns.Add("Column Name", 0, HorizontalAlignment.Left)
        'For Each dcl In dtb.Columns
        '    lvi = mTreeGrid.Items.Add(dcl.ColumnName)
        'Next
        'If Not headers Is Nothing Then
        '    For Each chd In headers
        '        lvi = mTreeGrid.FindFirstItem(chd)
        '        If lvi IsNot Nothing Then
        '            lvi.Checked = True
        '        Else
        '            'NOTE: THE CODE GOES HERE IF THE COLUMN NO LONGER EXISTS
        '        End If
        '    Next
        'End If
        m_Inited = True
    End Sub

    Public Function SelectString() As String
        SelectString = m_SelectString
    End Function

    Private Sub DeleteRows()
        For Each n As TreeGridNode In mTreeGrid.AllNodes
            If (Not n.Checked) And n.Databound Then
                If n.Row("Name").ToString <> "ID" Then
                    n.Row.Delete()
                End If
            End If
        Next

    End Sub
    Private Sub GetSelectString()
        DeleteRows()
        Dim gsn As Integer
        Dim dr As DataRow
        For Each c As ColumnHeader In MyPreView.Columns
            dr = mTreeGrid.DataSource.Select("Name=" & GSCOM.SQL.SQLFormat(c.Name))(0)
            dr("SeqNo") = c.DisplayIndex + 1 '  MyPreView.Columns.IndexOf(c) + 1

            gsn = CInt(dr("SeqNo")) + GroupCount - MyPreView.Columns.Count

            If gsn > 0 Then
                gsn = GroupCount - gsn + 1
                dr("GroupSeqNo") = gsn
            Else
                dr("GroupSeqNo") = 0
            End If

            '   i += 1
        Next

        Try
            CType(mTreeGrid.DataSource, GSCOM.SQL.ZDataTable).Update()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly)
        End Try
        'Init2(mMenuID, mConnection, mDataSource)

        ''Dim a As String = ""
        ''Dim chd As ColumnHeader
        ' ''ROBBIE Try 20060808
        ' ''a = "SELECT "
        ''For Each chd In MyPreView.Columns()
        ''    'a &= chd.Text() & ", "
        ''    a &= "[" & chd.Text() & "], "
        ''Next
        ''a = Strings.Left(a, a.Length - 2) & " "
        'GetSelectString = a
    End Sub


#End Region

    Private Sub ColumnsDialog_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub InfoForm_PrevewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown, MyPreView.PreviewKeyDown, tsMain.PreviewKeyDown, mTreeGrid.PreviewKeyDown ', mControl.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub


    'Private Sub mTreeGrid_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles mTreeGrid.BeforeCheck
    '    'If DirectCast(e.Node, TreeGridNode).Row IsNot Nothing Then
    '    '   If CType(e.Node, TreeGridNode).Row.Item("Name").ToString = "ID" Then
    '    '        'If m_Inited Then
    '    '        'If CBool((e.Action And TreeViewAction.ByKeyboard) Or (e.Action And TreeViewAction.ByMouse)) Then
    '    '        'If Not mManualChecking Then
    '    '        '    mManualChecking = True
    '    '        '    Application.DoEvents()
    '    '        '    e.Node.Checked = True
    '    '        '    mTreeGrid.Refresh()
    '    '        '    mManualChecking = False
    '    '        'End If
    '    '        'End If
    '    '        'End If
    '    '        e.Cancel = True
    '    '    End If
    '    'End If
    'End Sub



    'Private Sub LoadListClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim tg As TreeGrid
    '    tg = DirectCast(sender, TreeGrid)
    '    For Each dr As DataRow In tg.DataSource.Select
    '        If dr.RowState = DataRowState.Added Then
    '            tg.DataSource.Rows.Remove(dr)
    '        End If
    '    Next
    '    LoadTree(tg.DataSource, tg.DataSourceFilter, True)
    'End Sub

    'Private Sub mTreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mTreeGrid.Load

    'End Sub

    Private Sub ColumnsDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not m_Inited Then
            Init(mMenuID, mConnection, mDataSource)
        End If
        Init2(mMenuID, mConnection, mDataSource)
    End Sub


    Private Sub ColumnsDialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        mTreeGrid.ExpandAll()
        CanClose = False
    End Sub


    ''''''''''''''


    'Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    '    RaiseEvent OKClicked(sender, e)
    'End Sub

    'Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    '    RaiseEvent CloseClicked(sender, e)
    'End Sub

    Private Sub SaveDialog_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Static FirstTime As Boolean = True
        If FirstTime Then
            'Application.DoEvents()
            RaiseEvent ActivatedFirstTime(sender, e)
            FirstTime = False
        End If
    End Sub

    'Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
    '    If UCase(Strings.Right(Me.Name, 4)) = "FORM" Then Me.Text = Me.Name
    '    MyBase.OnLoad(e)
    'End Sub

    Private Sub SaveDialog_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        '       e.Cancel = Not CanClose
        If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
            DeleteRows()
            CanClose = True

        End If
    End Sub

    'Private Sub SaveDialog_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
    '    RaiseEvent CloseClicked(sender, e)
    'End Sub

    Private Sub MyPreView_ColumnReordered(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnReorderedEventArgs) Handles MyPreView.ColumnReordered
        If e.Header.Name = "ID" Or e.NewDisplayIndex = 0 Then e.Cancel = True
        'If MyPreView.Columns.ContainsKey (
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Save()
    End Sub

    Private Sub Save()
        Try
            GetSelectString()
            CanClose = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Property GroupCount As Integer

    Private Sub GroupCountBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupCountBox.TextChanged
        If IsNumeric(GroupCountBox.Text) AndAlso CInt(GroupCountBox.Text) > 0 Then
            GroupCount = CInt(GroupCountBox.Text)
        Else
            GroupCount = 0
        End If
    End Sub
End Class

