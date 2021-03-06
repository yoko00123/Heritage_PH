SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------- CREATE PROCEDURE FOR DATA HISTORY
CREATE PROCEDURE [dbo].[pDataHistory](@Date DATETIME)
AS 
BEGIN	

BEGIN TRY
BEGIN TRANSACTION

--DECLARE @Date DATETIME, @ID_Company INT  
--SET @Date = GETDATE()

DECLARE @RetDateTK DATETIME --, @RetDatePayroll DATETIME
SET @RetDateTK =  DATEADD(DAY,(CAST(dbo.fGetSetting('DataRetentionNumberOfDaysTK') AS INT)*-1),@Date)
--SET @RetDatePayroll = DATEADD(DAY,(CAST(dbo.fGetSetting('DataRetentionNumberOfDaysPayroll') AS INT)*-1),@Date)

--------------------------------------------------------------------------| Insert Employee Daily Schedule View
INSERT INTO dbo.tEmployeeDailyScheduleView_History
(
    ID_EmployeeDailyScheduleView
  , Code
  , [Name]
  , StartDate
  , EndDate
  , ID_Company
  , ID_Branch
  , ID_PayrollFrequency
  , ID_Department
  , ID_Designation
  , ID_EmployeeStatus
  , ID_Gender
  , ID_Employee
  , ID_Month
  , [Year]
  , SeqNo
  , IsActive
  , Comment
  , ID_Transaction_Created
  , ID_Transaction_Modified
  , DateTimeCreated
  , DateTimeModified
  , DateCreated
  , DateModified
  , IsFinalized
  , SalesProjection
  , ManPowerBudgetPercentage
  , ManPowerBudgetAmt
  , ManPowerComputedAmt
  , ManPowerDifferenceAmt
  , ManPowerDifferencePercentage
  , ID_CostCenter
  , ID_PayrollClassifi
)
SELECT 
  	edsv.ID
  , edsv.Code
  , edsv.[Name]
  , edsv.StartDate
  , edsv.EndDate
  , edsv.ID_Company
  , edsv.ID_Branch
  , edsv.ID_PayrollFrequency
  , edsv.ID_Department
  , edsv.ID_Designation
  , edsv.ID_EmployeeStatus
  , edsv.ID_Gender
  , edsv.ID_Employee
  , edsv.ID_Month
  , edsv.[Year]
  , edsv.SeqNo
  , edsv.IsActive
  , edsv.Comment
  , edsv.ID_Transaction_Created
  , edsv.ID_Transaction_Modified
  , edsv.DateTimeCreated
  , edsv.DateTimeModified
  , edsv.DateCreated
  , edsv.DateModified
  , edsv.IsFinalized
  , edsv.SalesProjection
  , edsv.ManPowerBudgetPercentage
  , edsv.ManPowerBudgetAmt
  , edsv.ManPowerComputedAmt
  , edsv.ManPowerDifferenceAmt
  , edsv.ManPowerDifferencePercentage
  , edsv.ID_CostCenter
  , edsv.ID_PayrollClassifi
FROM dbo.tEmployeeDailyScheduleView AS edsv
WHERE edsv.StartDate <= @RetDateTK AND edsv.EndDate <= @RetDateTK  

--------------------------------------------------------------------------| Insert Employee Daily Schedule
INSERT INTO dbo.tEmployeeDailySchedule_History
(
    ID_EmployeeDailySchedule
  , [Date]
  , ID_DailySchedule
  , ID_Employee
  , ID_ImportedSchedule
  , ID_Attribute
  , REG
  , EXT
  , OT
  , ND
  , NDOT
  , TARDY
  , UT
  , IsRD
  , TimeIn
  , [TimeOut]
  , IsForComputation
  , ID_LeavePayrollItem
  , ID_FirstHalfLeavePayrollItem
  , ID_SecondHalfLeavePayrollItem
  , Comment
  , IsAbsent
  , ActualTardy
  , IsActualAbsent
  , Absences
  , LeaveWithPay
  , FirstHalfLeaveWithPay
  , SecondHalfLeaveWithPay
  , OffsetREG
  , OffsetOT
  , OffsetND
  , OffsetNDOT
  , ComputedREG
  , ComputedOT
  , ComputedND
  , ComputedNDOT
  , RatedREG
  , RatedOT
  , RatedND
  , RatedNDOT
  , OffsetRate
  , ActualREG
  , ActualOT
  , ActualND
  , ActualNDOT
  , ForPerfectAttendance
  , StraightDuty
  , IsHDAbsent
  , MealAllowance
  , IsNoAttendance
  , ID_CostCenter
  , IsTentativeAbsent
  , HasStopEmail
  , HasSchedule
  , ActualUT
  , Posted
  , TardyAsLeavePayrollItem
  , UTAsLeavePayrollItem
  , TardyAsLeave
  , UTAsLeave
  , ID_TempDailySchedule
  , ID_DayType
  , DateTimeCreated
  , DateTimeModified
  , DateTimeProcessed
)
SELECT
    eds.ID
  , eds.[Date]
  , eds.ID_DailySchedule
  , eds.ID_Employee
  , eds.ID_ImportedSchedule
  , eds.ID_Attribute
  , eds.REG
  , eds.EXT
  , eds.OT
  , eds.ND
  , eds.NDOT
  , eds.TARDY
  , eds.UT
  , eds.IsRD
  , eds.TimeIn
  , eds.[TimeOut]
  , eds.IsForComputation
  , eds.ID_LeavePayrollItem
  , eds.ID_FirstHalfLeavePayrollItem
  , eds.ID_SecondHalfLeavePayrollItem
  , eds.Comment
  , eds.IsAbsent
  , eds.ActualTardy
  , eds.IsActualAbsent
  , eds.Absences
  , eds.LeaveWithPay
  , eds.FirstHalfLeaveWithPay
  , eds.SecondHalfLeaveWithPay
  , eds.OffsetREG
  , eds.OffsetOT
  , eds.OffsetND
  , eds.OffsetNDOT
  , eds.ComputedREG
  , eds.ComputedOT
  , eds.ComputedND
  , eds.ComputedNDOT
  , eds.RatedREG
  , eds.RatedOT
  , eds.RatedND
  , eds.RatedNDOT
  , eds.OffsetRate
  , eds.ActualREG
  , eds.ActualOT
  , eds.ActualND
  , eds.ActualNDOT
  , eds.ForPerfectAttendance
  , eds.StraightDuty
  , eds.IsHDAbsent
  , eds.MealAllowance
  , eds.IsNoAttendance
  , eds.ID_CostCenter
  , eds.IsTentativeAbsent
  , eds.HasStopEmail
  , eds.HasSchedule
  , eds.ActualUT
  , eds.Posted
  , eds.TardyAsLeavePayrollItem
  , eds.UTAsLeavePayrollItem
  , eds.TardyAsLeave
  , eds.UTAsLeave
  , eds.ID_TempDailySchedule
  , eds.ID_DayType
  , eds.DateTimeCreated
  , eds.DateTimeModified
  , eds.DateTimeProcessed
FROM dbo.tEmployeeDailySchedule AS eds
WHERE eds.[Date] <= @RetDateTK
AND ID NOT IN (SELECT eds.ID FROM dbo.tEmployeeDailySchedule_History AS EDSH
	INNER JOIN dbo.tEmployeeDailySchedule AS EDS ON edsh.ID_Employee = eds.ID_Employee AND edsh.[Date] = eds.[Date])

--------------------------------------------------------------------------| Insert Employee Daily Schedule Detail 
INSERT INTO dbo.tEmployeeDailySchedule_Detail_History
(
    ID_EmployeeDailySchedule
  , ID_EmployeeDailySchedule_Detail
  , ID_Hourtype
  , StartTime
  , EndTime
  , [Minutes]
  , ConsideredHours
  , Approved
  , ApprovedMinutes
  , Tardy
  , ActualTardy
  , Comment
  , ID_VerifierEmployee
  , ID_ApproverEmployee
  , VerificationDate
  , ApprovalDate
  , ForApproval
  , IsBasic
  , NDAMMinuteIn
  , NDAMMinuteOut
  , NDAMMinutes
  , NDPMMinuteIn
  , NDPMMinuteOut
  , NDPMMinutes
  , NDMinutes
  , NDHours
  , ID_WorkCredit
  , ComputedHours
)
SELECT
    edsd.ID_EmployeeDailySchedule
  , edsd.ID
  , edsd.ID_Hourtype
  , edsd.StartTime
  , edsd.EndTime
  , edsd.[Minutes]
  , edsd.ConsideredHours
  , edsd.Approved
  , edsd.ApprovedMinutes
  , edsd.Tardy
  , edsd.ActualTardy
  , edsd.Comment
  , edsd.ID_VerifierEmployee
  , edsd.ID_ApproverEmployee
  , edsd.VerificationDate
  , edsd.ApprovalDate
  , edsd.ForApproval
  , edsd.IsBasic
  , edsd.NDAMMinuteIn
  , edsd.NDAMMinuteOut
  , edsd.NDAMMinutes
  , edsd.NDPMMinuteIn
  , edsd.NDPMMinuteOut
  , edsd.NDPMMinutes
  , edsd.NDMinutes
  , edsd.NDHours
  , edsd.ID_WorkCredit
  , edsd.ComputedHours
FROM dbo.tEmployeeDailySchedule_Detail AS edsd
INNER JOIN dbo.tEmployeeDailySchedule AS eds ON edsd.ID_EmployeeDailySchedule = eds.ID
WHERE eds.Date <= @RetDateTK

--------------------------------------------------------------------------| Insert Attendance
INSERT INTO dbo.tAttendance_History
(
    ID_Attendance
  , ID_Employee
  , ID_DailySchedule
  , ID_Leave
  , ID_ImportedAttendance_Detail
  , ID_EmployeeDailySchedule
  , [Date]
  , TimeIn
  , [TimeOut]
  , MinuteIn
  , MinuteOut
  , [Days]
  , [Hours]
  , Tardy
  , OT
  , ND
  , IsComplete
  , SeqNo
  , IsActive
  , Comment
  , DateTimeCreated
  , DateTimeModified
  , ID_EmployeeAttendanceLog
  , ComputedTimeIn
  , ComputedTimeOut
  , TempMinuteIn
  , TempMinuteOut
  , FromOB
  , ID_AttendanceFile_Detail
  , OBIN
  , OBOUT
  , WorkDate
  --, ID_AttendanceLogType
)
SELECT
    a.ID
  , a.ID_Employee
  , a.ID_DailySchedule
  , a.ID_Leave
  , a.ID_ImportedAttendance_Detail
  , a.ID_EmployeeDailySchedule
  , a.[Date]
  , a.TimeIn
  , a.[TimeOut]
  , a.MinuteIn
  , a.MinuteOut
  , a.[Days]
  , a.[Hours]
  , a.Tardy
  , a.OT
  , a.ND
  , a.IsComplete
  , a.SeqNo
  , a.IsActive
  , a.Comment
  , a.DateTimeCreated
  , a.DateTimeModified
  , a.ID_EmployeeAttendanceLog
  , a.ComputedTimeIn
  , a.ComputedTimeOut
  , a.TempMinuteIn
  , a.TempMinuteOut
  , a.FromOB
  , a.ID_AttendanceFile_Detail
  , a.OBIN
  , a.OBOUT
  , a.WorkDate
  --, a.ID_AttendanceLogType
FROM dbo.tAttendance AS a
WHERE a.[Date] <= @RetDateTK

--------------------------------------------------------------------------| Insert Employee Attendance Log
INSERT INTO dbo.tEmployeeAttendanceLog_History
(
    ID_EmployeeAttendanceLog
  , Code
  , [Name]
  , [Source]
  , AccessNo
  , [DATETIME]
  , ID_Employee
  , ID_AttendanceLogType
  , ID_EmployeeAttendanceLogFile
  , WorkDate
  , [Date]
  , [Minute]
  , SeqNo
  , IsActive
  , Comment
  , ID_DailySchedule
  , ID_EditedByUser
  , DateTimeCreated
  , DateTimeModified
  , ID_EmployeeAttendanceLogCreditDate
  , ID_EmployeeMissedLog
  , ID_EmployeeMissedLogFile_Detail
  , ID_ManualAttendanceInput_Detail
  , ID_TimekeepingFile
)
SELECT
    eal.ID
  , eal.Code
  , eal.[Name]
  , eal.[Source]
  , eal.AccessNo
  , eal.[DateTime]
  , eal.ID_Employee
  , eal.ID_AttendanceLogType
  , eal.ID_EmployeeAttendanceLogFile
  , eal.WorkDate
  , eal.[Date]
  , eal.[Minute]
  , eal.SeqNo
  , eal.IsActive
  , eal.Comment
  , eal.ID_DailySchedule
  , eal.ID_EditedByUser
  , eal.DateTimeCreated
  , eal.DateTimeModified
  , eal.ID_EmployeeAttendanceLogCreditDate
  , eal.ID_EmployeeMissedLog
  , eal.ID_EmployeeMissedLogFile_Detail
  , eal.ID_ManualAttendanceInput_Detail
  , eal.ID_TimekeepingFile
FROM dbo.tEmployeeAttendanceLog AS eal
WHERE eal.[DateTime] <= @RetDateTK



--------------------------------------------------------------------------| DELETEEEEEEEE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
UPDATE pp SET 
	pp.ID_EmployeeDailyScheduleView = NULL,
	pp.StartDate = edsvh.StartDate,
	pp.EndDate = edsvh.EndDate
FROM  dbo.tPayrollPeriod AS pp 
INNER JOIN dbo.tEmployeeDailyScheduleView_History AS edsvh ON edsvh.ID_EmployeeDailyScheduleView = pp.ID_EmployeeDailyScheduleView

DELETE FROM dbo.tEmployeeDailyScheduleView WHERE StartDate <= @RetDateTK AND EndDate <= @RetDateTK

DELETE edsd 
FROM dbo.tEmployeeDailySchedule_Detail AS edsd
INNER JOIN dbo.tEmployeeDailySchedule AS eds ON edsd.ID_EmployeeDailySchedule = eds.ID
WHERE eds.Date <= @RetDateTK

DELETE FROM dbo.tEmployeeDailySchedule WHERE [Date] <= @RetDateTK

DELETE FROM dbo.tEmployeeAttendanceLog WHERE [DateTime] <= @RetDateTK

DELETE FROM dbo.tAttendance WHERE [Date] <= @RetDateTK


COMMIT TRAN
END TRY 
BEGIN CATCH
	DECLARE @ErrorMes VARCHAR(MAX)
	SELECT @ErrorMes = ERROR_MESSAGE()
	IF @@TRANCOUNT > 0
		RAISERROR(@ErrorMes,16,1)
		ROLLBACK TRAN
END CATCH
END
