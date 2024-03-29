ALTER FUNCTION dbo.fzAuditTrail(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE AS RETURN
--select * from tSession where id = 4810
--DECLARE @StartDate DATETIME = '03/05/2019', @EndDate DATETIME = '03/06/2019'

SELECT 
CASE WHEN LEFT (at.Name,1)= 't' THEN Stuff (at.Name, 1,1, '') ELSE  at.Name END ModuleName
,at.ID ID_AuditTrail
,at.ID_AuditTrailType
,att.Name AuditTrailType
--,at.OldValue
--,at.NewValue
,DATEADD(dd, DATEDIFF(dd, 0,at.DateTime), 0) [Date]
,at.Hostname
,at.DateTime [DateTime]
,@StartDate StartDate
,@EndDate EndDate
,u.Name
,u.Name Username
,at.ID_Session
,e.Code EmployeeCode
FROM tAuditTrailRev at 
LEFT OUTER JOIN dbo.tAuditTrailRev at2 ON at.ParentID = at2.ID
LEFT OUTER JOIN dbo.tAuditTrailType att ON at.ID_AuditTrailType = att.ID
LEFT OUTER JOIN dbo.tSession s ON s.ID = at.ID_Session
LEFT OUTER JOIN dbo.tUser u ON u.ID = s.ID_User
LEFT OUTER JOIN dbo.tEmployee e ON e.ID = s.ID_Employee
LEFT OUTER JOIN dbo.tCompany c ON c.ID = e.ID
WHERE DATEADD(dd, DATEDIFF(dd, 0,at.DateTime), 0) BETWEEN @StartDate AND @EndDate
AND at.ID_AuditTrailType IN(2,3,4,5) AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
GO
