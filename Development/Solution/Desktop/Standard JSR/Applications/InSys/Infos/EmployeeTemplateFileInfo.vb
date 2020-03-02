Option Explicit On
Option Strict On



Friend Class EmployeeTemplateFileInfo

    Inherits InfoSet

    Private myDT As New Database.Tables.tEmployeeTemplateFile(Connection)
    '  Private mtEmployeeTemplateFile_Detail As New Database.Tables.tEmployeeTemplateFile_Detail(Connection)

    Private mtEmployeeTemplateFileDetail_PersonalInfo As GSCOM.SQL.ZDataTable
    Private mtEmployeeTemplateFileDetail_EducationalBackground As GSCOM.SQL.ZDataTable
    Private mtEmployeeTemplateFileDetail_CompanyInfo As GSCOM.SQL.ZDataTable
    Private mtEmployeeTemplateFileDetail_Dependent As GSCOM.SQL.ZDataTable
    Private mtEmployeeTemplateFileDetail_EmploymentHistory As GSCOM.SQL.ZDataTable

    Private mControl As New InSys.DataControl 'Private mControl As New nDB.EmployeeLeaveCreditFileControl
    Private mImportButton As ToolStripButton
    Private mGenTemplateButton As ToolStripButton
    Private mApplyButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)

        End With
        InitControl(pMenu)

        mtEmployeeTemplateFileDetail_PersonalInfo = DirectCast(Me.mDataset.Tables("tEmployeeTemplateFileDetail_PersonalInfo"), GSCOM.SQL.ZDataTable)
        mtEmployeeTemplateFileDetail_EducationalBackground = DirectCast(Me.mDataset.Tables("tEmployeeTemplateFileDetail_EducationalBackground"), GSCOM.SQL.ZDataTable)
        mtEmployeeTemplateFileDetail_CompanyInfo = DirectCast(Me.mDataset.Tables("tEmployeeTemplateFileDetail_CompanyInfo"), GSCOM.SQL.ZDataTable)
        mtEmployeeTemplateFileDetail_Dependent = DirectCast(Me.mDataset.Tables("tEmployeeTemplateFileDetail_Dependent"), GSCOM.SQL.ZDataTable)
        mtEmployeeTemplateFileDetail_EmploymentHistory = DirectCast(Me.mDataset.Tables("tEmployeeTemplateFileDetail_EmploymentHistory"), GSCOM.SQL.ZDataTable)


        mGenTemplateButton = MyBase.AddButton("Generate Template", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf GenTemplate)
        mImportButton = MyBase.AddButton("Import File", gMainForm.imgList.Images("_ScheduleFile.ico"), AddressOf ImportFile)
        mApplyButton = MyBase.AddButton("Apply File", gMainForm.imgList.Images("misc.a.ico"), AddressOf ApplyFile)

        Me.ReloadAfterCommit = True
        AfterNew()
        mGrid = Me.GetDataGridView(mtEmployeeTemplateFileDetail_PersonalInfo)
        mGrid = Me.GetDataGridView(mtEmployeeTemplateFileDetail_EducationalBackground)
        mGrid = Me.GetDataGridView(mtEmployeeTemplateFileDetail_CompanyInfo)
        mGrid = Me.GetDataGridView(mtEmployeeTemplateFileDetail_Dependent)
        mGrid = Me.GetDataGridView(mtEmployeeTemplateFileDetail_EmploymentHistory)

        Me.UseTransaction = False
    End Sub

#Region "LoadInfo"

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtEmployeeTemplateFileDetail_PersonalInfo.Rows.Count > 0)
        mApplyButton.Enabled = (pID > 0)
    End Sub

#End Region


#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeTemplateFile)
        End Set
    End Property



#End Region


    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.EmployeeLeaveCreditFileAdapter

        sfd.FileName = "Employee Template File.xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Employee Template File.xls", sfd.FileName, True)
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
            Me.mImportButton.Enabled = False
            ' Me.mApplyButton.Enabled = False
        End If
    End Sub

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tEmployeeTemplateFile.Field.Name, s)
            s = GetSelectString()
            s = "*"
            GSCOM.SQL.GetExcelTable(FileName, "PersonalInfo", mtEmployeeTemplateFileDetail_PersonalInfo, s)

            mtEmployeeTemplateFileDetail_PersonalInfo.AcceptChanges()
            For Each dr As DataRow In mtEmployeeTemplateFileDetail_PersonalInfo.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeTemplateFileDetail_PersonalInfo
            Me.EndProcess()
            '-----------------------------------------------
            Dim ed As String
            ed = GetSelectString_Education()
            ed = "*"
            GSCOM.SQL.GetExcelTable(FileName, "EducationalBackground", mtEmployeeTemplateFileDetail_EducationalBackground, ed)

            mtEmployeeTemplateFileDetail_EducationalBackground.AcceptChanges()
            For Each dr As DataRow In mtEmployeeTemplateFileDetail_EducationalBackground.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeTemplateFileDetail_EducationalBackground
            Me.EndProcess()

            ''--------------------------
            Dim c As String
            c = GetSelectString_Company()
            c = "*"
            GSCOM.SQL.GetExcelTable(FileName, "CompanyInfo", mtEmployeeTemplateFileDetail_CompanyInfo, c)

            mtEmployeeTemplateFileDetail_CompanyInfo.AcceptChanges()
            For Each dr As DataRow In mtEmployeeTemplateFileDetail_CompanyInfo.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeTemplateFileDetail_CompanyInfo
            Me.EndProcess()
            ''----------------------------------------------------------------------
            Dim dep As String
            dep = GetSelectString_Dependent()
            dep = "*"
            GSCOM.SQL.GetExcelTable(FileName, "PersonaDependent", mtEmployeeTemplateFileDetail_Dependent, dep)

            mtEmployeeTemplateFileDetail_Dependent.AcceptChanges()
            For Each dr As DataRow In mtEmployeeTemplateFileDetail_Dependent.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeTemplateFileDetail_Dependent
            Me.EndProcess()
            ''---------------------------------------------------------------
            Dim emp As String
            emp = GetSelectString_EmployementHistory()
            emp = "*"
            GSCOM.SQL.GetExcelTable(FileName, "EmploymentHistory", mtEmployeeTemplateFileDetail_EmploymentHistory, emp)

            mtEmployeeTemplateFileDetail_EmploymentHistory.AcceptChanges()
            For Each dr As DataRow In mtEmployeeTemplateFileDetail_EmploymentHistory.Select()
                If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                    dr.SetAdded()
                End If
            Next
            mGrid.DataSource = mtEmployeeTemplateFileDetail_EmploymentHistory
            Me.EndProcess()
            ''---------------------------------------------------------------
        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.EmployeeCode.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.AccessNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.LastName.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.FirstName.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.MiddleName.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Suffix.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.NickName.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Nationality.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Citizenship.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.BirthDate.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.BirthPlace.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Gender.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Height.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Weight.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.CivilStatus.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.SSSStatus.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.BloodType.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HomeAddress.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HomePhoneNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.MobileNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HomeAddressRegion.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HomeAddressArea.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HomeAddressCity.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ProvincialAddress.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ProvincialPhoneNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ProvincialAddressRegion.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ProvincialAddressArea.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ProvincialAddressCity.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.EmailAddress1.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.EmailAddress2.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.HDMFNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.SSSNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.GSISNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.TinNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.PhilHealthNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.DriversLicenseNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.PassportNo.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ContactPerson.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ContactAddress.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ContactNumber.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.ContactPersonRelationship.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Spouse.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.SpouseBirthdate.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.SpouseEmployer.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.SpouseOccupation.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Father.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.FatherBirthday.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.FatherOccupation.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.Mother.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.MotherBirthday.ToString
        s &= ", " & Database.Tables.tEmployeeTemplateFileDetail_PersonalInfo.Field.MotherOccupation.ToString

        Return s
    End Function

    Private Function GetSelectString_Education() As String
        Dim ed As String
        ed = Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.EmployeeCode.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.School.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.SchoolAddress.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.SchoolLevel.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.Couse.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.YearGraduated.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.EducationalAttainment.ToString
        ed &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EducationalBackground.Field.Month.ToString
        Return ed
    End Function
    Private Function GetSelectString_Company() As String
        Dim c As String
        c = Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.EmployeeCode.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Company.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Address.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.ZipCode.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.TIN.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.SSSNo.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.PhilHealthNo.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.HDMFNo.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.email.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Branch.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Division.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Department.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.Designation.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.EmployeeStatus.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.JobClass.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.CostCenter.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.IsRequiredToLog.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.DateHired.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.DateRegularized.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.PayrollScheme.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.PayrollFrequency.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.PayrollStatus.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.MonthlySalary.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.DailySalary.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.HourlySalary.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.DaysperYear.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.HoursperDay.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.CompanyBank.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.CompanyBankNo.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.AcctNo.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.CompanyBank2.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.CompanyBankNo2.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.AcctNo2.ToString
        c &= ", " & Database.Tables.tEmployeeTemplateFileDetail_CompanyInfo.Field.DaysperMonth.ToString
        Return c
    End Function
    Private Function GetSelectString_Dependent() As String
        Dim dep As String
        dep = Database.Tables.tEmployeeTemplateFileDetail_Dependent.Field.EmployeeCode.ToString
        dep &= ", " & Database.Tables.tEmployeeTemplateFileDetail_Dependent.Field.DependentName.ToString
        dep &= ", " & Database.Tables.tEmployeeTemplateFileDetail_Dependent.Field.DependentRelationship.ToString
        dep &= ", " & Database.Tables.tEmployeeTemplateFileDetail_Dependent.Field.DependentBirthDate.ToString
        dep &= ", " & Database.Tables.tEmployeeTemplateFileDetail_Dependent.Field.DependentGender.ToString
        Return dep
    End Function
    Private Function GetSelectString_EmployementHistory() As String
        Dim emp As String
        emp = Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.EmployeeCode.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployer.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerSpecialization.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerCompanyIndustry.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousDepartment.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousDesignation.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousJobClass.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousJobRole.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerPositionLevel.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.StartDate.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.EndDate.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousMonthlyRate.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerNo.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerAddress.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerZipCode.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerImmediateSupervisor.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerContactNo.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerTIN.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.PreviousEmployerBenefits.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.ReasonForLeaving.ToString
        emp &= ", " & Database.Tables.tEmployeeTemplateFileDetail_EmploymentHistory.Field.YearsOfExperience.ToString
        Return emp
    End Function


    Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GSCOM.SQL.ExecuteNonQuery("EXEC p_EmployeeTemplateFile " & myDT.Get(Database.Tables.tEmployeeTemplateFile.Field.ID).ToString & " , " & gUser, Connection)
            MsgBox("Finished applying the file.", MsgBoxStyle.Information)
        End If
    End Sub

End Class
