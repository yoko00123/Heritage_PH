Option Explicit On
Option Strict Off



Friend Class PayrollFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tPayrollFile(Connection)
    Private mtPayrollFile_Detail As GSCOM.SQL.ZDataTable 'New Database.Tables.tPayrollFile_Detail(Connection)   'gLen.code 20110416
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.LeaveFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    '   Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(mtPayrollFile_Detail)   'gLen.code 20110416
        End With
        InitControl(pMenu)

        'gLen.code 20110416
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tPayrollFile.Field.ID)
        'cdc = mtPayrollFile_Detail.Columns(Database.Tables.tPayrollFile_Detail.Field.ID_PayrollFile)
        'rel = mDataset.Relations.Add(pdc, cdc)
        mtPayrollFile_Detail = DirectCast(Me.mDataset.Tables("tPayrollFile_Detail"), GSCOM.SQL.ZDataTable)
        '###

        myDT.Columns(Database.Tables.tPayrollFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        mGenTemplateButton = Me.GetStripButton("Generate Template") 'MyBase.AddButton("Generate Template", gMainForm.imgList.Images("GenerateTemplate.png"), AddressOf GenTemplate)
        AddHandler mGenTemplateButton.Click, AddressOf GenTemplate
        mImportButton = Me.GetStripButton("Import File") 'MyBase.AddButton("Import File", gMainForm.imgList.Images("ImportFile.png"), AddressOf ImportFile)
        AddHandler mImportButton.Click, AddressOf ImportFile
        ' mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ApplyFile)
        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtPayrollFile_Detail)
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtPayrollFile_Detail.Rows.Count > 0)
        Me.GetStripButton("Generate Template").Enabled = True
    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tPayrollFile)
        End Set
    End Property



#End Region


#Region "Template"
    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim sfd As New SaveFileDialog
        sfd.FileName = myDT.Get(Database.Tables.tPayrollFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.OBFileTable
        Dim a As New GSCOM.Applications.InSys.Database.Templates.OBFileAdapter
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "PayrollFile.xls", sfd.FileName, True)


            IO.File.SetAttributes(sfd.FileName, IO.FileAttributes.Normal)
            Dim dt As New DataTable
            
            'Dim s As String
            's &= "SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ")"

            Dim s As String = Me.PassParameters("SELECT Code,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'Employee Code', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Employee Code")
            
            'UseArray(sfd.FileName, dt)


            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub UseArray(ByVal pFileName As String, ByVal vDT As DataTable)
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

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


        oSheet = oBook.Worksheets(1)
        
        oSheet.Range("A2").Resize(vDT.Rows.Count, vDT.Columns.Count).Value = DataArray

        
        oBook.Save()

        oSheet = Nothing
        oBook = Nothing
        oExcel.Quit()
        oExcel = Nothing
        GC.Collect()
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
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tPayrollFile.Field.Name, s)
            GSCOM.SQL.GetExcelTable(FileName, "Sheet1", mtPayrollFile_Detail, s)

            mtPayrollFile_Detail.AcceptChanges()
            For Each dr As DataRow In mtPayrollFile_Detail.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtPayrollFile_Detail
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

        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            dt = SQL.GetExcelTable(FileName, "Sheet1")
            dtemp.ClearThenFill("")
            dtpay.ClearThenFill("")
            mtPayrollFile_Detail.Rows.Clear()
            Dim total As Decimal = 0
            For Each dr As DataRow In dt.Rows
                nr = mtPayrollFile_Detail.NewRow()

                'gLen.code 20110416
                Dim s As String
                For Each dc As DataColumn In mtPayrollFile_Detail.Columns
                    If Not dc.ColumnName.ToString.Contains("ID") Then
                        If dc.ColumnName.ToString = "EmployeeCode" Or dc.ColumnName.ToString = "Employee" Then
                            nr(dc.ColumnName.ToString) = dr.Item(dc.ColumnName.ToString)
                        Else
                            s = dr(dc.ColumnName.ToString).ToString
                            If s = "" Then
                                s = "0.00"
                            End If
                            nr(dc.ColumnName.ToString) = s
                        End If
                    End If
                Next

                'nr("EmployeeCode") = dr.Item("EmployeeCode")
                'nr("Employee") = dr.Item("Employee")
                'Dim s As String
                's = dr("REG").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("REG").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.REG.ToString) = s


                's = dr("TARDY").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("TARDY").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.TARDY.ToString) = s

                's = dr("UT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("UT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.UT.ToString) = s

                's = dr("LWOP").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LWOP").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LWOP.ToString) = s

                's = dr("OT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("OT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.OT.ToString) = s

                's = dr("RD").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("RD").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.RD.ToString) = s

                's = dr("RDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("RDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.RDOT.ToString) = s

                's = dr("SH").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SH").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SH.ToString) = s

                's = dr("SHOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHOT.ToString) = s

                's = dr("SHR").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHR").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHR.ToString) = s

                's = dr("SHROT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHROT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHROT.ToString) = s

                's = dr("LH").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LH").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LH.ToString) = s

                's = dr("LHOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHOT.ToString) = s

                's = dr("LHR").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHR").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHR.ToString) = s

                's = dr("LHROT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHROT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHROT.ToString) = s

                's = dr("ND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("ND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.ND.ToString) = s

                's = dr("NDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("NDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.NDOT.ToString) = s

                's = dr("RDND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("RDND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.RDND.ToString) = s

                's = dr("RDNDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("RDNDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.RDNDOT.ToString) = s

                's = dr("SHND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHND.ToString) = s

                's = dr("SHNDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHNDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHNDOT.ToString) = s

                's = dr("SHRND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHRND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHRND.ToString) = s


                's = dr("SHRNDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SHRNDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SHRNDOT.ToString) = s

                's = dr("LHND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHND.ToString) = s

                's = dr("LHNDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHNDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHNDOT.ToString) = s

                's = dr("LHRND").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHRND").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHRND.ToString) = s

                's = dr("LHRNDOT").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("LHRNDOT").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.LHRNDOT.ToString) = s

                's = dr("VL").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("VL").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.VL.ToString) = s

                's = dr("SL").ToString
                'If s = "" Then
                '    s = "0.00"
                'Else
                '    s = dr("SL").ToString
                'End If
                'nr(Database.Tables.tPayrollFile_Detail.Field.SL.ToString) = s
                '###

                mtPayrollFile_Detail.Rows.Add(nr)

            Next

            Dim dg As DataGridView
            dg = Me.GetDataGridView(mtPayrollFile_Detail)
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

    'Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        ' GSCOM.SQL.ExecuteNonQuery("EXEC p_EmployeeTemplateFile " & myDT.Get(Database.Tables.tEmployeeTemplateFile.Field.ID).ToString & " , " & gUser, Connection)
    '        MsgBox("Finished applying the file.", MsgBoxStyle.Information)
    '    End If
    'End Sub
#End Region

End Class
