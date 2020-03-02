Option Explicit On
Option Strict Off

Imports Excel = Microsoft.Office.Interop.Excel
Friend Class ScheduleFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tScheduleFile(Connection)
    Private mtScheduleFile_Detail As GSCOM.SQL.ZDataTable ' Database.Tables.tScheduleFile_Detail
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.ScheduleFileControl
    Private mImportButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtScheduleFile_Detail)
        End With
        Dim pdc As DataColumn
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tScheduleFile.Field.ID)
        myDT.Columns(Database.Tables.tScheduleFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        Dim b As ToolStripButton
        b = Me.GetStripButton("Import File")
        AddHandler b.Click, AddressOf ImportFile
        mImportButton = Me.GetStripButton("Import File")
        b = Me.GetStripButton("Generate Template")
        AddHandler b.Click, AddressOf GenTemplate

        b = Me.GetStripButton("Execute")
        AddHandler b.Click, AddressOf TransferSchedule


        ', gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        'mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf TransferSchedule)
        Me.ReloadAfterCommit = True
        mtScheduleFile_Detail = Me.mDataset.Tables("tScheduleFile_Detail")
        AfterNew()
        mGrid = Me.GetDataGridView(mtScheduleFile_Detail)
        'mGrid.ShowRowErrors = True
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtScheduleFile_Detail.Rows.Count > 0)
        '  mApplyButton.Enabled = (pID > 0)
        ' Me.GetStripButton("Generate Template").Enabled = True
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

    Private Sub TransferSchedule(ByVal sender As Object, ByVal e As EventArgs)
        'If MsgBox("Do you want to execute file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    GSCOM.SQL.ExecuteNonQuery("EXEC pScheduleFile_Apply " & myDT.Get(Database.Tables.tScheduleFile.Field.ID).ToString, Connection)
        '    'Me.mApplyButton.Enabled = False
        '    'LoadInfo(CInt(myDT.Get(Database.Tables.tScheduleFile.Field.ID)))
        '    MsgBox("Done.", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tScheduleFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule1.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule2.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule3.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule4.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule5.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule6.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Schedule7.ToString
        s &= ", " & Database.Tables.tScheduleFile_Detail.Field.Comment.ToString
        Return s
    End Function

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim o As Object = Nothing
        Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tScheduleFile.Field.Name, s)
            s = GetSelectString()
            mtScheduleFile_Detail.Clear()
            GSCOM.SQL.GetExcelTable(FileName, "Schedule", mtScheduleFile_Detail, s)
            o = mtScheduleFile_Detail.Rows(0)("Schedule1")
            If IsDate(o) Then
                myDT.Set(Database.Tables.tScheduleFile.Field.StartDate, CDate(o))
                For i = 0 To 0
                    mtScheduleFile_Detail.Rows(i).Delete()
                Next
                mtScheduleFile_Detail.AcceptChanges()
                For Each dr As DataRow In mtScheduleFile_Detail.Select()
                    If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                        dr.SetAdded()
                    End If
                Next
                mGrid.DataSource = mtScheduleFile_Detail
                Me.EndProcess()
            Else
                EndProcess("No starting date was specified in the file", False)
            End If
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Public Class ScheduleTable
        Inherits DataTable

        Public Sub New()
            With Me.Columns
                With .Add("No", GetType(Integer))
                    .AutoIncrement = True
                    .AutoIncrementSeed = 1
                    .AutoIncrementStep = 1
                End With
                .Add("Department", GetType(String))
                .Add("EmployeeStatus", GetType(String))
                .Add("Designation", GetType(String))
                .Add("Gender", GetType(String))
                .Add("EmployeeCode", GetType(String))
                .Add("Employee", GetType(String))
                .Add("Date1", GetType(Date))
            End With
        End Sub
    End Class


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New ScheduleTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New ScheduleAdapter

        sfd.FileName = myDT.Get(Database.Tables.tScheduleFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Schedule.xls", sfd.FileName, True)
            ' a.DataSource = sfd.FileName 'initialize datasource (filename)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            'Dim dt As New DataTable
            Dim o As Object
            o = myDT.Get(Database.Tables.tScheduleFile.Field.StartDate)
            Dim s As String
            's = "SELECT * FROM fReport_EmployeeOfficialTime(" & GSCOM.SQL.SQLFormat(o) & ")"

            s = "SELECT "
            '  s &= "NULL No,"
            s &= "Department"
            s &= ",EmployeeStatus"
            s &= ",Designation"
            s &= ",Gender"
            s &= ",Code EmployeeCode"
            s &= ",Name Employee"
            s &= " FROM " & nDB.GetMenuDataSourceValue(Database.Menu.INSYSPEOPLE_EmployeeRecords201File)
            s &= " ORDER BY"
            s &= " Department"
            s &= ",EmployeeStatus"
            s &= ",Designation"
            s &= ",Code"
            s &= ",Name"


            GSCOM.SQL.FillTable(vScheduleTable, s, Connection)
            'For Each drx As DataRow In dt.Select
            '    dr = vScheduleTable.NewRow
            '    dr = vScheduleTable.NewRow
            '    ' dr("No") = drx("No")
            '    dr("Department") = drx("Department")
            '    dr("EmployeeStatus") = drx("EmployeeStatus")
            '    dr("Designation") = drx("Designation")
            '    dr("Gender") = drx("Gender")
            '    dr("EmployeeCode") = drx("EmployeeCode")
            '    dr("Employee") = drx("Employee")
            '    vScheduleTable.Rows.Add(dr)
            'Next
            'a.Update(vScheduleTable.Select(""))
            UseArray(sfd.FileName, vScheduleTable, "A3")
            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Code', 'Working Hours' , 'No. of Days'", Connection)
            Dim DT As DataTable = GSCOM.SQL.TableQuery("SELECT Code , WorkingHours, Days FROM dbo.tDailySchedule", Connection)




            AddToExcelTemplateScheduleFile(sfd.FileName, HDT, DT, "A", "C", "DailySchedule", myDT.Get(Database.Tables.tScheduleFile.Field.StartDate))
            'GSCOM.SQL())
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub



    Private Sub AddToExcelTemplateScheduleFile(ByVal pFileName As String, ByVal vHeaderDT As DataTable, ByVal vDetailDT As DataTable, ByVal pRange1 As String, ByVal pRange2 As String, ByVal pSheetName As String, ByVal pRange3 As Date)


        Dim oExcel As New Excel.Application

        Dim workbooks As Excel.Workbooks
        Dim workbook As Excel._Workbook

        Dim sheets As Excel.Sheets
        Dim worksheet As Excel._Worksheet
        Dim worksheet2 As Excel._Worksheet
        oExcel.DisplayAlerts = False
        oExcel.AlertBeforeOverwriting = False
        If oExcel Is Nothing Then
            Environment.ExitCode = 0
            Exit Sub
        End If

        workbooks = oExcel.Workbooks
        workbook = workbooks.Open(pFileName)
        'worksheet2()
        worksheet2 = workbook.Sheets(1)
        'worksheet = workbook.Sheets(1)
        workbook.Sheets.Add(, workbook.Sheets(1))
        worksheet = workbook.Sheets(2)
        worksheet.Name = pSheetName
        worksheet2.Range("H2").Value = pRange3
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

        workbook.Save()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        workbook.Close()


        releaseObject(worksheet)
        releaseObject(workbook)
        releaseObject(workbooks)

        sheets = Nothing
        worksheet = Nothing
        workbook = Nothing
        workbooks = Nothing

        oExcel.Quit()
        releaseObject(oExcel)
        oExcel = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        'Dim oExcel As Object

        'Dim oBook As Object
        'Dim oSheet As Object
        'oExcel = CreateObject("Excel.Application")
        'oBook = oExcel.Workbooks.open(pFileName)
        'oSheet = oBook.Worksheets.Add
        'oSheet.Name = pSheetName

        'Dim DataArray(vDetailDT.Rows.Count + vHeaderDT.Rows.Count, vDetailDT.Columns.Count + vHeaderDT.Columns.Count) As Object
        'Dim r, c As Integer

        'With oSheet.Range(pRange1 + "1:" + pRange2 + "1")
        '    .Interior.Color = RGB(222, 222, 222)
        '    .Font.Bold = True
        '    .Font.Size = 8
        'End With

        'With oSheet.Range(pRange1 + "1:" + pRange2 + CStr(vDetailDT.Rows.Count + vHeaderDT.Rows.Count))
        '    .Font.Size = 8
        'End With

        'For c = 0 To vDetailDT.Columns.Count - 1
        '    DataArray(0, c) = vHeaderDT.Rows(0).Item(c)
        'Next c

        'For r = vHeaderDT.Rows.Count To vDetailDT.Rows.Count
        '    For c = 0 To vDetailDT.Columns.Count - 1
        '        DataArray(r, c) = vDetailDT.Rows(r - 1).Item(c)
        '    Next c
        'Next r

        'oSheet = oBook.Worksheets(pSheetName)
        'oSheet.Range(pRange1 + "1").Resize(r, c).Value = DataArray
        'oSheet.Range(pRange1 + ":" + pRange2).EntireColumn.AutoFit()
        ''oBook.Saveas(pFileName)

        'oBook.Save()
        'oBook.close()
        'GC.Collect()
        'GC.WaitForPendingFinalizers()
        'releaseObject(oSheet)
        'releaseObject(oBook)

        'oSheet = Nothing
        ''oBook.Close(SaveChanges:=True)
        'oBook = Nothing
        'oExcel.Quit()
        'releaseObject(oExcel)
        'oExcel = Nothing

        'GC.Collect()
        'GC.WaitForPendingFinalizers()
        'GC.Collect()
        'GC.WaitForPendingFinalizers()
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

    'Private Sub UseArray2(ByVal pFileName As String, ByVal vHDT As DataTable, ByVal vDT As DataTable, ByVal pRange1 As String, ByVal pRange2 As String, ByVal pSheetName As String)
    'Dim oExcel As Object
    'Dim oBook As Object
    'Dim oSheet As Object
    '    oExcel = CreateObject("Excel.Application")
    '    oBook = oExcel.Workbooks.open(pFileName)
    '    oSheet = oBook.Worksheets.Add
    '    oSheet.Name = pSheetName

    'Dim DataArray(vDT.Rows.Count + vHDT.Rows.Count, vDT.Columns.Count + vHDT.Columns.Count) As Object
    'Dim r, c As Integer

    '    With oSheet.Range(pRange1 + "1:" + pRange2 + "1")
    '        .Interior.Color = RGB(222, 222, 222)
    '        .Font.Bold = True
    '    End With

    '    For c = 0 To vDT.Columns.Count - 1
    '        DataArray(0, c) = vHDT.Rows(0).Item(c)
    '    Next c

    '    For r = vHDT.Rows.Count To vDT.Rows.Count
    '        For c = 0 To vDT.Columns.Count - 1
    '            DataArray(r, c) = vDT.Rows(r - 1).Item(c)
    '        Next c
    '    Next r

    '    oSheet = oBook.Worksheets(pSheetName)
    '    oSheet.Range(pRange1 + "1").Resize(r, c).Value = DataArray
    '    oSheet.Range(pRange1 + ":" + pRange2).EntireColumn.AutoFit()
    ''oSheet.Columns(vDT.Rows.Count, vDT.Columns.Count).ColumnWidth = 100
    '    oBook.Save()
    '    oSheet = Nothing
    '    oBook = Nothing
    '    oExcel.Quit()
    '    oExcel = Nothing
    '    GC.Collect()
    'End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tScheduleFile)
        End Set
    End Property



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

    Private Sub mGrid_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles mGrid.RowPrePaint
        Dim o As Object
        Dim r As DataGridViewRow
        Dim drv As DataRowView
        r = mGrid.Rows(e.RowIndex)
        drv = TryCast(r.DataBoundItem, DataRowView)
        If drv IsNot Nothing Then
            o = drv.Item("IsValid")
            If o IsNot DBNull.Value Then
                If Not CBool(o) Then
                    For Each c As DataGridViewCell In r.Cells
                        c.Style.ForeColor = Color.Red
                    Next
                    'r.ErrorText = "Invalid entry"

                End If

            End If
        End If
    End Sub

    Private Sub mGrid_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles mGrid.RowsAdded

    End Sub
End Class

Partial Public Class ScheduleAdapter
    Inherits System.ComponentModel.Component
    Private WithEvents _adapter As System.Data.OleDb.OleDbDataAdapter
    Private _connection As System.Data.OleDb.OleDbConnection
    Private _commandCollection() As System.Data.OleDb.OleDbCommand
    Private _clearBeforeFill As Boolean


    Private mConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}"";Extended Properties=""Excel 8.0"""
    Public DataSource As String

    Private Sub InitConnection()
        Me._connection = New System.Data.OleDb.OleDbConnection
        Me._connection.ConnectionString = String.Format(mConn, DataSource)
    End Sub

    Public Sub New()
        MyBase.New()
        Me.ClearBeforeFill = True
    End Sub

    Private ReadOnly Property Adapter() As System.Data.OleDb.OleDbDataAdapter
        Get
            If (Me._adapter Is Nothing) Then
                Me.InitAdapter()
            End If
            Return Me._adapter
        End Get
    End Property

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

    Protected ReadOnly Property CommandCollection() As System.Data.OleDb.OleDbCommand()
        Get
            If (Me._commandCollection Is Nothing) Then
                Me.InitCommandCollection()
            End If
            Return Me._commandCollection
        End Get
    End Property

    Public Property ClearBeforeFill() As Boolean
        Get
            Return Me._clearBeforeFill
        End Get
        Set(ByVal value As Boolean)
            Me._clearBeforeFill = value
        End Set
    End Property

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
        Me._adapter.TableMappings.Add(tableMapping)
        Me._adapter.InsertCommand = New System.Data.OleDb.OleDbCommand
        Me._adapter.InsertCommand.Connection = Me.Connection
        Me._adapter.InsertCommand.CommandText = "INSERT INTO `Database` (`No`, `Employee`, `Designation`, `Department`, `Gender`, `EmployeeCode`, `EmployeeStatus`) VALUES (?, ?, ?, ?, ?, ?, ?)"
        Me._adapter.InsertCommand.CommandType = System.Data.CommandType.Text
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("No", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "No", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Employee", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Employee", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Designation", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Designation", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Department", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Department", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Gender", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Gender", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeCode", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeCode", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("EmployeeStatus", System.Data.OleDb.OleDbType.VarChar, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EmployeeStatus", System.Data.DataRowVersion.Current, False, Nothing))
        Me._adapter.InsertCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("Date1", System.Data.OleDb.OleDbType.Date, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Date1", System.Data.DataRowVersion.Current, False, Nothing))
    End Sub

    Private Sub InitCommandCollection()
        Me._commandCollection = New System.Data.OleDb.OleDbCommand(0) {}
        Me._commandCollection(0) = New System.Data.OleDb.OleDbCommand
        Me._commandCollection(0).Connection = Me.Connection
        Me._commandCollection(0).CommandText = "SELECT `No`, `Employee`, `Designation`, `Department`, `Gender`, `EmployeeCode`, `EmployeeStatus`, `Date1` FROM `Database`"
        Me._commandCollection(0).CommandType = System.Data.CommandType.Text
    End Sub

    Public Overridable Overloads Function Update(ByVal dataRow As System.Data.DataRow) As Integer
        Return Me.Adapter.Update(New System.Data.DataRow() {dataRow})
    End Function

    Public Overridable Overloads Function Update(ByVal dataRows() As System.Data.DataRow) As Integer
        Return Me.Adapter.Update(dataRows)
    End Function

    Public Overridable Overloads Function Insert(ByVal No As Integer, ByVal Employee As String, ByVal Designation As String, ByVal Department As String, ByVal Gender As String, ByVal EmployeeCode As String, ByVal EmployeeStatus As String, ByVal Date1 As Date) As Integer
        Me.Adapter.InsertCommand.Parameters(0).Value = CType(No, Integer)
        Me.Adapter.InsertCommand.Parameters(1).Value = CType(Employee, String)
        Me.Adapter.InsertCommand.Parameters(2).Value = CType(Designation, String)
        Me.Adapter.InsertCommand.Parameters(3).Value = CType(Department, String)
        Me.Adapter.InsertCommand.Parameters(4).Value = CType(Gender, String)
        Me.Adapter.InsertCommand.Parameters(5).Value = CType(EmployeeCode, String)
        Me.Adapter.InsertCommand.Parameters(6).Value = CType(EmployeeStatus, String)
        Me.Adapter.InsertCommand.Parameters(7).Value = CType(Date1, Date)
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

#Region "Comments"
    'Public Overridable Overloads Function Fill(ByVal dataTable As Schedule.DatabaseDataTable) As Integer
    '    Me.Adapter.SelectCommand = Me.CommandCollection(0)
    '    If (Me.ClearBeforeFill = True) Then
    '        dataTable.Clear()
    '    End If
    '    Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
    '    Return returnValue
    'End Function

    'Public Overridable Overloads Function GetData() As Schedule.DatabaseDataTable
    '    Me.Adapter.SelectCommand = Me.CommandCollection(0)
    '    Dim dataTable As Schedule.DatabaseDataTable = New Schedule.DatabaseDataTable
    '    Me.Adapter.Fill(dataTable)
    '    Return dataTable
    'End Function

    'Public Overridable Overloads Function Update(ByVal dataTable As Schedule.DatabaseDataTable) As Integer
    '    Return Me.Adapter.Update(dataTable)
    'End Function

    'Public Overridable Overloads Function Update(ByVal dataSet As Schedule) As Integer
    '    Return Me.Adapter.Update(dataSet, "Database")
    'End Function

#End Region

End Class

