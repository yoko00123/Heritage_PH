Option Explicit On
Option Strict Off



Friend Class EmployeeCostCenterAssignmentFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeCostCenterAssignmentFile(Connection)
    Private mtDetail As GSCOM.SQL.ZDataTable
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
        mtDetail = DirectCast(mDataset.Tables("tEmployeeCostCenterAssignmentFile_Detail"), GSCOM.SQL.ZDataTable)
        mImportButton = Me.GetStripButton("Import File")
        AddHandler Me.GetStripButton("Generate Template").Click, AddressOf GenTemplate
        AddHandler mImportButton.Click, AddressOf ImportFile
        '============================================================================
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtDetail)
    End Sub


#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtDetail.Rows.Count > 0)
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
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtDetail, s)

            mtDetail.AcceptChanges()
            For Each dr As DataRow In mtDetail.Select()
                If dr.RowState = DataRowState.Unchanged Then '
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtDetail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileTable
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileAdapter

        sfd.FileName = "CostCenterAssignmentFile.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            BeginProcess("Generating Template...")
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "CostCenterAssignmentFile.xls", sfd.FileName, True)
            Try
                EndProcess("Done")
            Catch ex As Exception
                EndProcess(ex.Message, False)
            End Try

        End If
    End Sub

    
#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeCostCenterAssignmentFile)
        End Set
    End Property


#End Region


End Class
