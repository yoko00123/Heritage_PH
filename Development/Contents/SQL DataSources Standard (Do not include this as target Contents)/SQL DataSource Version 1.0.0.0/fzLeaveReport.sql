ALTER FUNCTION dbo.fzLeaveReport(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS TABLE
as
RETURN
--Edited by Yoku 03/08/2019
--declare @StartDate DATETIME,@EndDate DATETIME
--Select @StartDate = '03/01/2019',@EndDate = '03/05/2019'

Select 
e.ID ID_Employee
,e.Code EmpCode
,per.Name Employee
,c.ID ID_Company
,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,dep.ID ID_Department
,dep.Name Department
,l.ID_LeavePayrollItem [ID_PayrollItem]
,l.StartDate LStartDate
,l.EndDate LEndDate
,@StartDate StartDate
,@EndDate EndDate
,l.Days
,l.DaysWithPay
,l.ApprovalDate
,l.Reason
,l.PreviousApproverComment AppComment
,l.ApprovalHistory
,ss.Name as Users
,pit.Name LeavePayrollItem
,DATEADD(dd, DATEDIFF(dd, 0,l.FileDate), 0) Date
,l.FileDate DateFiled
,l.ID_FilingStatus
,br.ID ID_Branch
,br.Name Branch
,st.Name FilingStatus
from tLeave l
LEFT OUTER JOIN tEmployee e ON e.ID = l.ID_Employee
LEFT OUTER JOIN tUser ss ON ss.ID = l.ID_User
LEFT OUTER JOIN tFilingStatus st ON st.ID = l.ID_FilingStatus 
LEFT OUTER JOIN tPayrollItem pit ON pit.ID = l.ID_LeavePayrollItem
LEFT OUTER JOIN tPersona per ON per.ID = e.ID_Persona
LEFT OUTER JOIN tCompany c On c.ID = e.ID_Company
LEFT OUTER JOIN tDepartment dep ON dep.ID = e.ID_Department
LEFT OUTER JOIN dbo.tBranch br ON br.ID = e.ID_Branch
Where (DATEADD(dd, DATEDIFF(dd, 0,l.FileDate), 0)  between @StartDate AND @EndDate)
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
GO
