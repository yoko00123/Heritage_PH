Option Explicit On
Option Strict On

Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Friend Class MainForm : Inherits System.Windows.Forms.Form

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        Init()
    End Sub

#Region "Finalize"

    Protected Overrides Sub Finalize()
        'nDB.MenuTable = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#End Region

#Region "Variables"

    Private img As New PictureBox
    Private ClickedFavorite As Integer
    Friend gInfoSetCollection As New Collection
    'Private fltr As New FilterMenuForm()

    Delegate Sub dgRootPath()

#Region "Initialization For Alert"

    Public AlertClass As New GSCOM.Applications.InSys.AlertClass
    Private mUseAlert As Boolean

#End Region

#Region "Initialization For UnhandledErrorHandlers" 'Emil 07132012
    Dim ErrLog As New ErrorLoggerClass

    Private GiraffeBlue As Color = Color.FromArgb(0, 119, 234)
    Private GiraffeOrange As Color = Color.FromArgb(255, 120, 0)
#End Region

#End Region

#Region "Events"

#Region "MainForm_Load"
    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If mUseAlert Then
            InstantiateAlert()
        End If
    End Sub

#End Region

#Region "MainForm_FormClosing"
    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CloseAll()
    End Sub

#End Region

#Region "MainForm_KeyDown"
    Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim tp As TabPage
        If e.Control And e.KeyCode = Keys.F4 Then
            If Not IsNothing(tcMain.SelectedTab) Then
                tp = tcMain.SelectedTab
                Dim i As Integer = tcMain.TabPages.IndexOf(tp)
                If i <> 0 Then
                    CloseTabPage(tp)
                    'If tcMain.TabPages.Count > 1 Then
                    If i <= tcMain.TabPages.Count - 1 Then
                        tcMain.SelectedTab = tcMain.TabPages(i)
                    Else
                        tcMain.SelectedTab = tcMain.TabPages(i - 1)
                    End If
                    'Else
                    '    tcMain.SelectedTab = tcMain.TabPages(0)
                    'End If
                End If
            End If
            tvMain.SelectedNode = Nothing
        End If
    End Sub


#End Region

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        CloseAll()
        Global.System.Windows.Forms.Application.Exit()
        Me.Close()
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FavoritesToolStripMenuItem.Click
        FavoritesToolStripMenuItem.Checked = Not FavoritesToolStripMenuItem.Checked
        ToolStripContainer.LeftToolStripPanel.Visible = FavoritesToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarToolStripMenuItem.Click
        StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
        mStatusStrip.Visible = StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub FoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripMenuItem.Click
        ToggleFoldersVisible()
    End Sub

    Private Sub FoldersToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToggleFoldersVisible()
    End Sub

#Region "VisualStylesToolStripButton_Click"
    Private Sub VisualStylesToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToggleVisualStyles()
    End Sub

#End Region

#Region "mnuSystemGenerateTableDefinitions_Click"
    Private Sub mnuSystemGenerateTableDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemGenerateTableDefinitions.Click
        Dim s As String = Database.GetTableGenCode(gConnection)
        Dim f As String
        f = IO.Path.GetTempFileName
        FileOpen(1, f, OpenMode.Binary)
        FilePut(1, s)
        FileClose(1)
        Shell("Notepad """ & f & """", AppWinStyle.MaximizedFocus)
    End Sub

#End Region

    Private Sub mnuSystemTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemTest.Click
        'Save()
        Dim f As New InSightForm
        f.ShowDialog()

    End Sub

    Private Sub mnuSystemRunScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemRunScript.Click
        Dim f As New ScriptForm
        f.ShowDialog()
    End Sub

    Private Sub ImportEmployeeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportEmployeeToolStripMenuItem.Click
        Dim a As DataTable
        Dim s As String
        Dim ofd As New OpenFileDialog
        'Dim y As String
        ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        ofd.FilterIndex = 0
        ofd.CheckFileExists = False
        ofd.CheckPathExists = True
        If (ofd.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            a = GSCOM.SQL.GetExcelTable(ofd.FileName, "MASTER")
            a.TableName = "mTable"
            s = "TRUNCATE TABLE " & a.TableName
            GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            s = GSCOM.SQL.Insert.Statement(a.Select("active = 'A'"), False)
            GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            s = "EXEC p_JBCEmployee"
            GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub mnuFileBackUpDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileBackUpDatabase.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            BackUpDatabase()
            MsgBox("BackUp Operation Completed", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TaxCalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TaxCalculatorToolStripMenuItem.Click
        Dim f As New TaxCalculatorForm
        f.ShowDialog()
    End Sub

    Private Sub tvMain_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvMain.NodeMouseClick

        If nDB.GetUserID = 1 Then
            If e.Button = Windows.Forms.MouseButtons.Left And My.Computer.Keyboard.AltKeyDown Then
                Dim pInfoSet As InfoSet
                Dim m As Database.Menu
                Dim r As Integer
                m = Database.Menu.SYSTEM_Menu
                pInfoSet = GetInfoSet(m)
                r = CInt(e.Node.Name)
                If pInfoSet Is Nothing Then
                    pInfoSet = NewInfo(m, Nothing, r)
                Else
                    pInfoSet.LoadInfo(r)
                End If
                Application.DoEvents()
                If pInfoSet IsNot Nothing Then
                    pInfoSet.ShowDialog()
                End If
            End If
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            tvMain.SelectedNode = e.Node
        End If
    End Sub

    Private Sub mnuUpdateMenuColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdateMenuColor.Click
        UpdateMenuColors()
        MsgBox("Done", MsgBoxStyle.Information)
    End Sub

    Private Sub CollapseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseButton.Click
        tvMain.CollapseAll()
        If Not CollapseButton.Checked Then CheckCollapsedExpand()
    End Sub

    Private Sub ExpandButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandButton.Click
        tvMain.ExpandAll()
        If Not ExpandButton.Checked Then CheckCollapsedExpand()
    End Sub

    Private Sub mnuFileLogOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileLogOff.Click
        Me.DialogResult = Windows.Forms.DialogResult.Retry
        nDB.EndSession()
        GC.Collect()
        gDestroy()
        Me.Close()
    End Sub

    Private Sub mnuSetRootPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetRootPath.Click
        RootPath()
    End Sub

    Private Sub tvMain_MarginChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvMain.MarginChanged

    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        Using ck As New CheckUpdateForm
            ck.Icon = Me.Icon
            ck.ShowDialog(Me)
        End Using
    End Sub

    Private Sub AddTofavoritesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTofavoritesToolStripMenuItem.Click
        Try
            Dim s As String
            s = "ID_User=" & nDB.GetUserID
            s &= " AND ID_Menu=" & tvMain.SelectedNode.Name

            's = "INSERT INTO tUserFavMenu (ID_User,ID_Menu) VALUES (" & nDB.GetUserID.ToString & "," & tvMain.SelectedNode.Name & ")"
            'GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            's = "SELECT * FROM vUserFavMenu WHERE ID_User=" & nDB.GetUserID.ToString
            'nDB.UserFavMenuTable = GSCOM.SQL.TableQuery(s, gConnection)
            If nDB.UserFavMenuTable.Select(s).Length = 0 Then
                Dim dr As DataRow = nDB.UserFavMenuTable.NewRow
                dr(Database.Tables.tUserFavMenu.Field.ID_User.ToString) = nDB.GetUserID
                dr(Database.Tables.tUserFavMenu.Field.ID_Menu.ToString) = tvMain.SelectedNode.Name
                dr(Database.Tables.tUserFavMenu.Field.SeqNo.ToString) = Integer.MaxValue 'temporary just to place at the bottom, will be replaced before saving to the db.
                nDB.UserFavMenuTable.Rows.Add(dr)
                PopulateFavorites()
            Else
                MsgBox("The selected menu is already in the list", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveFavoritesStripMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFavoritesStripMenu.Click
        Try
            Dim s As String
            s = "ID_Menu=" & GSCOM.SQL.SQLFormat(ClickedFavorite)
            Dim dra() As DataRow
            dra = nDB.UserFavMenuTable.Select(s)
            If dra.Length > 0 Then
                For Each dr As DataRow In dra
                    dr.Delete()
                Next
                PopulateFavorites()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub TabPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TabPage1.Paint
        If img.Image Is Nothing Then
            Dim lgb As System.Drawing.Drawing2D.LinearGradientBrush = _
                New System.Drawing.Drawing2D.LinearGradientBrush(Me.TabPage1.ClientRectangle, _
                                                                 Color.White, _
                                                                 mStatusStrip.BackColor, _
                                                                 System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal)
            Me.TabPage1.CreateGraphics.FillRectangle(lgb, Me.TabPage1.ClientRectangle)
        End If
    End Sub

    Private Sub FilterButton_Click(sender As Object, e As EventArgs) Handles FilterButton.Click
        Try
            FilterButton.Checked = Not FilterButton.Checked
            Me.pnlMenuFilter.Visible = FilterButton.Checked

            If Me.pnlMenuFilter.Visible Then
                Me.spcMain.SplitterDistance += Me.pnlMenuFilter.Width
            Else
                Me.spcMain.SplitterDistance -= Me.pnlMenuFilter.Width
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtFilterValue_TextChanged(sender As Object, e As EventArgs) Handles txtFilterValue.TextChanged
        Try
            Dim op As eFilterOperation = CType(cmbOperator.SelectedIndex, eFilterOperation)
            Dim val As String = Me.txtFilterValue.Text
            Dim thrd As New Threading.Thread(Sub() FilterMenu(op, val))
            thrd.IsBackground = True
            thrd.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmbOperator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperator.SelectedIndexChanged
        If Me.txtFilterValue.Text.Trim <> "" Then
            txtFilterValue_TextChanged(sender, e)
        End If
    End Sub

#End Region

#Region "Methods"

#Region "Init"
    Private Sub Init()
        If IO.File.Exists(Application.StartupPath + "\Icon.ico") Then Me.Icon = Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\Icon.ico")
        'Dim sf As New SplashForm
        'sf.Show()
        mUseAlert = CBool(nDB.GetSetting(Database.SettingEnum.UseAlert, "False"))

        cmbOperator.SelectedIndex = 1

        mnuSystem.Visible = (nDB.GetUserID = 1)
        'mnuAdmin.Visible = (nDB.GetUserID = 1 Or nDB.GetUserID = 2)
        Me.Icon = gIcon

        'Dim s As String
        's = "SELECT isnull(dbo.fGetSetting('SingleCompanyOnly'),0)"
        'If CStr(GSCOM.SQL.ExecuteScalar(s, gConnection)) = "1" Then
        '    If (nDB.GetUserID = 1) Then
        '        mnuAdmin.Visible = True
        '    Else
        '        mnuAdmin.Visible = False
        '    End If

        'Else
        '    If (nDB.GetUserID = 1 Or nDB.GetUserID = 2) Then
        '        mnuAdmin.Visible = True
        '    Else
        '        mnuAdmin.Visible = False
        '    End If
        'End If

        If nDB.nGlobal.IsNetGlobal = 1 Then
            gLogInForm.ShowProgress("Trying to Download Resources")

            Using wbserv As New z.Web.Service.Client(nDB.nGlobal.GetNetPath)
                Dim thrd As New Threading.Thread(Sub() ProcessRootPath(wbserv, Nothing, True))
                thrd.Start()
            End Using

        End If

        'ROBBIE: for reconsideration
        'used by the datalookupcontrol
        gImageList = imgList
        Me.KeyPreview = True
        gLogInForm.ShowProgress("Loading Images")
        LoadImages()
        gLogInForm.ShowProgress("Applying Images")

        mStatusStrip.ImageList = imgList

        mCompanyLabel.Text = nDB.CompanyName 'gCompanyName
        mUserLabel.Text = nDB.UserName
        mServerLabel.Text = nDB.Connection.DataSource
        mDatabaseLabel.Text = nDB.Connection.Database
        'mStartDateTimeLabel.Text = Strings.Format(nDB.Session.Get(Database.Tables.tSession.Field.StartDateTime), "MMM dd, yyyy hh:mm tt")
        'mStartDateTimeLabel.ImageKey = "Clock.ico"

        tvMain.ImageList = imgList
        tvMain.ShowLines = False
        tcMain.ImageList = imgList
        tvMain.ContextMenuStrip = TreeViewContext
        mnuMain.ImageList = imgList
        FavToolStrip.AllowDrop = False
        FavToolStrip.AllowItemReorder = True 'billy
        gLogInForm.ShowProgress("Loading Menu")
        'Me.FavoritesToolStripMenuItem.Checked = False
        FavoritesToolStripMenuItem.AllowDrop = True 'billy
        Me.FavoritesToolStripMenuItem.Visible = True

        LoadTree()
        'after loadtree where menu is initialized
        'mUserLabel.ImageKey = nDB.GetMenuValue(Database.Menu.ADMINISTRATIVE_User, Database.Tables.tMenu.Field.ImageFile).ToString '"_User.png" 
        'mCompanyLabel.ImageKey = nDB.GetMenuValue(Database.Menu.ORGANIZATIONALMANAGEMENT_CompanyProfile, Database.Tables.tMenu.Field.ImageFile).ToString() '"_Company.png" '

        gLogInForm.ShowProgress("Preparing StartPage")


        ToolStripContainer.LeftToolStripPanel.Visible = FavoritesToolStripMenuItem.Checked
        tvMain.Select()

        'Dim t As New System.Threading.Thread(AddressOf Navigate)
        't.Start()
        Me.Navigate()
        MainModule.LookUpButtonClickEventHandler = AddressOf LookUpButtonClick '20061204

        'nDB.InitSystemTables() '20071117
        gLogInForm.ShowProgress("Performing daily automation")

        Try
            GSCOM.SQL.ExecuteNonQuery("EXEC pDailyAutomation", gConnection)

            'If mUseAlert Then
            '    GSCOM.SQL.ExecuteNonQuery("EXEC pInsertAlerts " & gUser, gConnection)
            '    'GSCOM.SQL.ExecuteNonQuery("EXEC pDeleteAlerts " & gUser, gConnection)
            'End If

        Catch ex As Exception
            'ROBBIE - remove this try catch.. this is just ensures backward compatibility
            'MsgBox("Testing Daily Automation", MsgBoxStyle.Information)
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        'sf.Close()
        gLogInForm.ShowProgress("Showing Main Window")
        gLogInForm.Hide()

        If gFromAlert Then
            Me.LoadList(63)
        End If
    End Sub

    Private Sub Navigate()
        'Dim sp As String
        'sp = nDB.GetSetting(Database.SettingEnum.StartPage).ToString
        'If sp = "" Then
        '    Dim n As New Navigator
        '    Dim a As New Xml.Xsl.XsltArgumentList
        '    Dim s As String
        '    s = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.CSS"
        '    a.AddParam("stylesheet", "", s)
        '    s = nDB.GetSetting(Database.SettingEnum.PhotoPath).ToString & "_logo.jpg"
        '    a.AddParam("image", "", s)
        '    n.Browser = Me.Browser
        '    n.DataSet = New DataSet
        '    n.StyleSheetPath = nDB.GetSetting(Database.SettingEnum.StyleSheetPath) & "main.xsl"
        '    n.ArgumentList = a
        '    n.Navigate()
        'Else
        '    Try
        '        Me.Browser.Navigate(sp) 'this raise errors sometimes
        '    Catch ex As Exception

        '    End Try


        'End If
        Dim logo() As String = GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('MainBG')", gConnection).ToString.Split(CChar(","))
        If Not logo.Length > 0 Then Exit Sub
        Dim logoBG As String = IO.Path.Combine(nDB.nGlobal.ResourcePath + "BG\", Trim(logo(0)))

        If logo.Length > 1 Then
            Dim logoName As String = IO.Path.Combine(nDB.nGlobal.ResourcePath + "BG\", Trim(logo(1)))
            If IO.File.Exists(logoName) Then
                pLogo.Load(logoName)
                Me.pLogo.BackColor = getColor("MainBGColor")
                pLogo.Visible = True
            End If
        Else
            pLogo.Image = Nothing
            pLogo.Visible = False
        End If

        If IO.File.Exists(logoBG) Then
            With img
                .Dock = DockStyle.Fill
                .SizeMode = PictureBoxSizeMode.StretchImage
                .Load(logoBG)
                Me.TabPage1.Controls.Add(img)
                .SendToBack()
            End With
        Else
            img.Image = Nothing
        End If

        '=====Colors=====
        Try
            Me.pLogo.BackColor = getColor("MainBGColor")
            Me.mnuMain.BackColor = getColor("MenuBGColor")
            Me.spcMain.BackColor = getColor("MenuBGColor")
            Me.mStatusStrip.BackColor = getColor("StatusBGColor")
            Me.mStatusStrip.ForeColor = getColor("StatusForeColor")
            Me.spcMain.BorderStyle = BorderStyle.None
            Me.spcMain.Panel1.BackColor = getColor("MainBGColor")
            Me.spcMain.Panel2.BackColor = getColor("MainBGColor")
            Me.FavToolStrip.BackColor = getColor("FavoritesBGColor")
            Me.ToolStrip1.BackColor = getColor("MainBGColor")
            Me.ToolStripLabel1.ForeColor = getColor("StatusForeColor")
            Me.TabPage1.BackColor = getColor("MainBGColor")
            Me.tvMain.BackColor = getColor("MainTreeBGColor")
            Me.tvMain.ForeColor = getColor("MainTreeForeColor")
            Me.img.BackColor = getColor("MainBGColor")
        Catch

        End Try
        '================
        'MAIN TITLE'
        Me.Text = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString & " - " & nDB.CompanyName ' gCompanyName
    End Sub

    Private Class Navigator
        Public Browser As WebBrowser
        Public DataSet As DataSet
        Public StyleSheetPath As String
        Public ArgumentList As System.Xml.Xsl.XsltArgumentList

        Public Sub Navigate()
            '    Try
            '        Dim f As String = IO.Path.GetTempFileName()
            '        Dim sb As New System.Text.StringBuilder
            '        Dim newxml As IO.StringWriter
            '        Dim doc As New Xml.XmlDataDocument()
            '        Dim xslt As New Xml.Xsl.XslCompiledTransform
            '        Dim w As Xml.XmlWriter
            '        w = Xml.XmlWriter.Create(sb)
            '        If DataSet IsNot Nothing Then
            '            DataSet.WriteXml(w)
            '        End If
            '        'doc.LoadXml(sb.ToString)
            '        doc.LoadXml(Replace(sb.ToString, "+08:00", ""))
            '        sb = New System.Text.StringBuilder
            '        newxml = New IO.StringWriter(sb)
            '        xslt.Load(StyleSheetPath)
            '        xslt.Transform(doc, ArgumentList, newxml)
            '        doc = Nothing
            '        GSCOM.Common.FileFromString(sb.ToString, f)
            '        Browser.Navigate(f)
            '    Catch ex As Exception
            '        'do not throw exception
            '    End Try
        End Sub
    End Class



#End Region

#Region "InitList"

    Private Sub InitList(ByVal mr As Database.MenuRow, ByVal pViewer As GSCOM.Interfaces.ZIDataList)
        'Dim vMenuID As Integer
        Try
            'vMenuID = CInt(pRow.Item(Database.Tables.tMenu.Field.ID.ToString).ToString)
            pViewer.Filter.ClearFilters()  'robbie: ZIDataList: must be able to suppress creation of filters on New so we wont need to clear anymore
            pViewer.AutoGenerateFilters = False
            'Dim cols As New Database.Tables.tMenuTabField(gConnection) 'GSCOM.SQL.ZDataTable(gConnection, "tMenuTabField")
            'cols.ClearThenFill("MenuTabMenuID=" & vMenuID, "MenuTabSeqNo,MenuTabMenuID,Panel,SeqNo,ID")
            Dim dra As DataRow()
            dra = nDB.MenuSet.tMenuTabField.Select("MenuTabMenuID=" & mr.ID.ToString) ', "MenuTabSeqNo,MenuTabMenuID,Panel,SeqNo,ID")
            Dim cs(dra.Length - 1) As String
            Dim i As Integer
            Dim mtfr As New Database.MenuTabFieldRow
            For Each dr As DataRow In dra
                mtfr.InnerRow = dr
                cs(i) = mtfr.Name
                i += 1
                pViewer.DataSource.Columns(mtfr.Name).Caption = mtfr.EffectiveLabel
            Next
            pViewer.Filter.CreateFilters(CType(pViewer.DataSource, DataTable).Columns, cs)
            For Each dr As DataRow In dra
                mtfr.InnerRow = dr
                Dim bb As GSCOM.UI.DataFilter.LookUpFilter
                Dim vFC As GSCOM.UI.DataFilter.FilterControl
                vFC = pViewer.Filter.GetFilter(mtfr.Name)
                If TypeOf vFC.GetFilterBase Is GSCOM.UI.DataFilter.LookUpFilter Then
                    bb = CType(vFC.GetFilterBase, GSCOM.UI.DataFilter.LookUpFilter)
                    MainModule.InitLookUp(bb.UpperFilter, CType(mtfr.ID_Menu, Database.Menu))
                    Dim bdlf As New BrowserDataListForm(CType(mtfr.ID_Menu, Database.Menu), False, , True)
                    With bdlf
                        .FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
                        .ControlBox = False
                        .StartPosition = FormStartPosition.Manual
                        .CloseAfterSelect = False
                    End With
                    bb.UpperFilter.Worker.Form = bdlf
                    'vFC.Text = bb.UpperFilter.Text
                End If
            Next
        Catch ex As Exception
            GSCOM.Common.Common.WriteEventLogEntry(ex)
            Throw ex
        End Try
    End Sub

#End Region

#Region "mMenuTable"
    'Private Function mMenuTable() As DataTable
    '    Return nDB.MenuTable
    'End Function

#End Region

#Region "ListExists"
    'Private Function ListExists(ByVal pRow As DataRow) As GSCOM.Interfaces.ZIDataTabPageList
    '    Dim f As TabPage
    '    Dim lf As GSCOM.Interfaces.ZIDataTabPageList
    '    For Each f In tcMain.TabPages
    '        If TypeOf f Is GSCOM.Interfaces.ZIDataTabPageList Then
    '            lf = CType(f, GSCOM.Interfaces.ZIDataTabPageList)
    '            If lf.Row Is pRow Then
    '                Return lf
    '            End If
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    're: respawning of tab: do not use row because menu can be regenerated. Use id
    Private Function ListExists(ByVal pID As Integer) As GSCOM.Interfaces.ZIDataTabPageList
        Dim f As TabPage
        Dim lf As GSCOM.Interfaces.ZIDataTabPageList
        For Each f In tcMain.TabPages
            If TypeOf f Is GSCOM.Interfaces.ZIDataTabPageList Then
                lf = CType(f, GSCOM.Interfaces.ZIDataTabPageList)
                If CInt(lf.Row("ID")) = pID Then
                    Return lf
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function ListExistsByName(ByVal tabName As String) As TabPage
        Dim f As TabPage
        For Each f In tcMain.TabPages
            If f.Name = tabName Then
                Return f
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "LoadList"
    Friend Sub LoadList(ByVal pID As Integer)
        Try
            Dim mr As New Database.MenuRow
            mr.InnerRow = nDB.MenuTable.Select("ID=" & pID.ToString)(0)
            If mr.DataSource <> "" Then
                If mr.ID_MenuType <> 4 Then
                    Dim lf As GSCOM.Interfaces.ZIDataTabPageList

                    'Else
                    '   pOddBackColor = Color.Green
                    'End If
                    lf = ListExists(pID)
                    If IsNothing(lf) Then
                        If mr.ReportFile = "" Then
                            Select Case mr.ID_MenuType
                                Case 3 'text
                                    Dim tdl As New TextDataList
                                    lf = New DataListTabPage(mr.DataSource, MainModule.gConnection, tdl)
                                    InitList(mr, tdl)
                                Case Else
                                    Dim b As Boolean
                                    Try
                                        b = CBool(nDB.GetSetting(Database.SettingEnum.Reserved01))
                                    Catch ex As Exception
                                    End Try
                                    Dim vDisplayColumnsTable As DataTable = nDB.GetDisplayColumnsTable(pID) 'vSettings.GetSetting(UI.DataList.Settings.KeyEnum.SelectString, "* ")
                                    If b Then
                                        lf = New BrowserDataListTabPage(mr.DataSource, MainModule.gConnection, "", vDisplayColumnsTable, mr.Sort, CType(pID, Database.Menu))
                                    Else
                                        lf = New GSCOM.UI.DataList.DataListTabPage(mr.DataSource, MainModule.gConnection, vDisplayColumnsTable, mr.Sort)
                                    End If
                                    InitDataListBase(lf, mr)
                            End Select
                        Else
                            Dim pds As New DataSet
                            Dim dt As DataTable
                            dt = GSCOM.SQL.TableQuery("SELECT Name,DataSource FROM tMenuSubDataSource msds WHERE ID_Menu=" & pID.ToString & " ORDER BY SeqNo,ID", gConnection)
                            Dim rv As New ZReportViewer
                            rv.SubDataSource = dt
                            lf = New DataListTabPage(mr.DataSource, MainModule.gConnection, rv)
                            rv.ReportTitle = mr.Name
                            InitList(mr, rv)
                        End If
                        InitZIDataTabPageList(lf, mr)
                        AddHandler lf.ButtonClick, AddressOf ButtonClick
                        tcMain.TabPages.Add(CType(lf, TabPage))


                    End If

                    tcMain.SelectedTab = CType(lf, TabPage)

                    'Else
                    '    Dim tst As TabPage
                    '    tst = ListExistsByName(mr.Name)
                    '    If IsNothing(tst) Then
                    '        tst = New TabPage(mr.Name)
                    '        tst.Name = mr.Name
                    '        tst.ImageIndex = gImageList.Images.IndexOfKey(mr.ImageFile)

                    'tst.Controls.Add(New ResumeSearch(tst.Name, mr.Name))
                    'tcMain.TabPages.Add(tst)
                End If

                '    tcMain.SelectedTab = CType(tst, TabPage)

            End If
            If Not tcMain.Focused Then
                tvMain.Focus()
            End If

            If mr.ID_MenuType = 5 Then

                Dim tst As TabPage

                tst = ListExistsByName(mr.Name)
                If IsNothing(tst) Then
                    tst = New TabPage(mr.Name)
                    tst.Name = mr.Name
                    tst.ImageIndex = gImageList.Images.IndexOfKey(mr.ImageFile)
                    tcMain.TabPages.Add(tst)
                    cusfrm(0) = New CustomizeContainer
                    ctrlOrgChart = New CtrlOrganizationalChart
                    getCustomizeForm(mr.Name, tst)
                    getCustomizeFormContainer()
                    tst.Controls.Add(cusfrm(0))
                    cusfrm(0).pnlContainer.Controls.Add(ctrlOrgChart)
                    AddHandler cusfrm(0).tsbClose.Click, AddressOf OrgChart_tsbCloseClick

                End If
                tcMain.SelectedTab = CType(tst, TabPage)
            End If
            If Not tcMain.Focused Then
                tvMain.Focus()
            End If


        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

#End Region

#Region "LoadTree"

    Delegate Sub dgLoadTree(dtsource As DataTable)
    Private Sub LoadTree(Optional dtsource As DataTable = Nothing)
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New dgLoadTree(AddressOf LoadTree), dtsource)
            Else
                If dtsource Is Nothing Then
                    GSCOM.UI.Common.PopulateTreeView(tvMain.Nodes, nDB.MenuTable, DBNull.Value, False, False)
                    GSCOM.UI.Common.PopulateMenu(mnuFolders, nDB.MenuTable, DBNull.Value, imgList)
                    PopulateFavorites() 'ROBBIE 20101014
                Else
                    GSCOM.UI.Common.PopulateTreeView(tvMain.Nodes, dtsource, DBNull.Value, False, False)
                    GSCOM.UI.Common.PopulateMenu(mnuFolders, dtsource, DBNull.Value, imgList)
                End If

                AddMenuClickHandlers(mnuFolders)

                If CollapseButton.Checked Then tvMain.CollapseAll()
                If ExpandButton.Checked Then tvMain.ExpandAll()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    Private Function MenuIsInTreeView(ByVal nc As TreeNodeCollection, ByVal pMenu As Integer) As TreeNode
        For Each n As TreeNode In nc
            If n.Name = pMenu.ToString Then
                Return n
            Else
                'If MenuIsInTreeView(n.Nodes, pMenu) Then
                '    Return True
                'End If
                Dim ret As TreeNode = MenuIsInTreeView(n.Nodes, pMenu)
                If ret IsNot Nothing Then
                    Return ret
                End If
            End If
        Next
        Return Nothing


    End Function

    Private Sub PopulateFavorites()
        If Me.FavoritesToolStripMenuItem.Checked Then

            Dim s As String
            Dim b As ToolStripButton
            FavToolStrip.ImageScalingSize = New Size(16, 16)
            FavToolStrip.Items.Clear()
            For Each drv As DataRowView In nDB.UserFavMenuTable.DefaultView
                s = "ID=" & drv("ID").ToString '& " AND IsActive"
                Dim n As TreeNode = Me.MenuIsInTreeView(tvMain.Nodes, CInt(drv("ID_Menu")))
                If n IsNot Nothing Then
                    'b = New ToolStripButton(drv("Menu").ToString, New Bitmap(nDB.ImagePath(drv("ImageFile").ToString)), AddressOf FoldersMenuClick)
                    b = New ToolStripButton(drv("Menu").ToString, gImageList.Images(n.ImageKey), AddressOf FoldersMenuClick)
                    With b
                        .Name = drv("ID_Menu").ToString
                        .DisplayStyle = ToolStripItemDisplayStyle.Image
                    End With
                    AddHandler b.MouseDown, AddressOf ShowRemoveContext

                    FavToolStrip.Items.Add(b)
                End If
            Next
        End If

    End Sub

    Private Sub ShowRemoveContext(ByVal sender As System.Object, ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim b As ToolStripButton
            b = CType(sender, ToolStripButton)
            FavStripContextMenu.Show(MousePosition.X, MousePosition.Y)
            ClickedFavorite = CInt(b.Name)
            'RemoveFavoritesStripMenu_Click(b, New System.EventArgs)
        End If

    End Sub

#Region "AddMenuClickHandlers"

    Private Sub AddMenuClickHandlers(ByVal nc As ToolStripDropDownItem)
        Dim a As ToolStripItem
        For Each a In nc.DropDownItems
            AddHandler a.Click, AddressOf FoldersMenuClick
            AddMenuClickHandlers(CType(a, ToolStripDropDownItem))
        Next
    End Sub

#End Region

#Region "PreLoadList"
    Private Sub PreLoadList(ByVal m As ToolStripItem)
        LoadList(CInt(m.Name))
    End Sub

    Private Sub PreLoadList(ByVal tv As TreeView)
        If Not IsNothing(tv.SelectedNode) Then
            LoadList(CInt(tv.SelectedNode.Name))
        End If
    End Sub

    'Private Sub PreLoadList(ByVal pID As Integer)
    '    Dim s As String
    '    s = pRow.Item("DataSource").ToString
    '    If s <> "" Then
    '        LoadList(pID)
    '    End If
    'End Sub

#End Region

#Region "mnuWindowCloseAll_Click"
    Private Sub mnuWindowCloseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWindowCloseAll.Click
        CloseAll()
    End Sub

#End Region

#Region "CloseAll"
    Private Sub CloseAll()
        Dim tp As TabPage
        For Each tp In tcMain.TabPages
            If tp IsNot TabPage1 Then
                CloseTabPage(tp)
            End If
        Next
        SaveFavorites()
    End Sub

    Sub SaveFavorites()
        Dim dr As DataRow
        Dim ctr As Integer = 1
        For Each i As ToolStripItem In FavToolStrip.Items
            dr = nDB.UserFavMenuTable.Select("ID_Menu=" & i.Name)(0)
            dr(Database.Tables.tUserFavMenu.Field.SeqNo.ToString) = ctr
            ctr += 1
        Next
        nDB.UserFavMenuTable.Update()
    End Sub

#End Region

#Region "CloseTabPage"

    Friend Sub CloseTabPage(ByVal pTabPage As TabPage)
        '''''test''''
        Try
            tcMain.SelectedIndex = CInt(IIf(tcMain.TabPages.IndexOf(pTabPage) = tcMain.TabPages.Count - 1, tcMain.TabPages.Count - 2, tcMain.TabPages.Count - 1))
        Catch
        End Try
        '''''''
        tcMain.TabPages.Remove(pTabPage)
        pTabPage.Dispose()
        pTabPage = Nothing
        GC.Collect()
    End Sub

#End Region

#Region "mnuHelpAbout_Click"
    Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        Dim f As Form = New AboutForm
        f.ShowDialog()
    End Sub

#End Region

#Region "tvMain"

    Private Sub tvMain_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvMain.DragDrop
        MsgBox("test")
    End Sub

    Private Sub tvMain_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvMain.DragEnter
        'MsgBox("test")
        'If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
        '    'TreeNode found allow move effect
        '    MsgBox("test")
        '    e.Effect = DragDropEffects.Move
        'Else
        '    'No TreeNode found, prevent move
        '    e.Effect = DragDropEffects.None
        'End If
        e.Effect = DragDropEffects.Move
    End Sub
    Private Sub tvMain_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvMain.DragOver
        MsgBox("test")
    End Sub
    Private Sub tvMain_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvMain.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub
    Private Sub tvMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            PreLoadList(tvMain)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tvMain_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvMain.AfterSelect
        If CBool(e.Action And TreeViewAction.ByMouse) Then

            Me.Cursor = Cursors.WaitCursor
            Task.Factory.StartNew(Sub() BeginInvoke(New MethodInvoker(Sub() PreLoadList(tvMain))))
            Me.Cursor = Cursors.Default

        End If

    End Sub

#End Region

#Region "mnuViewRefresh_Click"
    Public Sub viewRefresh()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf viewRefresh))
        Else
            nDB.LoadTables()
            gRefreshSettings()
            Navigate()
            LoadImages()
            LoadTree()
            nDB.RefreshLookUp() 'ROBBIE 20061125
            gMainForm.gInfoSetCollection.Clear()
        End If
    End Sub
    Private Sub mnuViewRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewRefresh.Click
        ''billy 03-01-2011
        Me.viewRefresh()
        'nDB.LoadTables()
        'gRefreshSettings()
        'LoadImages()
        'LoadTree()
        'nDB.RefreshLookUp() 'ROBBIE 20061125
        'gMainForm.gInfoSetCollection.Clear()
    End Sub

#End Region

#Region "FoldersMenuClick"
    Private Sub FoldersMenuClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PreLoadList(CType(sender, ToolStripItem))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "LoadImages"

    Private Sub LoadImages()
        Dim s As String
        imgList.ImageSize = New Size(CInt(nDB.GetSetting(Database.SettingEnum.IconWidth)), CInt(nDB.GetSetting(Database.SettingEnum.IconHeight)))
        tvMain.ItemHeight = imgList.ImageSize.Height + 2
        s = nDB.nGlobal.ResourcePath   'nDB.GetSetting(Database.SettingEnum.ResourcePath)
        If IO.Directory.Exists(s) Then
            Dim vImages() As String
            vImages = GSCOM.SQL.ColumnStringValues(nDB.MenuSet.tSystemImage, "ImageFile")
            Array.Sort(vImages, New System.Collections.CaseInsensitiveComparer())
            LoadImagesCore(imgList, vImages, s)
        End If
    End Sub

    Private Sub LoadImagesCore(ByVal img As ImageList, ByVal vImages() As String, ByVal vPath As String)
        Dim s As String
        Dim bmp As Bitmap
        Dim defbmp As Bitmap
        Try
            img.Images.Clear()
            defbmp = New Bitmap(img.ImageSize.Width, img.ImageSize.Height, 4, Imaging.PixelFormat.Format32bppArgb, IntPtr.Zero)
            For Each s In vImages
                Try
                    Dim fn As String = IO.Path.Combine(vPath, s)
                    If IO.File.Exists(fn) Then
                        bmp = New Bitmap(fn)
                        bmp = GSCOM.Grafix.ResizeImage(bmp, img.ImageSize.Width, img.ImageSize.Height)
                        img.Images.Add(s, bmp)
                    Else
                        img.Images.Add(s, defbmp)
                    End If
                Catch ex As Exception
                    img.Images.Add(s, defbmp)
                End Try
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "ToggleVisualStyles"
    Private Sub ToggleVisualStyles()
        'Change whether or not the folders pane is visible
        Me.Cursor = Cursors.WaitCursor
        'VisualStylesToolStripButton.Checked = Not VisualStylesToolStripButton.Checked
        'If VisualStylesToolStripButton.Checked Then
        '    Application.VisualStyleState = VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled
        'Else
        '    Application.VisualStyleState = VisualStyles.VisualStyleState.NoneEnabled
        'End If
        Me.Cursor = Cursors.Default

    End Sub

#End Region

#Region "ToggleFoldersVisible"

    Private Sub ToggleFoldersVisible()
        FoldersToolStripMenuItem.Checked = Not FoldersToolStripMenuItem.Checked
        'FoldersToolStripButton.Checked = FoldersToolStripMenuItem.Checked
        Me.spcMain.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
    End Sub

#End Region

    Private Sub BackUpDatabase()
        Dim s As String
        s = "EXEC pBackUpDatabase " & gConnection.Database
        GSCOM.SQL.ExecuteNonQuery(s, gConnection)
    End Sub

#Region "ALERT"

    Public Sub InstantiateAlert()
        Dim x, y As Integer
        x = Me.Bounds.Width - 200
        y = Me.Bounds.Height - 200

        'mobjController = New AgentObjects.Agent()
        AlertClass.SetPosition(x, y)
        ' AlertClass.NewChar(mobjController, mobjCharacter)
        ' AlertClass.NewChar(Me)
    End Sub

#End Region

    Private Sub UpdateMenuColors()
        Dim s As String
        Dim dt As DataTable
        Dim mr As New Database.MenuRow
        s = "SELECT ID, ImageFile,ReportFile,ID_MenuType FROM tMenu WHERE DataSource IS NOT NULL AND IsActive=1"
        dt = GSCOM.SQL.TableQuery(s, gConnection)
        Dim bmp As Bitmap
        Dim c, dc As Color
        For Each dr As DataRow In dt.Select
            mr.InnerRow = dr
            If mr.ImageFile = "" OrElse (Not IO.File.Exists(nDB.ImagePath(mr.ImageFile))) Then
                c = Color.Gainsboro
                dc = Color.DarkGray
            Else
                If mr.ReportFile <> "" Then
                    c = Color.LightGreen
                    dc = Color.DarkGreen
                Else
                    If mr.ID_MenuType = 3 Then 'TextFile
                        c = Color.LightSteelBlue
                        dc = Color.SteelBlue
                    Else
                        bmp = New Bitmap(nDB.ImagePath(mr.ImageFile))
                        bmp = GSCOM.Grafix.ResizeImage(bmp, 16, 16)
                        c = GSCOM.Grafix.DominantColor(bmp)
                        If c <> Color.White Then
                            c = GSCOM.Grafix.RectifyColor(c, 240)
                            Dim r, g, b As Integer
                            r = CInt(c.R / 1.75)
                            g = CInt(c.G / 1.75)
                            b = CInt(c.B / 1.75)
                            dc = Color.FromArgb(r, g, b)
                        Else
                            c = Color.Gainsboro
                            dc = Color.DarkGray
                        End If
                    End If
                End If
            End If

            s = "UPDATE tMenu Set"
            s &= " ColorRGB = " & GSCOM.SQL.SQLFormat(Hex(c.R) & Hex(c.G) & Hex(c.B)).ToLower
            s &= ",DarkColorRGB = " & GSCOM.SQL.SQLFormat(Hex(dc.R) & Hex(dc.G) & Hex(dc.B)).ToLower
            s &= " WHERE ID=" & mr.ID
            GSCOM.SQL.ExecuteNonQuery(s, gConnection)
        Next
    End Sub

#Region "Button"

    Public Sub OrgChart_tsbCloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.tcMain.SelectedTab.Dispose()
        Me.tcMain.SelectedIndex = Me.tcMain.TabCount() - 1
    End Sub

#End Region

    Sub RootPath()
        If Me.InvokeRequired Then
            Me.Invoke(New dgRootPath(AddressOf RootPath))
        Else
            nDB.nGlobal.Update()

            If nDB.nGlobal.IsNetGlobal = 0 Then
                '''''BILLY 02-28-2011
                Dim m As String
                If Not IsSharedContents Then
                    Dim od As New OpenFileDialog

                    With od
                        .CheckFileExists = False
                        .CheckPathExists = True
                        .Multiselect = False
                        .FileName = "(Folder)"
                        .Filter = "Folders only|*.FOLDER"
                        If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                            If od.FileName.Length > 15 Then
                                m = Strings.Left(od.FileName, od.FileName.Length - 15)
                                If m <> "" Then
                                    GSCOM.SQL.ExecuteNonQuery("pSetRootPath '" & IO.Path.Combine(m) & "'", gConnection)
                                    nDB.nGlobal.LoadSettingTableIfNull(True)
                                    nDB.nGlobal.Update()
                                    MsgBox("Root path successfully updated. InSys will refresh after you click OK.")
                                    viewRefresh()
                                End If
                            End If
                        End If

                    End With
                Else
                    '-----------------20130416 -EMIL----------------------------------------|
                    lg.DName.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ServerName')", gConnection).ToString
                    lg.UName.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('UserName')", gConnection).ToString
                    lg.PWord.Text = GSCOM.Common.EncryptA(GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('Password')", gConnection).ToString, 41)
                    lg.ReportPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ReportPath')", gConnection).ToString
                    lg.ResourcesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ResourcePath')", gConnection).ToString
                    lg.PhotosPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('PhotoPath')", gConnection).ToString
                    lg.StyleSheetsPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('StyleSheetPath')", gConnection).ToString
                    lg.TemplatesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ExcelTemplatePath')", gConnection).ToString
                    lg.FilesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('FilePath')", gConnection).ToString
                    lg.ShowDialog()

                    If CBool(lg.Connected) Then
                        GSCOM.SQL.ExecuteNonQuery("EXEC dbo.pUpdateContentsPath '" & lg.DName.Text & "', '" & lg.UName.Text & "', '" & GSCOM.Common.EncryptA(lg.PWord.Text, 41) & "', '" & lg.ReportPath.Text & "', '" & _
                            lg.ResourcesPath.Text & "', '" & lg.PhotosPath.Text & "', '" & lg.StyleSheetsPath.Text & "', '" & lg.TemplatesPath.Text & "', '" & lg.FilesPath.Text & "'", gConnection)
                        viewRefresh()
                    End If
                    '-----------------20130416 -EMIL-----------------------------------------
                End If
            Else
                Try
                    Using wbserv As New z.Web.Service.Client(nDB.nGlobal.GetNetPath)
                        Dim g As New DownloadBarForm
                        g.Show(Me)

                        Dim thrd As New Threading.Thread(Sub() ProcessRootPath(wbserv, g))
                        thrd.Start()

                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub

#Region " Update Root Path Net Global "

    Sub ProcessRootPath(wbserv As z.Web.Service.Client, g As DownloadBarForm, Optional Silent As Boolean = False)
        Try
            If Silent = False Then g.MainStatus("Contacting Server")
            wbserv.Connect() 'throws when not connected

            If Silent = False Then g.MainStatus("Checking Local Path")
            Dim lcpath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents")
            GenFolder(lcpath)

            If Silent = False Then g.MainStatus("Getting List of Contents")
            Dim rs As z.Web.Service.Result = wbserv.GetResult(wbserv.GetData("PATCHER/GetList?dir=Contents"), "GetListResult")

            If Silent = False Then g.MainStatus("Preparing...")
            Dim contents As List(Of Tuple(Of String, String, DateTime)) = wbserv.GetDataSet(Of List(Of Tuple(Of String, String, DateTime)))(rs)

            If Silent = False Then
                g.SetMaxProgress(contents.Count)
            Else
                UpdateWebBar(0, contents.Count)
            End If

            Dim inifile As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData, "Contents.ini")

            Dim localList As New List(Of Tuple(Of String, DateTime))

            If Silent = False Then g.MainStatus("Reading Local version")
            Dim strplt() As String
            If System.IO.File.Exists(inifile) Then
                For Each Str As String In System.IO.File.ReadAllLines(inifile)
                    strplt = Str.Split(CChar(vbTab))
                    localList.Add(New Tuple(Of String, DateTime)(strplt(0), Convert.ToDateTime(strplt(1))))
                Next
            End If

            If Silent = False Then g.MainStatus("Downloading Contents")

            Dim logini As New List(Of String)
            Dim i As Int32 = 0
            For Each s As Tuple(Of String, String, DateTime) In contents

                If Silent = False Then
                    If g.IsCancelled Then Throw New Exception("The process has been cancelled.")
                End If

                Dim fle As String = s.Item2.Replace(s.Item1, "")

                If fle.ToLower.Contains("thumbs.db") Then Continue For

                Dim fldr As String = lcpath & fle

                If Silent = False Then
                    g.SubStatus(".." & fle, i)
                Else
                    UpdateWebBar(i, 0)
                End If

                Dim isnew As String = (From kf As Tuple(Of String, DateTime) In localList
                            Where kf.Item1 = fldr And kf.Item2.ToString("yyyy/MM/dd HH:mm tt") = s.Item3.ToString("yyyy/MM/dd HH:mm tt")
                            Select kf.Item1).SingleOrDefault  'And CDate(kf.Item2) = CDate(s.Item3)

                If IsNothing(isnew) Then
                    rs = wbserv.GetResult(wbserv.GetData(String.Format("PATCHER/GetFile?Dir={0}", s.Item2.Replace("\", "$").Replace(":", "~"))), "GetFileResult")
                    GenFolder(System.IO.Path.GetDirectoryName(fldr))
                    wbserv.WriteFile(fldr, System.Text.ASCIIEncoding.Default.GetBytes(rs.ResultSet))
                End If

                i += 1
                logini.Add(fldr & vbTab & s.Item3)
            Next

            System.IO.File.WriteAllText(inifile, String.Join(vbCrLf, logini.ToArray()))

            'Upload Local Items
            If Silent = False Then g.MainStatus("Synching Contents")

            Dim WebRootPath As String = wbserv.GetResult(wbserv.GetData("PATCHER/GetRootPath"), "GetRootPathResult").ResultSet

            Dim strpath() As String
            Dim strfolder() As String = New String() {"Files", "Photos"} 'Add new folder if theres new content

            Dim localListSync As New List(Of String)
            For Each Str As String In System.IO.File.ReadAllLines(inifile)
                strplt = Str.Split(CChar(vbTab))

                strpath = strplt(0).Split(New String() {"\Contents\"}, StringSplitOptions.RemoveEmptyEntries)
                If strpath.Length > 1 Then

                    Dim k As String = (From j As String In strfolder
                                      Where strpath(1).Contains(j & "\") = True
                                      Select strpath(1).Replace(j & "\", "")).SingleOrDefault

                    If Not IsNothing(k) Then
                        localListSync.Add(k)
                    End If
                End If
            Next

            For Each lgpath As String In strfolder

                Dim locpath As String = System.IO.Path.Combine(lcpath, lgpath)
                GenFolder(locpath) ' what if not exist? then create it

                Dim jlst() As String = System.IO.Directory.GetFiles(locpath)

                If Silent = False Then
                    g.SetMaxProgress(jlst.Count)
                Else
                    UpdateWebBar(0, jlst.Count)
                End If

                i = 0

                For Each iph As String In jlst
                    Dim k As String = (From j As String In localListSync
                                      Where System.IO.Path.GetFileName(iph) = j
                                      Select j).SingleOrDefault

                    If iph.ToLower.Contains("thumbs.db") Then Continue For

                    If Silent = False Then
                        g.SubStatus(String.Format("..{0}/{1}", lgpath, System.IO.Path.GetFileName(iph)), i)
                    Else
                        UpdateWebBar(i, 0)
                    End If

                    If IsNothing(k) Then 'If not match means the file is not available to server so we must upload it
                        wbserv.PostData("PATCHER/UploadFile/" & System.IO.Path.Combine(WebRootPath, "Contents", "lgpath", System.IO.Path.GetFileName(iph)).Replace("\", "$").Replace(":", "~"),
                                        wbserv.ConvertFile(iph))
                    End If

                    i += 1
                Next
            Next

            nDB.nGlobal.Update()
            If Silent = False Then
                RelocateResourcesValues(lcpath & "\")
            Else
                UpdateWebBar(0, 0, True)
            End If
        Catch ex As Exception
            If Silent = False Then
                MessageBox.Show(ex.Message)
                GSCOM.SQL.ExecuteNonQuery("update tsetting set value = 0 where name = 'RESOURCE UPDATE'", gConnection)
                RootPath()
            Else
                'Dim ee As New EventLog
                'ee.WriteEntry(ex.Message, EventLogEntryType.Warning)
                System.IO.File.AppendAllText(System.IO.Path.Combine(Application.StartupPath, "ContentError.err"), ex.Message & vbCrLf)
            End If
        Finally
            If Silent = False Then g.Finish(True)

            Try
                System.Threading.Thread.CurrentThread.Abort()
            Catch
            End Try
            GC.Collect()
        End Try
    End Sub

    Delegate Sub dgRelocateResourcesValues(m As String)
    Sub RelocateResourcesValues(m As String)
        If Me.InvokeRequired Then
            Me.Invoke(New dgRelocateResourcesValues(AddressOf RelocateResourcesValues), m)
        Else
            MsgBox("Root path successfully updated. InSys will refresh after you click OK.")
            viewRefresh()
        End If
    End Sub

    Delegate Sub dgUpdateWebBar(i As Int32, max As Int32, done As Boolean)
    Sub UpdateWebBar(i As Int32, max As Int32, Optional done As Boolean = False)
        If Me.InvokeRequired Then
            Me.Invoke(New dgUpdateWebBar(AddressOf UpdateWebBar), i, max, done)
        Else
            If done Then
                Me.WebDwonloadBar.Visible = False
            Else
                If max > 0 Then
                    WebDwonloadBar.Maximum = max
                    WebDwonloadBar.Value = 0
                Else
                    WebDwonloadBar.Value = CInt(IIf(i > WebDwonloadBar.Maximum, WebDwonloadBar.Maximum, i))
                End If
            End If
        End If
    End Sub

#End Region

    Sub FilterMenu(op As eFilterOperation, val As String)
        Try
            If val.Trim <> "" Then
                Using dt As DataTable = nDB.MenuTable.Copy()
                    Dim h As IEnumerable(Of DataRow) = Nothing
                    Select Case op
                        Case eFilterOperation.Equals
                            h = From j As DataRow In dt.AsEnumerable
                                    Where j("Name").ToString().ToLower = val.Trim.ToLower
                                    Select j
                        Case eFilterOperation.Contains
                            h = From j As DataRow In dt.AsEnumerable
                                Where j("Name").ToString().ToLower.Contains(val.Trim.ToLower)
                                Select j
                        Case eFilterOperation.DoesNotContain
                            h = From j As DataRow In dt.AsEnumerable
                                Where j("Name").ToString().ToLower.Contains(val.Trim.ToLower) = False
                                Select j
                    End Select

                    If h.Count > 0 Then
                        LoadTree(RemapMenusource(h.CopyToDataTable(), nDB.MenuTable))
                    Else
                        ClearTreeItem()
                    End If
                End Using
            Else
                LoadTree()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function RemapMenusource(dt As DataTable, sourcedt As DataTable) As DataTable
        Dim retdt As DataTable = dt.Copy()
        For Each dr As DataRow In dt.Rows
            If dr("ID_Menu") Is DBNull.Value = False Then
                Dim k As IEnumerable(Of DataRow) = From j In sourcedt.AsEnumerable
                        Where Convert.ToInt32(j("ID")) = Convert.ToInt32(dr("ID_Menu"))
                        Select j
                recurMenuSource(retdt, k, sourcedt)
            End If
        Next
        Return retdt
    End Function

    Sub recurMenuSource(ByRef retdt As DataTable, sdr As IEnumerable(Of DataRow), sourcedt As DataTable)
        For Each dr As DataRow In sdr
            Dim jr As IEnumerable(Of DataRow) = From l As DataRow In retdt.AsEnumerable
                                               Where Convert.ToInt32(l("ID")) = Convert.ToInt32(dr("ID"))
                                               Select l
            If jr.Count = 0 Then
                retdt.ImportRow(dr)
                If dr("ID_Menu") Is DBNull.Value = False Then
                    Dim k As IEnumerable(Of DataRow) = From j In sourcedt.AsEnumerable
                            Where Convert.ToInt32(j("ID")) = Convert.ToInt32(dr("ID_Menu"))
                            Select j
                    recurMenuSource(retdt, k, sourcedt)
                End If
            End If
        Next
    End Sub

    Delegate Sub dgClearItem()
    Sub ClearTreeItem()
        If Me.InvokeRequired Then
            Me.Invoke(New dgClearItem(AddressOf ClearTreeItem))
        Else
            Me.tvMain.Nodes.Clear()
            Me.mnuFolders.DropDownItems.Clear()
        End If
    End Sub

    Sub CheckCollapsedExpand()
        CollapseButton.Checked = Not CollapseButton.Checked
        ExpandButton.Checked = Not ExpandButton.Checked
    End Sub

#End Region

#Region " Enumeration "

    Public Enum eFilterOperation As Int32
        Equals
        Contains
        DoesNotContain
    End Enum

#End Region

End Class
