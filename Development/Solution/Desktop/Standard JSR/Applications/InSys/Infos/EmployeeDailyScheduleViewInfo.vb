Option Explicit On
Option Strict Off



Friend Class EmployeeDailyScheduleViewInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tEmployeeDailyScheduleView(Connection)
    Private mtEmployeeDailySchedule As New Database.Tables.tEmployeeDailySchedule(Connection)

    Private mControl As New InSys.DataControl 'Private mControl As New nDB.EmployeeDailyScheduleViewControl
    Private mGeneratePayrollButton As ToolStripButton
    Private mRecomputeLogsButton As ToolStripButton
    Private mGrid As DataGridView
    Private mOTGrid As DataGridView
    Private mFilter As String
    'Dim btn As New DataGridViewButtonColumn

#Region "New Declaration"
    Private pId1 As Integer
#End Region

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtEmployeeDailySchedule)
        End With

        InitControl(pMenu)
        'mtEmployeeDailySchedule = mDataset.Tables("tEmployeeDailySchedule")

        myDT.Columns(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        mtEmployeeDailySchedule.DefaultView.Sort = "[Employee],[Date]"


        '--------------------------------Andrew 20110517------------------------------------------
        'MyBase.AddButton("Check All", gMainForm.imgList.Images("CheckedTrue.bmp"), AddressOf CheckAll)
        'MyBase.AddButton("UnCheck All", gMainForm.imgList.Images("CheckedFalse.bmp"), AddressOf UnCheckAll)
        '' MyBase.AddButton("Recompute Logs", gMainForm.imgList.Images("_EmployeeAttendanceLog.ico"), AddressOf RecomputeLogs)
        'MyBase.AddButton("Compute Hours", gMainForm.imgList.Images("timekeeping.png"), AddressOf NewComputation)
        'MyBase.AddButton("Compute Header", gMainForm.imgList.Images("timekeeping.png"), AddressOf ComputeHeader)
        'MyBase.AddButton("Generate Report", gMainForm.imgList.Images("report.png"), AddressOf GenerateReport)
        ''    MyBase.AddButton("Generate DBF", gMainForm.imgList.Images("clock.ico"), AddressOf GenerateAttran)
        ''MyBase.AddButton("Compute All", gMainForm.imgList.Images("clock.ico"), AddressOf ComputeAll)
        ''MyBase.AddButton("Generate Text File Report", gMainForm.imgList.Images("Clock.ico"), AddressOf GenerateCSVReport)
        'MyBase.AddButton("Post", gMainForm.imgList.Images("approve.png"), AddressOf Post)
        'MyBase.AddButton("Create Copy", gMainForm.imgList.Images("copy.png"), AddressOf Me.MeDotCreateCopy)

        AddHandler Me.GetStripButton("Check All").Click, AddressOf CheckAll
        AddHandler Me.GetStripButton("UnCheck All").Click, AddressOf UnCheckAll
        AddHandler Me.GetStripButton("Compute Hours").Click, AddressOf NewComputation
        'AddHandler Me.GetStripButton("Compute Header").Click, AddressOf ComputeHeader
        ' AddHandler Me.GetStripButton("Generate Report").Click, AddressOf GenerateReport
        'AddHandler Me.GetStripButton("Create Copy").Click, AddressOf Me.MeDotCreateCopy
        '-----------------------------------------------------------------------------------------------------------------

        Me.ReloadAfterCommit = True
        Me.NoTransactionTables = mtEmployeeDailySchedule.TableName
        mGrid = Me.GetDataGridView(mtEmployeeDailySchedule)
        AfterNew()

        mGrid = Me.GetDataGridView(mtEmployeeDailySchedule)
        mGrid.AllowUserToAddRows = False
        mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.IsAbsent.ToString).ReadOnly = True
        mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.Date.ToString).ReadOnly = True


        mGrid = Me.GetDataGridView(mtEmployeeDailySchedule)
        
        mGrid.ReadOnly = True
       
        Dim b As New DataGridViewButtonColumn
        mGrid.Columns.Insert(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1, b)
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
            'Dim btn As New DataGridViewButtonColumn
            'mGrid.Columns.Insert(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1, btn)
            mGrid.Columns(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1).Visible = False
        Else
            mGrid.Columns(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1).Visible = True
        End If
        mGrid.Columns("Day").Frozen = True

        With mGrid
            AddHandler .RowEnter, AddressOf mGrid_RowEnter
            AddHandler .RowHeaderMouseDoubleClick, AddressOf mGrid_RowHeaderMouseDoubleClick
            AddHandler .CellClick, AddressOf mGrid_CellClick
        End With
        If CBool(nDB.GetSetting(Database.SettingEnum.UseHoursAndMinutesFormat)) Then
            With mGrid
                AddHandler .CellFormatting, AddressOf mGrid_CellFormatting
                AddHandler .CellParsing, AddressOf mGrid_CellParsing
            End With
        End If

        For Each cc As DataGridViewColumn In mGrid.Columns
            If cc.Name.StartsWith("ID_") Then
                cc.HeaderText = Strings.Right(cc.Name, cc.Name.Length - 3)
            End If
        Next

        mGrid.Parent.Text = "Time Sheet"
        Dim label As ToolStrip
        label = mGrid.Parent.Controls(1)
        label.Items(0).Text = "TIME SHEET"

    End Sub

    Private Sub GenerateReport(ByVal sender As Object, ByVal e As EventArgs)
        'Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.ScheduleTable
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.ScheduleAdapter
        sfd.FileName = "HoursSummary.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "HoursSummary.xls", sfd.FileName, True)
            'a.DataSource = sfd.FileName 'initialize datasource (filename)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            Dim sd As Object
            Dim ed As Object
            sd = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)
            ed = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)
            Dim sqlString As String
            sqlString = "SELECT  EmpNo,Employee,RGHRS,Tardy,UT,ABSENT,RGOT,RDHRS,RDOT,SHHRS,SHOT,SHRHRS,SHROT,LHHRS,LHOT,LHRHRS,LHROT,RGND,RGNDOT,RDND,RDNDOT,SHND,SHNDOT,SHRND,SHRNDOT,LHND,LHNDOT,LHRND,LHRNDOT,VL,SL FROM dbo.fAUB(" & GSCOM.SQL.SQLFormat(sd) & "," & GSCOM.SQL.SQLFormat(ed) & ") A WHERE " & mFilter & " ORDER BY Employee" 'Replace(mFilter, "'", "''") & ") A"
            'dt = GSCOM.SQL.TableQuery("SELECT  EmpNo,Employee	,RegHRS,Tardy,UT,Absent,RegOT,SunOT,SunOTX,SplOT,SplOTX,SplSunOT,SplSunOTX,RegHolOT,RegHolOTX,RHOlSunOT,RHOlSunOTX,RegNP,RegOTNP,SunNP,SunOTNP,SplNP,SplOTNP,SplSunNP,SplSunOTNP,RegHolNP,	RHolSunOTNP,	RHolSunNP,	RegHolOTNP ,vl, sl FROM fAUB(" & GSCOM.SQL.SQLFormat(sd) & "," & GSCOM.SQL.SQLFormat(ed) & ")", Connection)
            dt = GSCOM.SQL.TableQuery(sqlString, Connection)
            UseArray(sfd.FileName, dt)
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        'Start a new workbook in Excel.
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.open(pFileName)


        'Create an array with 3 columns and 100 rows.
        Dim DataArray(vDT.Rows.Count - 1, vDT.Columns.Count - 1) As Object
        Dim r, c As Integer
        r = 0
        For Each drx As DataRow In vDT.Rows
            c = 0
            For Each col As DataColumn In vDT.Columns
                DataArray(r, c) = drx.Item(c)
                c += 1
            Next
            r += 1
        Next

        'Add headers to the worksheet on row 1.
        oSheet = oBook.Worksheets(1)
        'oSheet.Range("A1").Value = "Order ID"
        'oSheet.Range("B1").Value = "Amount"
        'oSheet.Range("C1").Value = "Tax"

        'Transfer the array to the worksheet starting at cell A2.
        oSheet.Range("A8").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

        'oSheet.Range("A2").Value = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate).ToString
        'oSheet.Range("A3").Value = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate).ToString




        'Save the workbook and quit Excel.
        oBook.Save()
        'oBook.SaveAs(sSampleFolder & "Book2.xls")
        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
    End Sub

    'Private Sub UseArray2(ByVal pFileName As String, ByVal vDT As DataTable, ByVal pDBF As String)
    '    Dim oExcel As Object
    '    Dim oBook As Object
    '    Dim oSheet As Object
    '    'Start a new workbook in Excel.
    '    oExcel = CreateObject("Excel.Application")
    '    oBook = oExcel.Workbooks.open(pFileName)
    '    'Create an array with 3 columns and 100 rows.
    '    Dim DataArray(vDT.Rows.Count - 1, 36 - 1) As Object
    '    Dim r, c As Integer
    '    r = 0
    '    For Each drx As DataRow In vDT.Rows
    '        c = 0
    '        For Each col As DataColumn In vDT.Columns
    '            DataArray(r, c) = drx.Item(c)
    '            c += 1
    '        Next
    '        r += 1
    '    Next

    '    'Add headers to the worksheet on row 1.
    '    oSheet = oBook.Worksheets(1)

    '    'Transfer the array to the worksheet starting at cell A2.
    '    oSheet.Range("A2").Resize(vDT.Rows.Count, 36).Value = DataArray

    '    'Save the workbook and quit Excel.
    '    oBook.Save()
    '    oBook.SaveAs(pDBF,11)
    '    oSheet = Nothing
    '    oBook = Nothing
    '    oExcel.Quit()
    '    oExcel = Nothing
    '    GC.Collect()
    'End Sub

    Private Sub UseArray2(ByVal pFileName As String, ByVal vDT As DataTable, ByVal pDBF As String)
        Dim oBook As Object
        Dim oSheet As Object
        oBook = GetObject(pFileName)
        'Create an array with 3 columns and 100 rows.
        Dim DataArray(vDT.Rows.Count - 1, 36 - 1) As Object
        Dim r, c As Integer
        r = 0
        For Each drx As DataRow In vDT.Rows
            c = 0
            For Each col As DataColumn In vDT.Columns
                DataArray(r, c) = drx.Item(c)
                c += 1
            Next
            r += 1
        Next

        'Add headers to the worksheet on row 1.
        oSheet = oBook.Worksheets(1)

        'Transfer the array to the worksheet starting at cell A2.
        oSheet.Range("A2").Resize(vDT.Rows.Count, 36).Value = DataArray

        'Save the workbook and quit Excel.
        oBook.Save()
        oBook.SaveAs(pDBF, 11)
        oSheet = Nothing
        oBook = Nothing
        'oExcel.Quit()
        'oExcel = Nothing
        GC.Collect()
    End Sub

    Private Sub GenerateCSVReport(ByVal sender As Object, ByVal e As EventArgs)
        'Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.ScheduleTable
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.ScheduleAdapter
        sfd.FileName = "HoursSummary.csv"
        'sfd.Filter = "Comma Delimiter (*.csv)|*.csv|All files (*.*)|*.*"
        sfd.Filter = "Comma Delimited (*.csv)|*.csv|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            'IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "HoursSummary(GoldTech).csv", sfd.FileName, True)
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "HoursSummary.txt", sfd.FileName, True)

            'a.DataSource = sfd.FileName 'initialize datasource (filename)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            Dim sd As Object
            Dim ed As Object
            sd = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)
            ed = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)
            dt = GSCOM.SQL.TableQuery("Select GLD From fGoldTechAttendanceSummary(" & GSCOM.SQL.SQLFormat(sd) & "," & GSCOM.SQL.SQLFormat(ed) & ")", Connection)
            UseArray(sfd.FileName, dt)
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub CheckAll(ByVal sender As Object, ByVal e As EventArgs)
        ' CheckOrUnCheckAll(True)
        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_CheckUnCheck " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID).ToString & ",1," & nDB.GetUserID, Connection)
        MsgBox("Done", MsgBoxStyle.Information)
        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
    End Sub

    Private Sub UnCheckAll(ByVal sender As Object, ByVal e As EventArgs)
        'CheckOrUnCheckAll(False)
        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_CheckUnCheck " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID).ToString & ",0," & nDB.GetUserID, Connection)
        MsgBox("Done", MsgBoxStyle.Information)
        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
    End Sub

    Public Sub CheckOrUnCheckAll(ByVal pCheck As Boolean)
        Dim dra As DataRow()
        Dim i As Integer
        Me.BeginProcess("Processing")
        dra = mtEmployeeDailySchedule.Select()
        mGrid.DataSource = Nothing
        For Each dr As DataRow In dra
            i += 1
            Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
            dr.Item(Database.Tables.tEmployeeDailySchedule.Field.IsForComputation.ToString) = pCheck
            Application.DoEvents()
        Next
        mGrid.DataSource = mtEmployeeDailySchedule
        Me.EndProcess("Done " & IIf(pCheck, "checking", "unchecking").ToString)
    End Sub

    Private Sub RecomputeLogs(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Recompute Logs?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            'GSCOM.SQL.ExecuteNonQuery("EXEC pRecomputeLogs " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID).ToString, Connection)
            'LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
            'Application.DoEvents()
            'MsgBox("Finished recomputing logs.", MsgBoxStyle.Information)
            Dim s As String
            Dim dt As DataTable
            Dim dra As DataRow()
            Dim i As Integer
            Me.BeginProcess("Processing")
            s = "SELECT * FROM dbo.fGetEmployeeDailyScheduleToCompute("
            s &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID))
            s &= ")"

            dt = GSCOM.SQL.TableQuery(s, gConnection, True)
            dra = dt.Select
            For Each dr As DataRow In dra
                i += 1
                Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
                s = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString
                s = "EXEC pEmployeeDailySchedule_ArrangeLogs " & s
                Try
                    GSCOM.SQL.ExecuteNonQuery(s, gConnection)
                Catch ex As Exception
                    If MsgBox("An error occured in record " & dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString & ". Do you want to continue computing the rest?", MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Yes Then
                        Exit For
                    End If
                End Try
                Application.DoEvents()
            Next
            Me.EndProcess("Done")

            LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
            Application.DoEvents()
            MsgBox("Finished computing hours.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ComputeHours(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Compute Hours?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            ComputeHours()
        End If
    End Sub

    Private Sub ComputeHeader(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Compute Header?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            ComputeHeader()
        End If
    End Sub

    Private Sub NewComputation(ByVal sender As Object, ByVal e As EventArgs)
     
                If MsgBox("Compute Hours?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
                    NewComputation()
                End If
    End Sub

    Private Sub ComputeHours()
        GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailyScheduleView_ComputeHours " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID).ToString & ", " & gUser, Connection)
        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
        Application.DoEvents()
        MsgBox("Finished computing hours.", MsgBoxStyle.Information)
    End Sub

    Private Sub NewComputation()
        Dim s As String
        Dim dt As DataTable
        Dim dra As DataRow()
        Dim i As Integer
        Me.BeginProcess("Processing")

        If mGrid IsNot Nothing Then
            dt = mGrid.DataSource 'Kunin kng anu lng ang nkadisplay sa grid ok? -EMIL
            dra = dt.Select("IsForcomputation = 1 AND Posted = 0", "Employee, Date ASC")
            For Each dr As DataRow In dra
                i += 1
                Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
                s = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString
                s = "EXEC pEmployeeDailySchedule_ComputeHours " & s
                Try
                    GSCOM.SQL.ExecuteNonQuery(s, gConnection)
                Catch ex As Exception
                    If MsgBox("An error occured in record " & dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString & ". Do you want to continue computing the rest?", MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Yes Then
                        Exit For
                    End If
                End Try
                Application.DoEvents()
            Next
            Me.EndProcess("Done")

            LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
            Application.DoEvents()
            MsgBox("Finished computing hours.", MsgBoxStyle.Information)
        Else
            MsgBox("No records found.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ComputeHeader()
        Dim s As String
        Dim dt As DataTable
        Dim dra As DataRow()
        Dim i As Integer
        Me.BeginProcess("Processing")

        s = "SELECT ID FROM vEmployeeDailySchedule WHERE [Date] BETWEEN "
        s &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate))
        s &= " AND "
        s &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate))
        s &= " AND ID_Company="
        s &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Company))
        's &= " AND ID_Branch="
        's &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Branch))
        s &= " AND ID_PayrollFrequency="
        s &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_PayrollFrequency))
        s &= " AND (IsForcomputation=1)"
        s &= " ORDER BY ID_Employee,[Date]" 'important for monthly grace period

        's = "SELECT * FROM dbo.fGetEmployeeDailyScheduleToCompute("
        's &= GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID))
        's &= ")"

        dt = GSCOM.SQL.TableQuery(s, gConnection, True)
        dra = dt.Select
        For Each dr As DataRow In dra
            i += 1
            Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
            s = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString
            s = "EXEC pEmployeeDailySchedule_ComputeHeader " & s
            Try
                GSCOM.SQL.ExecuteNonQuery(s, gConnection)
            Catch ex As Exception
                If MsgBox("An error occured in record " & dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID.ToString).ToString & ". Do you want to continue computing the rest?", MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Yes Then
                    Exit For
                End If
            End Try
            Application.DoEvents()
        Next
        Me.EndProcess("Done")

        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)))
        Application.DoEvents()
        MsgBox("Finished computing Header.", MsgBoxStyle.Information)
    End Sub


    Private Sub ClearOtherTables()
        'mtEmployeeAttendanceLog.Clear()
        'mtAttendance.Clear()
        'mtOvertime.Clear()
    End Sub

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID) 'must be after loadinfo coz of date range, not pID
        'Me.EnableExtraButtons(pID <> 0)
        ClearOtherTables()

        mForm.SaveButton.Enabled = Not (CBool(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized)))
        'Dim eds As String
        'mControl.TabCount()


        Dim sb As New System.Text.StringBuilder
        Dim a, b, d, e As String
        Dim o As Object
        Dim sd, ed As String
        sd = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate))
        ed = GSCOM.SQL.SQLFormat(myDT.Get(0, Database.Tables.tEmployeeDailyScheduleView.Field.EndDate))
        sb.Append("([Date] BETWEEN " & sd & " AND " & ed & ")")
        sb.Append(" AND ")
        a = sb.ToString
        sb = New System.Text.StringBuilder

        'comment for JBC only
        'sb.Append("[ID_Employee] IN (SELECT ID FROM " & nDB.GetMenuValue(Database.Menu.HumanResource_Employee, Database.Tables.tMenu.Field.DataSource).ToString & ")")        'sb.Append("(EXISTS (SELECT ID FROM " & nDB.GetMenuValue(Database.Menu.HumanResource_Employee, Database.Tables.tMenu.Field.DataSource).ToString & " WHERE ID=vEmployeeDailySchedule.[ID_Employee]))")
        'sb.Append(" AND ")

        o = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Company)
        sb.Append("(ID_Company = " & GSCOM.SQL.SQLFormat(o) & ")")
        sb.Append(" AND ")
        'o = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Branch)
        'sb.Append("(ID_Branch = " & GSCOM.SQL.SQLFormat(o) & ")")
        'sb.Append(" AND ")
        o = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_PayrollFrequency)
        sb.Append("(ID_PayrollFrequency = " & GSCOM.SQL.SQLFormat(o) & ")")


        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Branch) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_Branch = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Branch).ToString & ")")
        End If
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Department) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_Department = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Department).ToString & ")")
        End If
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Designation) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_Designation = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Designation).ToString & ")")
        End If
        'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_EmployeeStatus) IsNot DBNull.Value Then
        '    sb.Append(" AND ")
        '    sb.Append("(ID_EmployeeStatus = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_EmployeeStatus).ToString & ")")
        'End If
        'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Gender) IsNot DBNull.Value Then
        '    sb.Append(" AND ")
        '    sb.Append("(ID_Gender = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Gender).ToString & ")")
        'End If
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_PayrollClassifi) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_PayrollClassifi = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_PayrollClassifi).ToString & ")")
        End If
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Employee) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_Employee = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Employee).ToString & ")")
        End If
        If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_CostCenter) IsNot DBNull.Value Then
            sb.Append(" AND ")
            sb.Append("(ID_CostCenter = " & myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_CostCenter).ToString & ")")
        End If

        sb.Append(" AND ")
        sb.Append(" dbo.fEmployeeIsUnderUser(ID_Employee," & gUser & ") = 1")

        

        'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
        '    'Me.EnableButtons(False, pID)
        '    mControl.Enabled = False
        'Else
        '    'EnableButtons(True, pID)
        '    mControl.Enabled = True
        'End If
        b = sb.ToString
        d = a & b


        If mGrid IsNot Nothing Then
            mGrid.DataSource = Nothing
        End If
        Me.BeginProcess("Getting saved schedules")
        mFilter = d

        mtEmployeeDailySchedule.ClearThenFill(d)

        Dim c As DataTable
        a = "SELECT * FROM fEmployeeDailySchedule_Temporary(" & sd & ", " & ed & ") A WHERE "
        b &= " AND "
        b &= "(NOT EXISTS (SELECT ID FROM " & mtEmployeeDailySchedule.TableName & " B WHERE B.ID_Employee = A.ID_Employee and B.[Date] = A.[Date]))"
        e = (" AND ")
        e &= (" dbo.fEmployeeIsUnderUser(A.ID_Employee," & gUser & ") = 1")
        d = a & b & e

        c = GSCOM.SQL.TableQuery(d, Connection)
        Dim drx As DataRow
        Me.BeginProcess("Getting default schedules")
        For Each dr As DataRow In c.Rows
            drx = mtEmployeeDailySchedule.NewRow
            With drx
                .Item(Database.Tables.tEmployeeDailySchedule.Field.Date.ToString) = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.Date.ToString)
                .Item("Employee") = dr.Item("Employee")
                .Item(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee.ToString) = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee.ToString)
                .Item(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString) = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString)
                .Item(Database.Tables.tEmployeeDailySchedule.Field.IsRD.ToString) = dr.Item(Database.Tables.tEmployeeDailySchedule.Field.IsRD.ToString)
                .Item(Database.Tables.tEmployeeDailySchedule.Field.ID_CostCenter) = dr(Database.Tables.tEmployeeDailySchedule.Field.ID_CostCenter.ToString)
                '.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_Branch) = dr(Database.Tables.tEmployeeDailySchedule.Field.ID_Branch.ToString)
                .Item(Database.Tables.tEmployeeDailySchedule.Field.IsForComputation.ToString) = True
            End With
            mtEmployeeDailySchedule.Rows.Add(drx)
        Next


        If mGrid IsNot Nothing Then
            mGrid.DataSource = mtEmployeeDailySchedule
            If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
                'Dim btn As New DataGridViewButtonColumn
                'mGrid.Columns.Insert(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1, btn)
                mGrid.Columns(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1).Visible = False
            Else
                mGrid.Columns(mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString).Index + 1).Visible = True
            End If
        End If

            ComputeManPowerCost()

            Me.EndProcess("Done")

    End Sub

#End Region

    Private Sub EmployeeDailyScheduleViewInfo_Commited(ByVal sender As Object, ByVal e As CommitedEventArgs) Handles Me.Commited
        ' mGrid.CancelEdit() 'avoid "Operation did not succeed because the program cannot commit or quit a cell value change." 20070801
        Application.DoEvents()
        mGrid.DataSource = Nothing
        BeginProcess("Saving details")
        mtEmployeeDailySchedule.Update()
        EndProcess("Details saved")

     
    End Sub

#Region "Grid Events"

#Region "mGrid_CellClick"
    Private Sub mGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If e.ColumnIndex >= 0 Then
                If mGrid.Columns(e.ColumnIndex).HeaderText = "" AndAlso TypeOf mGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then 'button
                    Dim vID_Employee As Object
                    Dim vID_DailySchedule As Object
                    Dim vEmployee As String
                    Dim vDailySchedule As String
                    Dim drv As DataRowView
                    drv = TryCast(mGrid.Rows(e.RowIndex).DataBoundItem, DataRowView)
                    If drv IsNot Nothing Then
                        vID_Employee = drv.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_Employee.ToString)
                        vID_DailySchedule = drv.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString)
                        vEmployee = drv.Item("Employee").ToString
                        vDailySchedule = mGrid.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString, e.RowIndex).FormattedValue.ToString
                        Dim s As String
                        s = "Change the schedule of " & vEmployee
                        s &= " from " & GSCOM.SQL.SQLFormat(Format(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate), GSCOM.Common.DefaultDateFormat))
                        s &= " to " & GSCOM.SQL.SQLFormat(Format(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate), GSCOM.Common.DefaultDateFormat))
                        s &= " to " & vDailySchedule
                        s &= "?"
                        If MsgBox(s, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then

                            Dim dra As DataRow()
                            s = Database.Tables.tEmployeeDailySchedule.Field.ID_Employee.ToString & "=" & GSCOM.SQL.SQLFormat(vID_Employee) & "AND Posted = 0"
                            dra = mtEmployeeDailySchedule.Select(s)
                            For Each dr As DataRow In dra
                                dr.Item(Database.Tables.tEmployeeDailySchedule.Field.ID_DailySchedule.ToString) = vID_DailySchedule
                                dr.Item("DailySchedule") = vDailySchedule
                            Next
                            CType(sender, GSDetailDataGridView).Refresh()
                            CType(sender, GSDetailDataGridView).RefreshEdit()
                            CType(sender, DataGridView).RefreshEdit()
                            CType(sender, DataGridView).Refresh()
                            mtEmployeeDailySchedule.Update()

                        End If
                    End If
                End If
            End If
        Catch ex As ArgumentOutOfRangeException '-1
            'suppress
        End Try
    End Sub

#End Region

#Region "mGrid_CellFormatting"
    Private Sub mGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        Try

            Select Case e.ColumnIndex
                Case mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.REG.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.EXT.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.OT.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ND.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.NDOT.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = DecimalToHoursAndMinutes(CDec(e.Value))
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "mGrid_CellParsing"
    Private Sub mGrid_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs)
        Try
            Select Case e.ColumnIndex
                Case mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.REG.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.EXT.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.OT.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.ND.ToString).Index _
                , mGrid.Columns(Database.Tables.tEmployeeDailySchedule.Field.NDOT.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = HoursAndMinutesToDecimal(e.Value.ToString)
                        e.ParsingApplied = True

                    End If
            End Select
        Catch ex As Exception
            'err
        End Try
    End Sub

#End Region

#Region "mGrid_RowEnter"
    Private Sub mGrid_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        ClearOtherTables()
    End Sub

#End Region

#Region "mGrid_RowHeaderMouseDoubleClick"
    Private Sub mGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Dim vID As Integer
        Dim drv As DataRowView
        Try
            drv = TryCast(mGrid.Rows(e.RowIndex).DataBoundItem, DataRowView)
            If drv IsNot Nothing Then
                If drv.Row.RowState = DataRowState.Added Then
                    MsgBox("Can not open info. This record has not yet been saved.", MsgBoxStyle.Information)
                Else
                    vID = CInt(GSCOM.SQL.SQLFormat(drv.Item("ID")))
                    ShowPayrollInfo(vID)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub ShowPayrollInfo(ByVal pID As Integer)
        Dim oPayroll As InfoSet

        oPayroll = GetInfoSet(Database.Menu.INSYSORBIT_TimekeepingItems_EmployeeDailySchedule)

        If oPayroll Is Nothing Then

            oPayroll = New EmployeeDailyScheduleInfo(Database.Menu.INSYSORBIT_TimekeepingItems_EmployeeDailySchedule, Connection, mtEmployeeDailySchedule, pID)

            AddInfoSet(oPayroll, Database.Menu.INSYSORBIT_TimekeepingItems_EmployeeDailySchedule)

            'ROBBIE 20070517 ---------------\
            If Not CBool(GSCOM.Applications.InSys.nDB.GetMenuValue(Database.Menu.INSYSORBIT_TimekeepingItems_EmployeeDailySchedule, Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)) Then
                oPayroll.MakeReadOnly()
            End If
            'ROBBIE 20070517 ---------------/
            'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
            'oPayroll.GetStripButton("Compute Header").Enabled = False
            'oPayroll.GetStripButton("Compute Hours").Enabled = False
            'End If
        Else
            oPayroll.LoadInfo(pID)
            'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
            '   oPayroll.GetStripButton("Compute Header").Enabled = False
            '  oPayroll.GetStripButton("Compute Hours").Enabled = False
            'End If
        End If
        Application.DoEvents()
        'oPayroll.SaveButton.Enabled = mControl.Enabled

        'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
        '    oPayroll.GetStripButton("Compute Header").Enabled = False
        '    oPayroll.GetStripButton("Compute Hours").Enabled = False
        'Else
        '    oPayroll.GetStripButton("Compute Header").Enabled = True
        '    oPayroll.GetStripButton("Compute Hours").Enabled = True
        'End If

        'If myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized) Then
        '    oPayroll.GetStripButton("Compute Summary").Enabled = False
        '    oPayroll.GetStripButton("Compute Hours").Enabled = False
        '    oPayroll.SaveButton.Enabled = False
        'Else
        '    oPayroll.GetStripButton("Compute Summary").Enabled = True
        '    oPayroll.GetStripButton("Compute Hours").Enabled = True
        '    oPayroll.SaveButton.Enabled = True
        'End If


        'oPayroll.HideNewAndSaveButtons()
        oPayroll.Size = Me.Size
        oPayroll.IsPosted = CBool(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.IsFinalized))
        oPayroll.ShowDialog()
        'LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
    End Sub



#End Region

    Private Sub mOTGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        Dim dgv As DataGridView
        dgv = TryCast(sender, DataGridView)
        Try
            Select Case e.ColumnIndex
                Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = DecimalToHoursAndMinutes(CDec(e.Value))
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mOTGrid_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs)
        Dim dgv As DataGridView
        dgv = TryCast(sender, DataGridView)
        Try
            Select Case e.ColumnIndex
                Case dgv.Columns(Database.Tables.tOvertime.Field.ComputedHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ConsideredHours.ToString).Index _
                , dgv.Columns(Database.Tables.tOvertime.Field.ApprovedHours.ToString).Index
                    If e.Value.ToString <> "-" Then
                        e.Value = HoursAndMinutesToDecimal(e.Value.ToString)
                        e.ParsingApplied = True
                    End If
            End Select
        Catch ex As Exception
            'err
        End Try
    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeDailyScheduleView)
        End Set
    End Property



#End Region

#Region "ComputeAll"
    Private Sub ComputeAll(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Compute all?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            CheckOrUnCheckAll(True)
            Me.SaveButton.PerformClick()
            ComputeHours()
        End If
    End Sub
#End Region


    Private Sub GenerateAttran(ByVal sender As Object, ByVal e As EventArgs)
        'Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.ScheduleTable
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.ScheduleAdapter
        Dim tmp As String
        sfd.FileName = "Attran.dbf"
        sfd.Filter = "DBF 4 (dBase IV) (*.dbf)|*.dbf|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            tmp = IO.Path.GetTempFileName
            'tmp = "c:\attran.xls"
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Attran.xls", tmp, True)
            'a.DataSource = tmp
            IO.File.SetAttributes(tmp, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            Dim sd As Object
            Dim ed As Object
            sd = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)
            ed = myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)
            dt = GSCOM.SQL.TableQuery("SELECT * FROM fBancoFilipino(" & GSCOM.SQL.SQLFormat(sd) & "," & GSCOM.SQL.SQLFormat(ed) & ")", Connection)

            If IO.File.Exists(sfd.FileName) Then
                Kill(sfd.FileName)
            End If

            UseArray2(tmp, dt, sfd.FileName)


            'Dim app As Object
            'Dim workbook As Object
            'app = CreateObject("Excel.Application")
            'workbook = GetObject(tmp)
            'workbook.saveas(sfd.FileName, 11)





            'IO.File.Copy(tmp, nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & sfd.FileName, True)
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Post(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Are you sure you want to post?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
            Try
                SetEmpDSchedViewPosted(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID))
                MsgBox("Posting process complete")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Sub

    Private Sub SetEmpDSchedViewPosted(ByVal pId As Integer)
        Dim a As String
        a = "Update edsw Set"
        a &= " IsFinalized = 1"
        a &= " From tEmployeeDailyScheduleView edsw"
        a &= " Where ID=" & pId


        GSCOM.SQL.ExecuteNonQuery(a, Connection)
        LoadInfo(pId)
        'LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeDailySchedule.Field.ID)))
    End Sub

    Private Sub EnableButtons(ByVal cbol As Boolean, ByVal pID As Integer)
        mControl.Enabled = cbol
        Me.SaveButton.Enabled = cbol
        Me.GetStripButton("Check All").Enabled = cbol
        Me.GetStripButton("UnCheck All").Enabled = cbol
        Me.GetStripButton("Recompute Logs").Enabled = cbol
        Me.GetStripButton("Compute Hours").Enabled = cbol
        'Me.GetStripButton("Generate Report").Enabled = cbol
        'Me.GetStripButton("Generate DBF").Enabled = cbol
        'Me.GetStripButton("Generate Text File Report").Enabled = cbol
        Me.GetStripButton("Post").Enabled = cbol

    End Sub


#Region "Copy"

    Private Sub MeDotCreateCopy()
        'If Me.HasUnsavedChanges Then
        '    MsgBox("Can't copy an unsaved record", MsgBoxStyle.Exclamation)
        'Else
        '    If MsgBox("Are you sure you want to create a copy of this record?", vbYesNoCancel Or vbQuestion) = MsgBoxResult.Yes Then
        '        Dim f As New GSCOM.UI.GSForms.DateRangeDialog
        '        If f.ShowDialog() = DialogResult.OK Then

        '            Dim vNewID As Integer
        '            Dim s As String = "EXEC pEmployeeDailyScheduleView_Copy {0}, {1},{2}"
        '            s = String.Format(s, Me.RowID, GSCOM.SQL.SQLFormat(f.StartDate), GSCOM.SQL.SQLFormat(f.EndDate))
        '            vNewID = GSCOM.SQL.ExecuteScalar(s, gConnection)
        '            If MsgBox("Copying of record is successful. Do you want to close this record and open the new record?", MsgBoxStyle.Question Or vbYesNoCancel) = MsgBoxResult.Yes Then
        '                Me.LoadInfo(vNewID)
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub ComputeManPowerCost()
        'Dim dt As DataTable
        'dt = Me.ReportViewers(0).Table
        'Dim o As Object = (dt.Compute("SUM(RegAMT) + SUM(NDAMT) ", ""))
        'Dim d As Decimal = IIf(IsDBNull(o), 0, o)
        'Dim s As Integer = 2 ' dt.Columns("").AutoIncrementStep '  dt.Select("ColumnName='ManPowerComputedAmt'")(0)("Scale")
        'd = Math.Round(d, s)
        'If Me.Row("ManPowerComputedAmt") <> d Then
        '    Me.Row("ManPowerComputedAmt") = d
        'End If


    End Sub
#End Region



    'Protected Overrides Function CanSave() As Boolean
    '    Dim s As String
    '    Dim b As Boolean

    '    Dim d As String
    '    If pId1 = 0 Then
    '        d = "NULL"
    '    Else
    '        d = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID))
    '    End If

    '    s = "SELECT dbo.fSameDate(" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_Company)) & "," & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.ID_PayrollFrequency)) & ")"
    '    Try
    '        b = CType(GSCOM.SQL.ExecuteScalar(s, gConnection), Boolean)
    '    Catch ex As Exception
    '        b = False
    '    End Try
    '    If Not b Then
    '        MsgBox("Invalid Date")
    '    End If
    '    Return b
    'End Function
End Class

'Private Sub mLogGrid_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles mLogGrid.CellBeginEdit
'    Dim drv As DataRowView
'    drv = TryCast(mLogGrid.Rows(e.RowIndex).DataBoundItem, DataRowView)
'    If drv IsNot Nothing Then
'        If drv.Row IsNot Nothing Then
'            If (drv.Row.RowState = DataRowState.Added) Or (drv.Item(Database.Tables.tEmployeeAttendanceLog.Field.ID_EditedByUser.ToString) IsNot DBNull.Value) Then

'            Else
'                e.Cancel = True
'            End If
'        End If
'    End If
'End Sub


'Protected Overrides Function CanSave() As Boolean
'Dim a As String
'   a = "Select ID From"
'  a &= " tEmployeeDailyScheduleView eds"
' a &= " Where ((StartDate >= "
'a &= GSCOM.SQL.SQLFormat(CDate(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)))
' a &= " And StartDate <=" & GSCOM.SQL.SQLFormat(CDate(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)))
'a &= ") Or (EndDate >=" & GSCOM.SQL.SQLFormat(CDate(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate)))
'a &= " And EndDate<=" & GSCOM.SQL.SQLFormat(CDate(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate)))
'a &= ")) And IsFinalized= 1"
'If GSCOM.SQL.TableQuery(a, Connection).Rows.Count > 0 Then
'   MsgBox("There is already Start Date and End Date in the Database")
'        Return False
'Else
'   Return True
'End If
'End Function

'Private Sub GenerateAttran(ByVal sender As Object, ByVal e As EventArgs)
'    Dim a As New GSCOM.External.Attran.Attran_Export
'    Dim dt As DataTable
'    Dim sd As String
'    Dim ed As String

'    sd = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.StartDate))
'    ed = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailyScheduleView.Field.EndDate))

'    dt = GSCOM.SQL.TableQuery("SELECT * FROM fBancoFilipino(" & sd & "," & ed & ")", Connection)
'    a.Table = dt
'    a.Save(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Attran.dbf")
'    If System.IO.File.Exists(a.FileName) Then
'        MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
'    End If


'End Sub