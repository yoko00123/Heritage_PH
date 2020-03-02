Option Explicit On
Option Strict Off

Imports System.Collections.Generic
Imports System.Linq
Friend Class TimeKeepingFileInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tTimekeepingFile(Connection)
    Private myDT_Detail As New Database.Tables.tTimekeepingFile_Detail(Connection)
    Private mGenReportButton As ToolStripButton
    Private mImportButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_Detail)
        End With

        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tTimekeepingFile.Field.ID)
        cdc = myDT_Detail.Columns(Database.Tables.tTimekeepingFile_Detail.Field.ID_TimeKeepingFile)

        rel = mDataset.Relations.Add(pdc, cdc)
        mGenReportButton = MyBase.AddButton("Generate File", gMainForm.imgList.Images("GenerateFile.png"), AddressOf GenerateReport)
        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        'mGenReportButton = GetStripButton("Generate File")
        'AddHandler mGenReportButton.Click, AddressOf GenerateReport
        'mImportButton = GetStripButton("Import File")
        'AddHandler mImportButton.Click, AddressOf ImportFile

        Me.ReloadAfterCommit = True
        AfterNew()

    End Sub
#Region "Overrides"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'mGenReportButton.Enabled = Not pID = 0
        'mImportButton.Enabled = Not pID = 0
        MyBase.GetDataGridView(myDT_Detail).ReadOnly = True
        MyBase.LoadInfo(pID)

        If CBool(myDT.Get(Database.Tables.tTimekeepingFile.Field.IsApplied)) Then
            Me.EnableButtons(False, pID)
        Else
            Me.EnableButtons(True, pID)
        End If


    End Sub
    Private Sub EnableButtons(ByVal cbol As Boolean, ByVal pID As Integer)


        Me.SaveButton.Enabled = cbol
        Me.GetStripButton("Apply File").Enabled = cbol
        Me.GetStripButton("Import File").Enabled = cbol
        Me.mImportButton.Enabled = cbol
        Me.GetStripButton("Load List").Enabled = cbol
        Me.mGenReportButton.Enabled = cbol
    End Sub
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tTimekeepingFile)
        End Set
    End Property

    Private Sub GenerateReport()
        Dim sfd As New SaveFileDialog
        Dim file As String
        file = CStr(GSCOM.SQL.ExecuteScalar("Select *  from tTimekeepingFile_Detail WHERE ID_TimeKeepingFile = " & myDT.Get(Database.Tables.tTimekeepingFile.Field.ID).ToString, Connection))
        sfd.FileName = file & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "TimekeepingFile.xls", sfd.FileName, True)
            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            Dim sqlString As String
            sqlString = "SELECT Employee, EmployeeCode ,Date ,DefaultSched ,LogsTimeIn ,LogsTimeOut FROM tTimekeepingFile_Detail WHERE ID_TimeKeepingFile = " & myDT.Get(Database.Tables.tTimekeepingFile.Field.ID).ToString

            dt = GSCOM.SQL.TableQuery(sqlString, Connection)
            UseArray(sfd.FileName, dt)
            MsgBox("Done", MsgBoxStyle.Information)

            Dim dtdel As New DataTable
            Dim qry As String
            qry = "Delete FROM tTimekeepingFile_Detail WHERE ID_TimeKeepingFile = " & myDT.Get(Database.Tables.tTimekeepingFile.Field.ID).ToString
            dtdel = GSCOM.SQL.TableQuery(qry, Connection)
            Me.Refresh()
        End If

    End Sub
    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        If vDT.Rows.Count > 0 Then
            oExcel = CreateObject("Excel.Application")
            oBook = oExcel.Workbooks.open(pFileName)
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
            oSheet = oBook.Sheets(1)
            oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray
            oBook.Save()
            oSheet = Nothing
            oBook = Nothing
            oExcel.Quit()
            oExcel = Nothing
            GC.Collect()
        End If
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
        End If
    End Sub ' ok
    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tTimekeepingFile.Field.Name, s)
            s = GetSelectString()
            s = "*"
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", myDT_Detail, s)

            myDT_Detail.AcceptChanges()
            For Each dr As DataRow In myDT_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            '  mGrid.DataSource = myDT_Detail
            Me.EndProcess()
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try

    End Sub
    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tTimekeepingFile_Detail.Field.Employee.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.Date.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.DefaultSched.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.LogsTimeIn.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.LogsTimeOut.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.OTTimeIn.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.OTTimeOut.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.ConsideredHours.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.CostCenterCode.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.OBTimeIn.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.OBTimeOut.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.Leave.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.Schedule.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.IsRD.ToString
        s &= ", " & Database.Tables.tTimekeepingFile_Detail.Field.IsSD.ToString
        Return s
    End Function

End Class
