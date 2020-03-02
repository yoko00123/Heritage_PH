Option Explicit On
Option Strict Off
Imports System.IO
Imports System.Net.Mail



Friend Class PayrollPeriodInfo
    Inherits InfoSet

#Region "Declarations"
    Private myDT As New Database.Tables.tPayrollPeriod(Connection)
    Private mtPayroll As New Database.Tables.tPayroll(Connection)
    Private mtUserPayrollPeriod As New Database.Tables.tUserPayrollPeriod(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.PayrollPeriodControl
    Private mGrid As GSDetailDataGridView
    Private mGeneratePayrollButton As ToolStripButton
    Private mPostButton As ToolStripButton
    Private mCheckAllButton As ToolStripButton
    Private mUnCheckAllButton As ToolStripButton
    Private mAnnualizeButton As ToolStripButton
    Private mSendEPayslip As ToolStripButton
    Private mReset As ToolStripButton
    Private mAddEmployeeButton As ToolStripButton
    Private go As Boolean
    ' Private dgv As GSDetailDataGridView
    Private dgv2 As GSDetailDataGridView
    'Private WithEvents MainButton As System.Windows.Forms.Button

#End Region

#Region "Constructors"
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtPayroll)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tPayrollPeriod.Field.ID)
        cdc = mtPayroll.Columns(Database.Tables.tPayroll.Field.ID_PayrollPeriod)
        rel = mDataset.Relations.Add(pdc, cdc)
        'cdc = mtUserPayrollPeriod.Columns(Database.Tables.tUserPayrollPeriod.Field.ID_PayrollPeriod)
        'rel = mDataset.Relations.Add(pdc, cdc)
        'NOTE: CUSTOMIZED
        myDT.Columns(Database.Tables.tPayrollPeriod.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        myDT.Columns(Database.Tables.tPayrollPeriod.Field.ID_PayrollPeriodType).DefaultValue = 1 'BASIC PAY
        myDT.Columns(Database.Tables.tPayrollPeriod.Field.Year).DefaultValue = Now.Year

        mtUserPayrollPeriod.Columns(Database.Tables.tUserPayrollPeriod.Field.ID_User).DefaultValue = nDB.GetUserID


        Me.ReloadAfterCommit = True

        'mtPayroll.DefaultView.RowFilter = "NetAmt > 0"

        mAddEmployeeButton = Me.GetStripButton("Add Employee") 'HOMER 20130924
        AddHandler mAddEmployeeButton.Click, AddressOf AddEmployee


        'mGeneratePayrollButton = MyBase.AddButton("Generate Payroll", gMainForm.imgList.Images("misc.a.ico"), AddressOf GeneratePayroll)
        mGeneratePayrollButton = Me.GetStripButton("Generate Payroll")
        AddHandler mGeneratePayrollButton.Click, AddressOf GeneratePayroll

        mPostButton = Me.GetStripButton("Post") 'MyBase.AddButton("Post", gMainForm.imgList.Images("Checked.png"), AddressOf Post)
        ' AddHandler mPostButton.Click, AddressOf Post


        mCheckAllButton = Me.GetStripButton("Check All") 'MyBase.AddButton("Check All", gMainForm.imgList.Images("CheckedTrue.bmp"), AddressOf CheckAll)
        AddHandler mCheckAllButton.Click, AddressOf CheckAll

        mUnCheckAllButton = Me.GetStripButton("UnCheck All") 'MyBase.AddButton("UnCheck All", gMainForm.imgList.Images("CheckedFalse.bmp"), AddressOf UnCheckAll)
        AddHandler mUnCheckAllButton.Click, AddressOf UnCheckAll


        mAnnualizeButton = Me.GetStripButton("Annualize") 'MyBase.AddButton("Annualize", gMainForm.imgList.Images("Annualize.png"), AddressOf Annualize)
        AddHandler mAnnualizeButton.Click, AddressOf Annualize

        ' mReset = Me.GetStripButton("Reset Payroll")



        mSendEPayslip = Me.GetStripButton("Send e-Payslips")
        AddHandler mSendEPayslip.Click, AddressOf SendePayslips

        'dgv = Me.AddGrid("Positive Payroll")

        dgv2 = Me.AddGrid("Negative Payroll")

        'mReset = Me.AddButton("asda", _report.png, AddressOf SendePayslips)
        'mGrid = Me.AddGrid("Changes from previous payroll")

        'With mGrid
        '    .AutoGenerateColumns = True
        '    .ReadOnly = True
        '    .AllowUserToAddRows = False
        '    .AllowUserToDeleteRows = False
        'End With
        'AddHandler Me.GetStripButton("Send e-Payslips").Click, AddressOf SendePayslips
        Me.NoGridTables = mtUserPayrollPeriod.TableName

        AfterNew()

        Me.ReArrangeTab(False)

        ' dgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,Employee,TaxableAmt,GrossAmt,DeductionAmt,NetAmt,ForComputation FROM vPositivePayroll WHERE ID_PayrollPeriod=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)), gConnection)
        dgv2.DataSource = GSCOM.SQL.TableQuery("SELECT ID,Employee,TaxableAmt,GrossAmt,DeductionAmt,NetAmt,ForAnnualization [For Payroll] FROM vNegativePayroll WHERE ID_PayrollPeriod=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)), gConnection)
        '[•]

        With CType(MyBase.GetDataGridView(mtPayroll), GSDetailDataGridView)
            '.ReadOnly = True
            .AllowUserToAddRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns.Item(0).ReadOnly = True
            .Columns.Item(1).ReadOnly = True
            .Columns.Item(2).ReadOnly = True
            .Columns.Item(3).ReadOnly = True
            .Columns.Item(4).ReadOnly = True












            .Columns.Item(5).ReadOnly = True
            .Columns.Item(6).ReadOnly = False
            .Columns.Item(6).HeaderText = "For Payroll"
            AddHandler .ItemActivate, AddressOf dgv_ItemActivate

        End With

        'With dgv
        '    .AutoGenerateColumns = True
        '    .AllowUserToAddRows = False
        '    .AllowUserToDeleteRows = False
        '    '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    '.Columns.Item(6).HeaderText = "[•]"
        '    '.Columns.Item(6).ReadOnly = False
        '    AddHandler .ItemActivate, AddressOf dgv_ItemActivate
        'End With

        With dgv2

            .AutoGenerateColumns = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '.Columns.Item(6).HeaderText = "[•]"
            '.Columns.Item(6).ReadOnly = False
            .ReadOnly = True
            AddHandler .ItemActivate, AddressOf dgv_ItemActivate
        End With


    End Sub

#End Region

    Private Sub dgv_ItemActivate(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ItemActivateEventArgs)
        ShowPayrollInfo(CInt(CType(sender, DataGridView).Rows(e.RowIndex).Cells("ID").Value))
    End Sub

    Public Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String
        Dim a As Boolean
        s = "(ID_Company = " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_Company).ToString & ")"
        s &= " AND (ID_PayrollFrequency = " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollFrequency).ToString & ")" '" OR " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollFrequency).ToString & " IS NULL) "
        If a = True Then
            s &= " AND IsActive=1"
        Else
            s &= " AND IsActive=1"
        End If
        s &= " AND ID NOT IN (SELECT ID_Employee FROM tPayroll WHERE ID_PayrollPeriod =" & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString & ")"
        s &= " AND StartDate <= '" & myDT.Get(Database.Tables.tPayrollPeriod.Field.PayDate).ToString & "'"
        If Not IsDBNull(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollClassifi)) Then
            s &= " And ID_PayrollClassifi = " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollClassifi).ToString
        End If
        's &= " AND	 ID_PayrollStatus NOT IN (4,5)"
        Dim f As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, s, True)
        If f.ShowDialog() = DialogResult.OK And f.GetTable IsNot Nothing Then
            Application.DoEvents()

            For Each dr As DataRow In f.GetTable.Select()
                GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_NewEmployee " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString & ", " & gUser & ", " & dr("ID").ToString, Connection)
            Next
        Else
            Exit Sub
        End If

        LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
    End Sub
#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        Dim s As String
        s = "ID_PayrollPeriod=" & pID.ToString
        Dim dt1 As DataTable
        Dim dra1 As DataRow()
        'Dim pc As String
        MyBase.LoadInfo(pID)
        dt1 = GSCOM.SQL.TableQuery("SELECT * FROM tPayrollPeriod Where ID = " & pID, gConnection, True)
        dra1 = dt1.Select()
        'For Each dr As DataRow In dra1
        '    If Not IsDBNull(dr.Item(Database.Tables.tPayroll.Field.ID_Branch.ToString)) Then
        '        s &= " AND ID_Branch=" & dr.Item(Database.Tables.tPayroll.Field.ID_Branch.ToString).ToString
        '    End If
        '    If Not IsDBNull(dr.Item(Database.Tables.tPayroll.Field.ID_Department.ToString)) Then
        '        s &= " AND ID_Department=" & dr.Item(Database.Tables.tPayroll.Field.ID_Department.ToString).ToString
        '    End If
        'Next
        'If pID > 0 Then
        '    pc = GSCOM.SQL.ExecuteScalar(("SELECT ID_PayrollClassifi FROM tPayrollPeriod WHERE ID = " & pID.ToString), gConnection).ToString
        '    If Not pc = "" Then
        '        s &= " AND ID_PayrollClassifi = " & pc & " AND ID_PayrollClassifi IS NOT NULL"
        '    End If
        'End If
        s &= " AND "
        's &= "ID_Employee IN (SELECT ID FROM " & nDB.GetMenuDataSourceValue(Database.Menu.HumanResource_Employee) & ")"
        s &= "ID_Employee IN (SELECT ID FROM dbo.fSessionEmployee(" & nDB.GetUserID.ToString & ", " & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & ")" & ")"
        mtPayroll.ClearThenFill(s)
        s = "ID_PayrollPeriod=" & pID.ToString
        s &= " AND "
        s &= "ID_User=" & nDB.GetUserID.ToString
        mtUserPayrollPeriod.ClearThenFill(s)

        'mGeneratePayrollButton.Enabled = myDT.Rows(0).RowState <> DataRowState.Added        'CUSTOMIZED:'NOTE: LoadInfo is only called after commiting if ReloadAfterCommit is true

        Dim dra As DataRow()
        Dim p As Boolean
        dra = mtUserPayrollPeriod.Select("ID_User=" & nDB.GetUserID)
        p = dra.Length > 0
        mPostButton.Enabled = (Not p) AndAlso (mtPayroll.Rows.Count > 0)
        mGeneratePayrollButton.Enabled = (Not p)
        ' mReset.Enabled = (Not p)
        mControl.Enabled = (Not p)
        Me.SaveButton.Enabled = mControl.Enabled

        'Me.GetStripButton("Generate Payroll").Enabled = mControl.Enabled

        ' mGrid.DataSource = GSCOM.SQL.TableQuery("EXEC pPayrollPeriodEmployeeChanges " & pID.ToString, Connection)

        'dgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,Employee,TaxableAmt,GrossAmt,DeductionAmt,NetAmt,ForComputation FROM vPositivePayroll WHERE ID_PayrollPeriod=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)), gConnection)
        dgv2.DataSource = GSCOM.SQL.TableQuery("SELECT ID,Employee,TaxableAmt,GrossAmt,DeductionAmt,NetAmt,ForAnnualization [For Payroll] FROM vNegativePayroll WHERE ID_PayrollPeriod=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)), gConnection) '& If(IsDBNull(pc), "", " AND ID_PayrollClassifi = " & pc), gConnection)
        ''[•]
        'If dgv.Columns.Count > 0 Then
        '    dgv.Columns.Item(6).HeaderText = "[•]"
        '    dgv.Columns.Item(6).ReadOnly = False
        'End If

        Dim d As String = "SELECT IsNull(dbo.fGetCompanySetting('EnableAddEmployeeButton'," & nDB.GetCompanyID & "),0)"
        If GSCOM.SQL.ExecuteScalar(d, gConnection) = 0 Then
            mAddEmployeeButton.Visible = False
        End If
        Dim e As String = "SELECT IsNull(dbo.fGetSetting('EPayslip'),0)"
        If GSCOM.SQL.ExecuteScalar(e, gConnection) = 0 Then
            mSendEPayslip.Visible = False
        End If

    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tPayrollPeriod)
        End Set
    End Property



#End Region

#Region "Customized"
    Private Sub GeneratePayroll(ByVal sender As Object, ByVal e As EventArgs)
        Dim id As Integer
        id = CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString)

        If MsgBox("Generate Payroll?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            mForm.SaveButton.PerformClick()
            BeginProcess("Generating payroll details... Please wait.")
            Dim d As String = "SELECT IsNull(dbo.fGetCompanySetting('EnableAddEmployeeButton'," & nDB.GetCompanyID & "),0)"
            If GSCOM.SQL.ExecuteScalar(d, gConnection) = 0 Then
                GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_New " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString & ", " & gUser, Connection)
            End If
            Application.DoEvents()

            Dim s As String
            Dim dt As DataTable
            Dim dra As DataRow()
            Dim i As Integer
            s = "Select * from vPayroll Where ID_PayrollPeriod = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID))
            s &= " AND (IsProcessed = 0 OR ForAnnualization = 1)"
            If Not GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollClassifi)).ToString = "NULL" Then
                s &= " AND ID_PayrollClassifi = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID_PayrollClassifi))
            End If
            dt = GSCOM.SQL.TableQuery(s, gConnection, True)
            dra = dt.Select
            For Each dr As DataRow In dra
                i += 1
                Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
                s = dr.Item(Database.Tables.tPayroll.Field.ID.ToString).ToString
                If dr.Item(Database.Tables.tPayroll.Field.IsProcessed.ToString).ToString = True Then
                    s = "EXEC pPayroll_RecomputePerEmp " & s & "," & gUser
                Else
                    s = "EXEC pPayroll_NewPerEmp " & s & "," & gUser
                End If
                GSCOM.SQL.ExecuteNonQuery(s, gConnection)
                GSCOM.SQL.ExecuteNonQuery("Update tPayroll SET ForAnnualization = 0 WHERE ID = " & dr.Item("ID").ToString, Connection)
                Try
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Application.DoEvents()
            Next
            LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
            Application.DoEvents()
            EndProcess("")
            MsgBox("Finish generating payroll.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ShowPayrollInfo(ByVal pID As Integer)
        Dim oPayroll As InfoSet
        oPayroll = GetInfoSet(Database.Menu.INSYSPAYROLL_PayrollProcessing_Payroll)
        If oPayroll Is Nothing Then
            oPayroll = New PayrollInfo(Database.Menu.INSYSPAYROLL_PayrollProcessing_Payroll, Connection, mtPayroll, pID)
            AddInfoSet(oPayroll, Database.Menu.INSYSPAYROLL_PayrollProcessing_Payroll)
            'ROBBIE 20070517 ---------------\
            If Not CBool(GSCOM.Applications.InSys.nDB.GetMenuValue(Database.Menu.INSYSPAYROLL_PayrollProcessing_Payroll, Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)) Then
                oPayroll.MakeReadOnly()
            End If
            'ROBBIE 20070517 ---------------/
            'oPayroll.AllowNew = CBool(Database.MenuTable.Select("ID=" & GSCOM.SQL.SQLFormat(CInt(Database.Menu.Payroll_Payroll)))(0).Item("AllowNew"))
        Else
            oPayroll.LoadInfo(pID)
        End If
        Application.DoEvents()


        oPayroll.SaveButton.Enabled = mControl.Enabled

        oPayroll.Size = Me.Size
        oPayroll.ShowDialog()

        LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
    End Sub

#End Region

#Region "Annualize"


    Private Sub CheckAll(ByVal sender As Object, ByVal e As EventArgs)
        CheckOrUnCheckAll(True)
    End Sub

    Private Sub UnCheckAll(ByVal sender As Object, ByVal e As EventArgs)
        CheckOrUnCheckAll(False)
    End Sub
    Public Sub CheckOrUnCheckAll(ByVal pCheck As Boolean)
        Dim dra As DataRow()
        Dim i As Integer
        Me.BeginProcess("Processing")
        dra = mtPayroll.Select()
        '  mGrid.DataSource = Nothing
        For Each dr As DataRow In dra
            i += 1
            Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
            dr.Item(Database.Tables.tPayroll.Field.ForAnnualization.ToString) = pCheck
            Application.DoEvents()
        Next
        ' mGrid.DataSource = mtPayroll
        Me.EndProcess("Done " & IIf(pCheck, "checking", "unchecking").ToString)
    End Sub

    Public Sub Annualize(ByVal sender As Object, ByVal e As EventArgs)


        Dim s As String
        Dim dt As DataTable
        Dim dra As DataRow()
        Dim i As Integer
        s = "Select * from tPayroll Where ID_PayrollPeriod = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID))
        s &= " AND ForAnnualization = 1 AND IsAnnualize = 0"
        dt = GSCOM.SQL.TableQuery(s, gConnection, True)
        dra = dt.Select
        For Each dr As DataRow In dra
            i += 1
            Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
            s = dr.Item(Database.Tables.tPayroll.Field.ID_Employee.ToString).ToString
            s = "EXEC pPayroll_AnnualizationPerEmp " & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString & "," & s
            GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            Try
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Application.DoEvents()
        Next
        LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
        Application.DoEvents()
        EndProcess("")
        MsgBox("Annualization completed.", MsgBoxStyle.Information)

    End Sub
#End Region


#Region "E-Payslip"
    Public Sub SendePayslips(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Me.GetStripButton("Send e-Payslips") IsNot Nothing Then
                Me.GetStripButton("Send e-Payslips").Text = "Stop"
                go = True
            Else
                Try
                    Me.GetStripButton("Stop").Text = "Send e-Payslips"
                    go = False
                    Exit Sub
                Catch ex As Exception
                    go = False
                    Exit Sub
                End Try
            End If
            Dim cnt As Integer = 0
            'Me.BeginProcess("Sending E-Payslips...")
            'Dim dtEarnings As DataTable
            'Dim dtDeductions As DataTable
            'Dim dtPayslip As DataTable
            Dim dtPersonaEmail As DataTable

            Dim pIDTemp As String = mtPayroll.Rows(0).Item(Database.Tables.tPayroll.Field.ID_PayrollPeriod).ToString
            Dim a As New MessageBoxForm
            Dim f As New BrowserDataListForm(Database.Menu.INSYSPEOPLE_EmployeeRecords201File, False, GetFixedFilter, True)
            Dim s As String
            s = "ID_PayrollPeriod=" & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString
            s &= " AND "
            's &= "ID_Employee IN (SELECT ID FROM " & nDB.GetMenuDataSourceValue(Database.Menu.HumanResource_Employee) & ")"
            s &= "ID_Employee IN (SELECT ID FROM dbo.fSessionEmployee(" & nDB.GetUserID.ToString & ", " & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & ")" & ")"
            If CBool(myDT.Get(Database.Tables.tPayrollPeriod.Field.IsPaused)) = True Then
                a.isPaused = True
            Else
                a.isPaused = False
            End If
            a.ShowDialog()
            Select Case a.Result
                Case MessageBoxForm.MgsResult.Specific
                    If f.ShowDialog() = DialogResult.OK And f.GetTable IsNot Nothing Then
                        s &= " AND ID_Employee IN ("
                        For Each dr As DataRow In f.GetTable.Select()
                            s &= dr("ID").ToString & ","
                        Next
                        s = Left(s, s.Length - 1) & ") "
                    Else
                        Exit Sub
                    End If
                Case MessageBoxForm.MgsResult.Resume
                    Dim sqlTemp As String = "SELECT * FROM dbo.fPayslip(" & pIDTemp & "," & nDB.GetUserID & ")" &
                    " WHERE ID>=" & myDT.Get(Database.Tables.tPayrollPeriod.Field.CurrentID).ToString & " ORDER BY ID"
                    s &= GetFixedFilter(GSCOM.SQL.TableQuery(sqlTemp, Connection))
                    'GSCOM.SQL.ExecuteNonQuery("UPDATE tPayrollPeriod SET IsPaused=0 WHERE ID=" & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString, Connection)
                Case MessageBoxForm.MgsResult.Cancel
                    Me.GetStripButton("Stop").Text = "Send e-Payslips"
                    Exit Sub
                Case MessageBoxForm.MgsResult.All
                    'GSCOM.SQL.ExecuteNonQuery("UPDATE tPayrollPeriod SET IsPaused=0, CurrentID=0 WHERE ID=" & myDT.Get(Database.Tables.tPayrollPeriod.Field.ID).ToString, Connection)
            End Select
            Me.EndProcess("Loading...")
            Application.DoEvents()
            s &= " ORDER BY ID"
            'If f.Row IsNot Nothing Then
            '    s &= " AND ID_Employee=" & f.Row.Item("ID").ToString
            'End If
            mtPayroll.ClearThenFill(s)
            Dim reportTemp As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            reportTemp.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "EPayslip.rpt")
            Dim drPayslipMenu As DataRow = nDB.MenuSet.tMenu.Select("ID=64")(0)
            Dim drPayslipSubDS() As DataRow = nDB.MenuSet.tMenuSubDataSource.Select("ID_Menu=64")
            Dim daTemp As SqlClient.SqlDataAdapter
            'Dim ReportDS As New DataSet
            Dim dt As DataTable = GSCOM.SQL.TableQuery("SELECT * FROM " & Me.PassParameters(drPayslipMenu("DataSource").ToString.Replace("@ID_PayrollPeriod", myDT.Get("ID").ToString)), Connection)
            reportTemp.SetDataSource(dt)
            For Each dr As DataRow In drPayslipSubDS
                If reportTemp.Subreports.Item(dr("Name").ToString) IsNot Nothing Then
                    dt = New DataTable
                    dt = GSCOM.SQL.TableQuery("SELECT * FROM " & Me.PassParameters(dr("DataSource").ToString.Replace("@ID_PayrollPeriod", myDT.Get("ID").ToString)), Connection)
                    reportTemp.OpenSubreport(dr("Name").ToString).SetDataSource(dt)
                End If
            Next
            'daTemp = New SqlClient.SqlDataAdapter("SELECT * FROM fPayslipEarning(" & pIDTemp & "," & nDB.GetUserID & ")", Connection)
            'dtEarnings = New DataTable
            'daTemp.Fill(dtEarnings)
            'reportTemp.Subreports(1).SetDataSource(dtEarnings)
            'Application.DoEvents()
            'daTemp = New SqlClient.SqlDataAdapter("SELECT * FROM fPayslipDeduction(" & pIDTemp & "," & nDB.GetUserID & ") ", Connection)
            'dtDeductions = New DataTable
            'daTemp.Fill(dtDeductions)
            'reportTemp.Subreports(0).SetDataSource(dtDeductions)
            'Application.DoEvents()
            'daTemp = New SqlClient.SqlDataAdapter("SELECT * FROM fPayslip(" & pIDTemp & "," & nDB.GetUserID & ") ORDER BY ID", Connection)
            'dtPayslip = New DataTable
            'daTemp.Fill(dtPayslip)
            Application.DoEvents()
            daTemp = New SqlClient.SqlDataAdapter("select emailaddress,e.ID from tpersona p inner join temployee e on e.id_persona=p.id ", Connection)
            dtPersonaEmail = New DataTable
            daTemp.Fill(dtPersonaEmail)
            Application.DoEvents()
            'dtDeductions.Clear()
            'dtEarnings.Clear()
            Dim vTime As DateTime
            Dim b As New MessageBox2
            For Each drTemp As DataRow In mtPayroll.Rows
                Application.DoEvents()
                If go = False Then
                    Me.EndProcess("Stopped")
                    Exit For
                End If
                Application.DoEvents()
                Try
                    Application.DoEvents()
                    cnt += 1
                    Application.DoEvents()
Retry1:             vTime = Now
                    Application.DoEvents()
                    'reportTemp.SetDataSource(dtPayslip)
                    Application.DoEvents()
                    Dim eIDTemp As String = drTemp.Item("ID_Employee").ToString
                    Application.DoEvents()
                    'reportTemp.RecordSelectionFormula = "{vzPayslip.ID}=" & dtPayslip.Select("ID_Employee=" & eIDTemp.ToString)(0).Item("ID").ToString
                    reportTemp.RecordSelectionFormula = "{vzPayslip.ID_Employee}=" & drTemp("ID_Employee").ToString
                    Application.DoEvents()
                    Dim msgSubject As String = "Pay Slip for PPE " & DateValue(myDT.Get("StartDate").ToString) & " to " & DateValue(myDT.Get("EndDate").ToString)
                    Application.DoEvents()
                    Try
                        'reportTemp.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBDataSource)
                        'For Each dsc As CrystalDecisions.Shared.IConnectionInfo In reportTemp.DataSourceConnections
                        '    dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
                        'Next
                        'For Each SReport As CrystalDecisions.CrystalReports.Engine.ReportDocument In reportTemp.Subreports
                        '        'SReport.OpenSubreport(SReport.Name)
                        '    For Each dsc As CrystalDecisions.Shared.IConnectionInfo In SReport.DataSourceConnections
                        '        dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
                        '    Next
                        '    SReport.SetDatabaseLogon(gDBUser, gDBPassword, gDBDataSource, gDBInitialCatalog)
                        'Next
                        reportTemp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Path.Combine(System.IO.Path.GetTempPath, "Payslip.pdf"))
                        Application.DoEvents()
                    Catch ex As Exception
                        b.lblMessage.Text = ex.Message
                        Select Case b.ShowDialog
                            Case DialogResult.Abort
                                Try
                                    Me.GetStripButton("Stop").PerformClick()
                                Catch exx As Exception
                                End Try
                                Exit Sub
                            Case DialogResult.Retry
                                GoTo Retry1
                            Case DialogResult.Ignore
                                Continue For
                        End Select
                    End Try


                    Dim strTo As String = ""
                    If IsDBNull(dtPersonaEmail.Select("ID=" & eIDTemp)(0).Item("EmailAddress")) Then
                        strTo = nDB.GetSetting(Database.SettingEnum.AskHRAddress) ' dtTemp.Rows(0).Item(0).ToString
                    Else
                        strTo = dtPersonaEmail.Select("ID=" & eIDTemp)(0).Item("EmailAddress").ToString
                    End If
                    Dim mMsgTemp As New MailMessage
                    mMsgTemp.From = New MailAddress(nDB.GetSetting(Database.SettingEnum.PayslipSender)) ' dtTemp.Rows(0).Item("Sender").ToString)
                    mMsgTemp.To.Add(New MailAddress(strTo))
                    mMsgTemp.Subject = msgSubject
                    mMsgTemp.BodyEncoding = System.Text.Encoding.Default
                    mMsgTemp.Attachments.Add(New Attachment(Path.Combine(System.IO.Path.GetTempPath, "Payslip.pdf")))
                    mMsgTemp.Priority = MailPriority.High
                    Dim smtpServer As New SmtpClient(nDB.GetSetting(Database.SettingEnum.SMTPServer)) ' dtTemp.Rows(0).Item("SMTP").ToString)
                    smtpServer.Port = CInt(nDB.GetSetting(Database.SettingEnum.Port)) ' dtTemp.Rows(0).Item("SMTPPort").ToString)

                    smtpServer.Credentials = New Net.NetworkCredential(nDB.GetSetting(Database.SettingEnum.PayslipSender), nDB.GetSetting(Database.SettingEnum.PayslipSenderPassword)) ' dtTemp.Rows(0).Item("Sender").ToString, dtTemp.Rows(0).Item("Password").ToString)
                    Try
                        If nDB.GetSetting(Database.SettingEnum.SSLEnabled).ToString.Equals("1") Then ' dtTemp.Rows(0).Item("SSL").ToString.Equals("1") Then
                            smtpServer.EnableSsl = True
                        Else
                            smtpServer.EnableSsl = False
                        End If
                    Catch ex As Exception

                    End Try
Retry:
                    Application.DoEvents()
                    Try
                        Application.DoEvents()
                        Dim dm As Object = nDB.GetSetting(Database.SettingEnum.PayslipSenderDeliveryMethod, "2")
                        Select Case CInt(dm)
                            Case 0
                                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network
                            Case 1
                                smtpServer.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory
                            Case 2
                                smtpServer.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
                            Case 3

                        End Select
                        smtpServer.Send(mMsgTemp)
                        Application.DoEvents()
                        'GSCOM.SQL.ExecuteNonQuery("UPDATE tPayroll SET EMailDateTime=getdate() WHERE ID =" & dtPayslip.Select("ID_Employee=" & eIDTemp.ToString)(0).Item("ID").ToString, gConnection)
                        GSCOM.SQL.ExecuteNonQuery("UPDATE tPayroll SET EPayslipDateTime=getdate() WHERE ID =" & drTemp("ID").ToString, gConnection)
                        Application.DoEvents()
                        'Throw New Exception("EMail sending failed")
                    Catch ex As Exception
                        'Microsoft.VisualBasic.MsgBoxResult 
                        'Dim dResult As DialogResult
                        'dResult = CType(MessageBox("E-Mail Sending Failed!" & Chr(13), MsgBoxStyle.AbortRetryIgnore).ShowDialog, DialogResult)
                        b.lblMessage.Text = ex.Message
                        Select Case b.ShowDialog
                            Case DialogResult.Abort
                                Try
                                    Me.GetStripButton("Stop").PerformClick()
                                Catch exx As Exception
                                End Try
                                Exit Sub
                            Case DialogResult.Ignore
                                Exit Try
                            Case DialogResult.Retry
                                GoTo Retry
                        End Select
                        'If dResult = MsgBoxResult.Retry Then
                        'End If

                    End Try

                    Try

                        If IO.File.Exists(Path.Combine(System.IO.Path.GetTempPath, "Payslip.pdf")) Then
                            mMsgTemp.Dispose()
                            mMsgTemp = Nothing
                            IO.File.Delete(Path.Combine(System.IO.Path.GetTempPath, "Payslip.pdf"))
                        End If
                    Catch ex As Exception

                    End Try
                    GC.Collect()
                    Me.EndProcess("Sent " & cnt.ToString & " of " & mtPayroll.Rows.Count & "." & " (" & DateDiff(DateInterval.Second, vTime, Now).ToString & "seconds)")
                Catch ex As Exception
                    b.lblMessage.Text = ex.Message
                    Select Case b.ShowDialog
                        Case DialogResult.Abort
                            Try
                                Me.GetStripButton("Stop").PerformClick()
                            Catch exx As Exception
                            End Try
                            Exit Sub
                        Case DialogResult.Ignore
                            Exit Try
                        Case DialogResult.Retry
                            GoTo Retry1
                    End Select
                End Try
            Next
            'Me.LoadInfo(CInt(myDT.Get(Database.Tables.tPayrollPeriod.Field.ID)))
            Me.EndProcess("Done")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Me.GetStripButton("Stop") IsNot Nothing Then
                Me.GetStripButton("Stop").Text = "Send e-Payslips"
                go = True
            End If
            Me.Refresh()
        End Try
    End Sub

    Private Function GetFixedFilter(ByVal dtExcluded As DataTable) As String
        Dim s As String = " AND ID IN("
        For Each dr As DataRow In dtExcluded.Rows
            s &= dr.Item("ID").ToString & ","
        Next
        s = s.Remove(s.Length - 1)
        s &= ") "
        GetFixedFilter = s
    End Function

    Private Function GetFixedFilter() As String
        Dim s As String = "ID IN ("
        For Each dr As DataRow In mtPayroll.Select()
            s &= dr("ID_Employee").ToString & ","
        Next
        s = Left(s, s.Length - 1) & ")"
        Return s
    End Function
#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class

#Region "Old"
'Private Sub Navigate()
'    Dim f As String
'    Dim xw As Xml.XmlWriter
'    Dim t As String
'    Dim xs As New Xml.XmlWriterSettings()
'    Dim sb As New System.Text.StringBuilder
'    xs.Indent = True
'    f = IO.Path.GetTempFileName()
'    f = "d:\cc.xml"
'    xw = Xml.XmlWriter.Create(f, xs)
'    'xw = Xml.XmlWriter.Create(sb, xs)
'    t = "type=""text/xsl"" href=""" & gGetSetting(SettingEnum.StyleSheetPath) & "tPayrollPeriod.xsl"""
'    xw.WriteProcessingInstruction("xml-stylesheet", t)
'    'mtGender.WriteXml(xw, True)
'    DataSet.WriteXml(xw)
'    'xw.Flush()
'    t = "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf
'    'mForm.Browser.Navigate(f)
'    't &= sb.ToString
'    t = "<?xml version=""1.0"" encoding=""Unicode""?>"
'    't = ""
'    t &= "<?xml-stylesheet type=""text/xsl"" href=""C:\Documents and Settings\Robbie\Desktop\GSCOM\Applications\Zurdo\StyleSheets\tPayrollPeriod.xsl""?>"
'    t &= DataSet.GetXml
'    mForm.Browser.DocumentText = t
'End Sub
'Private Sub RefreshListing()
'    Dim dt As DataTable
'    Dim s As String
'    'ROBBIE NOTE: set the primary key so merge function would be able to determine which record would be update
'    If mListing.PrimaryKey.Length = 0 Then
'        Dim keys(0) As DataColumn
'        keys(0) = mListing.Columns("ID")
'        mListing.PrimaryKey = keys
'    End If
'    s = GSCOM.SQL.SelectStatement(mListing)
'    s &= " WHERE ID=" & mDR("ID").ToString
'    dt = GSCOM.SQL.TableQuery(s, Connection)
'    'ROBBIE NOTE: set preservechanges to false to be able to reupdate the values
'    mListing.Merge(dt, False, MissingSchemaAction.Ignore)
'End Sub

'Private Sub InitBindings1()
'    Dim b As Binding
'    With protControl
'        .txtID.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.ID.ToString)
'        .txtLastName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.LastName.ToString)
'        .txtFirstName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.FirstName.ToString)
'        .txtMiddleName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.MiddleName.ToString)
'        .mtbSSSNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.SSSNo.ToString)
'        .mtbHDMFNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.HDMFNo.ToString)
'        .mtbPhilHealthNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.PhilHealthNo.ToString)
'        .cboID_Gender.DataBindings.Add("SelectedValue", myDT, tPayrollPeriod.Field.ID_Gender.ToString)
'        b = New Binding("Text", myDT, tPayrollPeriod.Field.BirthDate.ToString)
'        AddHandler b.Format, AddressOf GSCOM.EventDelegates.BindingFormatTextBox
'        AddHandler b.Parse, AddressOf GSCOM.EventDelegates.BindingParseTextBox
'        .txtBirthDate.DataBindings.Add(b)
'    End With

'End Sub
#End Region