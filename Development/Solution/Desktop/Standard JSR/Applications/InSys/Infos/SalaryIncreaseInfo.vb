Option Explicit On
Option Strict On

Imports System.Collections.Generic



Friend Class SalaryIncreaseInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tSalaryIncrease(Connection)
    Private mtSalaryIncrease_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tSalaryIncrease_Detail(Connection)
    'Private mtSalaryIncreaseBankAcctNo As New Database.Tables.tSalaryIncreaseBankAcct(Connection)
    'Private mControl As New nDB.SalaryIncreaseControl
    Private mControl As New InSys.DataControl
    Private mAddEmployeeButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mImportButton As ToolStripButton
    'Private mComputeButton As ToolStripButton
    'Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView
    'Private mWeekDayTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name FROM vWeekDay_List", Connection)
    'Private mTVWeekDay As New WeekDaySelector
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            '.Add(mtSalaryIncrease_Detail)
            '.Add(mtSalaryIncreaseBankAcctNo)
        End With
        InitControl(pMenu)
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tSalaryIncrease.Field.ID)

        'cdc = mtSalaryIncrease_Detail.Columns(Database.Tables.tSalaryIncrease_Detail.Field.ID_SalaryIncrease)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mGenTemplateButton = MyBase.AddButton("Generate Template", gMainForm.imgList.Images("GenerateTemplate.png"), AddressOf GenTemplate)
        'mImportButton = Me.GetStripButton("Import File")
        myDT.Columns(Database.Tables.tSalaryIncrease.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        'mAddEmployeeButton = MyBase.AddButton("Add Employee", gMainForm.imgList.Images("misc.a.ico"), AddressOf AddEmployee)
        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        ' mComputeButton = MyBase.AddButton("Compute Salary", gMainForm.imgList.Images("Compute.png"), AddressOf Compute)

        ' mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("_ApplyFile.png"), AddressOf TransferSchedule)
        mtSalaryIncrease_Detail = DirectCast(Me.mDataset.Tables("tSalaryIncrease_Detail"), GSCOM.SQL.ZDataTable)
        ''mark
        'MyBase.AddButton("Check All", gMainForm.imgList.Images("misc.a.ico"), AddressOf CheckAll)
        'MyBase.AddButton("UnCheck All", gMainForm.imgList.Images("misc.a.ico"), AddressOf UnCheckAll)

        Me.ReloadAfterCommit = True
        'AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        'AddHandler mImportButton.Click, AddressOf ImportFile

        mGrid = Me.GetDataGridView(mtSalaryIncrease_Detail)
        AfterNew()
    End Sub

    Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        'Dim frm As New GSCOM.UI.GSForms.DataSelectForm
        'With frm.MainSelector
        '    .ImageList = gImageList
        '    .ImageKey = nDB.GetMenuValue(Database.Menu.HUMANRESOURCE_Employee, Database.Tables.tMenu.Field.ImageFile).ToString
        '    .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
        '    .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.MAINTENANCE_Company_Company, Database.Tables.tMenu.Field.ImageFile).ToString, "Company")
        '    .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.MAINTENANCE_Company_Branch, Database.Tables.tMenu.Field.ImageFile).ToString, "Branch")
        '    .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.MAINTENANCE_Company_Department, Database.Tables.tMenu.Field.ImageFile).ToString, "Department")
        '    .ImageKeys.Add(nDB.GetMenuValue(Database.Menu.MAINTENANCE_Company_Designation, Database.Tables.tMenu.Field.ImageFile).ToString, "Designation")
        '    .GroupCount = 3
        'End With


        'frm.Size = New Size(800, 600)
        'Dim s As String
        's = "(SELECT e.* FROM vEmployee_List e INNER JOIN fSessionEmployee(" & gUser & "," & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & ") se ON e.ID=se.ID) a"
        'frm.Init(s, Connection)
        'frm.CheckNodes(mtSalaryIncrease_Detail, "ID_Employee")
        'If frm.ShowDialog() = DialogResult.OK Then
        '    Dim dr As DataRow
        '    For Each i As Integer In frm.GetSelectedIDs
        '        If mtSalaryIncrease_Detail.Select("ID_Employee=" & i).Length = 0 Then
        '            dr = mtSalaryIncrease_Detail.NewRow
        '            dr.Item("ID_Employee") = i
        '            mtSalaryIncrease_Detail.Rows.Add(dr)
        '        End If
        '    Next
        'Me.mImportButton.Enabled = False
        '    Me.SaveButton.PerformClick()
        'End If

    End Sub
    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tSalaryIncrease.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "SalaryAdjustment.xls", sfd.FileName, True)

            a.DataSource = sfd.FileName
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable

            'Dim s As String
            's &= "SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ")"

            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & ", " & CInt(nDB.GetCompanyID) & ") where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")
            'UseArray(sfd.FileName, dt)



            MsgBox("Done", MsgBoxStyle.Information)

            Me.GetStripButton("Compute").Enabled = False

        End If
    End Sub
#Region "ImportFile"
    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        ofd.FilterIndex = 0
        ofd.CheckFileExists = True
        ofd.CheckPathExists = True
        If (ofd.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(ofd.FileName)
            ' Me.mImportButton.Enabled = False

            'Me.SaveButton.PerformClick()
            'GSCOM.SQL.ExecuteNonQuery("EXEC pSalaryIncrease_ImportPreviousMonthlyRate " & myDT.Get(Database.Tables.tSalaryIncrease.Field.ID).ToString, Connection)
            'Me.SaveButton.PerformClick()
            ' Me.mAddEmployeeButton.Enabled = False
            'Me.mComputeButton.Enabled = False
        End If
    End Sub

#End Region


#Region "TransferExcelData"
    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim dtemp As DataTable
        Dim nr As DataRow
        Dim r1 As DataRow()
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            dt = SQL.GetExcelTable(FileName, "Sheet1")
            'dtemp = GSCOM.SQL.TableQuery("Select ID, Code")

            Dim str As String = String.Format("Select * from vemployee where Code in ({0})", String.Join(", ", Me.PrepareArrayListFormDT(dt, "EmployeeCode").ToArray))
            dtemp = GSCOM.SQL.TableQuery(str, gConnection)

            mtSalaryIncrease_Detail.Rows.Clear()
            Dim total As Decimal = 0
            For Each dr As DataRow In dt.Rows
                nr = mtSalaryIncrease_Detail.NewRow()

                r1 = dtemp.Select("Code = " & GSCOM.SQL.SQLFormat(dr("EmployeeCode")))
                If r1.Length > 0 Then
                    nr(Database.Tables.tSalaryIncrease_Detail.Field.ID_Employee.ToString) = r1(0).Item(Database.Tables.tEmployee.Field.ID)
                    nr(Database.Tables.tSalaryIncrease_Detail.Field.PreviousMonthlyRate.ToString) = r1(0).Item(Database.Tables.tEmployee.Field.MonthlyRate)
                    'nr(Database.Tables.tSalaryIncrease_Detail.Field.PreviousMonthlyRate.ToString) = r1(0).Item(Database.Tables.tEmployee.Field.emplo)
                    'nr("EmployeeCode") = dr.Item("EmployeeCode")
                    'GSCOM.SQL.ExecuteNonQuery("UPDATE sid SET fromTemplate = 1 FROM dbo.tSalaryIncrease_Detail sid WHERE EmployeeCode = " & r1(0).Item(Database.Tables.tEmployee.Field.Code).ToString, Connection)
                    nr(Database.Tables.tSalaryIncrease_Detail.Field.fromTemplate.ToString) = 1
                Else
                    Continue For
                End If

                Dim s As String
                s = dr("NewMonthlyRate").ToString
                If s = "" Then
                    Continue For
                Else
                    If CDec(s) = 0 Then
                        Continue For
                    End If
                End If

                nr(Database.Tables.tSalaryIncrease_Detail.Field.NewMonthlyRate.ToString) = dr("NewMonthlyRate")
                mtSalaryIncrease_Detail.Rows.Add(nr)
                'total += CDec(dr("NewMonthlyRate"))
            Next
            '   ComputeTotal()
            mtSalaryIncrease_Detail.AcceptChanges()
            For Each dr As DataRow In mtSalaryIncrease_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtSalaryIncrease_Detail
            mGrid.Refresh()
            Me.SaveButton.PerformClick()

            'Dim dg As DataGridView
            'dg = Me.GetDataGridView(mtSalaryIncrease_Detail)
            'dg.Refresh()

            'For Each d As DataGridViewRow In dg.Rows
            '    dg.Update()
            'Next

            Me.EndProcess("Finish downloading file [" & FileName & "].")
        Catch ex As OleDb.OleDbException
            Me.EndProcess("Error occur while importing data. This is due to file connection, please check if sheet name is Sheet1.", False)
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Function PrepareArrayListFormDT(dt As DataTable, columnname As String) As List(Of String)

        Dim ddf As New List(Of String)

        For Each h As Object In From j As DataRow In dt.AsEnumerable() Select j(columnname)
            ddf.Add(GSCOM.SQL.SQLFormat(h.ToString))
        Next

        Return ddf
    End Function


#End Region

    Private Sub Compute(ByVal sender As Object, ByVal e As EventArgs)
        Me.SaveButton.PerformClick()
        GSCOM.SQL.ExecuteNonQuery("EXEC pSalaryIncrease_Compute " & myDT.Get(Database.Tables.tSalaryIncrease.Field.ID).ToString, Connection)
        Me.SaveButton.PerformClick()

    End Sub

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)

        Dim aj As Integer
        aj = CInt(myDT.Get(Database.Tables.tSalaryIncrease.Field.ID_FilingStatus).ToString)
        If aj <> 2 Then
            MsgBox("Please Approved first before applying the file", MsgBoxStyle.Information)
        Else
            If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                GSCOM.SQL.ExecuteNonQuery("EXEC pSalaryIncrease_Apply " & myDT.Get(Database.Tables.tSalaryIncrease.Field.ID).ToString, Connection)
                'Me.mApplyButton.Enabled = False
                'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
                MsgBox("Finished applying the file.", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        '  mAddEmployeeButton.Enabled = (pID <> 0) And Me.SaveButton.Enabled
        ' mComputeButton.Enabled = (pID <> 0) And Me.SaveButton.Enabled
        'If CInt(myDT.Get(Database.Tables.tSalaryIncrease.Field.ID_FilingStatus)) = 2 Then
        '    mApplyButton.Enabled = (pID <> 0) And Me.SaveButton.Enabled
        'Else
        '    mApplyButton.Enabled = (pID <> 0) And Me.SaveButton.Enabled = False
        'End If
        'mApplyButton.Enabled = (pID <> 0) And Me.SaveButton.Enabled

        MyBase.LoadInfo(pID)


        Dim d As String = "SELECT isApplied FROM dbo.tSalaryIncrease WHERE ID = " & pID.ToString
        If CBool(GSCOM.SQL.ExecuteScalar(d, gConnection)) = True Then
            MyBase.SaveButton.Enabled = False
            mGenTemplateButton.Enabled = False
            mImportButton.Enabled = False
        Else
            MyBase.SaveButton.Enabled = True
            mGenTemplateButton.Enabled = True
            mImportButton.Enabled = True
        End If

        'mImportButton.Enabled = (mtSalaryIncrease_Detail.Rows.Count = 0) And (pID <> 0)
        'mTVWeekDay.CheckNodes(mtSalaryIncreaseRestDay, Database.Tables.tSalaryIncreaseRestDay.Field.ID_WeekDay.ToString)
    End Sub

    Protected Overrides Function CanSave() As Boolean
        'mTVWeekDay.EndEdit(mtSalaryIncreaseRestDay, Database.Tables.tSalaryIncreaseRestDay.Field.ID_WeekDay.ToString)
        Return MyBase.CanSave()
    End Function

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tSalaryIncrease)
        End Set
    End Property

#End Region
    'Public Sub CheckAll(ByVal sender As Object, ByVal e As EventArgs)
    '    'CheckOrUnCheckAll(True)
    'End Sub
    'Public Sub UnCheckAll(ByVal sender As Object, ByVal e As EventArgs)
    '    'CheckOrUnCheckAll(False)
    'End Sub
    'Public Sub CheckOrUnCheckAll(ByVal pCheck As Boolean)
    '    Dim dra As DataRow()
    '    Dim i As Integer
    '    Me.BeginProcess("Processing")
    '    dra = mtSalaryIncrease_Detail.Select()

    '    mGrid.DataSource = Nothing
    '    For Each dr As DataRow In dra
    '        i += 1
    '        Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
    '        dr.Item(Database.Tables.tTrainingActivityEmployee.Field.Attended.ToString) = pCheck
    '        Application.DoEvents()
    '    Next
    '    mGrid.DataSource = myDT_TrainingActivityEmployee
    '    Me.EndProcess("Done " & IIf(pCheck, "checking", "unchecking").ToString)
    'End Sub

End Class
