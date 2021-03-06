ALTER FUNCTION [dbo].[fzPayrollRegister](@ID_PayrollPeriod INT,@ID_Session INT)
RETURNS TABLE AS
RETURN

--Declare @ID_PayrollPeriod INT, @ID_Session INT = 44818
--Select @ID_PayrollPeriod = 45

select 
e.ID ID_Employee
,e.Code EmpCode
,per.Name Employee
,c.Id ID_Company
,c.Name Company
,dep.ID ID_Department
,dep.Name Department 
,ISNULL(edsv.StartDate,pp.StartDate) StartDate
,ISNULL(edsv.EndDate,pp.EndDate) EndDate
,pp.ID ID_PayrollPeriod
,SUM(CASE WHEN pit.ID = 1 then pd.Total else 0 END) BasicPay
,SUM(CASE WHEN pit.ID_PayrollItemGroup = 2 then pd.Total else 0 END) OT
,SUM(CASE WHEN pit.ID_PayrollItemGroup = 5 then pd.Total else 0 END) NDPay
,SUM(CASE WHEN pit.ID_PayrollItemGroup = 4 then pd.Total else 0 END) Holiday
,SUM(CASE WHEN pit.ID_PayrollItemType = 1 AND pit.ID IN (50) then pd.Total else 0 END) ServiceCharge
,SUM(CASE WHEN pit.ID_PayrollItemType = 1 and pit.ID > 24 and pit.ID NOT IN (50) then pd.Total Else 0 END) OthInc
,SUM(CASE WHEN pit.ID_Income = 17 then pd.Total else 0 END) AUT
,p.GrossAmt
,SUM(CASE WHEN pit.ID = 29 then ISNULL(pd.Total,0) when pit.ID = 29 AND pd.Total = 0 THEN 0.00 else 0 END) SSS
,SUM(CASE WHEN pit.ID = 32 then ISNULL(pd.Total,0) else 0 END) PHIC
,SUM(CASE WHEN pit.ID = 34 then ISNULL(pd.Total,0) else 0 END) HDMF
,SUM(CASE WHEN pit.ID = 28 then ISNULL(pd.Total,0) else 0 END) Tax
,SUM(CASE WHEN pit.ID_PayrollItemCategory = 2  AND pit.ID IN (36,37,38) then ISNULL(pd.Total,0) else 0 END) SSSLoan 
,SUM(CASE WHEN pit.ID_PayrollItemCategory = 2  AND pit.ID IN (39) then ISNULL(pd.Total,0) else 0 END) HDMFLoan 
,SUM(CASE WHEN pit.ID_PayrollItemType = 2 AND pit.ID_PayrollItemCategory NoT IN (4,2) AND pit.ID <> 28 AND (pit.ID_Income NOT IN(15,17) OR pit.ID_Income IS NULL) then pd.Total else 0 END) OthDed
,p.DeductionAmt
,p.NetAmt
,pm.ID ID_PaymentMode
,pm.Name PaymentMode
,cc.ID ID_CostCenter
,cc.Name CostCenter
,jc.ID ID_JobClass
,jc.Name JobClass
,br.ID ID_Branch
,br.Name Branch 
from tPayroll_detail pd
INNER JOIN tPayroll p ON p.ID = pd.ID_Payroll
INNER JOIN tPayrollPeriod pp ON pp.ID = p.ID_PayrollPeriod
INNER JOIN tPayrollItem pit ON pit.ID = pd.ID_PayrollItem
INNER JOIN tEmployee e On e.ID = p.ID_Employee
LEFT OUTER JOIN tPersona per ON per.ID = e.ID_Persona
LEFT OUTER JOIN tCompany c ON c.ID = e.ID_Company
LEFT OUTER JOIN tDepartment dep ON dep.ID = p.ID_Department
LEFT OUTER JOIN tPaymentMode pm ON pm.ID = p.ID_PaymentMode
LEFT OUTER JOIN tCostCenter cc ON cc.ID = p.ID_CostCenter
LEFT OUTER JOIN dbo.tDesignation dg ON dg.ID = p.ID_Designation
LEFT OUTER JOIN dbo.tJobClass jc ON jc.ID = dg.ID_JobClass
LEFT OUTER JOIN tBranch br ON br.ID = p.ID_Branch
LEFT OUTER JOIN dbo.tEmployeeDailyScheduleView edsv ON edsv.ID = pp.ID_EmployeeDailyScheduleView
Where pp.ID = @ID_PayrollPeriod	
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
Group by p.ID,p.GrossAmt,p.DeductionAmt,p.GrossAmt,p.NetAmt,e.ID,e.Code,per.Name,c.ID,c.name,dep.ID,dep.Name,pp.ID,pm.ID,pm.Name,cc.ID,cc.Name,jc.ID,jc.Name,br.ID,br.Name
,ISNULL(edsv.StartDate,pp.StartDate)
,ISNULL(edsv.EndDate,pp.EndDate)