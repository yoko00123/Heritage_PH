ALTER FUNCTION dbo.fzEmployeeUndertime(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE AS RETURN

----DECLARE @StartDate DATETIME = '2014-01-01',@EndDate DATETIME = '2014-12-31',@ID_Session INT = 15595


SELECT 
eds.ID
,eds.ID_Employee
,e.Code AS EmployeeCode
,per.Name AS Employee
,eds.Date
,eds.ID_DailySchedule
,ds.Name DailySchedule
,eds.UT
,@StartDate StartDate
,@EndDate EndDate
,FORMAT(dbo.fGetTimeIn(eds.ID_Employee,eds.Date,e.ID_Company), 'hh:mm tt') TimeIn
,FORMAT(dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company), 'hh:mm tt') TimeOut
,e.ID_Company
,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,e.ID_Department
,dep.Name Department
,e.ID_Branch
,br.Name AS Branch
FROM dbo.tEmployeeDailySchedule eds
LEFT JOIN dbo.tEmployee e ON e.ID = eds.ID_Employee
LEFT JOIN dbo.tPersona per ON per.ID = e.ID_Persona
LEFT OUTER JOIN dbo.tBranch br ON br.ID = e.ID_Branch
LEFT OUTER JOIN dbo.tDepartment dep ON dep.ID = e.ID_Department
LEFT OUTER JOIN dbo.tCompany c ON c.ID = e.ID_Company
LEFT OUTER JOIN dbo.tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
WHERE eds.UT > 0
AND (eds.Date BETWEEN @StartDate AND @EndDate)
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND e.IsRequiredToLog = 1 AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO
