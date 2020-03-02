Option Explicit On
Option Strict On

Imports GSCOM.Applications.InSys.Database.Menu

Module ActiveModule

#Region "NewInfo"

    Private Property HUMANRESOURCE_TrainingActivity As Object

    Friend Function NewInfo(ByVal pMenu As Database.Menu, ByVal pListing As DataTable, ByVal pID As Integer) As InfoSet
        Dim a As InfoSet = Nothing
        Dim vAllowNew As Boolean
        Dim vAllowOpen As Boolean
        vAllowNew = CBool(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.AllowNew))
        vAllowOpen = CBool(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.AllowOpen))
        If vAllowNew Or vAllowOpen Then
            Select Case pMenu

                ' Case INSYSPAYROLL_PayrollProcessing : a = New Accounting.PayrollPeriodInfo(gConnection, pListing, pID)
                Case MAINTENANCE_INSYSPEOPLE_EMPLOYEEPHOTOFILEINFO : a = New EmployeePhotoFileInfo(pMenu, gConnection, pListing, pID)
                Case ADMINISTRATIVE_EmployeeTemplate : a = New EmployeeTemplateFileInfo(pMenu, gConnection, pListing, pID)
                Case ADMINISTRATIVE_User : a = New UserInfo(pMenu, gConnection, pListing, pID)
                Case ADMINISTRATIVE_UserGroup : a = New UserGroupInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPEOPLE_EmployeeRecords201File : a = New EmployeeInfo(pMenu, gConnection, pListing, pID)
                    ' Case HumanResource_EmployeeMovement : a = New EmployeeMovementInfo(pMenu, gConnection, pListing, pID)
                    'Case MAINTENANCE_INSYSORBIT_LeaveCreditFiling : a = New LeaveCreditInfo(pMenu, gConnection, pListing, pID)
                    'Case HUMANRESOURCE_Recruitment_Vacancy : a = New VacancyInfo(pMenu, gConnection, pListing, pID) 'gLen.code 20110525
                Case INSYSPEOPLE_SeparatedEmployees : a = New EmployeeInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPEOPLE_SalaryAdjustment : a = New SalaryIncreaseInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_Loans_LoanFile : a = New ImportedLoanInfo(pMenu, gConnection, pListing, pID)
                Case ORGANIZATIONALMANAGEMENT_BankAccounts : a = New CompanyBankAcctInfo(pMenu, gConnection, pListing, pID)
                Case MAINTENANCE_INSYSORBIT_DailySchedule : a = New DailyScheduleInfo(pMenu, gConnection, pListing, pID)
                Case MEALLOG_MealLogFile : a = New MealLogFileInfo(pMenu, gConnection, pListing, pID)
                Case MEALLOG_MealSchedule : a = New MealSchedInfo(pMenu, gConnection, pListing, pID)
                    'Case MAINTENANCE_INSYSORBIT_LogDevice : a = New LogDeviceInfo(pMenu, gConnection, pListing, pID)
                    'Case FINGERPRINTENROLLMENT_Employee : a = New LogDeviceEmployeeInfo(pMenu, gConnection, pListing, pID)
                    'Case FINGERPRINTENROLLMENT_BatchFingerDataTransfer : a = New BatchFingerDataTransferInfo(pMenu, gConnection, pListing, pID)
                    'Case GIRAFFEBIOMETRICDEVICES_DeviceManager : a = New DeviceManagerInfo(pMenu, gConnection, pListing, pID)
                    'Case Payroll_13thMonth : a = New TMonthInfo(pMenu, gConnection, pListing, pID) 'gLen.code 20110516
                Case INSYSPAYROLL_AnnualReports_AlphaList : a = New AlphaListInfo(pMenu, gConnection, pListing, pID)
                    ' Case PAYROLLSYSTEM_Employee : a = New Payroll.EmployeeInfo(gConnection, pListing, pID)
                Case INSYSPAYROLL_FinalPay : a = New FinalPayInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_HDMFReports_HDMFExport : a = New HDMFExportInfo(pMenu, gConnection, pListing, pID) ' Change to Mr
                Case [INSYSPAYROLL_HDMFLoanReports_HDMFExport] : a = New HDMFLoanExportInfo(pMenu, gConnection, pListing, pID) ' Change to Mr
                Case INSYSPAYROLL_IncomeAndDeductionhardcoded : a = New IncomeAndDeductionInfo(pMenu, gConnection, pListing, pID)
                    'Case Payroll_LeaveConversion : a = New LeaveConversionInfo(pMenu, gConnection, pListing, pID)  'gLen.code 20110416
                Case INSYSPAYROLL_PayrollProcessing : a = New PayrollPeriodInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_PayrollProcessing_Payroll : a = New PayrollInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_EmployeeWorkedHoursFile : a = New PayrollFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_PayrollTransitionFile : a = New PayrollTransitionInfo(pMenu, gConnection, pListing, pID)
                    ' Case GIRAFFEPAYROLL_PHICReports_PhilhealthQuarterlyRF1 : a = New RF1Info(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_ServiceCharge : a = New ServiceChargeInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_SSSReports_SSSLoan : a = New SSSLoanInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_SSSReports_SSSNet : a = New SSSNETInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_PHICReports_PhilhealthMonthlyRF1 : a = New RF1MonthlyInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_BankExport : a = New BankExportInfo(pMenu, gConnection, pListing, pID)

                Case SYSTEM_SystemApplication : a = New SystemApplicationInfo(pMenu, gConnection, pListing, pID)

                Case INSYSORBIT_TimeandAttendance_EmployeeAttendanceLogFile : a = New EmployeeAttendanceLogFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimeandAttendance_EmployeeTimesheetFile : a = New EmployeeDailyScheduleFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimeandAttendance_ManualAttendanceInput : a = New ManualAttendanceLogInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_EmployeeDailySchedule : a = New EmployeeDailyScheduleInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_TimeandAttendanceProcessing : a = New EmployeeDailyScheduleViewInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_Leave : a = New LeaveInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_LeaveFile : a = New LeaveFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_OfficialBusiness : a = New OBInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_MissedLog : a = New OBInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_OfficialBusinessFile : a = New OBFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_MissedLogFile : a = New OBFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_OvertimeFile : a = New OTFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_TimekeepingItems_Overtime : a = New OvertimeInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_WorkSchedule_RestDayFile : a = New EmployeeRestDayFileInfo(pMenu, gConnection, pListing, pID)
                    'Case TIMEKEEPING_Schedule_ScheduleAssignment : a = New ScheduleAssignmentInfo(pMenu, gConnection, pListing, pID)   'UNKNOWN???

                Case INSYSORBIT_WorkSchedule_ScheduleFile : a = New ScheduleFileInfo(pMenu, gConnection, pListing, pID)
                    'Case HUMANRESOURCE_TrainingActivity : a = New TrainingActivityInfo(pMenu, gConnection, pListing, pID)

                Case ACCOUNTING_JournalVoucher : a = New JournalVoucherInfo(pMenu, gConnection, pListing, pID)

                    'Case CRM_SalesandMarketing_EmailBlast : a = New EmailBlastInfo(pMenu, gConnection, pListing, pID)
                Case ALERT_Alert : a = New AlterInfo(pMenu, gConnection, pListing, pID)
                    'Case MAINTENANCE_Payroll_TaxTableFile : a = New TaxFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSORBIT_WorkSchedule_CostCenterAssignmentFile : a = New EmployeeCostCenterAssignmentFileInfo(pMenu, gConnection, pListing, pID)

                Case INSYSORBIT_TimekeepingItems_TimeKeepingFile : a = New TimeKeepingFileInfo(pMenu, gConnection, pListing, pID)
                Case INSYSPAYROLL_AnnualReports_AlphalistTextGenerator : a = New AlphalistTextGeneratorInfo(pMenu, gConnection, pListing, pID)

                Case CType(1000011, Database.Menu)
                    a = New EmailBlastInfo(pMenu, gConnection, pListing, pID)
                Case Else
                    'double check if tablename was set
                    If nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.TableName).ToString <> "" Then
                        a = New ZInfo(pMenu, gConnection, pListing, pID)
                    Else
                        MsgBox("Table name is not specified", MsgBoxStyle.Exclamation)
                    End If
            End Select
        Else
            MsgBox("Creating or viewing information for " & nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.Name).ToString & " is not available.", MsgBoxStyle.Information)
            a = Nothing
        End If
        If a IsNot Nothing Then
            'a.Text = pMenu.ToString.Replace("_", " -> ")
            a.Text = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.Name).ToString
            'ROBBIE 20070517 ---------------\
            'If Not CBool(nDB.GetMenuValue(pMenu, Database.Tables.tUserGroupMenu.Field.AllowEdit.ToString)) And Not CBool(nDB.GetMenuValue(pMenu, Database.Tables.tUserGroupMenu.Field.AllowNew.ToString)) Then
            '    a.MakeReadOnly()
            'End If
            'a.AllowNew = CBool(Database.MenuTable.Select("ID=" & GSCOM.SQL.SQLFormat(CInt(pMenu)))(0).Item("AllowNew"))
            'ROBBIE 20070517 ---------------/

            AddInfoSet(a, pMenu)
            'If My.Computer.Screen.WorkingArea.Width > 1024 Then
            '    a.Width = 1024
            'End If
            'If My.Computer.Screen.WorkingArea.Height > 768 Then
            '    a.Height = 768
            'End If

            a.Size = gInfoSize

            'ROBBIE: DONT REMOVE-----------\
            'Dim c As Control
            'c = gMainForm.tcMain
            'a.Rect = c.RectangleToScreen(c.ClientRectangle)  '  c.RectangleToScreen(New Rectangle(c.Location, c.Size))
            'ROBBIE: DONT REMOVE-----------/
            'a.TopBackColor = GSCOM.Grafix.ColorFromRGB(CStr(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.DarkColorRGB)))
            'a.BackColor = GSCOM.Grafix.ColorFromRGB(CStr(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.ColorRGB)))
        End If
        Return a

    End Function

#End Region

End Module
