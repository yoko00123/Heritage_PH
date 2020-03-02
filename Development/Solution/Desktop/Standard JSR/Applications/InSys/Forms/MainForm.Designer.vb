<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents ToolStripContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FavoritesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents mStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.mStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.WebDwonloadBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.mStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mUserLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mCompanyLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mServerLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mDatabaseLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileBackUpDatabase = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileLogOff = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFolders = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FavoritesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportEmployeeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TaxCalculatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWindow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWindowCloseAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetRootPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSystem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSystemGenerateTableDefinitions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSystemRunScript = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSystemTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateMenuColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.spcMain = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tvMain = New System.Windows.Forms.TreeView()
        Me.pnlMenuFilter = New System.Windows.Forms.Panel()
        Me.txtFilterValue = New System.Windows.Forms.TextBox()
        Me.btnFilterValue = New System.Windows.Forms.Button()
        Me.cmbOperator = New System.Windows.Forms.ComboBox()
        Me.btnOperator = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.FilterButton = New System.Windows.Forms.ToolStripButton()
        Me.ExpandButton = New System.Windows.Forms.ToolStripButton()
        Me.CollapseButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pLogo = New System.Windows.Forms.PictureBox()
        Me.FavToolStrip = New System.Windows.Forms.ToolStrip()
        Me.FavStripContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveFavoritesStripMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.TreeViewContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddTofavoritesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mStatusStrip.SuspendLayout()
        Me.mnuMain.SuspendLayout()
        Me.ToolStripContainer.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.LeftToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlMenuFilter.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.pLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FavStripContextMenu.SuspendLayout()
        Me.TreeViewContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'mStatusStrip
        '
        Me.mStatusStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.mStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WebDwonloadBar, Me.mStatusLabel, Me.mUserLabel, Me.mCompanyLabel, Me.mServerLabel, Me.mDatabaseLabel, Me.ToolStripStatusLabel1})
        Me.mStatusStrip.Location = New System.Drawing.Point(0, 0)
        Me.mStatusStrip.Name = "mStatusStrip"
        Me.mStatusStrip.Size = New System.Drawing.Size(1008, 25)
        Me.mStatusStrip.TabIndex = 6
        '
        'WebDwonloadBar
        '
        Me.WebDwonloadBar.Name = "WebDwonloadBar"
        Me.WebDwonloadBar.Size = New System.Drawing.Size(100, 19)
        Me.WebDwonloadBar.Visible = False
        '
        'mStatusLabel
        '
        Me.mStatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.mStatusLabel.Name = "mStatusLabel"
        Me.mStatusLabel.Size = New System.Drawing.Size(607, 20)
        Me.mStatusLabel.Spring = True
        Me.mStatusLabel.Text = " "
        '
        'mUserLabel
        '
        Me.mUserLabel.BackColor = System.Drawing.Color.Transparent
        Me.mUserLabel.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.mUserLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mUserLabel.Name = "mUserLabel"
        Me.mUserLabel.Size = New System.Drawing.Size(37, 20)
        Me.mUserLabel.Text = "User"
        '
        'mCompanyLabel
        '
        Me.mCompanyLabel.BackColor = System.Drawing.Color.Transparent
        Me.mCompanyLabel.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.mCompanyLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mCompanyLabel.Name = "mCompanyLabel"
        Me.mCompanyLabel.Size = New System.Drawing.Size(62, 20)
        Me.mCompanyLabel.Text = "Company"
        '
        'mServerLabel
        '
        Me.mServerLabel.BackColor = System.Drawing.Color.Transparent
        Me.mServerLabel.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.mServerLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mServerLabel.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.servers
        Me.mServerLabel.Name = "mServerLabel"
        Me.mServerLabel.Size = New System.Drawing.Size(65, 20)
        Me.mServerLabel.Text = "Server"
        '
        'mDatabaseLabel
        '
        Me.mDatabaseLabel.BackColor = System.Drawing.Color.Transparent
        Me.mDatabaseLabel.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.mDatabaseLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mDatabaseLabel.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.VSProject_database
        Me.mDatabaseLabel.ImageTransparentColor = System.Drawing.Color.Fuchsia
        Me.mDatabaseLabel.Name = "mDatabaseLabel"
        Me.mDatabaseLabel.Size = New System.Drawing.Size(78, 20)
        Me.mDatabaseLabel.Text = "Database"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(144, 20)
        Me.ToolStripStatusLabel1.Text = "Powered by Intellismart"
        '
        'mnuMain
        '
        Me.mnuMain.BackColor = System.Drawing.Color.Orange
        Me.mnuMain.Dock = System.Windows.Forms.DockStyle.None
        Me.mnuMain.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuFolders, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.mnuWindow, Me.mnuAdmin, Me.HelpToolStripMenuItem, Me.mnuSystem})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(1008, 24)
        Me.mnuMain.TabIndex = 0
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileBackUpDatabase, Me.ToolStripMenuItem2, Me.mnuFileLogOff, Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(38, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuFileBackUpDatabase
        '
        Me.mnuFileBackUpDatabase.Name = "mnuFileBackUpDatabase"
        Me.mnuFileBackUpDatabase.Size = New System.Drawing.Size(171, 22)
        Me.mnuFileBackUpDatabase.Text = "&BackUp Database"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(168, 6)
        '
        'mnuFileLogOff
        '
        Me.mnuFileLogOff.Name = "mnuFileLogOff"
        Me.mnuFileLogOff.Size = New System.Drawing.Size(171, 22)
        Me.mnuFileLogOff.Text = "&Log Off"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(171, 22)
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuFolders
        '
        Me.mnuFolders.Name = "mnuFolders"
        Me.mnuFolders.Size = New System.Drawing.Size(51, 20)
        Me.mnuFolders.Text = "Menu"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FavoritesToolStripMenuItem, Me.FoldersToolStripMenuItem, Me.StatusBarToolStripMenuItem, Me.ToolStripMenuItem1, Me.mnuViewRefresh})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ViewToolStripMenuItem.Text = "&View"
        '
        'FavoritesToolStripMenuItem
        '
        Me.FavoritesToolStripMenuItem.Checked = True
        Me.FavoritesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FavoritesToolStripMenuItem.Name = "FavoritesToolStripMenuItem"
        Me.FavoritesToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.FavoritesToolStripMenuItem.Text = "&Favorites"
        Me.FavoritesToolStripMenuItem.Visible = False
        '
        'FoldersToolStripMenuItem
        '
        Me.FoldersToolStripMenuItem.Checked = True
        Me.FoldersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FoldersToolStripMenuItem.Name = "FoldersToolStripMenuItem"
        Me.FoldersToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.FoldersToolStripMenuItem.Text = "&Menu"
        '
        'StatusBarToolStripMenuItem
        '
        Me.StatusBarToolStripMenuItem.Checked = True
        Me.StatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StatusBarToolStripMenuItem.Name = "StatusBarToolStripMenuItem"
        Me.StatusBarToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.StatusBarToolStripMenuItem.Text = "&Status Bar"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(128, 6)
        '
        'mnuViewRefresh
        '
        Me.mnuViewRefresh.Name = "mnuViewRefresh"
        Me.mnuViewRefresh.Size = New System.Drawing.Size(131, 22)
        Me.mnuViewRefresh.Text = "&Refresh"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportEmployeeToolStripMenuItem, Me.TaxCalculatorToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        Me.ToolsToolStripMenuItem.Visible = False
        '
        'ImportEmployeeToolStripMenuItem
        '
        Me.ImportEmployeeToolStripMenuItem.Name = "ImportEmployeeToolStripMenuItem"
        Me.ImportEmployeeToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ImportEmployeeToolStripMenuItem.Text = "Import Employee"
        Me.ImportEmployeeToolStripMenuItem.Visible = False
        '
        'TaxCalculatorToolStripMenuItem
        '
        Me.TaxCalculatorToolStripMenuItem.Name = "TaxCalculatorToolStripMenuItem"
        Me.TaxCalculatorToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.TaxCalculatorToolStripMenuItem.Text = "Tax Calculator"
        '
        'mnuWindow
        '
        Me.mnuWindow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuWindowCloseAll})
        Me.mnuWindow.Name = "mnuWindow"
        Me.mnuWindow.Size = New System.Drawing.Size(65, 20)
        Me.mnuWindow.Text = "Window"
        '
        'mnuWindowCloseAll
        '
        Me.mnuWindowCloseAll.Name = "mnuWindowCloseAll"
        Me.mnuWindowCloseAll.Size = New System.Drawing.Size(120, 22)
        Me.mnuWindowCloseAll.Text = "Close All"
        '
        'mnuAdmin
        '
        Me.mnuAdmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSetRootPath})
        Me.mnuAdmin.Name = "mnuAdmin"
        Me.mnuAdmin.Size = New System.Drawing.Size(55, 20)
        Me.mnuAdmin.Text = "&Admin"
        '
        'mnuSetRootPath
        '
        Me.mnuSetRootPath.Name = "mnuSetRootPath"
        Me.mnuSetRootPath.Size = New System.Drawing.Size(151, 22)
        Me.mnuSetRootPath.Text = "Set Root &Path"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.SearchToolStripMenuItem, Me.ToolStripSeparator6, Me.mnuHelpAbout, Me.CheckForUpdatesToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ContentsToolStripMenuItem.Text = "&Contents"
        Me.ContentsToolStripMenuItem.Visible = False
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.IndexToolStripMenuItem.Text = "&Index"
        Me.IndexToolStripMenuItem.Visible = False
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SearchToolStripMenuItem.Text = "&Search"
        Me.SearchToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(174, 6)
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Name = "mnuHelpAbout"
        Me.mnuHelpAbout.Size = New System.Drawing.Size(177, 22)
        Me.mnuHelpAbout.Text = "&About..."
        '
        'CheckForUpdatesToolStripMenuItem
        '
        Me.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem"
        Me.CheckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.CheckForUpdatesToolStripMenuItem.Text = "&Check for Updates"
        '
        'mnuSystem
        '
        Me.mnuSystem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuSystem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSystemGenerateTableDefinitions, Me.mnuSystemRunScript, Me.mnuSystemTest, Me.mnuUpdateMenuColor})
        Me.mnuSystem.Name = "mnuSystem"
        Me.mnuSystem.Size = New System.Drawing.Size(60, 20)
        Me.mnuSystem.Text = "System"
        '
        'mnuSystemGenerateTableDefinitions
        '
        Me.mnuSystemGenerateTableDefinitions.Name = "mnuSystemGenerateTableDefinitions"
        Me.mnuSystemGenerateTableDefinitions.Size = New System.Drawing.Size(223, 22)
        Me.mnuSystemGenerateTableDefinitions.Text = "Generate Table Definitions"
        '
        'mnuSystemRunScript
        '
        Me.mnuSystemRunScript.Name = "mnuSystemRunScript"
        Me.mnuSystemRunScript.Size = New System.Drawing.Size(223, 22)
        Me.mnuSystemRunScript.Text = "Run Script..."
        '
        'mnuSystemTest
        '
        Me.mnuSystemTest.Name = "mnuSystemTest"
        Me.mnuSystemTest.Size = New System.Drawing.Size(223, 22)
        Me.mnuSystemTest.Text = "Test"
        '
        'mnuUpdateMenuColor
        '
        Me.mnuUpdateMenuColor.Name = "mnuUpdateMenuColor"
        Me.mnuUpdateMenuColor.Size = New System.Drawing.Size(223, 22)
        Me.mnuUpdateMenuColor.Text = "Update Menu Colors"
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.BottomToolStripPanel
        '
        Me.ToolStripContainer.BottomToolStripPanel.Controls.Add(Me.mStatusStrip)
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.spcMain)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(982, 513)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        '
        'ToolStripContainer.LeftToolStripPanel
        '
        Me.ToolStripContainer.LeftToolStripPanel.Controls.Add(Me.FavToolStrip)
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(1008, 562)
        Me.ToolStripContainer.TabIndex = 7
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.mnuMain)
        '
        'spcMain
        '
        Me.spcMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.spcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.spcMain.Location = New System.Drawing.Point(0, 0)
        Me.spcMain.Name = "spcMain"
        '
        'spcMain.Panel1
        '
        Me.spcMain.Panel1.Controls.Add(Me.Panel2)
        Me.spcMain.Panel1.Controls.Add(Me.pnlMenuFilter)
        Me.spcMain.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'spcMain.Panel2
        '
        Me.spcMain.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.spcMain.Panel2.Controls.Add(Me.tcMain)
        Me.spcMain.Size = New System.Drawing.Size(982, 513)
        Me.spcMain.SplitterDistance = 250
        Me.spcMain.SplitterWidth = 1
        Me.spcMain.TabIndex = 0
        Me.spcMain.Text = "SplitContainer1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tvMain)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(164, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(86, 488)
        Me.Panel2.TabIndex = 38
        '
        'tvMain
        '
        Me.tvMain.BackColor = System.Drawing.SystemColors.Window
        Me.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvMain.FullRowSelect = True
        Me.tvMain.Location = New System.Drawing.Point(0, 0)
        Me.tvMain.Name = "tvMain"
        Me.tvMain.Size = New System.Drawing.Size(86, 488)
        Me.tvMain.TabIndex = 35
        '
        'pnlMenuFilter
        '
        Me.pnlMenuFilter.BackColor = System.Drawing.Color.White
        Me.pnlMenuFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMenuFilter.Controls.Add(Me.txtFilterValue)
        Me.pnlMenuFilter.Controls.Add(Me.btnFilterValue)
        Me.pnlMenuFilter.Controls.Add(Me.cmbOperator)
        Me.pnlMenuFilter.Controls.Add(Me.btnOperator)
        Me.pnlMenuFilter.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMenuFilter.Location = New System.Drawing.Point(0, 25)
        Me.pnlMenuFilter.Name = "pnlMenuFilter"
        Me.pnlMenuFilter.Size = New System.Drawing.Size(164, 488)
        Me.pnlMenuFilter.TabIndex = 37
        Me.pnlMenuFilter.Visible = False
        '
        'txtFilterValue
        '
        Me.txtFilterValue.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtFilterValue.Location = New System.Drawing.Point(0, 67)
        Me.txtFilterValue.Name = "txtFilterValue"
        Me.txtFilterValue.Size = New System.Drawing.Size(162, 21)
        Me.txtFilterValue.TabIndex = 3
        '
        'btnFilterValue
        '
        Me.btnFilterValue.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnFilterValue.Location = New System.Drawing.Point(0, 44)
        Me.btnFilterValue.Name = "btnFilterValue"
        Me.btnFilterValue.Size = New System.Drawing.Size(162, 23)
        Me.btnFilterValue.TabIndex = 2
        Me.btnFilterValue.Text = "Value"
        Me.btnFilterValue.UseVisualStyleBackColor = True
        '
        'cmbOperator
        '
        Me.cmbOperator.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperator.FormattingEnabled = True
        Me.cmbOperator.Items.AddRange(New Object() {"Equals", "Contains", "Does Not Contain"})
        Me.cmbOperator.Location = New System.Drawing.Point(0, 23)
        Me.cmbOperator.Name = "cmbOperator"
        Me.cmbOperator.Size = New System.Drawing.Size(162, 21)
        Me.cmbOperator.TabIndex = 1
        '
        'btnOperator
        '
        Me.btnOperator.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnOperator.Location = New System.Drawing.Point(0, 0)
        Me.btnOperator.Name = "btnOperator"
        Me.btnOperator.Size = New System.Drawing.Size(162, 23)
        Me.btnOperator.TabIndex = 0
        Me.btnOperator.Text = "Operator"
        Me.btnOperator.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterButton, Me.ExpandButton, Me.CollapseButton, Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(250, 25)
        Me.ToolStrip1.TabIndex = 36
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'FilterButton
        '
        Me.FilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FilterButton.Image = CType(resources.GetObject("FilterButton.Image"), System.Drawing.Image)
        Me.FilterButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FilterButton.Name = "FilterButton"
        Me.FilterButton.Size = New System.Drawing.Size(23, 22)
        Me.FilterButton.Text = "Filter"
        '
        'ExpandButton
        '
        Me.ExpandButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExpandButton.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.Collapsed
        Me.ExpandButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExpandButton.Name = "ExpandButton"
        Me.ExpandButton.Size = New System.Drawing.Size(23, 22)
        Me.ExpandButton.Text = "Expand All"
        '
        'CollapseButton
        '
        Me.CollapseButton.Checked = True
        Me.CollapseButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CollapseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CollapseButton.Image = Global.GSCOM.Applications.InSys.My.Resources.Resources.Expanded
        Me.CollapseButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CollapseButton.Name = "CollapseButton"
        Me.CollapseButton.Size = New System.Drawing.Size(23, 22)
        Me.CollapseButton.Text = "Collapse All"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.ForeColor = System.Drawing.Color.Black
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(38, 22)
        Me.ToolStripLabel1.Text = "Menu"
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.HotTrack = True
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(731, 513)
        Me.tcMain.TabIndex = 47
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pLogo)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(723, 487)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Start Page"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pLogo
        '
        Me.pLogo.Location = New System.Drawing.Point(214, 128)
        Me.pLogo.Name = "pLogo"
        Me.pLogo.Size = New System.Drawing.Size(500, 128)
        Me.pLogo.TabIndex = 0
        Me.pLogo.TabStop = False
        '
        'FavToolStrip
        '
        Me.FavToolStrip.AllowDrop = True
        Me.FavToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.FavToolStrip.CanOverflow = False
        Me.FavToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.FavToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.FavToolStrip.ImageScalingSize = New System.Drawing.Size(64, 64)
        Me.FavToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.FavToolStrip.Name = "FavToolStrip"
        Me.FavToolStrip.Size = New System.Drawing.Size(26, 513)
        Me.FavToolStrip.Stretch = True
        Me.FavToolStrip.TabIndex = 0
        '
        'FavStripContextMenu
        '
        Me.FavStripContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveFavoritesStripMenu})
        Me.FavStripContextMenu.Name = "TreeViewContext"
        Me.FavStripContextMenu.Size = New System.Drawing.Size(195, 26)
        '
        'RemoveFavoritesStripMenu
        '
        Me.RemoveFavoritesStripMenu.Name = "RemoveFavoritesStripMenu"
        Me.RemoveFavoritesStripMenu.Size = New System.Drawing.Size(194, 22)
        Me.RemoveFavoritesStripMenu.Text = "&Remove from favorites"
        '
        'imgList
        '
        Me.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgList.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        '
        'TreeViewContext
        '
        Me.TreeViewContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddTofavoritesToolStripMenuItem})
        Me.TreeViewContext.Name = "TreeViewContext"
        Me.TreeViewContext.Size = New System.Drawing.Size(159, 26)
        '
        'AddTofavoritesToolStripMenuItem
        '
        Me.AddTofavoritesToolStripMenuItem.Name = "AddTofavoritesToolStripMenuItem"
        Me.AddTofavoritesToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.AddTofavoritesToolStripMenuItem.Text = "Add to &favorites"
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(1008, 562)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Name = "MainForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mStatusStrip.ResumeLayout(False)
        Me.mStatusStrip.PerformLayout()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ToolStripContainer.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.LeftToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.LeftToolStripPanel.PerformLayout()
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel1.PerformLayout()
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlMenuFilter.ResumeLayout(False)
        Me.pnlMenuFilter.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tcMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.pLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FavStripContextMenu.ResumeLayout(False)
        Me.TreeViewContext.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents tvMain As System.Windows.Forms.TreeView
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents mnuWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWindowCloseAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuViewRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFolders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSystem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSystemGenerateTableDefinitions As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents DefaultLookAndFeel As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents mnuSystemTest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mDatabaseLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mServerLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mCompanyLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mUserLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents mStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuSystemRunScript As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportEmployeeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileBackUpDatabase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TaxCalculatorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateMenuColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FavToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ExpandButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CollapseButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFileLogOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAdmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetRootPath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TreeViewContext As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddTofavoritesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FavStripContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveFavoritesStripMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pLogo As System.Windows.Forms.PictureBox
    Friend WithEvents CheckForUpdatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebDwonloadBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents FilterButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMenuFilter As System.Windows.Forms.Panel
    Friend WithEvents txtFilterValue As System.Windows.Forms.TextBox
    Friend WithEvents btnFilterValue As System.Windows.Forms.Button
    Friend WithEvents cmbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents btnOperator As System.Windows.Forms.Button
    'Friend WithEvents AxAgent2 As AxAgentObjects.AxAgent

End Class
