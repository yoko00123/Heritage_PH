ALTER FUNCTION dbo.fzOBReport(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
 RETURNS TABLE
 as
 RETURN
--SELECT * FROM dbo.tCompany
 --Declare @StartDate DATETIME,@EndDate DATETIME, @ID_Session INT = 5266, @ID_Company INT = NULL
 --Select @StartDate = '01/01/2019',@EndDate = '03/31/2019'
 


 Select  
 obd.ID
 ,e.ID ID_Employee
 ,e.Code EmpCode
 ,per.Name Employee
 ,c.ID ID_Company
 ,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
 ,dep.ID ID_Department
 ,dep.Name Department
 ,obd.Date WorkDate
 --,obd.StartTime Date
 ,DATEADD(dd, DATEDIFF(dd, 0,obd.StartTime), 0) Date
 ,obd.ComputedTimeIn 
 ,obd.ComputedTimeOut
 --,obd.StartTime WorkDate
 ,@StartDate StartDate
 ,@EndDate EndDate
 ,br.ID ID_Branch
 ,br.Name Branch
 ,ob.Reason Rason
 ,ob.StartDate SD
 ,ob.EndDate ED
 ,ob.FileDate
 ,ob.ApprovalDate
 ,ob.ApprovalHistory
 ,st.Name FilingStatus
 ,s2.Name Users
 ,ob.PreviousApproverComment AppComment
 from tOB ob
 INNER JOIN tOB_Detail obd ON obd.ID_OB = ob.ID
 INNER JOIN tUser ss ON ss.ID_Employee = ob.ID_Employee
 INNER JOIN tEmployee e On e.ID = ss.ID_Employee
 INNER JOIN tFilingStatus st ON st.ID = ob.ID_FilingStatus
 LEFT OUTER JOIN tUser s2 ON s2.ID = ob.ID_User
 LEFT OUTER JOIN tPersona per ON per.ID = e.ID_Persona
 LEFT OUTER JOIN tCompany c ON c.ID = e.ID_Company
 LEFT OUTER JOIN tDepartment dep ON dep.ID = e.ID_Department
 LEFT OUTER JOIN dbo.tBranch br ON br.ID = e.ID_Branch
 Where DATEADD(dd, DATEDIFF(dd, 0,obd.StartTime), 0)
 BEtween @StartDate AND @EndDate
 AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
 AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
GO
