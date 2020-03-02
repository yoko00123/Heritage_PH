Option Explicit On
Option Strict On



Friend Class ScheduleAssignmentInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tScheduleAssignment(Connection)
    Private mtScheduleAssignment_Detail As DataTable  'New Database.Tables.tScheduleAssignment_Detail(Connection)
    Private mControl As New InSys.DataControl  'Private mControl As New nDB.ScheduleAssignmentControl
    Private mAddEmployeeButton As ToolStripButton
    'Private mSaveButton As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtScheduleAssignment_Detail)
        End With
        InitControl(pMenu)
        mAddEmployeeButton = MyBase.AddButton("Add Employee", gMainForm.imgList.Images("misc.a.ico"), AddressOf AddEmployee)
        'Dim mApplyButton As ToolStripButton = Me.GetStripButton("Apply File")

        ' AddHandler mApplyButton.Click, AddressOf ApplyFile
        Me.ReloadAfterCommit = True
        AfterNew()
        Dim dg As DataGridView
        mtScheduleAssignment_Detail = Me.mDataset.Tables("tScheduleAssignment_Detail")
        dg = Me.GetDataGridView(mtScheduleAssignment_Detail)
        dg.MultiSelect = True
        dg.AllowUserToAddRows = False
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        Dim b As Boolean
        b = (pID <> 0)
        ' mAddEmployeeButton.Enabled = b And Me.SaveButton.Enabled
        'mApplyButton.Enabled = b
        MyBase.LoadInfo(pID)
    End Sub


    Private Sub EnforceColumns(ByVal ss As DataTable)
        Dim dv As DataView
        Dim s As String

        dv = nDB.MenuSet.tMenuDetailTabField.DefaultView
        dv.RowFilter = "[ID_MenuDetailTab]=92 AND [CopyFromList]=1 AND [Name]<>'ID'"
        For Each drv As DataRowView In dv
            s = drv("ListColumn").ToString
            If s = "" Then
                s = drv("Name").ToString
            End If

            If ss.Select("Name IN (" & GSCOM.SQL.SQLFormat(s) & "," & GSCOM.SQL.SQLFormat("ID_" & s) & ")").Length = 0 Then
                Dim dr As DataRow = ss.NewRow
                With dr
                    .Item("Name") = s
                    .Item("EffectiveLabel") = drv("EffectiveLabel")
                    .Item("MenuTabSeqNo") = Integer.MaxValue 'needed for sorting
                    ss.Rows.Add(dr)
                End With
            End If
        Next
    End Sub

#Region "AddEmployee"
    Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        Dim frm As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, , True)

        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Size = New Size(800, 600)
        'frm.SelectionMode = True
        frm.ShowDialog()

        Dim a As Integer()
        a = frm.CheckedRowID

        If a IsNot Nothing Then
            Dim dr As DataRow
            For Each i As Integer In a
                If mtScheduleAssignment_Detail.Select("ID_Employee=" & i).Length = 0 Then
                    dr = mtScheduleAssignment_Detail.NewRow
                    dr.Item("ID_Employee") = i
                    mtScheduleAssignment_Detail.Rows.Add(dr)
                End If
            Next
        End If

        Me.SaveButton.PerformClick()
    End Sub
    'Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim frm As New DataSelectForm

    '    With frm.MainSelector
    '        .ImageList = gImageList
    '        .ImageKey = nDB.GetMenuValue(Database.Menu.HumanResource_Employee, Database.Tables.tMenu.Field.ImageFile).ToString
    '        .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString

    '        With .mGrid
    '            .mInfoMenuSet = mInfoMenuSet
    '            .mMenuDetailTabRow = New Database.MenuDetailTabRow(mInfoMenuSet.tMenuDetailTab.Select("TableName=" & GSCOM.SQL.SQLFormat(mtScheduleAssignment_Detail.TableName))(0))
    '            '   .DrawMode = TreeViewDrawMode.Normal
    '            .CheckBoxes = True
    '            .TrueDataSource = mtScheduleAssignment_Detail
    '            ' .ListSource = CType(.DataSource, DataTable) ' "select * from vemployee"
    '            '.Groups.Add(dr("Name").ToString, dr("Text").ToString, dr("Sort").ToString, dr("ImageFile").ToString)
    '            '.Groups.Add("Department", "Department", "", "_department.png")
    '            .Groups.Add("ID_Employee", "Employee", "", "_employee.png")
    '            .Columns.Add("Department", 200, HorizontalAlignment.Left)
    '            .Columns.Add("Designation", 200, HorizontalAlignment.Left)

    '        End With

    '        '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Company, Database.Tables.tMenu.Field.ImageFile).ToString, "Company")
    '        '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Branch, Database.Tables.tMenu.Field.ImageFile).ToString, "Branch")
    '        '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Department, Database.Tables.tMenu.Field.ImageFile).ToString, "Department")
    '        '.ImageKeys.Add(nDB.GetMenuValue(Database.Menu.Maintenance_Company_Designation, Database.Tables.tMenu.Field.ImageFile).ToString, "Designation")
    '        '.GroupCount = 0
    '    End With
    '    frm.CheckedTable = mtScheduleAssignment_Detail
    '    '   frm.ColumnName = "ID_Employee"
    '    'frm.Size = Me.Size
    '    Dim ss As DataTable = Nothing
    '    ss = nDB.GetDisplayColumnsTable(9)
    '    Dim ds As New DataSet
    '    ds.Tables.Add(ss)
    '    ds.EnforceConstraints = False
    '    EnforceColumns(ss)


    '    Dim VRestoreIDs As New Collections.Specialized.StringCollection
    '    frm.MainSelector.mGrid.mDataIDs.Clear()
    '    For Each dr2 As DataRowView In frm.MainSelector.mGrid.TrueDataSource.DefaultView
    '        frm.MainSelector.mGrid.mDataIDs.Add(dr2("ID_Employee").ToString)
    '        VRestoreIDs.Add(dr2("ID_Employee").ToString)
    '    Next

    '    'Dim dt As DataTable
    '    'dt = GSCOM.SQL.SelectIntoDataTable("", mtScheduleAssignment_Detail)




    '    frm.Init(nDB.GetMenuDataSourceValue(Database.Menu.HumanResource_Employee), Connection, ss, "")
    '    Dim r As DialogResult
    '    Dim dg As DataGridView
    '    dg = Me.GetDataGridView(mtScheduleAssignment_Detail)
    '    If dg IsNot Nothing Then dg.DataSource = Nothing
    '    frm.Size = Me.Size
    '    r = frm.ShowDialog()
    '    dg.DataSource = mtScheduleAssignment_Detail
    '    If r = DialogResult.OK Then
    '        frm.MainSelector.mGrid.RetainChecked()
    '    Else
    '        frm.MainSelector.mGrid.CleanUp(VRestoreIDs, "ID_Employee")
    '        ' mtScheduleAssignment_Detail = dt

    '        'Dim dr As DataRow
    '        'For Each i As Integer In frm.GetSelectedIDs
    '        '    If mtScheduleAssignment_Detail.Select("ID_Employee=" & i).Length = 0 Then
    '        '        dr = mtScheduleAssignment_Detail.NewRow
    '        '        dr.Item("ID_Employee") = i
    '        '        mtScheduleAssignment_Detail.Rows.Add(dr)
    '        '    End If
    '        'Next
    '        'Me.SaveButton.PerformClick()
    '    End If
    'End Sub

    'Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
    '       GSCOM.SQL.ExecuteNonQuery("EXEC pScheduleAssignment " & myDT.Get(Database.Tables.tScheduleAssignment.Field.ID).ToString, Connection)
    ' End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tScheduleAssignment)
        End Set
    End Property

#End Region

End Class
