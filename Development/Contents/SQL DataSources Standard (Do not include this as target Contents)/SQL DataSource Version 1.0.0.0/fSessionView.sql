ALTER FUNCTION dbo.fSessionView(@ID_Session INT, @ID_Company INT, @StartDate DATETIME, @EndDate DATETIME)
RETURNS TABLE AS RETURN
----Edited by Yok 06/11/2019
--DECLARE @ID_Session INT = 1 , @ID_Company INT = NULL , @StartDate DATETIME = '06/10/2019', @EndDate DATETIME = '06/11/2019'
SELECT 
s.ID
,DATEADD(dd, DATEDIFF(dd, 0,s.StartDateTime), 0) [Date]
,s.StartDateTime
,@StartDate StartDate
,@EndDate EndDate
,s.ID_User
,COALESCE (p.Name, u.Name, u.LogInName) AS Name
,s.ElapsedTime
,s.ID_Company, 
COALESCE (p.Name, u.Name, u.LogInName) AS [User],
u.ImageFile,
s.GUID,
CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,e.ID_Department
,d.Name AS Department,
p.Name AS Employee,
ug.Name AS Usergroup,
u.ID_UserGroup,
e.ID_Branch,
br.Name AS Branch,
--ug.CanViewEmployeeSalary,
dbo.fGetDate() CurrentDate,
e.ID AS ID_Employee,
ug.ID_ApplicationType,
CASE WHEN ug.ID_ApplicationType = 1 THEN 'InSys' ELSE 'IONS' END AS [ApplicationType],
CASE WHEN ISNULL(u.ID_SecretQuestion, 0) = 0 THEN 0 ELSE 1 END [IsSecretQuestionReady], 
CASE WHEN p.ID_Gender = 1 THEN 'Male' ELSE 'Female' END AS [Gender]
FROM dbo.tSession AS s 
LEFT OUTER JOIN tUser u ON u.ID = s.ID_User
LEFT OUTER JOIN dbo.tEmployee AS e ON e.ID = s.ID_Employee
LEFT OUTER JOIN dbo.tCompany AS c ON s.ID_Company = c.ID
LEFT OUTER JOIN dbo.tUserGroup AS ug ON u.ID_UserGroup = ug.ID 
LEFT OUTER JOIN dbo.tDepartment AS d ON d.ID = e.ID_Department 
LEFT OUTER JOIN dbo.tDesignation AS des ON des.ID = e.ID_Designation 
LEFT OUTER JOIN dbo.tPersona AS p ON p.ID = u.ID_Persona 
LEFT OUTER JOIN dbo.tBranch AS br ON br.ID = e.ID_Branch
WHERE --dbo.fEmployeeRights(@ID_Session,e.ID) = 1 AND 
DATEADD(dd, DATEDIFF(dd, 0,s.StartDateTime ), 0) BETWEEN @StartDate AND @EndDate
 AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO

