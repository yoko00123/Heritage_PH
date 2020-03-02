Option Explicit On
Option Strict On



Friend Class PayrollTransitionInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tPayrollTransitionFile(Connection)
    Private mtPayrollTransitionFile_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tPayrollTransitionFile_Detail(Connection)   'gLen.code 20110416
    'Private mControl As New InSys.DataControl 'Private mControl As New nDB.LeaveFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtPayrollTransitionFile_Detail)
        End With
        InitControl(pMenu)

        'gLen.code
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tPayrollTransitionFile.Field.ID)
        'cdc = mtPayrollTransitionFile_Detail.Columns(Database.Tables.tPayrollTransitionFile_Detail.Field.ID_PayrollTransitionFile)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mtPayrollTransitionFile_Detail = DirectCast(Me.mDataset.Tables("tPayrollTransitionFile_Detail"), GSCOM.SQL.ZDataTable)
        '###

        ' mGenTemplateButton = Me.GetStripButton("Generate Template") 'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("GenerateTemplate.png"), AddressOf GenTemplate)
        ' AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        ' mImportButton = Me.GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        ' AddHandler mImportButton.Click, AddressOf ImportFile
        'mApplyButton = Me.GetStripButton("Apply File") 'MyBase.AddButton("Apply File", gMainForm.imgList.Images("ApplyFile.png"), AddressOf ApplyFile)
        ' AddHandler mApplyButton.Click, AddressOf ApplyFile

        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtPayrollTransitionFile_Detail)
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tPayrollTransitionFile)
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

#Region "Template"
    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        '  Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        'Dim a As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileAdapter

        sfd.FileName = "PayrollTransitionFile.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "PayrollTransitionFile.xls", sfd.FileName, True)
            MsgBox("Done", MsgBoxStyle.Information)
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
            Me.mImportButton.Enabled = False
        End If
    End Sub

#End Region

    Private Sub TransferExcelDataOLD(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tPayrollTransitionFile.Field.Name, s)
            '  s = GetSelectString()
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtPayrollTransitionFile_Detail, s)

            mtPayrollTransitionFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtPayrollTransitionFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtPayrollTransitionFile_Detail
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub
#Region "TransferExcelData"
    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        Dim dtemp As New Database.Tables.tEmployee(Connection)
        Dim dtpay As New Database.Tables.tPayrollItem(Connection)
        Dim nr As DataRow
        ' Dim r1 As DataRow()
        ' Dim r2 As DataRow()
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            dt = SQL.GetExcelTable(FileName, "Sheet1")
            dtemp.ClearThenFill("")
            dtpay.ClearThenFill("")
            mtPayrollTransitionFile_Detail.Rows.Clear()
            Dim total As Decimal = 0
            For Each dr As DataRow In dt.Rows
                nr = mtPayrollTransitionFile_Detail.NewRow()

                nr("EmployeeCode") = dr.Item("EmployeeCode")

                Dim s As String
                s = dr("REG").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("REG").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.REG.ToString) = s


                s = dr("Tardy").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("Tardy").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.Tardy.ToString) = s

                s = dr("UT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("UT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.UT.ToString) = s

                s = dr("LWOP").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LWOP").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LWOP.ToString) = s

                s = dr("OT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("OT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.OT.ToString) = s

                s = dr("RD").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("RD").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.RD.ToString) = s

                s = dr("RDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("RDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.RDOT.ToString) = s

                s = dr("SH").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SH").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SH.ToString) = s

                s = dr("SHOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHOT.ToString) = s

                s = dr("SHR").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHR").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHR.ToString) = s

                s = dr("SHROT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHROT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHROT.ToString) = s

                s = dr("LH").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LH").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LH.ToString) = s

                s = dr("LHOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHOT.ToString) = s

                s = dr("LHR").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHR").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHR.ToString) = s

                s = dr("LHROT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHROT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHROT.ToString) = s

                s = dr("ND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("ND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.ND.ToString) = s

                s = dr("NDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("NDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.NDOT.ToString) = s

                s = dr("RDND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("RDND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.RDND.ToString) = s

                s = dr("RDNDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("RDNDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.RDNDOT.ToString) = s

                s = dr("SHND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHND.ToString) = s

                s = dr("SHNDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHNDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHNDOT.ToString) = s

                s = dr("SHRND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHRND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHRND.ToString) = s


                s = dr("SHRNDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SHRNDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SHRNDOT.ToString) = s

                s = dr("LHND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHND.ToString) = s

                s = dr("LHNDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHNDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHNDOT.ToString) = s

                s = dr("LHRND").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHRND").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHRND.ToString) = s

                s = dr("LHRNDOT").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("LHRNDOT").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.LHRNDOT.ToString) = s

                s = dr("VL").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("VL").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.VL.ToString) = s

                s = dr("SL").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SL").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SL.ToString) = s
                '-------------------------------------------------------------------------------------------
                s = dr("Basic13thMonth").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("Basic13thMonth").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.Basic13thMonth.ToString) = s

                s = dr("WithholdingTax").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("WithholdingTax").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.WithholdingTax.ToString) = s

                s = dr("EL").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("EL").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.EL.ToString) = s

                s = dr("ML").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("ML").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.ML.ToString) = s

                s = dr("SSSEE").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SSSEE").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SSSEE.ToString) = s

                s = dr("SSSER").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SSSER").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SSSER.ToString) = s

                s = dr("PHICEE").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("PHICEE").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.PHICEE.ToString) = s

                s = dr("PHICER").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("PHICER").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.PHICER.ToString) = s

                s = dr("HDMFEE").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("HDMFEE").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.HDMFEE.ToString) = s

                s = dr("HDMFER").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("HDMFER").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.HDMFER.ToString) = s

                s = dr("VLConversion").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("VLConversion").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.VLConversion.ToString) = s

                s = dr("SLConversion").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("SLConversion").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.SLConversion.ToString) = s

                s = dr("DeMinimisBenefits").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("DeMinimisBenefits").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.DeMinimisBenefits.ToString) = s

                s = dr("HazardPay").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("HazardPay").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.HazardPay.ToString) = s

                s = dr("HospitalInsurance").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("HospitalInsurance").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.HospitalInsurance.ToString) = s

                s = dr("Representation").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("Representation").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.Representation.ToString) = s

                s = dr("TransportationAllowance").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("TransportationAllowance").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.TransportationAllowance.ToString) = s

                s = dr("HousingAllowance").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("HousingAllowance").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.HousingAllowance.ToString) = s

                s = dr("ProfitSharing").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("ProfitSharing").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.ProfitSharing.ToString) = s

                s = dr("DirectorFee").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("DirectorFee").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.DirectorFee.ToString) = s

                s = dr("TaxRefund").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("TaxRefund").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.TaxRefund.ToString) = s

                s = dr("ECOLA").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("ECOLA").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.ECOLA.ToString) = s

                s = dr("Commission").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("Commission").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.Commission.ToString) = s

                s = dr("CanteenDues").ToString
                If s = "" Then
                    s = "0.00"
                Else
                    s = dr("CanteenDues").ToString
                End If
                nr(Database.Tables.tPayrollTransitionFile_Detail.Field.CanteenDues.ToString) = s

                mtPayrollTransitionFile_Detail.Rows.Add(nr)

            Next

            Dim dg As DataGridView
            dg = Me.GetDataGridView(mtPayrollTransitionFile_Detail)
            dg.Refresh()

            For Each d As DataGridViewRow In dg.Rows
                dg.Update()
            Next

            Me.EndProcess("Finish downloading file [" & FileName & "].")
        Catch ex As OleDb.OleDbException
            Me.EndProcess("Error occur while importing data. This is due to file connection, please check if sheet name is Sheet1.", False)
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

#End Region

    Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC pPayrollTransitionFileApply " & myDT.Get(Database.Tables.tPayrollTransitionFile.Field.ID).ToString, Connection)
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub
#End Region

End Class
