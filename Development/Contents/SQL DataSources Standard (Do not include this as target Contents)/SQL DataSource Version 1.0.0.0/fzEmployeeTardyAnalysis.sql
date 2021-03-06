ALTER FUNCTION dbo.fzEmployeeTardyAnalysis(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE AS RETURN



--DECLARE @StartDate DATETIME = '01/01/2019', @EndDate DATETIME = '02/28/2019', @ID_Company INT = NULL , @ID_Session INT = 6415


SELECT 
		eds.ID,
		e.Code EmployeeCode,
		p.Name Employee,
		eds.Date,
		eds.TARDY,
		dl.Code SchedCode
		,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
		,d.Name Department,
		c.Address CompanyAddress,
		b.Name Branch,
		FORMAT(dbo.fGetTimeIn(eds.ID_Employee,eds.Date,e.ID_Company), 'hh:mm tt') TimeIn,
		FORMAT(dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company), 'hh:mm tt') TimeOut,
		e.ID_Company,
		e.ID_Branch,
		e.ID_Department,
		e.ID_Designation,
		e.ID_EmployeeStatus,
		eds.ID_Employee,
		@StartDate StartDate1,
		@EndDate EndDate1,
		e.ID_Section,
		s.Name SECTION,
		eds.ActualTardy,
		e.IsActive,
		des.Name,
		des.GraceMinutes,
		Date [StartDate]
		,Date [EndDate],
		CASE WHEN(eds.ActualTardy - des.GraceMinutes) < 0 Then 0 Else eds.ActualTardy - des.GraceMinutes END  as Excess
FROM dbo.tEmployeeDailySchedule eds
--LEFT JOIN dbo.tEmployeeAttendanceLog eal ON eal.Date = eds.Date AND eal.ID_Employee = eds.ID_Employee
LEFT JOIN dbo.tDailySchedule dl ON dl.ID = eds.ID_DailySchedule
LEFT JOIN dbo.tEmployee e ON e.ID = eds.ID_Employee
LEFT JOIN dbo.tPersona p ON p.ID = e.ID_Persona
LEFT JOIN dbo.tCompany c ON c.ID = e.ID_Company
LEFT JOIN dbo.tDepartment d ON d.ID = e.ID_Department
LEFT JOIN dbo.tBranch b ON b.ID = e.ID_Branch
LEFT JOIN dbo.tSection s ON s.ID = e.ID_Section
LEFT JOIN dbo.tDesignation des ON des.ID = e.ID_Designation
WHERE eds.TARDY > 0 AND e.IsRequiredToLog = 1 
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO