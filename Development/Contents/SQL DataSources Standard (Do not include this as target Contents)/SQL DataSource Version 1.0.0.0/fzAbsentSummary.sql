ALTER FUNCTION dbo.fzAbsentSummary(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE
RETURN
--DECLARE @StartDate DATETIME = '2019-01-01', @EndDate DATETIME = '2019-04-30', @ID_Session INT = 5017, @ID_Company INT = 3
--Edited by Yoku 03/07/2019

SELECT 
TOP 100
e.ID_Company,
CASE WHEN @ID_Company IS NOT NULL THEN c.[Name] ELSE 'All Companies' END Company,
e.ID_Department,
d.[Name] Department,
e.ID ID_Employee,
e.Code,
p.[Name] Employee,
@StartDate StartDate,
@EndDate EndDate,
eds.[Date],
eds.Absences
FROM dbo.tEmployeeDailySchedule eds
INNER JOIN dbo.tEmployee e ON e.ID = eds.ID_Employee
INNER JOIN dbo.tPersona p ON p.ID = e.ID_Persona
INNER JOIN dbo.tCompany c ON c.ID = e.ID_Company 
INNER JOIN dbo.tDepartment d ON d.ID = e.ID_Department
WHERE eds.[Date] BETWEEN @StartDate AND @EndDate
AND eds.Absences > 0
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
AND e.IsRequiredToLog = 1
ORDER BY eds.[Date] ASC
GO 

