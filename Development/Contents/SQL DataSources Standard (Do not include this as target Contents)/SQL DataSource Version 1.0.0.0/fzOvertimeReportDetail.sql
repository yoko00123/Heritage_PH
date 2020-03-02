ALTER FUNCTION dbo.fzOvertimeReportDetail(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE
as
RETURN
--SELECT * FROM tuser WHERE name LIKE '%alcopra%'
--Declare @StartDate DATETIME,@EndDate DATETIME, @ID_Employee INT = 558, @ID_Company INT = 3
--Select @StartDate = '02/02/2019',@EndDate = '02/28/2020'
 

Select 
ot.ID
,e.ID ID_Employee
,e.Code EmpCode
,per.Name Employee
,CASE WHEN eds.IsRD = 1 AND eds.REG > 0 THEN CAST(ISNULL(eds.REG,0) AS DEC(18,2)) ELSE CAST(ISNULL(eds.OT,0) AS DEC(18,2)) end ComputedHours
--,e.HourlyRate
--,pir.rate
--,((eds.OT * e.HourlyRate)*pir.Rate) Amount
,c.ID ID_Company
 ,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,dep.ID ID_Department
,dep.Name Department
,@StartDate StartDate
,@EndDate EndDate
,br.ID ID_Branch
,br.Name Branch
,ot.Reason
,Convert(VARCHAR(50), CAST(dbo.fGetTimeIN(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100) TimeIn
,Convert(VARCHAR(50), CAST(dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100) TimeOut
,Convert(VARCHAR(50), CAST(ot.StartTime AS TIME),100) StartTime
,Convert(VARCHAR(50), CAST(ot.EndTime AS TIME),100) EndTime
,ot.WorkDate
,ot.WorkDate [Date]
,ot.ApprovalDate
,ot.FileDate
,dv.ID ID_Division
,dv.Name Division
,ds.Name Schedule
,st.Name FilingStatus
,tw.Name WorkCredit
,dt.Name DayType
,ss.Name Users
,ot.PreviousApproverComment AppComment
,ot.ApprovalHistory
,ot.ID_WorkCredit
,ot.ID_FilingStatus
from tOvertime ot
LEFT OUTER JOIN tFilingStatus st ON st.ID = ot.ID_FilingStatus 
LEFT OUTER JOIN tUser ss ON ss.ID = ot.ID_User
LEFT OUTER JOIN tWorkCredit tw ON tw.ID = ot.ID_WorkCredit 
LEFT OUTER JOIN tEmployeeDailySchedule eds ON ot.Date = eds.Date AND ot.ID_Employee = eds.ID_Employee
LEFT OUTER JOIN tDayType dt ON dt.ID = eds.ID_DayType 
LEFT OUTER JOIN tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
LEFT OUTER JOIN tEmployee e ON e.ID = ot.ID_Employee
LEFT OUTER JOIN tPersona per ON per.ID = e.ID_Persona
LEFT OUTER JOIN tParameter par ON par.ID = e.ID_Parameter
LEFT OUTER JOIN tPayrollItemRate pir ON pir.ID_Parameter = par.ID AND pir.ID_PayrollItem = (eds.ID_DayType+6)
LEFT OUTER JOIN tCompany c On c.ID  = e.ID_Company
LEFT OUTER JOIN tDepartment dep On dep.ID = e.ID_Department
LEFT OUTER JOIN tBranch br ON br.ID = e.ID_Branch
LEFT OUTER JOIN dbo.tDivision dv ON dv.ID = dep.ID_Division
Where ot.WorkDate Between @StartDate AND @EndDate 
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO