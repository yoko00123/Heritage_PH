Option Explicit On
Option Strict Off

Imports GSCOM.UI.DataLookUp
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic

Friend MustInherit Class InfoSet
    Inherits Database.InfoSetBase
    Implements IDisposable

#Region "IDisposable"
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
                Table.Dispose()
                'Control.Dispose()
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

#Region "Classes"
    Friend Class SavedEventArgs
        Inherits EventArgs

        'Public Enum SaveModeEnum
        '    Insert
        '    Update
        'End Enum

        Public Transaction As SqlClient.SqlTransaction
        Public SaveQuery As String
        Public RowState As DataRowState
        Public Sub New(ByVal pTransaction As SqlClient.SqlTransaction, ByVal pRowState As DataRowState, pSaveQuery As String)
            MyBase.New()
            Transaction = pTransaction
            RowState = pRowState
        End Sub
    End Class

#End Region

    Friend Class CommitedEventArgs
        Inherits EventArgs
        Public MainTableChanged As Boolean
        Public SaveQuery As String
    End Class

#Region "Declarations"
    Protected mListing As DataTable
    Private mRect As Rectangle
    Protected WithEvents mForm As InfoForm
    'Protected MustOverride Property Control() As Control
    Protected Event Saved(ByVal sender As Object, ByVal e As SavedEventArgs)
    Protected Event Commited(ByVal sender As Object, ByVal e As CommitedEventArgs)
    Public NoGridTables As String
    Public mInitedTables As Specialized.StringCollection 'robbie 20090618 'temporary just for the transition re: menudetailtab
    Public isFinal As Boolean = False
    Public NoTransactionTables As String


#End Region

    Public Property Size As Size
        Get
            Return mForm.Size
        End Get
        Set(ByVal value As Size)
            mForm.Size = value
        End Set
    End Property

#Region "Additiontal"
    Public Property IsPosted As Boolean
        Get
            Return isFinal
        End Get
        Set(ByVal value As Boolean)
            isFinal = value
        End Set
    End Property

#End Region
#Region "Properties"

#Region "Text"
    Public Property Text() As String
        Get
            Return mForm.Text
        End Get
        Set(ByVal value As String)
            mForm.Text = value
        End Set
    End Property

#End Region

#Region "Rect"
    Public Property Rect() As Rectangle
        Get
            Return New Rectangle(mForm.Location, mForm.Size)
        End Get
        Set(ByVal value As Rectangle)
            mForm.Location = value.Location
            mForm.Size = value.Size
        End Set
    End Property

#End Region


#End Region

#Region "Constructors"
    Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(nDB, pID)
        mListing = pListing
        mForm = New InfoForm
        mForm.tcMain.ImageList = gImageList
        mForm.BasicTab.ImageList = gImageList
        'mForm.Panel1.BackColor = GSCOM.Grafix.ColorFromRGB(nDB.GetMenuValue (pid,  Database.Tables.tMenu.Field.ColorRGB )  
        mForm.HelpButton = True
        'mForm.StartPosition = FormStartPosition.Manual
        mInitedTables = New Specialized.StringCollection
        Dim b As Boolean = (nDB.GetUserID = 1)
        mForm.GenerateXMLFileToolStripMenuItem.Visible = b
        mForm.ImportXMLFileToolStripMenuItem.Visible = b
    End Sub

    Private Sub RelocateGrid(ByVal dt As DataTable, ByVal mdt As Database.MenuDetailTabRow)
        Dim ctrl As Control
        Dim dra As DataRow()
        dra = mInfoMenuSet.tMenuDetailTab.Select("TableName=" & GSCOM.SQL.SQLFormat(mdt.ParentTableName))
        If dra.Length > 0 Then
            ctrl = CType(Me.GetControl(TreeGrid.TreeGridPrefix & dra(0)("ID").ToString), Control)
            If ctrl IsNot Nothing Then
                Dim tg As TreeGrid
                tg = DirectCast(ctrl, TreeGrid)
                tg.DetailInfo = New DetailClass
                tg.DetailInfo.SetDetailInfo(dt, mdt.ParentColumn, mdt.ChildColumn)
            Else
                Dim tg As GSDetailDataGridView
                tg = DirectCast(Me.GetDataGridView(Me.mDataset.Tables(mdt.ParentTableName)), GSDetailDataGridView)
                ctrl = tg
                'EMIL 20130305----------------------------------------------------------
                If tg.DetailInfo Is Nothing Then
                    tg.DetailInfo = New DetailClass
                    tg.DetailInfo.SetDetailInfo(dt, mdt.ParentColumn, mdt.ChildColumn)
                Else
                    tg.DetailInfo2 = New DetailClass
                    tg.DetailInfo2.SetDetailInfo(dt, mdt.ParentColumn, mdt.ChildColumn)
                End If
                '-----------------------------------------------------------------------
            End If
            If ctrl IsNot Nothing Then
                Dim dg As DataGridView
                Dim ts As ToolStrip = Nothing
                Dim tp As Control
                Dim tp2 As Control
                Dim sp As SplitContainer
                dg = Me.GetDataGridView(dt)
                For Each c As Control In dg.Parent.Controls
                    If TypeOf (c) Is ToolStrip Then
                        ts = CType(c, ToolStrip)
                    End If
                Next


                tp = Me.GetTabPage(mdt.Name)
                tp2 = ctrl.Parent
                If tp2 IsNot Nothing Then
                    If TypeOf tp2 Is SplitterPanel Then
                        sp = CType(tp2.Parent, SplitContainer)
                        If tp2 Is sp.Panel1 Then
                            tp2 = sp.Panel2
                        End If
                    End If
                End If
                tp.Controls.Remove(dg)
                Dim dgx As Control = Nothing
                For Each c As Control In tp2.Controls
                    If c.Dock = DockStyle.Fill Then
                        dgx = c
                        Exit For
                    End If
                Next
                Dim tsx As ToolStrip = Nothing
                For Each c As Control In dgx.Parent.Controls
                    If TypeOf (c) Is ToolStrip Then
                        tsx = CType(c, ToolStrip)
                    End If
                Next

                sp = New SplitContainer
                With sp
                    .Dock = DockStyle.Fill
                    .FixedPanel = FixedPanel.None
                    If tsx IsNot Nothing Then .Panel1.Controls.Add(tsx)
                    .Panel1.Controls.Add(dgx)
                    dgx.BringToFront()
                    If ts IsNot Nothing Then .Panel2.Controls.Add(ts)
                    .Panel2.Controls.Add(dg)
                    dg.BringToFront()
                    tp2.Controls.Add(sp)
                    sp.BringToFront()
                    If TypeOf (tp2) Is SplitterPanel Then
                        .Orientation = Orientation.Vertical
                        .SplitterDistance = sp.Width \ 2
                    Else
                        .Orientation = Orientation.Horizontal
                        .SplitterDistance = sp.Height \ 2
                    End If
                End With
                dg.Dock = DockStyle.Fill
                tp.Parent.Controls.Remove(tp)
                tp.Dispose()
            End If
        End If
    End Sub

    Dim DetailMenus As New Collections.Generic.List(Of InfoSetDetailMenu)
    Protected ReportViewers As New Collections.Generic.List(Of InfoSetReportViewer)

    Protected Overrides Sub InitControl(ByVal pMenu As Integer)
        MyBase.InitControl(pMenu)
        Try
            Try
                Dim s As String = CStr(nDB.GetMenuValue(CType(Me.MenuID, Database.Menu), Database.Tables.tMenu.Field.ImageFile))
                mForm.Icon = GSCOM.Grafix.IconFromBitmap(gImageList.Images(s))
            Catch ex As Exception
                mForm.Icon = gIcon
            End Try
            Dim dt As GSCOM.SQL.ZDataTable
            Me.InitControl(mForm, pMenu, mInfoMenuSet.tMenuTab, mInfoMenuSet.tMenuTabField)
            For Each dr As DataRow In mInfoMenuSet.tMenuDetailTab.Select
                Dim mdt As New Database.MenuDetailTabRow(dr) 'IMPORTANT TO RENEW BECAUSE ITS BINDED TO TREEGRID

                dt = CType(mDataset.Tables(mdt.TableName), SQL.ZDataTable) ' New GSCOM.SQL.ZDataTable(gConnection, s)

                Select Case mdt.ID_MenuDetailTabType
                    Case Database.MenuDetailTabTypeEnum.Grid, Database.MenuDetailTabTypeEnum.Form
                        InitDetail(dt, New Database.MenuDetailTabRow(mdt.InnerRow))

                        RelocateGrid(dt, mdt)
                    Case Database.MenuDetailTabTypeEnum.TreeView
                        InitTreeGrid(dt, mdt)
                    Case Database.MenuDetailTabTypeEnum.List
                        Dim idl As New InfoSetDetailMenu(Me, mdt, CType(mdt.ID_DetailMenu, Database.Menu), mdt.ParentColumn, mdt.ChildColumn)
                        mForm.tcMain.TabPages.Add(idl.mPage)
                        Me.DetailMenus.Add(idl)
                    Case Database.MenuDetailTabTypeEnum.Report
                        Dim idl As New InfoSetReportViewer
                        idl.DataSource = mdt.DataSource
                        idl.ReportFile = mdt.ReportFile
                        idl.ReportTitle = mdt.Name
                        idl.Text = mdt.Name
                        mForm.AddTabPage(idl.mPage)
                        Me.ReportViewers.Add(idl)
                End Select

                Dim dgv As DataGridView
                dgv = Me.GetDataGridView(dt)
                If dgv IsNot Nothing Then
                    Dim mdtfr As New Database.MenuDetailTabFieldRow
                    For Each dr2 As DataRow In mInfoMenuSet.tMenuDetailTabField.Select(Database.Tables.tMenuDetailTabField.Field.ID_MenuDetailTab.ToString & "=" & mdt.ID.ToString)
                        mdtfr.InnerRow = dr2
                        If mdtfr.ReadOnly Then
                            Dim vdgc As DataGridViewColumn = dgv.Columns(mdtfr.Name)
                            If vdgc IsNot Nothing Then
                                With vdgc
                                    .ReadOnly = True
                                    .InheritedStyle.BackColor = GSCOM.Common.DefaultReadOnlyFieldBackColor
                                End With
                            End If
                        End If
                    Next
                End If
                If dt IsNot Nothing Then 'case of detail list!
                    mInitedTables.Add(dt.TableName)

                End If
            Next
            InitFileBrowser()

            If Me.Table.Columns.Contains("Comment") Then
                mForm.tcMain.AddTabPageWithTextBox("Comment")
            End If

            MainModule.InitControl(mForm.BasicTab, pMenu)
            MainModule.InitControl(mForm.tcMain, pMenu)

            Me.InitButtons()

        Catch ex As Exception
            MsgBox(pMenu & vbCrLf & ex.Message)

        End Try

    End Sub

    Protected Overrides Sub Finalize()
        mDataset = Nothing
        mForm = Nothing
        Connection = Nothing
        Me.Row = Nothing
        mListing = Nothing
        'Control = Nothing
        Table = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "Methods"

#Region "Private"

#Region "Navigate"
    'Private Sub Navigate()
    '    '20061105
    '    Dim ssp As String
    '    Dim tmp As String
    '    ssp = nDB.GetSetting(Database.SettingEnum.StyleSheetPath)
    '    If Table.Rows(0).RowState = DataRowState.Added Then
    '        ShowHelp()

    '    Else
    '        Dim s As String
    '        Dim xsl As String = ssp & Table.TableName & ".xsl"
    '        If IO.File.Exists(xsl) Then
    '            Dim a As New Xml.Xsl.XsltArgumentList
    '            s = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.CSS"
    '            a.AddParam("stylesheet", "", s)
    '            s = nDB.GetSetting(Database.SettingEnum.ResourcePath)
    '            a.AddParam("resourcepath", "", s)
    '            mForm.Navigate(Dataset, xsl, a)
    '        Else
    '            Dim a As New DocClass(mInfoMenuSet, Me.Dataset)
    '            a.StyleSheet = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.css"
    '            a.ResourcePath = nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString
    '            s = a.GetHTML
    '            tmp = IO.Path.GetTempFileName
    '            IO.File.WriteAllText(tmp, s)
    '            Try
    '                mForm.Navigate(tmp)
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End If
    'End Sub


    Private Sub Navigate()
        Dim tmp As String
        'Dim mMenuRow As New Database.MenuRow
        'mMenuRow.InnerRow = mInfoMenuSet.Tables(0).Rows(0)
        If Table.Rows(0).RowState = DataRowState.Added Then
            If IO.File.Exists(nDB.ImagePath(mMenuRow.ImageFile)) Then
                Dim bmp As Image
                bmp = Image.FromFile(nDB.ImagePath(mMenuRow.ImageFile))
                If bmp.Size = mForm.ImageBox.Size Then
                    mForm.ImageBox.Image = bmp
                Else
                    mForm.ImageBox.Image = bmp.GetThumbnailImage(mForm.ImageBox.Width, mForm.ImageBox.Height, Nothing, IntPtr.Zero)
                End If
            Else
                mForm.ImageBox.Image = Nothing
            End If
            mForm.NameLabel.Text = mMenuRow.Name
        Else
            If Me.Table.Columns.Contains("ImageFile") Then
                tmp = Me.Row("ImageFile").ToString
                If tmp = "" Then
                    tmp = nDB.ImagePath(mMenuRow.ImageFile)
                Else
                    tmp = nDB.ImagePath(tmp)
                End If
            Else
                tmp = nDB.ImagePath(mMenuRow.ImageFile)
            End If


            If IO.File.Exists(tmp) Then
                Dim bmp As Image
                bmp = Image.FromFile(tmp)
                If bmp.Size = mForm.ImageBox.Size Then
                    mForm.ImageBox.Image = bmp
                Else
                    mForm.ImageBox.Image = bmp.GetThumbnailImage(mForm.ImageBox.Width, mForm.ImageBox.Height, Nothing, IntPtr.Zero)
                End If
            Else
                mForm.ImageBox.Image = Nothing
            End If
            If Me.Table.Columns.Contains("Name") Then
                mForm.NameLabel.Text = Me.Row.Item("Name").ToString
            Else
                mForm.NameLabel.Text = mMenuRow.Name
            End If
        End If
    End Sub

    Private Sub ShowHelp()
        Dim s, tmp As String
        Dim f As New BrowserForm

        Dim ssp As String
        ssp = nDB.GetSetting(Database.SettingEnum.StyleSheetPath)
        Dim a As New Xml.Xsl.XsltArgumentList
        s = nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.CSS"
        a.AddParam("stylesheet", "", s)
        If IO.File.Exists(ssp & "_Help.xsl") Then
            Dim b As New Html.HelpClass(mInfoMenuSet.InnerDataSet)
            b.StyleSheet = nDB.nGlobal.StyleSheetPath 'nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.css"
            b.ResourcePath = nDB.nGlobal.ResourcePath  'nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString
            s = b.GetHTML
            tmp = IO.Path.GetTempFileName
            IO.File.WriteAllText(tmp, s)
            Try
                f.Size = mForm.Size
                f.StartPosition = mForm.StartPosition
                Try
                    f.MainBrowser.Navigate(tmp)
                    f.ShowDialog()
                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try
        Else
            'mForm.Navigate("")
            MsgBox("Error")
        End If
        Try
            'mForm.SelectTab("Browser")
        Catch ex As Exception
            If TypeOf (ex) Is InvalidOperationException Then

            Else
                MsgBox(ex.Message)
            End If
        End Try

    End Sub
#End Region

    Public Property Width() As Integer
        Get
            If mForm IsNot Nothing Then
                Return mForm.Width
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If mForm IsNot Nothing Then
                mForm.Width = value
            End If
        End Set
    End Property

    Public Property Height() As Integer
        Get
            If mForm IsNot Nothing Then
                Return mForm.Height
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If mForm IsNot Nothing Then
                mForm.Height = value
            End If
        End Set
    End Property

    Private Const ButtonPrefix As String = "mButton"
    Private Sub InitButtons()
        For Each dr As DataRow In mInfoMenuSet.tMenuButton.Select("ID_MenuDetailTab IS NULL")
            Dim s As String = ButtonPrefix & dr(Database.Tables.tMenuButton.Field.ID).ToString
            Dim b As ToolStripButton
            b = Me.GetStripButton(s)
            If b Is Nothing Then
                b = Me.AddButton(dr(Database.Tables.tMenuButton.Field.Name).ToString, gImageList.Images(dr(Database.Tables.tMenuButton.Field.ImageFile).ToString), AddressOf ButtonClick)
                b.Name = s
            Else
                AddHandler b.Click, AddressOf ButtonClick
            End If

        Next
    End Sub

    Private Function GetButtonID(ByVal b As ToolStripButton) As Integer
        Return CInt(Strings.Right(b.Name, Len(b.Name.ToString) - Len(ButtonPrefix)))
    End Function

#Region "ButtonClick"
    Public Overridable Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim b As ToolStripButton
        Dim s As String
        Dim dra As DataRow()
        Dim vID As Integer
        Dim conf As String
        Dim succ As String
        Dim GeneratedTextFile As String
        Dim DefaultFileName As String
        Dim t As String
        Dim cont As Boolean
        Dim dr As DataRow
        Try
            b = CType(sender, ToolStripButton)
            vID = GetButtonID(b)
            dra = mInfoMenuSet.tMenuButton.Select("ID=" & GSCOM.SQL.SQLFormat(vID))
            If dra.Length > 0 Then
                dr = dra(0)
                t = b.Text.Replace("&", "")
                Dim drp As DataRow = Nothing
                Dim dgvp As DataGridView
                Dim drvp As DataRowView
                If dr.Table.ParentRelations.Count > 0 Then
                    dgvp = Me.GetDataGridView(dr.Table.ParentRelations(0).ParentTable)
                    drvp = TryCast(dgvp.CurrentRow.DataBoundItem, DataRowView)
                    If drvp IsNot Nothing Then
                        drp = drvp.Row
                    End If
                End If
                conf = Me.PassParameters(dr.Item("ConfirmationText").ToString, dr.Table, drp)
                succ = Me.PassParameters(dr.Item("SuccessInfoText").ToString, dr.Table, drp)
                GeneratedTextFile = Me.PassParameters(dr.Item("IsGeneratedTextFile").ToString, dr.Table, drp)
                DefaultFileName = Me.PassParameters(dr.Item("DefaultFileName").ToString, dr.Table, drp)

                If Me.SaveButton.Enabled And Me.SaveButton.Visible And Me.CanSave Then
                    mForm.EndEdit()
                    If Me.HasUnsavedChanges _
                        And dr.RowState <> DataRowState.Added _
                        And CBool(dr("MustSaveFirst").ToString) Then
                        'DisabledOnNewInfo handles validation
                        Dim vCancel As Boolean
                        TrySave(vCancel, "Pending changes must be saved first. ")
                        If vCancel Then Exit Sub
                    End If
                End If

                Dim btnGenerateTextFile As Boolean = CBool(GeneratedTextFile)

                If succ = "" Then succ = "Done"
                If conf = "" Then
                    cont = True
                Else
                    cont = MsgBox(conf, MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes
                End If
                If cont Then
                    s = dr.Item(Database.Tables.tMenuButton.Field.CommandText.ToString).ToString.Trim
                    If s <> "" Then
                        s = Me.PassParameters(s)
                        Try
                            mForm.Cursor = Cursors.WaitCursor
                            'GSCOM.SQL.ExecuteNonQuery(s, gConnection)
                            If btnGenerateTextFile = True Then
                                pGenerateTextFile(s, Me.GetDefaultFileName(DefaultFileName, gConnection), gConnection)
                            Else
                                GSCOM.SQL.ExecuteNonQuery(s, gConnection)
                            End If

                            Me.LoadInfo(CInt(Me.Row("ID")))
                            'If s.Contains("pUpdateVersion_HideMenu") And Not gMainForm Is Nothing Then
                            '    gMainForm.viewRefresh()
                            'End If
                            mForm.Cursor = Cursors.Default
                            MsgBox(succ, MsgBoxStyle.Information)
                        Catch ex As Exception
                            s = getSystemMessage(ex.Message) 'EMIL 20130327
                            mForm.Cursor = Cursors.Default
                            MsgBox(s, MsgBoxStyle.Information)
                            'MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region
    Public Sub MakeReadOnly()
        mForm.MakeReadOnly()
    End Sub

    Public Sub HideNewAndSaveButtons()
        mForm.HideNewAndSaveButtons()
    End Sub

    Protected Sub EnableExtraButtons(Optional ByVal pEnable As Boolean = True)
        mForm.EnableExtraButtons(pEnable)
    End Sub

#Region "GetStripButton"
    Public Function GetStripButton(ByVal text As String) As System.Windows.Forms.ToolStripButton
        Return mForm.GetStripButton(text)
    End Function

    Public ReadOnly Property SaveButton() As ToolStripButton
        Get
            Return mForm.GetSaveButton
        End Get
    End Property

#End Region


    Private Sub RobAdded(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs)
        'Console.WriteLine(e.RowIndex)
        UpdateFilteredRowCount(CType(sender, DataGridView))
    End Sub
    Private Sub RobRemoved(ByVal sender As Object, ByVal e As DataGridViewRowsRemovedEventArgs)
        UpdateFilteredRowCount(CType(sender, DataGridView))
    End Sub

    Private Sub UpdateFilteredRowCount(ByVal dgv As DataGridView)
        Dim mFilteredRowCount As Integer
        Dim t, s As String
        t = ""
        Dim dv As DataView
        Dim dt As DataTable
        dt = TryCast(dgv.DataSource, DataTable)
        If dt IsNot Nothing Then
            dv = dt.DefaultView
            mFilteredRowCount = dv.Count
            If mForm.GetTabPage(dgv) IsNot Nothing Then
                t = mForm.GetTabPage(dgv).Text
            End If

            If mFilteredRowCount = 0 Then
                s = "No"
            Else
                s = mFilteredRowCount.ToString
            End If
            s &= " record"
            If mFilteredRowCount > 1 Then
                s &= "s"
            End If
            s &= " found"
            mForm.SetDetailStatusLabelText(t, s)
        End If
    End Sub

    Public Sub InitTreeGrid(ByVal dt As DataTable, ByVal pMenuDetailTabRow As Database.MenuDetailTabRow)
        Dim tg As New TreeGrid
        tg.mInfoMenuSet = Me.mInfoMenuSet
        tg.mMenuDetailTabRow = pMenuDetailTabRow
        Dim dra As DataRow()
        Dim s As String

        tg.OriginalListSource = pMenuDetailTabRow.ListSource

        tg.DataSource = dt

        tg.Name = TreeGrid.TreeGridPrefix & pMenuDetailTabRow.ID.ToString
        tg.ImageList = gImageList

        s = "ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(pMenuDetailTabRow.ID)
        dra = mInfoMenuSet.tMenuDetailTabField.Select(s & "AND IsGroup=1")
        For Each dr As DataRow In dra
            tg.Groups.Add(dr("Name").ToString, dr("Text").ToString, dr("Sort").ToString, dr("ImageFile").ToString)
        Next
        dra = mInfoMenuSet.tMenuDetailTabField.Select(s & " AND (IsColumn=1)")
        For Each dr As DataRow In dra
            tg.Columns.Add(dr("Name").ToString, CInt(dr("Width")), HorizontalAlignment.Right)
        Next

        tg.CheckBoxes = pMenuDetailTabRow.CheckBoxes
        Dim tp As TabPage = Me.AddControl(tg, pMenuDetailTabRow.Name)
        tp.ImageKey = pMenuDetailTabRow.ImageFile

        Dim ts As ToolStrip
        ts = CType(tp.Tag, ToolStrip)
        With ts.Items.Add("Load List")
            .ImageKey = tg.Groups(tg.Groups.Count - 1).ImageKey
            .Name = ButtonPrefix & "TreeGrid" & pMenuDetailTabRow.ID
            .Alignment = ToolStripItemAlignment.Right
            AddHandler .Click, AddressOf LoadListClick
            .Tag = tg
        End With

    End Sub


#Region "mGrid_RowHeaderMouseDoubleClick"
    'Private Sub mGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
    Private Sub mGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim vID As Integer
        Dim drv As DataRowView
        Dim mGrid As DataGridView
        mGrid = CType(sender, DataGridView)
        Try
            If e.ColumnIndex = 0 AndAlso mGrid.Columns(e.ColumnIndex).Name = "Open" Then
                drv = TryCast(mGrid.Rows(e.RowIndex).DataBoundItem, DataRowView)
                If drv IsNot Nothing Then
                    If drv.Row.RowState <> DataRowState.Unchanged Then
                        MsgBox("Can not open info. This record has not yet been saved.", MsgBoxStyle.Information)
                    Else
                        vID = CInt(GSCOM.SQL.SQLFormat(drv.Item("ID")))
                        'temporary
                        ShowDetailInfo(CInt(mGrid.Tag), vID, CType(mGrid.DataSource, DataTable))
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub DetailAddButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim vID As Database.Menu
        Dim brDT As DataTable
        Dim brDDT As DataTable
        Dim b As ToolStripButton
        Dim vAllowDuplicateList As Boolean = False

        b = CType(sender, ToolStripButton)
        vID = CType(b.Tag, Database.Menu)

        Dim mdt As New Database.MenuDetailTabRow(Me.mInfoMenuSet.tMenuDetailTab.Select("ID=" & CInt(Strings.Right(b.Name, Len(b.Name.ToString) - Len(ButtonPrefix & "_Add_"))))(0))
        Dim destDT As DataTable = Me.mDataset.Tables(mdt.TableName)

        Dim ss As String = Me.PassParameters(mdt.ListMenuFixedFilter)
        Dim sss As String = Me.PassParameters(mdt.ListMenuDetailSource)
        If CBool(mdt.AllowDuplicateList) Then
            vAllowDuplicateList = True
        End If
        Dim br As New BrowserDataListForm(vID, vAllowDuplicateList, ss, True, destDT)
        br.StartPosition = FormStartPosition.CenterScreen
        br.Size = Me.Size
        Dim i As Integer = br.ShowDialog()

        If i = 1 Then
            brDT = br.GetTable
        End If

        brDT = br.GetTable
        Dim dr As DataRow
        Dim drr As DataRow

        Dim mdtf As New Database.MenuDetailTabFieldRow

        If brDT IsNot Nothing Then
            If mdt.ListMenuDetailSource <> "" Then

                brDDT = br.GetDetailTable(sss)

                Dim dra2() As DataRow = Me.mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab = " & mdt.ID & " AND ListColumn IS NOT NULL")

                For Each dr2 As DataRow In brDDT.Select
                    drr = destDT.NewRow
                    For Each drx2 As DataRow In dra2
                        mdtf.InnerRow = drx2
                        drr(mdtf.Name) = dr2(mdtf.ListColumn)
                    Next
                    If drr.RowState = DataRowState.Detached Then
                        destDT.Rows.Add(drr)
                    End If

                Next
            Else
                Dim dra() As DataRow = Me.mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab = " & mdt.ID & " AND ListColumn IS NOT NULL")

                For Each dr1 As DataRow In brDT.Select
                    dr = destDT.NewRow
                    For Each drx As DataRow In dra
                        mdtf.InnerRow = drx
                        dr(mdtf.Name) = dr1(mdtf.ListColumn)
                    Next
                    If dr.RowState = DataRowState.Detached Then
                        destDT.Rows.Add(dr)
                    End If

                Next
            End If


        End If

    End Sub
    Private Sub DetailNewButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim vID As Database.Menu
        'Dim drv As DataRowView
        Dim b As ToolStripButton
        b = CType(sender, ToolStripButton)
        vID = CType(b.Tag, Database.Menu)
        Try
            Dim pInfoSet As InfoSet
            pInfoSet = GetInfoSet(vID)
            'IMPORTANT: SAVE THE ID OF THE RECORD THAT 
            'TRIGGERED THE LOADING OF INFO BECAUSE mID IS 
            'RESET TO 0 ON LOADINFO() FUNCTION IF SAME MODULE
            Dim mdt As New Database.MenuDetailTabRow(Me.mInfoMenuSet.tMenuDetailTab.Select("ID=" & CInt(Strings.Right(b.Name, Len(b.Name.ToString) - Len(ButtonPrefix & "_New_"))))(0))
            If mdt.ParentTableName <> "" AndAlso mdt.ParentTableName <> Me.Table.TableName Then
                MsgBox("Parent table aside from the main table is not currently supported", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim vCallingRecordID As Integer = CInt(Me.Row(mdt.ParentColumn)) 'mID is string for GUID only.. e.g. FileBrowser
            If pInfoSet Is Nothing Then
                pInfoSet = NewInfo(vID, Nothing, 0)
            Else
                pInfoSet.LoadInfo(0) 'mID IS RESET TO 0 IF SAME MODULE
            End If
            pInfoSet.mDataset.Tables(0).Rows(0).Item(mdt.ChildColumn) = vCallingRecordID 'INITIALIZE THE COLUMN. SET TO PARENT
            Application.DoEvents()
            If pInfoSet IsNot Nothing Then
                pInfoSet.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    'Private Sub DetailNewButtonClick(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim vID As Database.Menu
    '    'Dim drv As DataRowView
    '    Dim mGrid As ToolStripButton
    '    mGrid = CType(sender, ToolStripButton)
    '    vID = CType(mGrid.Tag, Database.Menu)
    '    Try
    '        Dim dl As New BrowserDataListForm(vID)
    '        dl.Size = Me.Size
    '        dl.StartPosition = FormStartPosition.CenterScreen
    '        dl.ShowDialog()

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '    End Try
    'End Sub


#End Region

#Region "InitDetail"
    Private Sub InitDetailButtons(ByVal pMenuDetailTabRow As Database.MenuDetailTabRow, ByVal tp As TabPage)
        If pMenuDetailTabRow IsNot Nothing Then
            Dim ts As ToolStrip
            If tp.Tag IsNot Nothing Then
                ts = CType(tp.Tag, ToolStrip)
                For Each dr As DataRow In mInfoMenuSet.tMenuButton.Select("ID_MenuDetailTab=" & pMenuDetailTabRow.ID.ToString)
                    With ts.Items.Add(dr(Database.Tables.tMenuButton.Field.Name).ToString)
                        .ImageKey = dr(Database.Tables.tMenuButton.Field.ImageFile).ToString '  gImageList.Images(dr(Database.Tables.tMenuButton.Field.ImageFile).ToString), AddressOf ButtonClick
                        .Name = ButtonPrefix & dr(Database.Tables.tMenuButton.Field.ID).ToString
                        .Alignment = ToolStripItemAlignment.Right
                        AddHandler .Click, AddressOf ButtonClick
                    End With
                Next

                For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTab.Select("ImportFile IS NOT NULL AND Name = '" + tp.Text + "'") 'EMIL 20130207

                    Dim ImportButton As New ToolStripButton
                    Dim GenButton As New ToolStripButton
                    Dim ExpButton As New ToolStripButton

                    ImportButton.Name = "_Import"
                    ImportButton.Text = "Import"
                    ImportButton.Alignment = ToolStripItemAlignment.Right
                    ImportButton.Tag = dr("ID").ToString
                    ImportButton.ImageKey = "ImportFile.png" 'Add Image --20130207
                    AddHandler ImportButton.Click, AddressOf ImportFiles
                    ts.Items.Add(ImportButton)

                    GenButton.Name = "_Generate Template"
                    GenButton.Text = "Generate Template"
                    GenButton.Alignment = ToolStripItemAlignment.Right
                    GenButton.Tag = dr("ImportFile").ToString + "," + dr("ID").ToString
                    GenButton.ImageKey = "_generateFile.png" 'Add Image --20130207
                    AddHandler GenButton.Click, AddressOf GenerateExcelTemplate
                    ts.Items.Add(GenButton)

                Next
            End If
        End If
    End Sub

    Private Sub InitDetailAddButton(ByVal pMenuDetailTabRow As Database.MenuDetailTabRow, ByVal tp As TabPage)
        If pMenuDetailTabRow IsNot Nothing Then
            If tp.Tag IsNot Nothing Then
                Dim ts As ToolStrip
                ts = CType(tp.Tag, ToolStrip)

                Dim lbl As String

                If pMenuDetailTabRow.Label Is DBNull.Value Then
                    lbl = nDB.GetMenuValue(CType(pMenuDetailTabRow.ID_ListMenu, Database.Menu), Database.Tables.tMenu.Field.Name).ToString
                Else
                    lbl = pMenuDetailTabRow.Label.ToString
                End If

                With ts.Items.Add("Add " & lbl, GSCOM.UI.TableButtonImage, AddressOf DetailAddButtonClick)
                    .Tag = pMenuDetailTabRow.ID_ListMenu
                    .Name = ButtonPrefix & "_Add_" & pMenuDetailTabRow.ID
                    .Alignment = ToolStripItemAlignment.Right
                End With
            End If

        End If
    End Sub

    Private Sub InitDetailNewButton(ByVal pMenuDetailTabRow As Database.MenuDetailTabRow, ByVal tp As TabPage)
        If pMenuDetailTabRow IsNot Nothing Then
            If tp.Tag IsNot Nothing Then
                Dim ts As ToolStrip
                ts = CType(tp.Tag, ToolStrip)

                With ts.Items.Add("New", GSCOM.UI.NewButtonImage, AddressOf DetailNewButtonClick)
                    .Tag = pMenuDetailTabRow.ID_DetailMenu
                    .Name = ButtonPrefix & "_New_" & pMenuDetailTabRow.ID
                    .Alignment = ToolStripItemAlignment.Right
                End With
            End If
        End If
    End Sub

    Public Overloads Sub InitDetail(ByVal dt As DataTable, Optional ByVal pMenuDetailTabRow As Database.MenuDetailTabRow = Nothing)
        Dim tp As TabPage
        Dim dgv As GSDetailDataGridView
        Dim pTabName As String = ""
        Dim pDataSource As String = ""
        Dim pMenuDetailTabID As Integer = 0
        Dim vSortable As Boolean = True
        Dim aa As DataTable = Nothing

        If pMenuDetailTabRow IsNot Nothing Then
            pTabName = pMenuDetailTabRow.Name
            pDataSource = pMenuDetailTabRow.DataSource
            pMenuDetailTabID = pMenuDetailTabRow.ID
            vSortable = pMenuDetailTabRow.Sortable
        End If
        If pTabName = "" Then
            pTabName = Strings.Right(Replace(dt.TableName, "_", " "), dt.TableName.Length - 1)
        End If
        tp = New TabPage(pTabName)

        '20100302 image
        If pMenuDetailTabRow IsNot Nothing Then
            'tp.ImageKey = pMenuDetailTabRow.ImageFile
            tp.ImageIndex = gImageList.Images.IndexOfKey(pMenuDetailTabRow.ImageFile)
        End If
        dgv = New GSDetailDataGridView

        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabField.Select()
            If CBool(dr("IsWordWrap")) Then
                dgv.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            End If
        Next

        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabProperty.Select("ID_MenuDetailTab=" & GSCOM.SQL.SQLFormat(pMenuDetailTabID))
            CallByName(dgv, dr("Name").ToString, CallType.Set, dr("Value").ToString)
        Next
        If Not vSortable Then
            For Each dgvc As DataGridViewColumn In dgv.Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End If

        'moved
        dgv.DataSource = dt

        'EMIL 20130301''''''''''''''''''''''
        If Not pMenuDetailTabRow Is Nothing Then dgv.AllowUserToAddRows = pMenuDetailTabRow.AllowNewRow
        If Not pMenuDetailTabRow Is Nothing Then dgv.AllowUserToDeleteRows = pMenuDetailTabRow.AllowDeleteRow
        ''''''''''''''''''''''''''''''''''''

        tp.Controls.Add(dgv)
        mForm.AddTabPage(tp)
        InitDetailButtons(pMenuDetailTabRow, tp)
        AddHandler dgv.RowsAdded, AddressOf RobAdded
        AddHandler dgv.RowsRemoved, AddressOf RobRemoved
        AddHandler dgv.EditingControlShowing, AddressOf MyEditingControlShowing
        If pMenuDetailTabRow IsNot Nothing Then
            If pMenuDetailTabRow.ID_DetailMenu IsNot DBNull.Value Then
                Dim b As New DataGridViewImageColumn  'DataGridViewButtonColumn
                With b
                    .Name = "Open"
                    .Image = GSCOM.UI.OpenButtonImage
                    .DefaultCellStyle.NullValue = Nothing
                End With
                dgv.Columns.Insert(0, b)
                dgv.Tag = pMenuDetailTabRow.ID_DetailMenu
                'AddHandler .RowHeaderMouseDoubleClick, AddressOf mGrid_RowHeaderMouseDoubleClick
                AddHandler dgv.CellClick, AddressOf mGrid_RowHeaderMouseDoubleClick

                InitDetailNewButton(pMenuDetailTabRow, tp)
            End If

            If pMenuDetailTabRow.ID_ListMenu IsNot DBNull.Value Then

                InitDetailAddButton(pMenuDetailTabRow, tp)
            End If

        End If
        Dim mdtfr As New Database.MenuDetailTabFieldRow
        If pMenuDetailTabRow IsNot Nothing AndAlso mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & pMenuDetailTabRow.ID).Length > 0 Then
            Dim dc As DataColumn
            aa = GSCOM.Common.IgnoreErrors(Of DataTable)(Function() GSCOM.SQL.TableQuery(String.Format("SELECT * FROM dbo.fGetTableDef({0})", GSCOM.SQL.SQLFormat(dt.TableName)), gConnection))

            For Each drx As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & pMenuDetailTabRow.ID & " AND ShowInInfo=1", "SeqNo,ID")
                mdtfr.InnerRow = drx
                If Not dt.Columns.Contains(mdtfr.Name) Then
                    '   MsgBox(drx("Name").ToString & "/ does not exist in " & dt.TableName, MsgBoxStyle.Exclamation)
                Else
                    dc = dt.Columns(mdtfr.Name)

                    Dim colq As DataRow = (From j As DataRow In aa.AsEnumerable()
                                           Where CStr(j("ColumnName")) = dc.ColumnName
                                           Select j).SingleOrDefault()

                    InitColumn(dc, dgv, mdtfr.ID_Menu, mdtfr, colq)
                End If
            Next
        Else
            Dim a As DataTable

            If pDataSource <> "" Then
                a = GSCOM.SQL.TableQuery("SELECT TOP 0 * FROM (" & pDataSource & ") a", gConnection)
            Else
                Try 'legacy if v_list exists 20100823
                    a = GSCOM.SQL.TableQuery("SELECT TOP 0 * FROM v" & Strings.Right(dt.TableName, dt.TableName.Length - 1) & "_List", gConnection)
                Catch ex As Exception

                    a = GSCOM.SQL.TableQuery("SELECT TOP 0 * FROM v" & Strings.Right(dt.TableName, dt.TableName.Length - 1), gConnection)
                    aa = GSCOM.Common.IgnoreErrors(Of DataTable)(Function() GSCOM.SQL.TableQuery(String.Format("SELECT * FROM dbo.fGetTableDef({0})", GSCOM.SQL.SQLFormat(pDataSource)), gConnection))

                End Try
            End If
            Dim vMenu As Object = DBNull.Value

            For Each dc As DataColumn In a.Columns
                Dim dra() As DataRow = nDB.SystemDataLookUpTable.Select("Name='" & dc.ColumnName & "'")
                If dra.Length > 0 Then
                    vMenu = dra(0)("ID_Menu")
                Else
                    vMenu = DBNull.Value
                End If

                If Not IsNothing(aa) Then
                    Dim colq As DataRow = (From j As DataRow In aa.AsEnumerable()
                                           Where CStr(j("ColumnName")) = dc.ColumnName
                                           Select j).SingleOrDefault()

                    InitColumn(dc, dgv, vMenu, Nothing, colq)
                Else
                    InitColumn(dc, dgv, vMenu, Nothing)
                End If

            Next
        End If

        AddHandler dgv.DataError, AddressOf dgv_DataError
    End Sub

    Private Sub InitColumn(ByVal dc As DataColumn, ByVal dgvc As DataGridViewColumn)
        dgvc.Name = dc.ColumnName
        If Strings.Left(dc.Caption, 3) <> "ID_" Then
            dgvc.HeaderText = dc.Caption
        Else
            dgvc.HeaderText = Strings.Right(dc.Caption, dc.Caption.Length - 3)
        End If
        dgvc.DataPropertyName = dc.ColumnName

    End Sub

    Private Sub InitColumn(ByVal dc As DataColumn, ByVal dgv As DataGridView, ByVal pMenu As Object, ByVal pmdtfr As Database.MenuDetailTabFieldRow, Optional rowprop As DataRow = Nothing)
        Dim cbo As DataGridViewColumn
        If dc.DataType Is GetType(System.Boolean) Then
            cbo = New DataGridViewCheckBoxColumn
            InitColumn(dc, cbo)
        ElseIf Strings.Left(dc.ColumnName, 3) = "ID_" Then
            Dim vMenu As Integer
            If pMenu IsNot DBNull.Value Then
                vMenu = CInt(pMenu)
                Dim b As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn
                b = New GSCOM.UI.DataLookUp.DataGridViewLookUpColumn
                cbo = b
                b.ValueMember = "ID"
                b.DisplayMember = "Name"
                b.ColumnName = dc.ColumnName
                b.Sort = nDB.GetMenuValue(CType(vMenu, Database.Menu), Database.Tables.tMenu.Field.Sort.ToString).ToString
                If pmdtfr IsNot Nothing Then 'Andrew 20110530
                    b.FixedFilter = pmdtfr.FixedFilter
                    cbo.HeaderText = IIf(pmdtfr.Label <> "", pmdtfr.Label, nDB.GetMenuValue(CType(vMenu, Database.Menu), Database.Tables.tMenu.Field.Name.ToString).ToString).ToString
                Else
                    b.FixedFilter = ""
                    cbo.HeaderText = nDB.GetMenuValue(CType(vMenu, Database.Menu), Database.Tables.tMenu.Field.Name.ToString).ToString
                End If
                cbo.Name = dc.ColumnName
                'dont set the datapropertyname
                MainModule.InitLookUp(b, vMenu)
                If pmdtfr IsNot Nothing Then 'Andrew 20110530
                    '20110105---------------------------------------------------------------------------------------------\
                    Dim mtf As New Database.MenuDetailTabFieldRow
                    Dim s As String
                    s = Database.Tables.tMenuDetailTabField.Field.ParentLookUp.ToString & "=" & GSCOM.SQL.SQLFormat(pmdtfr.Name)
                    s &= " AND " & Database.Tables.tMenuDetailTabField.Field.ParentLookUpChildColumn.ToString & " IS NOT NULL"
                    s &= " AND " & Database.Tables.tMenuDetailTabField.Field.ID_MenuDetailTab.ToString & "=" & pmdtfr.ID_MenuDetailTab

                    For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTabField.Select(s)
                        mtf.InnerRow = dr
                        b.ExtraFields.Add(mtf.Name, mtf.ParentLookUpChildColumn)
                    Next
                    '20110105---------------------------------------------------------------------------------------------/
                End If



            Else
                cbo = New DataGridViewComboBoxColumn
                CType(cbo, DataGridViewComboBoxColumn).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing

                InitColumn(dc, cbo)

                gInitLookUp(CType(cbo, DataGridViewComboBoxColumn))
            End If
        Else
            cbo = New DataGridViewTextBoxColumn
            InitColumn(dc, cbo)
            If Not IsNothing(rowprop) Then
                CType(cbo, DataGridViewTextBoxColumn).MaxInputLength = IIf(Convert.ToString(rowprop("DataType")) = "text", Convert.ToInt32(2147483647), Convert.ToInt32(rowprop("Length")))
            End If
        End If
        dgv.Columns.Add(cbo)
        If pmdtfr IsNot Nothing Then
            cbo.Frozen = pmdtfr.IsFrozen
            If pmdtfr.Width <> 0 Then
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                cbo.Width = pmdtfr.Width
            End If
        End If
        If TypeOf cbo Is DataGridViewComboBoxColumn Then 'IMPORTAN WORKS ONLY AFTER ADDED TO THE GRID
            cbo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft  'not working

        End If

    End Sub

#End Region

    Protected Overrides Function ValidData() As Boolean
        ValidateTree()
        Return MyBase.ValidData()
    End Function

#Region "Save"

    Private Function Save() As Boolean
        Dim tran As SqlClient.SqlTransaction = Nothing
        'Dim t As New System.Threading.Thread(AddressOf Navigate)
        Dim r As Boolean
        Dim i As Integer
        Try
            mForm.EndEdit()
            SetDocumentNo()
            SetCodeNo()
            'ValidData() 'Nilipat ko ung valid data sa loob ng FieldRequiredIf() na nasa loob ng CanSave(), para sa requiredIF BILLY
            Dim SaveTriggerQuery As String
            SaveTriggerQuery = mMenuRow.SaveTrigger
            If CanSave() Then
                Me.Row.EndEdit()
                Connection.Open()
                If UseTransaction Then
                    tran = Connection.BeginTransaction
                End If
                Try
                    'NOTE: MUST SAVE THE ROWSTATE BEFORE SAVING
                    'NOTE: THIS WOULD BE USED ON THE "SAVED" EVENT
                    If CBool(nDB.GetSetting(Database.SettingEnum.UseAuditTrail)) Then
                        nDB.AuditTrail.LogInfoSave(Me, tran)
                    End If



                    Dim rs As DataRowState
                    rs = Me.Row.RowState


                    If UseTransaction Then
                        Table.Adapter.InsertCommand.Transaction = tran
                        Table.Adapter.UpdateCommand.Transaction = tran
                        Table.Adapter.DeleteCommand.Transaction = tran
                        'SetDocumentNo(tran)
                    Else
                        'SetDocumentNo()
                    End If
                    i = Table.Update()
                    Dim dg As DataGridView
                    For Each dt As GSCOM.SQL.ZDataTable In mDataset.Tables
                        If dt IsNot Table Then
                            If InStr(NoTransactionTables, dt.TableName) = 0 Then
                                dg = Me.GetDataGridView(dt) 'for fast operation-----\
                                If dg IsNot Nothing Then dg.DataSource = Nothing
                                If UseTransaction Then
                                    dt.Adapter.InsertCommand.Transaction = tran
                                    dt.Adapter.UpdateCommand.Transaction = tran
                                    dt.Adapter.DeleteCommand.Transaction = tran
                                End If
                                Try
                                    dt.Update()
                                Catch ex As Exception
                                    Throw ex
                                Finally
                                    If dg IsNot Nothing Then dg.DataSource = dt
                                End Try
                            End If
                        End If
                    Next
                    RaiseEvent Saved(Me, New SavedEventArgs(tran, rs, SaveTriggerQuery))
                    If UseTransaction Then
                        tran.Commit()
                    End If
                    'moved from here
                    r = True
                    Save = r
                    IsSaved = r
                    If rs = DataRowState.Added Then
                        nDB.AuditTrail.UpdateDocumentroperties(CInt(Me.Row(0)), MenuID, "1")
                    ElseIf rs = DataRowState.Modified Then
                        nDB.AuditTrail.UpdateDocumentroperties(CInt(Me.Row(0)), MenuID, "2")
                    End If
                    'EMIL'
                    Me.Refresh()
                Catch ex As Exception
                    Try
                        If UseTransaction Then
                            tran.Rollback()
                        End If


                        ''this is to check constraints-------------------------\
                        Dim s As String
                        s = getSystemMessage(ex.Message)
                        ''to make sure the rows are ordered by name use this: 
                        ''.Select("", "Name"). but this is already done in query
                        'For Each dr As DataRow In nDB.SystemMessageTable.Rows
                        '    s = dr("Name").ToString
                        '    If InStr(ex.Message, s, CompareMethod.Text) > 0 Then
                        '        s = dr("Description").ToString
                        '        Throw New Exception(s, ex)
                        '    End If
                        'Next
                        ''this is to check constraints-------------------------/

                        Throw New Exception(s, ex)
                        'Throw ex
                    Catch ex2 As Exception
                        Throw ex2
                    End Try
                End Try

                If r Then 'robbie 20061102
                    'moved here
                    SetDefaultValues() 'ROBBIE 20060629 Implicitly call setdafualtvalues because IDs are changed
                    Dim e As New CommitedEventArgs
                    e.MainTableChanged = i > 0
                    e.SaveQuery = SaveTriggerQuery
                    RaiseEvent Commited(Me, e)
                    Me.mReloadAfterCommit = True
                    If mReloadAfterCommit Then
                        Application.DoEvents()
                        LoadInfo(CInt(Me.Row(0)))
                        IsSaved = True 'must reset to true because loadinfo clears this flag
                    Else
                        Dim tg As TreeGrid
                        Dim cc() As Control = Nothing
                        GSCOM.UI.AllControls(cc, mForm)
                        For Each c As Control In cc
                            If TypeOf c Is TreeGrid Then
                                tg = DirectCast(c, TreeGrid)
                                tg.LoadTree(tg.DataSourceFilter, False, "") 'no need to update listsource
                            End If
                        Next
                        EnableButtons(CInt(Me.Row("ID"))) 'else because this is also called on loadinfo
                        EnableDetailMenus()
                        'LoadDetailMenus()
                        Navigate() 'ROBBIE|20091119|refresh browser
                    End If
                End If

                'Refresh() '
                'RefreshListing() '
            Else
                MsgBox("Invalid input", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
            'If tran.Connection Is Nothing Then
            '    Console.WriteLine("nothing")
            'End If
        End Try
    End Function

#End Region

#Region "RefreshListing"
    Private Sub RefreshListing()
        If mListing IsNot Nothing Then
            If mListing.PrimaryKey.Length = 0 Then        'ROBBIE NOTE: set the primary key so merge function would be able to determine which record would be update
                Dim keys(0) As DataColumn
                keys(0) = mListing.Columns("ID")
                mListing.PrimaryKey = keys
            End If
            If Me.Row.RowState = DataRowState.Unchanged Then

                Try
                    mListing.Merge(Table, False, MissingSchemaAction.Ignore)         'ROBBIE NOTE: set preservechanges to false to be able to reupdate the values
                    mListing.Select("ID=" & RowID)(0).AcceptChanges()
                    'mListing.AcceptChanges()
                    Dim dt As DataTable
                    Dim s As String
                    s = Strings.Right(Table.TableName, Table.TableName.Length - 1)
                    s = "v" & s '& "_List" 'ROBBIE 20061125 Use the base view bcoz its the one being used by the InitLookUp
                    dt = nDB.GetLookUp(s, False)
                    If dt IsNot Nothing Then
                        dt.Merge(Table, False, MissingSchemaAction.Ignore)
                    End If
                Catch ex As Exception

                End Try
            End If

        End If
    End Sub

#End Region

#End Region

#Region "Protected"
    Private Property Loading As Boolean
    Public Property IsSaved As Boolean

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        Loading = True
        MyBase.LoadInfo(pID)
        If ClearThenFillOnLoadInfo Then
            LoadTreeGrids(pID)
        End If
        Me.Navigate()
        Try
            EnableButtons(pID)
            FieldReadOnly(pID)
            FieldInvisible(pID)
            FieldRequiredIf(pID)
            'FieldEnabled()
            GSCOM.Common.IgnoreErrors(Of Exception)(Sub() IsSalaryAuthenticatedField())
        Catch ex As Exception
            EndProcess(ex.Message, False)
        End Try


        If mFileTabPage IsNot Nothing Then
            If Me.Table.Columns.Contains("GUID") Then
                mFileTabPage.Go()
                mFileTabPage.Enabled = (Me.Row.RowState <> DataRowState.Added)
            End If
        End If

        LoadDetailMenus()
        LoadReportViewers()
        IsSaved = False
        Loading = False

        If pID = 0 Then
            Dim c() As Control = Nothing
            GSCOM.UI.AllControls(c, mForm)
            For Each cc As Control In c
                If TypeOf cc Is GSCOM.UI.DataLookUp.DataLookUp Then
                    mShown = True
                    DataLookUp_Validated(cc, New EventArgs)
                    mShown = False
                    '-------------------------------------------------
                ElseIf TypeOf cc Is ComboBox Then 'EMIL 20130214
                    For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select("FixedFilter IS NOT NULL AND Name = '" + Right(cc.Name, cc.Name.Length - 1) + "'")
                        FixedFilterComboBox(DirectCast(cc, ComboBox).DataSource, cc.Name, Trim(dr("FixedFilter").ToString))
                    Next
                End If
                '-----------------------------------------------------
            Next
        End If

        '----------------------------------------------------------------------------
        If pID > 0 Then 'EMIL 20130214
            Dim c() As Control = Nothing
            GSCOM.UI.AllControls(c, mForm)
            For Each cc As Control In c
                '--------------------------------------------------------------------
                If TypeOf cc Is ComboBox Then 'EMIL 20130214
                    For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select("FixedFilter IS NOT NULL AND Name = '" + Right(cc.Name, cc.Name.Length - 1) + "'")
                        FixedFilterComboBox(DirectCast(cc, ComboBox).DataSource, cc.Name, Trim(dr("FixedFilter").ToString))
                    Next
                End If
                '--------------------------------------------------------------------
            Next
        End If

        mShown = True ' set by default, 20140728
        FixedFilterGridComboBox(pID) 'EMIL 20130214
        GSCOM.Common.IgnoreErrors(Of Exception)(Sub() CheckSalaryAuthenticatedTabs())
        '----------------------------------------------------------------------------
    End Sub

    '--------------------------------------------------------------
    Private Sub FixedFilterComboBox(ByRef dt As DataTable, ByVal ControlName As String, ByVal xFilter As String) 'EMIL 20130214
        If xFilter.Length > 0 Then
            xFilter = Me.PassParameters(xFilter)
            dt.DefaultView.RowFilter = xFilter
        Else
            dt.DefaultView.RowFilter = Nothing
        End If
    End Sub

    Private Sub FixedFilterGridComboBox(ByVal id As Integer) 'EMIL 20130214
        Dim dgvc As DataGridViewColumn
        Dim dtg As DataGridView
        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("FixedFilter IS NOT NULL")
            dtg = Me.GetDataGridView(mDataset.Tables(CStr(mInfoMenuSet.tMenuDetailTab.Select("ID=" & CStr(dr("ID_MenuDetailTab")))(0)("TableName"))))

            If dtg Is Nothing Then Continue For
            If Not dtg.Columns.Contains(CStr(dr("Name"))) Then Continue For

            dgvc = dtg.Columns(CStr(dr("Name")))
            If TypeOf dgvc Is DataGridViewComboBoxColumn Then
                Dim dt As DataTable
                dt = DirectCast(dgvc, DataGridViewComboBoxColumn).DataSource
                Dim xFilter As String = Trim(dr("FixedFilter").ToString)
                xFilter = Me.PassParameters(xFilter)
                If xFilter <> "" Then
                    dt.DefaultView.RowFilter = xFilter
                Else
                    dt.DefaultView.RowFilter = Nothing
                End If
            End If
        Next
    End Sub
    '----------------------------------------------------------------
    ' <summary>
    ' 20130514 - by EMIL
    ' Check if MenuTab or DetailMenuTab is Salary related. If yes the system will check if user has the right to view the Tab.
    ' </summary>
    ' <returns></returns>

    Private Sub CheckSalaryAuthenticatedTabs()
        Dim dtss As New DataTable
        GSCOM.SQL.FillTable(dtss, "Select ID, Name, '1' Type From tMenuTab Where Name <> 'General' AND IsSalaryAuthenticatedTab = 1 AND ID_Menu = " + Me.MenuID.ToString, gConnection)
        GSCOM.SQL.FillTable(dtss, "Select ID, Name, '2' Type From tMenuDetailTab Where IsSalaryAuthenticatedTab = 1 AND ID_Menu = " + Me.MenuID.ToString, gConnection)
        Dim tp As TabPage
        Dim ret As Boolean = True
        For Each dr As DataRow In dtss.Rows
            tp = mForm.GetTabPage(dr(1).ToString)
            If dr(2).ToString = "1" Then
                ret = CBool(GSCOM.SQL.ExecuteScalar("Select dbo.fCanViewSalaryAuthenticatedTabs(" + dr(0).ToString + ", " + nDB.GetUserID.ToString + ")", gConnection))
            Else
                ret = CBool(GSCOM.SQL.ExecuteScalar("Select dbo.fCanViewSalaryAuthenticatedDetailTabs(" + dr(0).ToString + ", " + nDB.GetUserID.ToString + ")", gConnection))
            End If
            If Not tp Is Nothing Then
                If mForm.tcMain.TabPages.Contains(tp) And Not ret Then mForm.tcMain.TabPages.Remove(tp)
                If mForm.BasicTab.TabPages.Contains(tp) And Not ret Then mForm.BasicTab.TabPages.Remove(tp)
            End If
        Next
    End Sub


    Private Sub IsSalaryAuthenticatedField()
        Dim c As Control
        Dim cc As Control
        Dim ei As Boolean = False
        For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select("IsSalaryAuthenticatedField = 1")
            c = Me.GetControl("_" & dr("Name").ToString)
            cc = Me.GetControl("LABEL__" & dr("Name").ToString)
            ei = CBool(GSCOM.SQL.ExecuteScalar("Select dbo.fIsCanViewSalary(" + nDB.GetUserID.ToString + ")", gConnection))
            If Not c Is Nothing Then c.Visible = ei
            If Not cc Is Nothing Then cc.Visible = ei
        Next
    End Sub

    ' <summary>
    ' 20130516 - by EMIL
    ' Hide Control based on MenuTabField "IsSalaryAuthenticatedField"
    ' </summary>
    ' <returns></returns>


    Private Sub FieldReadOnly(ByVal id As Integer)
        Dim ei As String
        Dim c As Control
        For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select
            c = Me.GetControl("_" & dr("Name").ToString)

            ei = dr("WritableIf").ToString
            If ei <> "" Then
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString

                If TypeOf c Is DataGridView Then
                    Dim o As DataGridView
                    o = CType(c, DataGridView)
                    o.Enabled = CBool(ei)
                ElseIf TypeOf c Is TextBox Then
                    Dim o As TextBox
                    o = CType(c, TextBox)
                    o.Enabled = CBool(ei)
                ElseIf TypeOf c Is ComboBox _
                    Or TypeOf c Is CheckBox _
                    Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
                    Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
                    Then
                    c.Enabled = CBool(ei)
                ElseIf TypeOf c Is MaskedTextBox Then
                    Dim o As MaskedTextBox
                    o = CType(c, MaskedTextBox)
                    o.Enabled = CBool(ei)
                End If
            End If
        Next
        If mShown Then
            Dim dgvc As DataGridViewColumn
            Dim dtg As DataGridView
            For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ReadOnlyIf IS NOT NULL")
                dtg = Me.GetDataGridView(mDataset.Tables(CStr(mInfoMenuSet.tMenuDetailTab.Select("ID=" & CStr(dr("ID_MenuDetailTab")))(0)("TableName"))))
                If dtg Is Nothing AndAlso Not dtg.Columns.Contains(CStr(dr("Name"))) Then Continue For
                dgvc = dtg.Columns(CStr(dr("Name")))
                ei = dr("ReadOnlyIf").ToString
                If ei <> "" Then
                    Dim dt As DataTable = mDataset.Tables(CStr(mInfoMenuSet.tMenuDetailTab.Select("ID=" & CStr(dr("ID_MenuDetailTab")))(0)("TableName")))
                    Dim drvp As DataRowView
                    Dim drx As DataRow = Nothing
                    For Each dgr As DataGridViewRow In dtg.Rows
                        drvp = TryCast(dgr.DataBoundItem, DataRowView)
                        If drvp IsNot Nothing Then
                            drx = drvp.Row
                        Else
                            Continue For
                        End If
                        If drx Is Nothing Then Continue For
                        Dim f As String = Me.PassParameters(ei, dt, , drx)
                        f = Table.Select(f).Length
                        dgr.Cells(dgvc.Name).ReadOnly = CBool(f)
                    Next
                    'ei = Me.PassParameters(ei)
                    'ei = Me.Table.Select(ei).Length.ToString
                    'dgvc.ReadOnly = CBool(ei)
                End If
            Next
        End If

    End Sub

    Private Sub FieldRequiredIf(ByVal id As Integer)
        Dim ei As String
        Dim c As Control
        Dim mtf As New Database.MenuTabFieldRow
        For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select
            c = Me.GetControl("_" & dr("Name").ToString)
            ei = dr("RequiredIf").ToString
            If ei <> "" Then
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString
                mtf.InnerRow = dr
                If CBool(ei) Then
                    Me.Table.Columns(mtf.Name).AllowDBNull = False
                Else
                    Me.Table.Columns(mtf.Name).AllowDBNull = True
                End If

            End If
        Next
    End Sub

    Private Sub FieldRequiredIf() 'As Boolean
        Dim ei As String
        Dim c As Control
        For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select()
            c = Me.GetControl("_" & dr("Name").ToString)
            ei = dr("RequiredIf").ToString
            If ei <> "" Then
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString
                mDataset.Tables(0).Columns(dr("Name").ToString).AllowDBNull = Not CBool(ei)
            End If
        Next
        ValidData()
    End Sub

    Private Sub FieldInvisible(ByVal id As Integer)

        'Dim cc() As System.Windows.Forms.Control = Nothing
        'GSCOM.UI.AllControls(cc, mForm)

        Dim ei As String
        Dim c As Control
        Dim cc As Control
        For Each dr As DataRow In mInfoMenuSet.tMenuTabField.Select
            c = Me.GetControl("_" & dr("Name").ToString)
            cc = Me.GetControl("LABEL__" & dr("Name").ToString)
            ei = dr("VisibleIf").ToString
            If ei <> "" Then
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString

                'For Each l As Control In cc
                '    If TypeOf l Is Label And (l.Text = dr("Name").ToString Or l.Text = dr("Label").ToString) Then
                '        CType(l, Label).Visible = CBool(ei)
                '    End If
                'Next

                If TypeOf cc Is Label Then
                    Dim o As Label
                    o = CType(cc, Label)
                    o.Visible = CBool(ei)
                End If

                If TypeOf c Is DataGridView Then
                    Dim o As DataGridView
                    o = CType(c, DataGridView)
                    o.Visible = CBool(ei)
                ElseIf TypeOf c Is TextBox Then
                    Dim o As TextBox
                    o = CType(c, TextBox)
                    o.Visible = CBool(ei)

                ElseIf TypeOf c Is ComboBox _
                    Or TypeOf c Is CheckBox _
                    Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
                    Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
                    Then
                    c.Visible = CBool(ei)
                ElseIf TypeOf c Is MaskedTextBox Then
                    Dim o As MaskedTextBox
                    o = CType(c, MaskedTextBox)
                    o.Visible = CBool(ei)
                End If
            End If
        Next

        'for details
        Dim fdt As List(Of DataRow) = (From jls In mInfoMenuSet.tMenuDetailTabField.AsEnumerable() Where jls("VisibleIf") IsNot DBNull.Value Select jls).ToList
        Dim dtg As DataGridView
        For Each sdt As DataRow In fdt
            Dim dtblname As String = (From j In mInfoMenuSet.tMenuDetailTab.AsEnumerable Where Convert.ToInt32(j("ID")) = Convert.ToInt32(sdt("ID_MenuDetailTab")) Select Convert.ToString(j("TableName"))).SingleOrDefault
            dtg = Me.GetDataGridView(mDataset.Tables(dtblname))
            If dtg Is Nothing AndAlso Not dtg.Columns.Contains(CStr(sdt("Name"))) Then Continue For
            ei = sdt("VisibleIf").ToString
            If ei <> "" Then
                Dim f As String = Me.PassParameters(ei)
                f = Table.Select(f).Length
                dtg.Columns(CStr(sdt("Name"))).Visible = CBool(f)
            End If
        Next

        'tabs
        If mInfoMenuSet.tMenuTab.Columns.Contains("VisibleIf") Then
            For Each tbs As DataRow In (From jks In mInfoMenuSet.tMenuTab.AsEnumerable() Where jks("VisibleIf") IsNot DBNull.Value Select jks).ToList()
                If Me.Table.Select(PassParameters(tbs("VisibleIf"))).Length = 0 Then
                    Dim tp As TabPage = mForm.GetTabPage(tbs("Name").ToString())
                    If mForm.tcMain.TabPages.Contains(tp) AndAlso tp IsNot Nothing Then mForm.tcMain.TabPages.Remove(tp)
                    If mForm.BasicTab.TabPages.Contains(tp) AndAlso tp IsNot Nothing Then mForm.BasicTab.TabPages.Remove(tp)
                End If
            Next
        End If

    End Sub

    Private Sub FieldEnabled()
        Dim ei As String
        Dim c As Control

        Dim h As List(Of DataRow) = (From j In mInfoMenuSet.tMenuTabField.AsEnumerable
                                     Where j("EnabledIf") IsNot DBNull.Value Select j).ToList

        For Each dr As DataRow In h
            c = Me.GetControl("_" & dr("Name").ToString)

            ei = dr("EnabledIf").ToString
            If ei <> "" Then
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString

                If TypeOf c Is DataGridView Then
                    Dim o As DataGridView
                    o = CType(c, DataGridView)
                    o.Enabled = CBool(ei)
                ElseIf TypeOf c Is TextBox Then
                    Dim o As TextBox
                    o = CType(c, TextBox)
                    o.Enabled = CBool(ei)

                ElseIf TypeOf c Is ComboBox _
                    Or TypeOf c Is CheckBox _
                    Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
                    Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
                    Then
                    c.Enabled = CBool(ei)
                ElseIf TypeOf c Is MaskedTextBox Then
                    Dim o As MaskedTextBox
                    o = CType(c, MaskedTextBox)
                    o.Enabled = CBool(ei)
                End If
            End If
        Next

    End Sub

    Private Sub ShowWizard(ByVal pID As Integer)
        If pID = 0 Then
            If (Not IsDBNull(mMenuRow.ID_ListMenu)) And (Not mMenuRow.IsSpanView) Then
                Dim frm As New BrowserDataListForm(CType(mMenuRow.ID_ListMenu, Database.Menu), False, mMenuRow.ListFixedFilter)
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.Size = gInfoSize
                frm.ShowDialog()
                If frm.SelectedID <> 0 Then
                    Me.Row("ID_" & mMenuRow.ListRowField) = frm.SelectedID
                End If
            End If
        End If
    End Sub

    Private Sub EnableDetailMenus()
        For Each o As InfoSetDetailMenu In Me.DetailMenus
            o.Enabled = (Me.Row.RowState <> DataRowState.Added)
        Next
    End Sub

    Private Sub LoadDetailMenus()
        For Each o As InfoSetDetailMenu In Me.DetailMenus
            o.Go()
            o.Enabled = (Me.Row.RowState <> DataRowState.Added)
        Next
    End Sub

#Region "ReportViewer"
    Private Sub LoadReportViewers()
        For Each o As InfoSetReportViewer In Me.ReportViewers
            If CType(o.mPage.Parent, TabControl).SelectedTab Is o.mPage Then
                LoadReportViewer(o)
            End If
        Next
    End Sub

    Private Sub LoadReportViewer(ByVal o As InfoSetReportViewer)
        Dim dt As DataTable
        Dim s As String = o.DataSource
        If s <> "" Then
            s = Me.PassParameters(s, Me.mInfoMenuSet.tMenuDetailTabField)
            dt = GSCOM.SQL.TableQuery(s, Connection)
            o.Go(dt)
            'o.Go(Me.mDataset, Me.mInfoMenuSet)
        End If
        o.Enabled = (Me.Row.RowState <> DataRowState.Added)
        o.List.ShowPrintButton = Me.OkToPrintInfo
    End Sub
#End Region

    Private Sub ValidateTree()
        Dim dt As DataTable
        Dim tg As TreeGrid
        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTab.Select
            Dim mdt As New Database.MenuDetailTabRow(dr)
            If mdt.ID_MenuDetailTabType = Database.MenuDetailTabTypeEnum.TreeView Then
                For Each dr2 As DataRow In nDB.MenuSet.tConstraint.Select("TableName=" & GSCOM.SQL.SQLFormat(mdt.TableName))
                    dt = Me.mDataset.Tables(dr2("TableName").ToString)
                    For Each dr3 As DataRow In dt.Select("NOT " & dr2("Constraint").ToString)
                        dr3.Delete()
                    Next
                Next
                tg = DirectCast(Me.GetControl(TreeGrid.TreeGridPrefix & mdt.ID), TreeGrid)
                tg.RetainChecked()
            End If
        Next
    End Sub

    Protected Overridable Function CanSave() As Boolean
        FieldRequiredIf()
        Return True
    End Function

    Private Sub ColorControls(ByVal pControl As System.Windows.Forms.Control)
        Dim cc As Control() = Nothing
        Dim c As Control
        Dim mtfr As Database.MenuTabFieldRow
        GSCOM.UI.AllControls(cc, pControl)
        For Each drf As DataRow In mInfoMenuSet.tMenuTabField.Select()
            mtfr = New Database.MenuTabFieldRow(drf)
            If mtfr.ReadOnly Then
                c = Nothing
                For Each c1 As Control In cc
                    If c1.Name = "_" & mtfr.Name Then
                        c = c1
                        Exit For
                    End If
                Next
                If c IsNot Nothing Then
                    If TypeOf c Is TextBoxBase Then
                        Dim o As TextBoxBase
                        o = CType(c, TextBoxBase)
                        o.ReadOnly = True
                    ElseIf TypeOf c Is ComboBox _
                        Or TypeOf c Is CheckBox _
                        Or TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp _
                        Or TypeOf c Is GSCOM.UI.DataImage.DataImage _
                        Then
                        c.Enabled = False
                    End If

                    c.TabStop = False
                    c.BackColor = GSCOM.Common.DefaultReadOnlyFieldBackColor

                End If
                'Next
            End If
        Next
    End Sub

    Private mFileTabPage As FileTabPage
    Private Sub InitFileBrowser()
        If Me.Table.Columns.Contains("GUID") Then
            mFileTabPage = New FileTabPage(Me)
            mForm.tcMain.TabPages.Add(mFileTabPage.mPage)
            DetailMenus.Add(mFileTabPage)
        End If
    End Sub

    Protected Sub AfterNew()
        GSCOM.UI.BindControls(mForm, Table)
        ColorControls(mForm)

        For Each dt As DataTable In mDataset.Tables
            If dt IsNot Table Then
                If InStr(NoGridTables, dt.TableName) = 0 Then

                    If Not mInitedTables.Contains(dt.TableName) Then
                        InitDetail(dt)
                    End If
                End If
            End If
        Next
        For Each dc As DataColumn In Me.Table.Columns
            Dim c As Control = Me.GetControl("LABEL__" & dc.ColumnName)

            If c IsNot Nothing Then dc.Caption = CType(c, Label).Text
        Next
        LoadInfo(mInitID)
        'ROBBIE: 20090522
        Dim img As GSCOM.UI.DataImage.DataImage
        img = TryCast(Me.GetControl("_ImageFile"), GSCOM.UI.DataImage.DataImage)
        If img IsNot Nothing Then
            img.Path = nDB.nGlobal.PhotosPath 'nDB.GetSetting(Database.SettingEnum.PhotoPath)
        End If
        img = TryCast(Me.GetControl("_TempImageFile"), GSCOM.UI.DataImage.DataImage)
        If img IsNot Nothing Then
            img.Path = nDB.nGlobal.PhotosPath 'nDB.GetSetting(Database.SettingEnum.PhotoPath)
        End If
        img = TryCast(Me.GetControl("_ReportImageFile"), GSCOM.UI.DataImage.DataImage)
        If img IsNot Nothing Then
            img.Path = nDB.nGlobal.PhotosPath 'nDB.GetSetting(Database.SettingEnum.PhotoPath)
        End If

        If Me.Table.Columns.Contains("ID_ApprovedBy") And Me.Table.Columns.Contains("ID_FilingStatus") Then
            If CInt(Me.Table.Rows(0)("ID_FilingStatus")) = 1 Then
                Me.Table.Columns("ID_ApprovedBy").DefaultValue = Nothing
            Else
                Me.Table.Columns("ID_ApprovedBy").DefaultValue = mSession.Session.Get(Database.Tables.tSession.Field.ID_Employee)
            End If
        End If
    End Sub

#End Region

#Region "Public"
    Public Function GetDataGridView(ByVal pTable As DataTable) As DataGridView
        Return mForm.GetDataGridView(pTable)
    End Function

    Public Function GetControl(ByVal pName As String) As System.Windows.Forms.Control
        Return mForm.GetControl(pName)
    End Function

    Public Sub SelectTab(ByVal pText As String)
        mForm.SelectTab(pText)
    End Sub

    Public Function GetTabPage(ByVal pText As String) As TabPage
        Return mForm.GetTabPage(pText)
    End Function

    Public Sub ChangeTabPageText(ByVal pText As String, ByVal pNewText As String)
        mForm.ChangeTabPageText(pText, pNewText)
    End Sub

    Public Sub ShowDialog()
        If Not mForm.Visible Then 'Form that is already visible cannot be displayed as a modal dialog box
            'mForm.Visible = False
            mForm.ShowDialog()
        End If

    End Sub

    Public Function AddButton(ByVal text As String, ByVal image As System.Drawing.Image, ByVal onClick As System.EventHandler) As System.Windows.Forms.ToolStripButton
        Return mForm.AddStripButton(text, image, onClick)
    End Function

#End Region
    'Private mAllowNew As Boolean = False

    Private ReadOnly Property AllowNew() As Boolean
        Get
            Return CBool(mMenuRow.AllowNew)
        End Get
    End Property

    Private ReadOnly Property ReadOnlyModule() As Boolean
        Get
            Return CBool(mMenuRow.ReadOnly)
        End Get
    End Property
    Private IsReadOnly As Boolean = False
    Private Property ReadOnlyInfo() As Boolean
        Get
            If Me.Table.Columns.Contains("ReadOnly") Then
                If mMenuID <> Database.Menu.SYSTEM_Menu Then
                    If Me.Row("ReadOnly") IsNot DBNull.Value Then
                        Return CBool(Me.Row("ReadOnly"))
                    Else
                        Return IsReadOnly
                    End If
                Else
                    Return IsReadOnly
                End If
            Else
                Return IsReadOnly
            End If
        End Get
        Set(ByVal value As Boolean)

            IsReadOnly = value
        End Set
    End Property
    Private ReadOnly Property OkToPrintInfo() As Boolean
        Get
            If Me.Table.Columns.Contains("OkToPrintInfo") Then
                'If mMenuID <> Database.Menu.SYSTEM_Menu Then
                If Me.Row("OkToPrintInfo") IsNot DBNull.Value Then
                    Return CBool(Me.Row("OkToPrintInfo"))
                    'Else
                    '    Return True
                    'End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Get
    End Property
    Private ReadOnly Property OkToEmailInfo() As Boolean
        Get
            If Me.Table.Columns.Contains("OkToEmailInfo") Then
                If mMenuID <> Database.Menu.SYSTEM_Menu Then
                    If Me.Row("OkToEmailInfo") IsNot DBNull.Value Then
                        Return CBool(Me.Row("OkToEmailInfo"))
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    Private Sub EnableButtons(ByVal id As Integer)
        Dim vNewInfo As Boolean
        Dim vOldInfo As Boolean
        vNewInfo = (id = 0)
        vOldInfo = (id <> 0)

        Dim AllowEdit As Boolean
        Dim AllowEditMenu As Boolean

        AllowEdit = nDB.GetMenuValue(CType(Me.MenuID, GSCOM.Applications.InSys.Database.Menu), Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)
        AllowEditMenu = CBool(mInfoMenuSet.tMenu.Rows(0)("AllowEdit"))

        mForm.NewButton.Enabled = vOldInfo And Me.AllowNew
        mForm.RefreshButton.Enabled = vOldInfo
        mForm.SaveButton.Enabled = (Not Me.ReadOnlyModule) And (Not Me.ReadOnlyInfo) And (AllowEdit And AllowEditMenu)
        mForm.EmailButton.Enabled = CBool(Me.OkToEmailInfo)
        mForm.PrintButton.Enabled = CBool(Me.OkToPrintInfo)

        Dim b As New ToolStripButton
        Dim ei As String
        For Each dr As DataRow In mInfoMenuSet.tMenuButton.Select

            Dim ButtonUser As Integer

            Dim s As String
            s = "SELECT dbo.fGetApprover(" & Me.mSession.GetSessionID.ToString & "," & Me.MenuID.ToString & "," & dr("ID").ToString & ")"

            Dim ss As String
            ss = "SELECT dbo.fGetButtonApprover(" & Me.MenuID.ToString & "," & dr("ID").ToString & ")"

            Dim ButtonHasApprover As Boolean
            ButtonHasApprover = CBool(GSCOM.SQL.ExecuteScalar(ss, Connection))

            b = mForm.GetStripButton(dr("Name").ToString)
            ei = dr("EnabledIf").ToString
            If ei = "" Then
                ei = "True"
            Else
                ei = Me.PassParameters(ei)
                ei = Me.Table.Select(ei).Length.ToString
            End If

            'BILLY 20110617''''''''''''''''''''''''''''''''''''''\
            If ButtonHasApprover Then
                If GSCOM.SQL.ExecuteScalar(s, Connection) IsNot DBNull.Value Then 'If button is on tmenuapprover
                    ButtonUser = CInt(GSCOM.SQL.ExecuteScalar(s, Connection))

                    If mSession.GetUserID = ButtonUser Then
                        b.Enabled = (
                        (Not Me.ReadOnlyModule) _
                        And (Not Me.ReadOnlyInfo) _
                        And (vOldInfo Or Not (CBool(dr("DisabledOnNewInfo")))) _
                        And (CBool(ei))
                        )
                    Else
                        b.Enabled = False
                    End If
                Else
                    b.Enabled = False
                End If
            Else 'normal button checking
                b.Enabled = (
                       (Not Me.ReadOnlyModule) _
                       And (Not Me.ReadOnlyInfo) _
                       And (vOldInfo Or Not (CBool(dr("DisabledOnNewInfo")))) _
                       And (CBool(ei))
                       )
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''/
            If b.Owner IsNot mForm.tsMain Then
                b.Visible = b.Enabled
            Else
                b.Visible = True
            End If
        Next

    End Sub

#End Region

#Region "Events"
    Private Sub dgv_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)


        ' e.Cancel = True
    End Sub

    Private Sub mForm_CreateCopyButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.CreateCopyButtonClicked
        CreateCopy()
    End Sub

    Private Sub mForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles mForm.FormClosed
        If IsSaved Then
            RefreshListing()
        End If
        mShown = False
    End Sub

    Private Sub mForm_HelpButtonClicked(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mForm.HelpButtonClicked
        ShowPrintPreview(True)
        e.Cancel = True
    End Sub

    'Private Sub dgv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs)
    '    Dim c As DataGridViewComboBoxCell
    '    c = TryCast(CType(sender, DataGridView).Item(e.ColumnIndex, e.RowIndex), DataGridViewComboBoxCell)
    '    If c IsNot Nothing Then
    '        If e.FormattedValue.ToString = "" Then
    '            Dim drv As DataRowView
    '            drv = CType(c.DataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView)
    '            drv(c.OwningColumn.DataPropertyName) = DBNull.Value
    '            '    e.Cancel = True
    '        End If
    '    End If
    'End Sub


    Private Sub mForm_SaveButtonClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mForm.SaveButtonClicked
        Try
            BeginProcess("Saving data")
            Save()
            EndProcess("Data saved")
        Catch ex As DBConcurrencyException
            EndProcess(ex.Message, False)
            LoadInfo(CInt(Me.Row("ID")))
        Catch ex As InconsistentDataException
            EndProcess(ex.Message, False)
            LoadInfo(CInt(Me.Row("ID")))
        Catch ex As Exception
            EndProcess(ex.Message, False)
        End Try

    End Sub

    Public Function CanNew() As Boolean
        Return MsgBox("Are you sure you want to create new?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes
    End Function

    Private Sub mForm_NewButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.NewButtonClicked
        If CanNew() Then
            Me.BeginProcess("Preparing a new file")
            LoadInfo(0)
            Me.EndProcess()
        End If
    End Sub

    Protected Overridable Sub Refresh()
        'EndEdit()
        'ValidData()
        'Me.Row.EndEdit() 
        'ROBBIE: TODO 

        mForm.CancelEdit()

        nDB.RefreshLookUp() 'ROBBIE 20061125


        If False Then 'DataSet.HasChanges Then
            Select Case MsgBox("Do you want to save the changes?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton1)
                Case MsgBoxResult.Yes
                    BeginProcess("Refreshing Data")
                    Save()
                Case MsgBoxResult.No
                    BeginProcess("Refreshing Data")
                    LoadInfo(CInt(Me.Row.Item("ID")))
            End Select
        Else
            BeginProcess("Refreshing Data")
            LoadInfo(CInt(Me.Row.Item("ID")))
        End If
        EndProcess()
    End Sub

    Private Sub mForm_RefreshButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.RefreshButtonClicked
        Try
            Refresh()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

#End Region


#Region "AddReportViewer"
    Friend Function AddReportViewer(ByVal pText As String) As CrystalDecisions.Windows.Forms.CrystalReportViewer
        'Dim tp As TabPage
        Dim crv As CrystalDecisions.Windows.Forms.CrystalReportViewer
        'tp = New TabPage(pText)
        crv = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        AddControl(crv, pText)
        'crv.Dock = DockStyle.Fill
        'crv.Visible = True
        'tp.Controls.Add(crv)
        'mForm.AddTabPage(tp)
        Return crv
    End Function

#End Region

#Region "AddControl"
    Friend Function AddControl(ByVal pControl As Control, ByVal pText As String) As TabPage
        Dim tp As TabPage
        tp = New TabPage(pText)
        pControl.Dock = DockStyle.Fill
        pControl.Visible = True
        tp.Controls.Add(pControl)
        mForm.AddTabPage(tp)
        Return tp
    End Function

#End Region

#Region "AddGrid"
    Friend Function AddGrid(ByVal pText As String) As GSDetailDataGridView
        Dim tp As TabPage
        Dim crv As GSDetailDataGridView
        tp = New TabPage(pText)
        crv = New GSDetailDataGridView

        AddHandler crv.RowsAdded, AddressOf RobAdded
        AddHandler crv.RowsRemoved, AddressOf RobRemoved

        crv.Dock = DockStyle.Fill
        crv.Visible = True
        tp.Controls.Add(crv)
        mForm.AddTabPage(tp)
        Return crv
    End Function

#End Region

#Region "ReArrange Tab"

    Friend Sub ReArrangeTab(Optional orderbyascending As Boolean = True)
        mForm.RearrangeTab(orderbyascending)
    End Sub

#End Region

#Region "BeginProcess"
    Protected Sub BeginProcess(Optional ByVal vMessage As String = "")
        mForm.BeginProcess(vMessage)
    End Sub

    Protected Sub SetStatusLabel(Optional ByVal vMessage As String = "")
        mForm.SetStatusLabel(vMessage)
    End Sub

#End Region

#Region "EndProcess"
    Protected Sub EndProcess(Optional ByVal vMessage As String = "", Optional ByVal vGood As Boolean = True)
        mForm.EndProcess(vMessage, vGood)
    End Sub

#End Region


#Region "InconsistentDataException"
    Friend Class InconsistentDataException
        Inherits Exception
        Public Sub New()
            MyBase.New("The data are inconsistent")
        End Sub
    End Class

#End Region

    Private Sub TrySave(ByRef pCancel As Boolean, Optional ByVal pPrefix As String = "")
        Dim s As String = pPrefix
        If Me.Table.Rows(0).RowState = DataRowState.Added Then
            s &= "Are you sure you want to cancel this new record?"
            Select Case MsgBox(s, MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)
                Case MsgBoxResult.No ', MsgBoxResult.Cancel
                    pCancel = True
            End Select
        Else
            If Me.SaveButton.Enabled = False Then
                pCancel = False
                Exit Sub
            End If

            s &= "Do you want to save the changes?"
            If IsSaved = False Then
                Select Case MsgBox(s, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton1)
                    Case MsgBoxResult.Yes
                        If Not Save() Then
                            pCancel = True
                        End If
                    Case MsgBoxResult.Cancel
                        pCancel = True
                End Select
            End If
        End If

    End Sub
    Private Sub mForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles mForm.FormClosing
        Try
            mForm.EndEdit()
            If Me.HasUnsavedChanges Then
                TrySave(e.Cancel)
            End If


            'mForm.EndEdit()
            ''ValidData()
            ''Me.Row.EndEdit()
            'If Dataset.HasChanges Then
            '    Select Case MsgBox("Do you want to save the changes?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton1, "")
            '        Case MsgBoxResult.Yes
            '            If Not Save() Then 
            '                e.Cancel = True
            '            End If
            '        Case MsgBoxResult.Cancel
            '            e.Cancel = True
            '    End Select
            'End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            EndProcess()
        End Try
    End Sub

    Private Sub LoadListClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tg As TreeGrid
        tg = DirectCast(DirectCast(sender, ToolStripItem).Tag, TreeGrid)
        'For Each dr As DataRow In tg.DataSource.Select
        '    If dr.RowState = DataRowState.Added Then
        '        tg.DataSource.Rows.Remove(dr)
        '    End If
        'Next

        tg.CleanUp(Nothing, "")
        Dim s As String = Me.PassParameters(tg.OriginalListSource)
        tg.LoadTree(tg.DataSourceFilter, True, s)
    End Sub

    Private Sub ShowDetailInfo(ByVal pMenu As Integer, ByVal pID As Integer, ByVal pListing As DataTable)
        Dim vInfo As InfoSet
        vInfo = GetInfoSet(CType(pMenu, Database.Menu))
        If vInfo Is Nothing Then
            vInfo = New ZInfo(pMenu, Connection, pListing, pID)
            AddInfoSet(vInfo, CType(pMenu, Database.Menu))
            'ROBBIE 20070517 ---------------\
            'If Not CBool(GSCOM.Applications.InSys.nDB.GetMenuValue(Database.Menu.Payroll_Payroll, Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)) Then
            ' vInfo.MakeReadOnly()
            'End If
            'ROBBIE 20070517 ---------------/
            '''''''''''''''''''vInfo.AllowNew = CBool(Database.MenuTable.Select("ID=" & GSCOM.SQL.SQLFormat(CInt(Database.Menu.Payroll_Payroll)))(0).Item("AllowNew"))
        Else
            vInfo.LoadInfo(pID)
        End If
        Application.DoEvents()
        ' vInfo.SaveButton.Enabled = mControl.Enabled
        vInfo.mForm.Size = mForm.Size
        vInfo.ShowDialog()
        'LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
    End Sub

    Public Sub SetCompanyMenu(ByVal pReportInfo As Html.ReportInfo, ByVal xMenu As Integer)
        Dim pM As Database.Menu = CType(577, Database.Menu)
        Dim ss As String = "SELECT ID FROM tCompanyMenu cm WHERE cm.ID_Company=" & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & " AND cm.ID_Menu=" & xMenu
        Dim vID As Object = GSCOM.SQL.ExecuteScalar(ss, gConnection)
        Dim pInfoSet As InfoSet = Nothing
        If IsNothing(vID) Then
            pInfoSet = MainModule.NewReport(MyBase.MenuID)
            If pInfoSet.Row.RowState = DataRowState.Added Then
                Exit Sub
            End If
        Else
            pInfoSet = GetInfoSet(pM)
            If pInfoSet IsNot Nothing Then
                pInfoSet.LoadInfo(CInt(vID))
            Else
                pInfoSet = ActiveModule.NewInfo(pM, Nothing, CInt(vID))
            End If
        End If
    End Sub

    Private Sub mForm_PrintButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.PrintButtonClicked
        ShowPrintPreview(False)
    End Sub

    Public Overridable Sub EmailButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.EmailButtonClicked

        Dim s, ss As String
        Dim x As Integer = 0
        Dim f As New BrowserForm
        Dim aMenu As Database.Menu = CType(444, Database.Menu)
        Dim aInfoSet As InfoSet = Nothing
        Dim dt As DataTable
        Dim dtt As DataTable

        aInfoSet = GetInfoSet(aMenu)
        ss = "SELECT e.ID,e.CompanyEmail,d.Name FROM dbo.tEmployee e LEFT JOIN" &
            " dbo.tDesignation d ON e.ID_Designation = d.ID INNER JOIN" &
            " dbo.tDepartment de ON d.ID = de.ID_HeadDesignation"
        dtt = GSCOM.SQL.TableQuery(ss, Connection)
        dt = aInfoSet.mDataset.Tables("tClientAttendees")

        Dim MailFrom As String = nDB.Session.Get("CompanyEmail").ToString
        Dim CPassword As String = nDB.Session.Get("EmailPassword").ToString

        Dim MailTo() As String = Nothing
        Dim MailCC() As String = Nothing
        Dim MailSubject As String = InputBox("Please input subject.", "Email", "Minutes of the Meeting")

        For Each dr As DataRow In dt.Rows
            ReDim Preserve MailTo(x)
            MailTo(x) = dr.Item("EmailAdd").ToString
            x += 1
        Next

        For Each drr As DataRow In dtt.Rows
            ReDim Preserve MailCC(x)
            MailCC(x) = drr.Item("CompanyEmail").ToString
            x += 1
        Next

        Dim MailToString As String
        Dim MailCCString As String
        MailToString = String.Join(";", MailTo)
        MailCCString = String.Join(";", MailCC)
        f.Size = mForm.Size
        f.StartPosition = mForm.StartPosition

        Dim hc As New Html.HtmlContent(mInfoMenuSet, Me.mDataset)
        Dim a As New Html.HtmlInfo(hc, True)
        Dim b As New BrowserDataList
        a.ReportInfo = New Html.ReportInfo(b.SetCompanyMenu(Me.MenuID), Me, Nothing, Me.MenuID)

        Dim em As New EmailClass

        Dim origimg As String = nDB.ImagePath(Me.mMenuRow.ImageFile)
        Dim img As String = IO.Path.GetTempFileName

        Dim bmp As Bitmap = New Bitmap(origimg)
        bmp.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg)

        s = em.CreateEmailView(img, a.GetHtml)
        em.SetServerProperties()
        em.SetMailProperties(MailFrom, MailToString, MailSubject, True, Nothing, MailFrom, CPassword, MailCCString)

        Try
            BeginProcess("Sending Email")
            em.Send()
            EndProcess("Email Sent")
        Catch ex As Exception
            EndProcess(ex.Message, False)
        End Try
    End Sub

    Public Overloads Sub InitControl(ByVal pControl As InSys.InfoForm, ByVal pMenu As Integer, ByVal pMenuTab As DataTable, ByVal pMenuTabField As DataTable)
        Dim vCurrentTabPage As GSCOM.UI.DataTabPage.DataTabPage
        Dim mtr As New Database.MenuTabRow
        For Each dr As DataRow In pMenuTab.Rows

            mtr.InnerRow = dr
            If mtr.Name = "General" Then
                'vCurrentTabPage = pControl.BasicTab.AddTable(mtr.Name, mtr.HasTable, 1)
                'InitControlCore(pMenuTabField, mtr, vCurrentTabPage, 1)
                'addScrollBar(vCurrentTabPage) 'EMil 8/17/2012

                vCurrentTabPage = pControl.BasicTab.AddTable(mtr.Name, mtr.HasTable, 1)

                InitControlCore(pMenuTabField.Copy().AsEnumerable().Select(Function(idr As DataRow)
                                                                               idr("Panel") = 1
                                                                               Return idr
                                                                           End Function).CopyToDataTable(), mtr, vCurrentTabPage, 1)
                addScrollBar(vCurrentTabPage) 'EMil 8/17/2012
            Else
                vCurrentTabPage = pControl.tcMain.AddTable(mtr.Name, mtr.HasTable, 2)
                InitControlCore(pMenuTabField, mtr, vCurrentTabPage, 1)
                InitControlCore(pMenuTabField, mtr, vCurrentTabPage, 2)
            End If
            vCurrentTabPage.ImageKey = mtr.ImageFile
        Next



    End Sub

    Private Sub addScrollBar(ByRef t As TabPage) 'EMil 8/17/2012
        For Each c As Control In t.Controls
            If Object.ReferenceEquals(c.GetType, GetType(GSCOM.DataPanel)) Then
                CType(c, GSCOM.DataPanel).AutoScroll = True
                'CType(c, GSCOM.DataPanel).Font = New Font("Calibiri", 8, FontStyle.Regular)
                Exit For
            End If
        Next
    End Sub

    Private Sub InitControlCore(ByVal pMenuTabField As DataTable, ByVal mt As Database.MenuTabRow, ByVal vCurrentTabPage As GSCOM.UI.DataTabPage.DataTabPage, ByVal p As Integer)
        Dim ct As Database.SystemControlTypeEnum
        Dim c As Control
        Dim m As Integer
        Dim h As String
        Dim drv As New DataView(pMenuTabField)
        drv.RowFilter = "ID_MenuTab=" & mt.ID.ToString & " AND Panel=" & p.ToString & " AND ShowInInfo=1"
        For Each drf As DataRowView In drv
            Dim mtf As New Database.MenuTabFieldRow(drf.Row)
            ct = CType(mtf.ID_SystemControlType, Database.SystemControlTypeEnum)
            Select Case ct
                Case Database.SystemControlTypeEnum.DataLookUp
                    If nDB.MenuSet.tMenu.Select("ID=" & CInt(mtf.ID_Menu)).Length = 0 Then
                        drf("ID_SystemControlType") = 1 'textbox
                        drf("ReadOnly") = True
                    End If

            End Select
        Next
        For Each drf As DataRow In pMenuTabField.Select(drv.RowFilter) ''FOR REVISION
            Dim mtf As New Database.MenuTabFieldRow(drf)
            ct = CType(mtf.ID_SystemControlType, Database.SystemControlTypeEnum)
            If mtf.ID_Menu IsNot DBNull.Value Then
                m = CInt(mtf.ID_Menu)
            End If
            h = mtf.Header
            p = mtf.Panel
            Select Case ct
                Case Database.SystemControlTypeEnum.DataLookUp
                    c = vCurrentTabPage.SetLabel(mtf.Name, ct, mtf.EffectiveLabel, h, p, mtf.StringFormat)
                    Dim dl As GSCOM.UI.DataLookUp.DataLookUp
                    dl = CType(c, GSCOM.UI.DataLookUp.DataLookUp)
                    dl.Worker.FixedFilter = mtf.FixedFilter
                    InitLookUp(dl, CType(m, Database.Menu))
                    If mtf.ParentLookUp <> "" And mtf.ParentLookUpChildColumn <> "" Then
                        Dim vParentLookUp As GSCOM.UI.DataLookUp.DataLookUp = TryCast(Me.GetControl("_" & mtf.ParentLookUp), UI.DataLookUp.DataLookUp)
                        If vParentLookUp IsNot Nothing Then
                            dl.Worker.SetParentLookUp(vParentLookUp, mtf.ParentLookUpChildColumn)
                        Else
                            MsgBox("Parent lookup does not exist: " & mtf.ParentLookUp)

                        End If
                    End If
                    Dim s As String
                    'MENUTABFIELD,EXTRAFIELDS----------------------------------------------------------------------------------------------------
                    Dim mtf2 As New Database.MenuTabFieldRow
                    s = Database.Tables.tMenuTabField.Field.ParentLookUp.ToString & "=" & GSCOM.SQL.SQLFormat(mtf.Name)
                    s &= " AND " & Database.Tables.tMenuTabField.Field.ListColumn.ToString & " IS NOT NULL"
                    For Each dr As DataRow In Me.mInfoMenuSet.tMenuTabField.Select(s)
                        mtf2.InnerRow = dr
                        dl.Worker.ExtraFields.Add(mtf2.Name, mtf2.ListColumn)
                    Next
                    'MENUTAB,EXTRATABLES----------------------------------------------------------------------------------------------------
                    Dim mdt As New Database.MenuDetailTabRow
                    s = Database.Tables.tMenuDetailTab.Field.ParentLookUp.ToString & "=" & GSCOM.SQL.SQLFormat(mtf.Name)
                    s &= " AND (ID_MenuDetailTabType=" & CInt(Database.MenuDetailTabTypeEnum.Grid)
                    s &= " OR ID_MenuDetailTabType=" & CInt(Database.MenuDetailTabTypeEnum.TreeView) & ")"
                    For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTab.Select(s)
                        mdt.InnerRow = dr
                        dl.Worker.ExtraTables.Add(mdt.TableName, mdt.DataSource)
                    Next
                    AddHandler dl.SelectedValueChanged, AddressOf DataLookUp_Validated
                    AddHandler dl.Worker.GettingFixedFilter, AddressOf DataLookUp_GettingFixedFilter
                Case Database.SystemControlTypeEnum.DockedTextBox
                    If mt.HasTable Then
                        c = vCurrentTabPage.SetLabel(mtf.Name, ct, mtf.EffectiveLabel, h, p, mtf.StringFormat, mtf.Height)
                    Else
                        c = vCurrentTabPage.AddTextBox(mtf.Name)
                    End If
                Case Database.SystemControlTypeEnum.TextBox
                    c = vCurrentTabPage.SetLabel(mtf.Name, ct, mtf.EffectiveLabel, h, p, mtf.StringFormat)
                    'CHECK IF COLUMN NAME IS COLOR
                    If c.Name.ToLower.Contains("color") Then
                        DirectCast(c, TextBox).ReadOnly = True
                        AddHandler c.Click, AddressOf ColorClicked
                    End If
                Case Database.SystemControlTypeEnum.ComboBox
                    c = vCurrentTabPage.SetLabel(mtf.Name, ct, mtf.EffectiveLabel, h, p, mtf.StringFormat)
                    'AddHandler DirectCast(c, ComboBox).DropDownClosed, AddressOf DropDownClosed
                Case Else
                    c = vCurrentTabPage.SetLabel(mtf.Name, ct, mtf.EffectiveLabel, h, p, mtf.StringFormat)
            End Select
        Next
        vCurrentTabPage.AddRow(p)
    End Sub



    Private cd As New ColorDialog
    Private Sub ColorClicked(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim colorHex As String
        'cd.AnyColor = True
        cd.FullOpen = True
        If DirectCast(sender, TextBox).Text <> "" Then
            cd.Color = getColorValue(DirectCast(sender, TextBox).Text)
        End If
        If cd.ShowDialog = vbOK Then
            colorHex = GetHexColor(cd.Color)
            DirectCast(sender, TextBox).Text = colorHex
        End If
    End Sub

    Private Sub mForm_PlotValuesButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.PlotValuesButtonClicked
        Dim f As New BrowserDataListForm(CType(mMenuID, Database.Menu), False)
        With f
            .CallingInfoSet = Me
            .Size = Me.mForm.Size
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub DataLookUp_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not mShown Then Exit Sub
        If Me.Loading Then Exit Sub
        Dim ro As Boolean
        Dim dl As DataLookUp = CType(sender, DataLookUp)
        For Each de As String In dl.Worker.ExtraFields.Keys
            ro = Me.Row.Table.Columns(de).ReadOnly ''''''''''''''READONLY
            If ro Then Me.Row.Table.Columns(de).ReadOnly = False ''''''''''''''READONLY
            If dl.Worker.Row Is Nothing Then
                Me.Row(de) = Me.Row.Table.Columns(de).DefaultValue
            Else
                Me.Row(de) = dl.Worker.Row(dl.Worker.ExtraFields.Item(de))
            End If
            If ro Then Me.Row.Table.Columns(de).ReadOnly = True '''''''''''''''READONLY
        Next

        '''''''''''''''''''''''''''''''HERE
        Dim drr2 As DataRow()
        Dim s As String
        Dim mdt As New Database.MenuDetailTabRow
        Dim mdtf As New Database.MenuDetailTabFieldRow
        Dim myDT As DataTable



        For Each dr As String In dl.Worker.ExtraTables.Keys
            'If mdt.ID_MenuDetailTabType = 1 Then
            Dim vDestDT As DataTable = Me.mDataset.Tables(dr)
            s = GSCOM.SQL.GetFilter(Database.Tables.tMenuDetailTab.Field.TableName.ToString, dr)
            mdt.InnerRow = Me.mInfoMenuSet.tMenuDetailTab.Select(s)(0)
            s = GSCOM.SQL.GetFilter(Database.Tables.tMenuDetailTabField.Field.ID_MenuDetailTab.ToString, mdt.ID)
            s &= " AND ParentLookUpListColumn IS NOT NULL"
            drr2 = Me.mInfoMenuSet.tMenuDetailTabField.Select(s)
            Dim ss As String = "SELECT * FROM " & Me.PassParameters(mdt.ListSource)
            myDT = GSCOM.SQL.TableQuery(ss, gConnection)
            vDestDT.Clear()
            Dim tg As TreeGrid
            'Dim mdtr As New Database.MenuDetailTabRow(dra(0))
            tg = DirectCast(Me.GetControl(TreeGrid.TreeGridPrefix & mdt.ID), TreeGrid)

            'If tg Is Nothing Or Me.Row.RowState = DataRowState.Added Then BILLY EDIT
            If tg Is Nothing And Me.Row.RowState = DataRowState.Added Then
                For Each drz As DataRow In myDT.Select
                    Dim drx As DataRow = vDestDT.NewRow
                    For Each dr2 As DataRow In drr2
                        mdtf.InnerRow = dr2
                        ss = mdtf.Name
                        drx.Item(ss) = drz(mdtf.ParentLookUpListColumn)
                    Next
                    If drx.RowState = DataRowState.Detached Then
                        vDestDT.Rows.Add(drx)
                    End If

                Next
            End If
            If Me.Row.RowState = DataRowState.Added And tg IsNot Nothing Then
                If tg IsNot Nothing Then
                    s = tg.OriginalListSource
                    s = Me.PassParameters(s)
                    tg.LoadTree(tg.DataSourceFilter, False, s)
                    CheckTreeNodes(tg.Nodes)
                End If
            End If
            'End If
        Next


        'LoadTreeGrids(Me.RowID)
    End Sub

    Sub CheckTreeNodes(ByVal nc As TreeNodeCollection)
        For Each n As TreeNode In nc
            n.Checked = True
            CheckTreeNodes(n.Nodes)
        Next
    End Sub

    Private Sub DataLookUp_GettingFixedFilter(ByVal sender As Object, ByVal e As GSCOM.UI.DataLookUp.DataLookUpWorker.GettingFixedFilterEventArgs)
        If Me.Row IsNot Nothing AndAlso Me.Row.RowState <> DataRowState.Deleted AndAlso Me.Row.RowState <> DataRowState.Detached Then
            e.FixedFilter = Me.PassParameters(e.FixedFilter)
        End If
    End Sub

    Private mShown As Boolean
    Private Sub mForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.Shown
        mShown = True
        Try
            Dim dgvc As DataGridViewColumn
            Dim dtg As DataGridView
            Dim ei As String
            For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ReadOnlyIf IS NOT NULL")
                dtg = Me.GetDataGridView(mDataset.Tables(CStr(mInfoMenuSet.tMenuDetailTab.Select("ID=" & CStr(dr("ID_MenuDetailTab")))(0)("TableName"))))
                If dtg Is Nothing AndAlso Not dtg.Columns.Contains(CStr(dr("Name"))) Then Continue For
                dgvc = dtg.Columns(CStr(dr("Name")))
                ei = dr("ReadOnlyIf").ToString
                If ei <> "" Then
                    Dim dt As DataTable = mDataset.Tables(CStr(mInfoMenuSet.tMenuDetailTab.Select("ID=" & CStr(dr("ID_MenuDetailTab")))(0)("TableName")))
                    Dim drvp As DataRowView
                    Dim drx As DataRow = Nothing
                    For Each dgr As DataGridViewRow In dtg.Rows
                        drvp = TryCast(dgr.DataBoundItem, DataRowView)
                        If drvp IsNot Nothing Then
                            drx = drvp.Row
                        Else
                            Continue For
                        End If
                        If drx Is Nothing Then Continue For
                        Dim f As String = Me.PassParameters(ei, dt, , drx)
                        f = Table.Select(f).Length
                        dgr.Cells(dgvc.Name).ReadOnly = CBool(f)
                    Next
                    'ei = Me.PassParameters(ei)
                    'ei = Me.Table.Select(ei).Length.ToString
                    'dgvc.ReadOnly = CBool(ei)
                End If
            Next
        Catch ex As Exception
            EndProcess(ex.Message, False)
        End Try

    End Sub

    Private Sub MyEditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If TypeOf dgv.Columns(dgv.CurrentCell.ColumnIndex) Is GSCOM.UI.DataLookUp.DataGridViewLookUpColumn Then
            Dim dlc As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn = CType(dgv.Columns(dgv.CurrentCell.ColumnIndex), UI.DataLookUp.DataGridViewLookUpColumn)

            Dim d As GSCOM.UI.DataLookUp.LookUpEditingControl
            d = TryCast(e.Control, GSCOM.UI.DataLookUp.LookUpEditingControl)
            If d IsNot Nothing Then
                Dim dt As DataTable = CType(dgv.DataSource, DataTable)
                Dim drv As DataRowView
                Dim dr As DataRow = Nothing
                drv = TryCast(dgv.CurrentRow.DataBoundItem, DataRowView)
                If drv IsNot Nothing Then
                    dr = drv.Row
                End If

                Dim drp As DataRow = Nothing
                Dim dgvp As DataGridView
                Dim drvp As DataRowView
                If dt.ParentRelations.Count > 0 Then
                    dgvp = Me.GetDataGridView(dt.ParentRelations(0).ParentTable)
                    If dgvp IsNot Nothing Then
                        drvp = TryCast(dgvp.CurrentRow.DataBoundItem, DataRowView)
                        If drvp IsNot Nothing Then
                            drp = drvp.Row
                        End If
                    End If
                End If
                If drp IsNot Nothing Then
                    d.Worker.FixedFilter = Me.PassParameters(dlc.FixedFilter, dt, dr, drp)
                Else
                    d.Worker.FixedFilter = Me.PassParameters(dlc.FixedFilter, dt, dr)
                End If


            End If

        End If

    End Sub

    Private Sub InfoSet_Commited(ByVal sender As Object, ByVal e As CommitedEventArgs) Handles Me.Commited
        For Each dt As DataTable In Me.mDataset.Tables
            For Each dr As DataRow In dt.Select("", "", DataViewRowState.Unchanged)
                For Each dc As DataColumn In dt.Columns
                    If dc.ColumnName.StartsWith("ORIGX_") Then
                        dr(dc.ColumnName) = dr(dc.ColumnName.Replace("ORIGX_", ""))
                    End If
                Next
                If dr.RowState = DataRowState.Modified Then
                    dr.AcceptChanges()
                End If
            Next
        Next
        If e.SaveQuery IsNot Nothing And e.SaveQuery <> "" Then
            SaveTrigger(e.SaveQuery, Connection)
            LoadInfo(Me.RowID)
        End If
    End Sub

    Private Sub mForm_TabPageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.TabPageChanged
        For Each o As InfoSetReportViewer In Me.ReportViewers
            If o.mPage Is CType(o.mPage.Parent, TabControl).SelectedTab Then
                LoadReportViewer(o)
            End If
        Next
    End Sub

    Private Sub mForm_PropertyButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.PropertyButtonClicked
        Dim DocMenuID As Integer
        Dim sq As String = "SELECT ID FROM tMenu where Name LIKE 'Document Properties'"
        DocMenuID = CInt(GSCOM.SQL.ExecuteScalar(sq, gConnection))
        Dim DocProp As Database.Menu = CType(DocMenuID, Database.Menu)

        Dim ss As String = "SELECT ID FROM tDocumentProperties dp WHERE " & "ID_Original = " & Me.RowID
        Dim vID As Object = GSCOM.SQL.ExecuteScalar(ss, gConnection)

        Dim pInfoSet As InfoSet
        pInfoSet = GetInfoSet(DocProp)
        If pInfoSet Is Nothing Then
            pInfoSet = New ZInfo(DocProp, Connection, Nothing, CInt(vID))
            AddInfoSet(pInfoSet, DocProp)
        Else
            pInfoSet.LoadInfo(CInt(vID))
        End If
        Application.DoEvents()
        pInfoSet.Height = 500
        pInfoSet.Width = 800
        pInfoSet.ShowDialog()

    End Sub

    Sub LoadTreeGrids(ByVal pID As Integer)
        Dim cdt As GSCOM.SQL.ZDataTable
        For Each dr As DataRelation In Me.mDataset.Relations
            cdt = DirectCast(dr.ChildTable, GSCOM.SQL.ZDataTable)
            Dim dra As DataRow()
            dra = mInfoMenuSet.tMenuDetailTab.Select("TableName=" & GSCOM.SQL.SQLFormat(dr.ChildTable.TableName) & " AND (ID_MenuDetailTabType=2)")
            If dra.Length > 0 Then
                Dim tg As TreeGrid
                Dim mdtr As New Database.MenuDetailTabRow(dra(0))

                tg = DirectCast(Me.GetControl(TreeGrid.TreeGridPrefix & mdtr.ID), TreeGrid)
                If tg IsNot Nothing Then

                    Dim s As String = tg.OriginalListSource

                    s = Me.PassParameters(s)

                    tg.LoadTree(cdt.GetFilter, (pID = 0), s)
                End If
            End If
        Next
    End Sub

    Sub ShowPrintPreview(ByVal pHelpMode As Boolean)
        Dim s, tmp As String
        Dim f As New BrowserForm
        f.Size = mForm.Size
        f.StartPosition = mForm.StartPosition
        Dim hc As New Html.HtmlContent(mInfoMenuSet, Me.mDataset)
        Dim a As New Html.HtmlInfo(hc, False)
        a.HelpMode = pHelpMode
        Dim b As New BrowserDataList
        'a.ReportInfo = New Html.ReportInfo(b.SetCompanyMenu(Me.MenuID), Me, Nothing, Me.MenuID)
        s = a.GetHtml
        tmp = IO.Path.GetTempFileName
        '--\ use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save --\
        Dim tf As New IO.StreamWriter(tmp, False, System.Text.Encoding.UTF8)
        tf.Write(s)
        tf.Flush()
        '--/ use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save --/
        Try
            f.MainBrowser.Navigate(tmp)
            f.ShowDialog()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub SaveTrigger(ByVal pProcedure As String, ByVal pConnection As SqlClient.SqlConnection)
        Dim s As String
        s = "EXEC "
        s &= Me.PassParameters(pProcedure)
        Try
            GSCOM.SQL.ExecuteNonQuery(s, pConnection)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveTrigger(ByVal pProcedure As String, ByVal pTransaction As SqlClient.SqlTransaction)
        Dim s As String
        s = "EXEC "
        s &= Me.PassParameters(pProcedure)
        Try
            GSCOM.SQL.ExecuteNonQuery(s, pTransaction)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InfoSet_Saved(ByVal sender As Object, ByVal e As SavedEventArgs) Handles Me.Saved
        If e.SaveQuery IsNot Nothing Then
            Dim s As String
            s = "EXEC "
            s &= Me.PassParameters(e.SaveQuery)
            Try
                GSCOM.SQL.ExecuteNonQuery(s, e.Transaction)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

#Region "Import/Generate Template"
    '*******************************************************************'
    'BILLY - Experimental Import/Generate Template SoftCode 20110720
    '*******************************************************************'
    Public Overridable Sub GenerateExcelTemplate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '20130318 EMIL
        Dim sfd As New SaveFileDialog
        Dim tsb As ToolStripButton
        tsb = CType(sender, ToolStripButton)
        Dim arr() As String = tsb.Tag.ToString.Split(",") '<---
        sfd.FileName = arr(0)
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & arr(0), sfd.FileName, True)
            '20130318 EMIL-----DYNAMIC TEMPLATE----------------------------------------------------------|

            Dim dr As DataRow() = Nothing
            If arr.Length > 1 Then
                dr = Me.mInfoMenuSet.tMenuDetailTab.Select("ID = " + arr(1))
            ElseIf arr.Length = 1 Then
                dr = Me.mInfoMenuSet.tMenuDetailTab.Select("ImportFile = '" + arr(0) + "' AND FileReferenceDataSource IS NOT NULL")
            End If

            Dim EndColumn As String = Nothing

            'TEMPLATE DESIGN
            If dr(0).Item("ImportFile").ToString = "DynamicTemplate.xls" Then
                Dim dtHeader As DataTable = GSCOM.SQL.TableQuery("SELECT * FROM tMenuDetailTabField Where ID_MenuDetailTab = '" + dr(0).Item("ID").ToString + "'", Connection)
                Dim dtHeader2 As New DataTable
                Dim hd As String = Nothing
                For x As Integer = 0 To dtHeader.Rows.Count - 1
                    If dtHeader.Rows(x).Item("ListColumn").ToString <> "" Then
                        Dim dc As New DataColumn
                        dc.ColumnName = dtHeader.Rows(x).Item("ListColumn").ToString
                        dc.AllowDBNull = Not CBool(dtHeader.Rows(x).Item("IsRequired"))
                        dtHeader2.Columns.Add(dc)
                        hd = hd + "'" + dtHeader.Rows(x).Item("ListColumn").ToString + "',"
                    End If
                Next
                dtHeader = Nothing
                dtHeader = GSCOM.SQL.TableQuery("SELECT " + hd.Substring(0, hd.Length - 1), Connection)

                EndColumn = ChrW(64 + dtHeader.Columns.Count).ToString
                'CREATE DYNAMIC TEMPLATE
                CreateExcelTemplate(sfd.FileName, dtHeader, dtHeader2, "A", EndColumn, "Sheet1") '--DO NOT CHANGE SHEET NAME since Importing default sheet is Sheet1
            End If


            'ADD REFERENCE SHEETS FROM FileReferenceDataSource
            AddReferenceSheets(dr, EndColumn, sfd)

            '20130318 EMIL------------------------------------------------------------------------------------------------|
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub AddReferenceSheets(ByRef dr() As DataRow, ByRef EndCol As String, ByRef sfd As SaveFileDialog)
        If dr.Length > 0 And dr(0).Item("FileReferenceDataSource").ToString <> "" Then
            Dim arrRef() As String = dr(0).Item("FileReferenceDataSource").ToString.Split("|")
            Dim arrRefSort() As String = dr(0).Item("FileReferenceSort").ToString.Split("|")
            For c As Integer = 0 To arrRef.Length - 1
                Dim DT As DataTable = GSCOM.SQL.TableQuery("SELECT * FROM " + Me.PassParameters(arrRef(c) + IIf(arrRefSort(c) <> "", " ORDER BY " + arrRefSort(c), "")), Connection)
                Dim s As String = Nothing
                For Each dc As DataColumn In DT.Columns
                    s = s + "'" + dc.ColumnName + "', "
                Next
                EndCol = ChrW(64 + DT.Columns.Count).ToString
                If Not s Is Nothing Then
                    Dim HDT As DataTable = GSCOM.SQL.TableQuery("Select " + s.Substring(0, s.Length - 2), Connection)
                    AddToExcelTemplate(sfd.FileName, HDT, DT, "A", EndCol, DT.Columns(0).ColumnName)
                    HDT = Nothing
                End If
                DT = Nothing
            Next
        End If
    End Sub

    Protected Sub ExportToExcel(ByVal pFileName As String, ByVal vHeaderDT As DataTable, ByVal vHeaderDT2 As DataTable, ByVal vDetailDT As DataTable, ByVal pRange1 As String, ByVal pRange2 As String, ByVal pSheetName As String)

        Dim oExcel As New Excel.Application

        Dim workbooks As Excel.Workbooks
        Dim workbook As Excel._Workbook

        Dim worksheet As Excel._Worksheet

        Dim misValue As Object = System.Reflection.Missing.Value

        oExcel.DisplayAlerts = False
        oExcel.AlertBeforeOverwriting = False
        If oExcel Is Nothing Then
            Environment.ExitCode = 0
            Exit Sub
        End If

        workbooks = oExcel.Workbooks

        workbook = workbooks.Add(misValue)

        worksheet = workbook.Sheets(1)
        worksheet.Name = pSheetName

        Dim DataArray(vDetailDT.Rows.Count + vHeaderDT.Rows.Count, vDetailDT.Columns.Count + vHeaderDT.Columns.Count) As Object
        Dim r As Integer
        Dim c As Integer = 1

        For Each dc As DataColumn In vHeaderDT2.Columns
            With worksheet.Cells(1, c)
                If dc.AllowDBNull Then
                    .Interior.Color = RGB(202, 202, 202)
                Else
                    .Interior.Color = RGB(255, 153, 204)
                End If
                .Font.Bold = True
                .Font.Size = 10
            End With
            c += 1
        Next

        With worksheet.Range(pRange1 + "1:" + pRange2 + CStr(vDetailDT.Rows.Count + vHeaderDT.Rows.Count))
            .Font.Size = 10
        End With

        For c = 0 To vDetailDT.Columns.Count - 1
            DataArray(0, c) = vHeaderDT.Rows(0).Item(c)
        Next c

        For r = vHeaderDT.Rows.Count To vDetailDT.Rows.Count
            For c = 0 To vDetailDT.Columns.Count - 1
                DataArray(r, c) = vDetailDT.Rows(r - 1).Item(c)
            Next c
        Next r

        worksheet.Range(pRange1 + "1").Resize(r, c).Value = DataArray

        worksheet.Range(pRange1 + ":" + pRange2).EntireColumn.AutoFit()

        workbook.SaveAs(pFileName)

        workBookRelease(worksheet, workbook, workbooks, oExcel)

    End Sub

    Protected Sub CreateExcelTemplate(ByVal pFileName As String, ByVal vHeaderDT As DataTable, ByVal vHeaderDT2 As DataTable, ByVal pRange1 As String, ByVal pRange2 As String, ByVal pSheetName As String)
        '20130318 EMIL-----DYNAMIC TEMPLATE----------------------------------------------------------|
        'CREATES A TEMPLATE BASED ON DETAIL FIELDS---------------------------------------------------|

        Dim oExcel As New Excel.Application

        Dim workbooks As Excel.Workbooks
        Dim workbook As Excel._Workbook

        Dim worksheet As Excel._Worksheet

        'Dim misValue As Object = System.Reflection.Missing.Value

        oExcel.DisplayAlerts = False
        oExcel.AlertBeforeOverwriting = False
        If oExcel Is Nothing Then
            Environment.ExitCode = 0
            Exit Sub
        End If

        workbooks = oExcel.Workbooks
        workbook = workbooks.Open(pFileName)
        'workbook = workbooks.Add(misValue)

        worksheet = workbook.Sheets(1)
        worksheet.Name = pSheetName

        Dim DataArray(vHeaderDT.Rows.Count, vHeaderDT.Columns.Count) As Object
        Dim c As Integer = 1

        For Each dc As DataColumn In vHeaderDT2.Columns
            With worksheet.Cells(1, c)
                If dc.AllowDBNull Then
                    .Interior.Color = RGB(202, 202, 202)
                Else
                    .Interior.Color = RGB(255, 153, 204)
                End If
                .Font.Bold = True
                .Font.Size = 10
            End With
            c += 1
        Next

        For c = 0 To vHeaderDT.Columns.Count - 1
            DataArray(0, c) = vHeaderDT.Rows(0).Item(c)
        Next c

        worksheet.Range(pRange1 + "1").Resize(1, c).Value = DataArray

        'worksheet.Range(pRange1 + "1:" + IIf(pRange2 = "", "Z", pRange2) + "256").EntireColumn.AutoFit()
        workbook.Worksheets(1).Columns.Autofit()

        workbook.SaveAs(pFileName)

        workBookRelease(worksheet, workbook, workbooks, oExcel)

    End Sub

    Protected Sub AddToExcelTemplate(ByVal pFileName As String, ByVal vHeaderDT As DataTable, ByVal vDetailDT As DataTable, ByVal pRange1 As String, ByVal pRange2 As String, ByVal pSheetName As String)

        Dim oExcel As New Excel.Application

        Dim workbooks As Excel.Workbooks
        Dim workbook As Excel._Workbook
        'Dim sheets As Excel.Sheets
        Dim worksheet As Excel._Worksheet
        oExcel.DisplayAlerts = False
        oExcel.AlertBeforeOverwriting = False
        If oExcel Is Nothing Then
            Environment.ExitCode = 0
            Exit Sub
        End If

        workbooks = oExcel.Workbooks
        workbook = workbooks.Open(pFileName)

        'worksheet = workbook.Sheets(1)
        workbook.Sheets.Add(, workbook.Sheets(1))
        worksheet = workbook.Sheets(2)
        worksheet.Name = pSheetName

        Dim DataArray(vDetailDT.Rows.Count + vHeaderDT.Rows.Count, vDetailDT.Columns.Count + vHeaderDT.Columns.Count) As Object
        Dim r, c As Integer

        With worksheet.Range(pRange1 + "1:" + pRange2 + "1")
            .Interior.Color = RGB(222, 222, 222)
            .Font.Bold = True
            .Font.Size = 8
        End With

        With worksheet.Range(pRange1 + "1:" + pRange2 + CStr(vDetailDT.Rows.Count + vHeaderDT.Rows.Count))
            .Font.Size = 8
        End With

        For c = 0 To vDetailDT.Columns.Count - 1
            DataArray(0, c) = vHeaderDT.Rows(0).Item(c)
        Next c

        For r = vHeaderDT.Rows.Count To vDetailDT.Rows.Count
            For c = 0 To vDetailDT.Columns.Count - 1
                DataArray(r, c) = vDetailDT.Rows(r - 1).Item(c)
            Next c
        Next r

        worksheet.Range(pRange1 + "1").Resize(r, c).Value = DataArray

        worksheet.Range(pRange1 + ":" + pRange2).EntireColumn.AutoFit()

        worksheet = workbook.Sheets(1)

        worksheet.Activate()

        workbook.Save()

        workBookRelease(worksheet, workbook, workbooks, oExcel)

    End Sub

    Private Sub workBookRelease(ByRef ws As Excel._Worksheet, ByRef wk As Excel._Workbook, ByRef wks As Excel.Workbooks, ByRef exApp As Excel.Application)

        GC.Collect()
        GC.WaitForPendingFinalizers()
        wk.Close()


        releaseObject(ws)
        releaseObject(wk)
        releaseObject(wks)

        ws = Nothing
        wk = Nothing
        wks = Nothing

        exApp.Quit()
        releaseObject(exApp)
        exApp = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
        GC.WaitForPendingFinalizers()

    End Sub

    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(obj)
        Catch
            'Usually because the object is Nothing or has already been released...  Ignore.
        Finally
            If obj IsNot Nothing Then obj = Nothing
        End Try
    End Sub

    Public Overridable Sub ImportFiles(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyDialog As New OpenFileDialog()
        Dim tp As TabPage
        Dim tsb As ToolStripButton
        tsb = CType(sender, ToolStripButton)
        tp = CType(tsb.Owner.Parent, TabPage)
        Dim gsdg As GSDetailDataGridView
        gsdg = CType(tp.Controls(0), GSDetailDataGridView)
        Dim dt As DataTable
        dt = gsdg.DataSource

        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True

        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Me.Row("Name") = MyDialog.FileName.Split("\")(MyDialog.FileName.Split("\").Length - 1)
            TransferExcelDatas(MyDialog.FileName, dt, gsdg, tsb.Tag)
        End If
    End Sub

    Private Function GetSelectString(ByVal DetailMenuID As Integer) As String
        Dim s As String


        Dim c() As String = Nothing
        Dim x As Integer = 0
        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab =" & DetailMenuID.ToString & " AND ListColumn IS NOT NULL")
            ReDim Preserve c(x)
            c(x) = dr("ListColumn").ToString
            x += 1
        Next
        s = Join(c, ",")
        Return s
    End Function

    Public Overridable Sub TransferExcelDatas(ByVal FileName As String, ByVal myDT As DataTable, ByVal pDataGridView As DataGridView, ByVal pDetailMenuID As Integer)
        Dim dt As New DataTable

        Try
            pDataGridView.DataSource = Nothing
            Dim s As String
            s = IO.Path.GetFileName(FileName)

            s = GetSelectString(pDetailMenuID)
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", myDT, s)

            myDT.AcceptChanges()
            For Each dr As DataRow In myDT.Select()
                If dr.RowState = DataRowState.Unchanged Then
                    dr.SetAdded()
                End If
            Next
            pDataGridView.DataSource = myDT
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub
#End Region

    Private Sub mForm_GenerateXMLFileButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.GenerateXMLFileButtonClicked
        Dim sfd As New SaveFileDialog
        With sfd
            .DefaultExt = ".xml"
            .Filter = "Xml Files (*.xml)|*.xml"
            .CheckPathExists = True
            .FileName = Me.mMenuRow.Name & " " & Format(Row("ID"), "000000")
        End With
        If sfd.ShowDialog = DialogResult.OK Then
            mDataset.WriteXml(sfd.FileName)
        End If
    End Sub

    Private Sub mForm_ImportXMLFileButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles mForm.ImportXMLFileButtonClicked
        Dim sfd As New OpenFileDialog
        With sfd
            .DefaultExt = ".xml"
            .Filter = "Xml Files (*.xml)|*.xml"
            .CheckPathExists = True
            .FileName = Me.mMenuRow.Name '& " " & Format(Row("ID"), "000000")
        End With
        If sfd.ShowDialog = DialogResult.OK Then
            mDataset.Clear()
            mDataset.ReadXml(sfd.FileName)
            Me.Row = Table.Rows(0)

            'For Each dt As DataTable In Me.mDataset.Tables
            '    For Each drv As DataRowView In dt.DefaultView
            '        If drv.Row.RowState = DataRowState.Unchanged Then
            '            drv.Row.SetAdded()
            '        End If
            '    Next
            'Next
            'Me.Table.Columns("ID").ReadOnly = False
            'Me.Row("ID") = 0
            'Me.Table.Columns("ID").ReadOnly = True


        End If
    End Sub

    '*******************************************************************'
    ' KEVIN - SOFTCODE TEXTFILE 2013-05-15
    '*******************************************************************'
    Private Sub pGenerateTextFile(ByVal s As String, ByVal a As String, ByVal vConnection As System.Data.SqlClient.SqlConnection, Optional ByVal vCommandTimeOut As Integer = 3600)
        Dim fnum As Integer
        Dim MyDialog As New SaveFileDialog()
        Dim FileName As String

        MyDialog.Filter = "Text File  s (*.txt)|*.txt|All Files|*.*"
        'MyDialog.FileName = ExportFileName
        MyDialog.FileName = a.ToString
        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            fnum = FreeFile()
            FileName = MyDialog.FileName
            FileClose(fnum)
            FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)
            FileSystem.Print(fnum, Me.GetText(s, gConnection))
            FileClose(fnum)
        End If

    End Sub

    Public Function GetText(ByVal s As String, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim dt As New DataTable

        GSCOM.SQL.FillTable(dt, "EXEC " + s, gConnection)
        s = ""
        For Each dr As DataRow In dt.Rows
            s &= dr(0).ToString & vbCrLf
        Next
        s = s.Trim

        Return s
    End Function

    Public Function GetDefaultFileName(ByVal s As String, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim dt As New DataTable
        s = Me.PassParameters(s)
        Try
            s = GSCOM.SQL.ExecuteScalar("SELECT TOP 1 " & s, Connection).ToString()
        Catch ex As Exception
            s = ".txt"
        End Try
        Return s
    End Function

    'Private Sub DropDownClosed(sender As Object, e As EventArgs)
    '    Dim b As Binding = sender.DataBindings("SelectedValue")
    '    If b IsNot Nothing Then
    '        b.WriteValue()
    '        Me.Table.AcceptChanges()
    '        FieldEnabled()
    '    End If
    'End Sub

End Class