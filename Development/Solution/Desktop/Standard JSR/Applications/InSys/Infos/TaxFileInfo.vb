Option Explicit On
Option Strict Off



Friend Class TaxFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tTaxFile(Connection)
    Private mtTaxFile_Detail As GSCOM.SQL.ZDataTable
    Private mControl As New InSys.DataControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private WithEvents mGrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)

        End With
        InitControl(pMenu)
        mtTaxFile_Detail = DirectCast(mDataset.Tables("tTaxFile_Detail"), GSCOM.SQL.ZDataTable)
        mImportButton = Me.GetStripButton("Import File")
        AddHandler Me.GetStripButton("Generate Template").Click, AddressOf GenTemplate
        AddHandler mImportButton.Click, AddressOf ImportFile
        '============================================================================
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtTaxFile_Detail)
    End Sub


#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtTaxFile_Detail.Rows.Count > 0)
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
            'Me.mImportButton.Enabled = False
            'Me.mApplyButton.Enabled = False
        End If
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        's = "TaxExcemptionCode, PayrollFrequencyCode, Bracket, Lbound, Ubound, Fix, Rate"
        s = "*"
        Return s
    End Function

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tTaxFile.Field.Name, s)
            s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtTaxFile_Detail, s)

            mtTaxFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtTaxFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then '
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtTaxFile_Detail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileTable
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileAdapter

        sfd.FileName = "TaxTable.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            BeginProcess("Generating Template...")
            'IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "TaxTable.xls", sfd.FileName, True)
            Try
                Dim dt As DataTable = GSCOM.SQL.TableQuery("SELECT ExemptionCode TaxExcemptionCode,PayrollFrequencyCode,Bracket,Lbound,Ubound,Fix,Rate FROM vTax", Connection)
                UseArray(sfd.FileName, dt)
                'MsgBox("Done", MsgBoxStyle.Information)
                EndProcess("Done")
            Catch ex As Exception
                EndProcess(ex.Message, False)
            End Try

        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        'Start a new workbook in Excel.
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.ADD
        Dim DataArray(vDT.Rows.Count - 1, vDT.Columns.Count) As Object
        Dim ColName(2, vDT.Columns.Count) As Object
        Dim r, c As Integer
        r = 0
        For Each drx As DataRow In vDT.Rows
            c = 0
            For Each col As DataColumn In vDT.Columns
                If col.ColumnName <> "ID" Then
                    DataArray(r, c) = drx.Item(col.ColumnName)
                    c += 1
                End If
            Next
            r += 1
        Next
        Dim i As Integer = 0
        For Each dc As DataColumn In vDT.Columns
            If dc.ColumnName <> "ID" Then
                ColName(0, i) = dc.ColumnName
                i += 1
            End If
        Next
        'Add headers to the worksheet on row 1.
        oSheet = oBook.Worksheets(1)


        'Transfer the array to the worksheet starting at cell A2.
        oSheet.Range("A1").Resize(1, vDT.Columns.Count).Value = ColName
        oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

        'Save the workbook and quit Excel.
        'oBook.Save()
        If IO.File.Exists(pFileName) Then
            IO.File.Delete(pFileName)
        End If
        oBook.SaveAs(pFileName)
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
            myDT = CType(value, Database.Tables.tTaxFile)
        End Set
    End Property


#End Region

   
End Class
