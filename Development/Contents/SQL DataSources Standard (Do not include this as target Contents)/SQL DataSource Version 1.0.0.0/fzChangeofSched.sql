CREATE OR ALTER FUNCTION dbo.fzChangeofSched(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
 RETURNS TABLE
 as
 RETURN

--Created by Yoku 03/06/2019
--DECLARE @StartDate DATETIME = '01/01/2018', @EndDate DATETIME = 'Oct 30, 2019', @ID_Session INT = 5242
 SELECT
 TOP 100 PERCENT
 e.ID ID_Employee
 ,e.Code EmpCode
 ,per.Name Employee
 ,c.ID ID_Company
 ,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
 ,dep.ID ID_Department
 ,dep.Name Department
 ,cosd.OldSched
 ,cosd.SchedDate 
 ,@StartDate StartDate
 ,@EndDate EndDate
 ,coss.ID_FilingStatus
 ,fs.Name FilingStatus
 ,CASE WHEN cosd.IsRD = 1 THEN 'Rest Day' WHEN cosd.IsSD = 1 THEN 'Straight Duty' WHEN cosd.ReqRD = 1 THEN 'Required RestDay' ELSE 'Regular' END DayType
 ,coss.StartDate AS SD
 ,coss.EndDate AS ED
 ,br.ID ID_Branch
 ,br.Name Branch
 ,cc.Name AS CostCenterName
 ,coss.Reason AS Rason
 ,coss.FileDate AS FiledDate
 ,coss.FileDate AS Date
 ,coss.ApprovalDate AS ApproveDate
 ,ds.Name AS NewSched
 ,coss.ApprovalHistory
 ,coss.PreviousApproverComment AppComment
 FROM tEmployeeChangeOfSchedule_Detail cosd
 INNER JOIN tEmployeeChangeOfSchedule coss ON coss.ID = cosd.ID_EmployeeChangeOfSchedule
 INNER JOIN dbo.tDailySchedule ds ON ds.ID = cosd.ID_NewSched
 INNER JOIN tEmployee e On e.ID = coss.ID_Employee
 LEFT OUTER JOIN tFilingStatus fs ON fs.ID = coss.ID_FilingStatus
 LEFT OUTER JOIN tPersona per ON per.ID = e.ID_Persona
 LEFT OUTER JOIN tCompany c ON c.ID = e.ID_Company
 LEFT OUTER JOIN tDepartment dep ON dep.ID = e.ID_Department
 LEFT OUTER JOIN dbo.tBranch br ON br.ID = e.ID_Branch
 LEFT OUTER JOIN dbo.tCostCenter cc ON cc.ID = e.ID_CostCenter
 Where coss.FileDate BEtween @StartDate AND @EndDate
 AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
 AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO
