ALTER FUNCTION dbo.fzMissedLogReport(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE
as
RETURN

--Created by Yoku 03/06/2019
--Declare @StartDate DATETIME,@EndDate DATETIME ,@ID_Session INT = 5254
--Select @StartDate = '01/01/2019',@EndDate = '06/30/2020'


SELECT DISTINCT
e.ID ID_Employee
,e.Code EmpCode
,per.Name Employee
,c.ID ID_Company
,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,FileDate DateFile
,dep.ID ID_Department
,dep.Name Department
,ml.workdate [Date]
,x.LogTime ComputedTimeIn
,z.LogTime ComputedTimeOut
, (CASE WHEN lg.ID = 1 THEN 'M - ' + Convert(VARCHAR(50), CAST(dbo.fGetTimeIN(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100) ELSE Convert(VARCHAR(50), CAST(dbo.fGetTimeIN(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100)  END) TimeIn
, (CASE WHEN lg.ID = 2 THEN 'M - ' + Convert(VARCHAR(50), CAST(dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100) ELSE  Convert(VARCHAR(50), CAST(dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company) AS TIME),100) END) TimeOut
,@StartDate StartDate
,@EndDate EndDate
,st.Name FilingStatus
,b.ID ID_Branch
,b.Name Branch
,ss.Name Users
,ml.WorkDate MissedDate
,ml.Reason
,lg.Name LogType
,ml.ApprovalDate
,ml.ApprovalHistory
,ml.PreviousApproverComment AppComment
FROM tEmployeeMissedLog ml
INNER JOIN dbo.tEmployeeMissedLog_Detail mld ON mld.ID_EmployeeMissedLog = ml.ID
INNER JOIN dbo.tEmployee e ON e.ID = ml.ID_Employee
INNER JOIN tFilingStatus st ON st.ID = ml.ID_FilingStatus
LEFT OUTER JOIN dbo.tEmployeeAttendanceLog eds ON eds.ID_Employee = ml.ID_Employee AND eds.Date = ml.WorkDate
LEFT OUTER JOIN dbo.tUser ss ON ss.ID = ml.ID_User
LEFT OUTER JOIN dbo.tPersona per  ON per.ID = e.ID_Persona
LEFT OUTER JOIN dbo.tCompany c ON c.ID = e.ID_Company
LEFT OUTER JOIN dbo.tDepartment dep ON dep.ID = e.ID_Department
LEFT OUTER JOIN dbo.tBranch b ON b.ID = e.ID_Branch
LEFT OUTER JOIN dbo.tAttendanceLogType lg ON lg.ID = mld.ID_AttendanceLogType
LEFT JOIN (SELECT  DISTINCT ml.ID_Employee,	mld.LogTime		, mld.LogDate			 
				FROM tEmployeeMissedLog ml
				INNER JOIN dbo.tEmployeeMissedLog_Detail mld ON mld.ID_EmployeeMissedLog = ml.ID 
				WHERE mld.LogDate BETWEEN @StartDate AND @EndDate 
				AND ml.ID_FilingStatus = 2  
				AND ID_AttendanceLogType = 1 
				
				) x ON x.ID_Employee = e.ID  AND x.LogDate = ml.WorkDate

LEFT JOIN (SELECT  DISTINCT ml.ID_Employee,	mld.LogTime				, mld.LogDate			 
				FROM tEmployeeMissedLog ml
				INNER JOIN dbo.tEmployeeMissedLog_Detail mld ON mld.ID_EmployeeMissedLog = ml.ID 
				WHERE mld.LogDate BETWEEN @StartDate AND @EndDate 
				AND ml.ID_FilingStatus = 2  
				AND ID_AttendanceLogType = 2
				
				) z ON z.ID_Employee = e.ID  AND z.LogDate = ml.WorkDate

WHERE ml.WorkDate BETWEEN @StartDate AND @EndDate
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO
