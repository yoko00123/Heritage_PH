Option Explicit On
Option Strict Off



Friend Class EmployeeRestDayFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeRestDayFile(Connection)
    Private mtEmployeeRestDayFile_Detail As GSCOM.SQL.ZDataTable 'Database.Tables.tEmployeeRestDayFile_Detail(Connection)
    Private mControl As New InSys.DataControl
    'Private mImportButton As ToolStripButton
    'Private mGenTemplateButton As ToolStripButton
    'BILLY'Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtEmployeeRestDayFile_Detail)
        End With
        Dim pdc As DataColumn
        Me.InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tEmployeeRestDayFile.Field.ID)
        myDT.Columns(Database.Tables.tEmployeeRestDayFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        Dim b As ToolStripButton
        b = Me.GetStripButton("Import File")
        AddHandler b.Click, AddressOf ImportFile

        b = Me.GetStripButton("Generate Template")
        AddHandler b.Click, AddressOf GenTemplate

        b = Me.GetStripButton("Execute")
        AddHandler b.Click, AddressOf TransferRestDay

        'mGenTemplateButton = Me.GetStripButton("Generate Template") 'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf GenTemplate)
        'AddHandler mGenTemplateButton.Click, AddressOf GenTemplate

        'mImportButton = GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        'AddHandler mImportButton.Click, AddressOf ImportFile

        'BILLY'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferRestDay)
        Me.ReloadAfterCommit = True
        mtEmployeeRestDayFile_Detail = Me.mDataset.Tables("tEmployeeRestDayFile_Detail")
        AfterNew()

        mGrid = Me.GetDataGridView(mtEmployeeRestDayFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        'mImportButton.Enabled = Not (mtEmployeeRestDayFile_Detail.Rows.Count > 0)
        'BILLY'mApplyButton.Enabled = (pID > 0)
        Me.GetStripButton("Generate Template").Enabled = True
    End Sub

#End Region

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            ' Me.mImportButton.Enabled = False
            '  Me.mApplyButton.Enabled = False
        End If
    End Sub

    Private Sub TransferRestDay(ByVal sender As Object, ByVal e As EventArgs)
        'BILLY
        'If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeRestDayFile_Apply " & myDT.Get(Database.Tables.tEmployeeRestDayFile.Field.ID).ToString, Connection)
        '    'Me.mApplyButton.Enabled = False
        '    'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
        '    MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tEmployeeRestDayFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day1.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day2.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day3.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day4.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day5.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day6.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Day7.ToString
        s &= ", " & Database.Tables.tEmployeeRestDayFile_Detail.Field.Comment.ToString
        Return s
    End Function

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim o As Object = Nothing
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tEmployeeRestDayFile.Field.Name, s)
            s = GetSelectString()
            mtEmployeeRestDayFile_Detail.Clear()
            GSCOM.SQL.GetExcelTable(FileName, "RestDay", mtEmployeeRestDayFile_Detail, s)
            'o = mtEmployeeRestDayFile_Detail.Get(0, Database.Tables.tEmployeeRestDayFile_Detail.Field.Day1)
            For Each dr As DataRow In mtEmployeeRestDayFile_Detail.Select()
                For i As Integer = 1 To 7
                    If IsDBNull(dr("Day" & i.ToString)) Then
                        dr("Day" & i.ToString) = False
                    End If
                Next
            Next
            mtEmployeeRestDayFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtEmployeeRestDayFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeRestDayFile_Detail
            Me.EndProcess()
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Public Class RestDayTable
        Inherits DataTable

        Public Sub New()
            With Me.Columns
                '.Add("No", GetType(Integer))
                '.Add("Employee", GetType(String))
                '.Add("Designation", GetType(String))
                '.Add("Department", GetType(String))
                '.Add("Gender", GetType(String))
                '.Add("EmployeeCode", GetType(String))
                '.Add("EmployeeStatus", GetType(String))
                '.Add("Day1", GetType(Boolean))
                '.Add("Day2", GetType(Boolean))
                '.Add("Day3", GetType(Boolean))
                '.Add("Day4", GetType(Boolean))
                '.Add("Day5", GetType(Boolean))
                '.Add("Day6", GetType(Boolean))
                '.Add("Day7", GetType(Boolean))
                '.Add("Comment", GetType(String))



                .Add("No", GetType(Integer))
                .Add("Department", GetType(String))
                .Add("EmployeeStatus", GetType(String))
                .Add("Designation", GetType(String))
                .Add("Gender", GetType(String))
                .Add("EmployeeCode", GetType(String))
                .Add("Employee", GetType(String))
                .Add("Day1", GetType(Boolean))
                .Add("Day2", GetType(Boolean))
                .Add("Day3", GetType(Boolean))
                .Add("Day4", GetType(Boolean))
                .Add("Day5", GetType(Boolean))
                .Add("Day6", GetType(Boolean))
                .Add("Day7", GetType(Boolean))
                .Add("Comment", GetType(String))
            End With
        End Sub
    End Class

    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vRestDayTable As New RestDayTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New RestDayAdapter

        sfd.FileName = myDT.Get(Database.Tables.tEmployeeRestDayFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "RestDayFile.xls", sfd.FileName, True)
            ' a.DataSource = sfd.FileName 'initialize datasource (filename)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            'Dim dt As New DataTable
            Dim o As Object
            o = myDT.Get(Database.Tables.tEmployeeRestDayFile.Field.StartDate)
            Dim s As String
            s = "SELECT No"
            s &= ",Department,EmployeeStatus,Designation,Gender,EmployeeCode,Employee FROM fReport_EmployeeRestDay(" & GSCOM.SQL.SQLFormat(o) & "," & nDB.GetCompanyID & "," & nDB.GetSessionID & ")"
            's &= ",Employee"
            's &= ",Designation"
            's &= ",Department"
            's &= ",EmployeeStatus"
            's &= ",Company FROM fReport_EmployeeRestDay(" & GSCOM.SQL.SQLFormat(o) & ")"
            s &= " ORDER BY"
            s &= " Department"
            s &= ",EmployeeStatus"
            s &= ",Designation"
            s &= ",EmployeeCode"
            s &= ",Employee"

            GSCOM.SQL.FillTable(vRestDayTable, s, Connection)
            'dt = GSCOM.SQL.TableQuery(sqlStr, Connection)
            'For Each drx As DataRow In dt.Select
            '    'dr = dt.NewRow
            '    dr = vRestDayTable.NewRow
            '    dr("No") = drx("No")
            '    dr("Department") = drx("Department")
            '    dr("EmployeeStatus") = drx("EmployeeStatus")
            '    dr("Designation") = drx("Designation")
            '    dr("Gender") = drx("Gender")
            '    dr("EmployeeCode") = drx("EmployeeCode")
            '    dr("Employee") = drx("Employee")
            '    dr("Day1") = drx("Day1")
            '    dr("Day2") = drx("Day2")
            '    dr("Day3") = drx("Day3")
            '    dr("Day4") = drx("Day4")
            '    dr("Day5") = drx("Day5")
            '    dr("Day6") = drx("Day6")
            '    dr("Day7") = drx("Day7")
            '    dr("Comment") = drx("Comment")
            '    vRestDayTable.Rows.Add(dr)
            'Next
            '
            'a.Update(vRestDayTable.Select(""))
            UseArray(sfd.FileName, vRestDayTable, "A2")

            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable, ByVal pStartingCell As String)
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
        oSheet.Range(pStartingCell).Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

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
#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeRestDayFile)
        End Set
    End Property

    'Protected Overrides Property Control() As Control
    '    Get
    '        Return mControl
    '    End Get
    '    Set(ByVal value As Control)
    '        mControl = CType(value, InSys.DataControl)
    '    End Set
    'End Property

#End Region

    'Private Sub mGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles mGrid.CellFormatting
    '    Select Case mGrid.Columns(e.ColumnIndex).DataPropertyName.ToLower
    '        Case Database.Tables.tScheduleFile_Detail.Field.Schedule1.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule2.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule3.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule4.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule5.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule6.ToString.ToLower _
    '            , Database.Tables.tScheduleFile_Detail.Field.Schedule7.ToString.ToLower
    '    End Select
    'End Sub

    Private Function drx() As DataRow
        Throw New NotImplementedException
    End Function

End Class

Partial Public Class RestDayAdapter
    Inherits System.ComponentModel.Component
    Private WithEvents _adapter As System.Data.OleDb.OleDbDataAdapter
    Private _connection As System.Data.OleDb.OleDbConnection
    Private _commandCollection() As System.Data.OleDb.OleDbCommand
    Private _clearBeforeFill As Boolean


    Private mConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}"";Extended Properties=""Excel 8.0"""
    Public DataSource As String
    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitConnection()
        Me._connection = New System.Data.OleDb.OleDbConnection
        Me._connection.ConnectionString = String.Format(mConn, DataSource)
    End Sub

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Sub New()
        MyBase.New()
        Me.ClearBeforeFill = True
    End Sub

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private ReadOnly Property Adapter() As System.Data.OleDb.OleDbDataAdapter
        Get
            If (Me._adapter Is Nothing) Then
                Me.InitAdapter()
            End If
            Return Me._adapter
        End Get
    End Property

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Friend Property Connection() As System.Data.OleDb.OleDbConnection
        Get
            If (Me._connection Is Nothing) Then
                Me.InitConnection()
            End If
            Return Me._connection
        End Get
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Me._connection = value
            If (Not (Me.Adapter.InsertCommand) Is Nothing) Then
                Me.Adapter.InsertCommand.Connection = value
            End If
            If (Not (Me.Adapter.DeleteCommand) Is Nothing) Then
                Me.Adapter.DeleteCommand.Connection = value
            End If
            If (Not (Me.Adapter.UpdateCommand) Is Nothing) Then
                Me.Adapter.UpdateCommand.Connection = value
            End If
            Dim i As Integer = 0
            Do While (i < Me.CommandCollection.Length)
                If (Not (Me.CommandCollection(i)) Is Nothing) Then
                    CType(Me.CommandCollection(i), System.Data.OleDb.OleDbCommand).Connection = value
                End If
                i = (i + 1)
            Loop
        End Set
    End Property

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected ReadOnly Property CommandCollection() As System.Data.OleDb.OleDbCommand()
        Get
            If (Me._commandCollection Is Nothing) Then
                Me.InitCommandCollection()
            End If
            Return Me._commandCollection
        End Get
    End Property

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Property ClearBeforeFill() As Boolean
        Get
            Return Me._clearBeforeFill
        End Get
        Set(ByVal value As Boolean)
            Me._clearBeforeFill = value
        End Set
    End Property

    <System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitAdapter()
        Me._adapter = New System.Data.OleDb.OleDbDataAdapter
        Dim tableMapping As System.Data.Common.DataTableMapping = New System.Data.Common.DataTableMapping
        tableMapping.SourceTable = "Table"
        tableMapping.DataSetTable = "Database"
        tableMapping.ColumnMappings.Add("No", "No")
        tableMapping.ColumnMappings.Add("Employee", "Employee")
        tableMapping.ColumnMappings.Add("Designation", "Designation")
        tableMapping.ColumnMappings.Add("Department", "Department")
        tableMapping.ColumnMappings.Add("Gender", "Gender")
        tableMapping.ColumnMappings.Add("EmployeeCode", "EmployeeCode")
        tableMapping.ColumnMappings.Add("EmployeeStatus", "EmployeeStatus")
        tableMapping.ColumnMappings.Add("Day1", "Day1")
        tableMapping.ColumnMappings.Add("Day2", "Day2")
        tableMapping.ColumnMappings.Add("Day3", "Day3")
        tableMapping.ColumnMappings.Add("Day4", "Day4")
        tableMapping.ColumnMappings.Add("Day5", "Day5")
        tableMapping.ColumnMappings.Add("Day6", "Day6")
        tableMapping.ColumnMappings.Add("Day7", "Day7")

        Me._adapter.TableMappings.Add(tableMapping)
        Me._adapter.InsertCommand = New System.Data.OleDb.OleDbCommand
        Me._adapter.InsertCommand.Connection = Me.Connection
        Me._adapter.InsertCommand.CommandText = "INSERT INTO `Database` (`No`, `Employee`, `Designation`, `Department`, `Gender`, `EmployeeCode`, `EmployeeStatus`, `Day1`, `Day2`, `Day3`, `Day4`, `Day5`, `Day6`, `Day7`) VALUES (?, ?, ?, ?, ?, ?, ?,?,?,?,?,?,?,?)"
        Me._adapter.InsertCommand.CommandType = System.Data.CommandType.Text
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("No", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "No", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Employee", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Employee", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Designation", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Designation", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Department", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Department", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Gender", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Gender", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeCode", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeCode", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeStatus", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeStatus", System.Data.DataRowVersion.Current, False, Nothing))

        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day1", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day1", System.Data.DataRowVersion.Current, False, Nothing))
        'Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day1", System.Data.OleDb.OleDbType.Boolean))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day2", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day2", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day3", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day3", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day4", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day4", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day5", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day5", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day6", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day6", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Day7", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Day7", System.Data.DataRowVersion.Current, False, Nothing))



    End Sub



    '<System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitCommandCollection()
        Me._commandCollection = New System.Data.OleDb.OleDbCommand(0) {}
        Me._commandCollection(0) = New System.Data.OleDb.OleDbCommand
        Me._commandCollection(0).Connection = Me.Connection
        Me._commandCollection(0).CommandText = "SELECT `No`, `Employee`, `Designation`, `Department`, `Gender`, `EmployeeCode`, `EmployeeStatus`, `Day1`, `Day2`, `Day3`, `Day4`, `Day5`, `Day6`, `Day7` FROM `Database`"
        Me._commandCollection(0).CommandType = System.Data.CommandType.Text
    End Sub

    '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
    ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
    ' System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Fill, True)> _
    'Public Overridable Overloads Function Fill(ByVal dataTable As Schedule.DatabaseDataTable) As Integer
    '    Me.Adapter.SelectCommand = Me.CommandCollection(0)
    '    If (Me.ClearBeforeFill = True) Then
    '        dataTable.Clear()
    '    End If
    '    Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
    '    Return returnValue
    'End Function

    '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
    ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
    ' System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.[Select], True)> _
    'Public Overridable Overloads Function GetData() As Schedule.DatabaseDataTable
    '    Me.Adapter.SelectCommand = Me.CommandCollection(0)
    '    Dim dataTable As Schedule.DatabaseDataTable = New Schedule.DatabaseDataTable
    '    Me.Adapter.Fill(dataTable)
    '    Return dataTable
    'End Function

    '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
    ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
    'Public Overridable Overloads Function Update(ByVal dataTable As Schedule.DatabaseDataTable) As Integer
    '    Return Me.Adapter.Update(dataTable)
    'End Function

    '<System.Diagnostics.DebuggerNonUserCodeAttribute(), _
    ' System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
    'Public Overridable Overloads Function Update(ByVal dataSet As Schedule) As Integer
    '    Return Me.Adapter.Update(dataSet, "Database")
    'End Function

    <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
    Public Overridable Overloads Function Update(ByVal dataRow As System.Data.DataRow) As Integer
        Return Me.Adapter.Update(New System.Data.DataRow() {dataRow})
    End Function

    <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
    Public Overridable Overloads Function Update(ByVal dataRows() As System.Data.DataRow) As Integer
        Return Me.Adapter.Update(dataRows)
    End Function

    <System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
     System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, True)> _
    Public Overridable Overloads Function Insert(ByVal No As Integer, ByVal Employee As String, ByVal Designation As String, ByVal Department As String, ByVal Gender As String, ByVal EmployeeCode As String, ByVal EmployeeStatus As String, ByVal Day1 As Boolean, ByVal Day2 As Boolean, ByVal Day3 As Boolean, ByVal Day4 As Boolean, ByVal Day5 As Boolean, ByVal Day6 As Boolean, ByVal Day7 As Boolean) As Integer
        Me.Adapter.InsertCommand.Parameters(0).Value = CType(No, Integer)
        Me.Adapter.InsertCommand.Parameters(1).Value = CType(Employee, String)
        Me.Adapter.InsertCommand.Parameters(2).Value = CType(Designation, String)
        Me.Adapter.InsertCommand.Parameters(3).Value = CType(Department, String)
        Me.Adapter.InsertCommand.Parameters(4).Value = CType(Gender, String)
        Me.Adapter.InsertCommand.Parameters(5).Value = CType(EmployeeCode, String)
        Me.Adapter.InsertCommand.Parameters(6).Value = CType(EmployeeStatus, String)
        Me.Adapter.InsertCommand.Parameters(7).Value = CType(Day1, Boolean)
        Me.Adapter.InsertCommand.Parameters(8).Value = CType(Day2, Boolean)
        Me.Adapter.InsertCommand.Parameters(9).Value = CType(Day3, Boolean)
        Me.Adapter.InsertCommand.Parameters(10).Value = CType(Day4, Boolean)
        Me.Adapter.InsertCommand.Parameters(11).Value = CType(Day5, Boolean)
        Me.Adapter.InsertCommand.Parameters(12).Value = CType(Day6, Boolean)
        Me.Adapter.InsertCommand.Parameters(13).Value = CType(Day7, Boolean)

        Dim previousConnectionState As System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
        If ((Me.Adapter.InsertCommand.Connection.State And System.Data.ConnectionState.Open) _
                    <> System.Data.ConnectionState.Open) Then
            Me.Adapter.InsertCommand.Connection.Open()
        End If
        Try
            Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery
            Return returnValue
        Finally
            If (previousConnectionState = System.Data.ConnectionState.Closed) Then
                Me.Adapter.InsertCommand.Connection.Close()
            End If
        End Try
    End Function
End Class
