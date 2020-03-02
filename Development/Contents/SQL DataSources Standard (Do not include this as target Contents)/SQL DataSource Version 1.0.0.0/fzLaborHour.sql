
CREATE FUNCTION [dbo].[fzLaborHour](@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS
TABLE AS
RETURN


--DECLARE @StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT
--Select @StartDate = '01/01/2019',@EndDate = '01/05/2019', @ID_Company = 2, @ID_Session = 44828

SELECT
a.ID_Employee
,a.ID_Company
,a.ID_Department
,Employee
,EmpCode
,Company
,Department
,@StartDate [Date]
,@StartDate StartDate
,@EndDate EndDate
,dsn.ID_JobClass
,(SUM(Absences) * 8) Absences
,SUM(Absences) AbsencesDay
,LEFT(SUM(a.TardyN / 60),4) TardyRH
,SUM(DaysTardy)DaysTardy
,LEFT(SUM(UTRH),4) UTRH
,SUM(DaysUT)DaysUT
,SUM(Tardy)Tardy
,SUM(UT)UT
,SUM(a.Reg) REG
,SUM(OT)OT
,SUM(ND)ND
,SUM(NDOT)NDOT
,SUM(LeaveWithPay) LeaveWithPay, SUM(LeaveWithOPay) LeaveWithOPay
,SUM(REGRestDay)REGRestDay
,SUM(REGRestDayOT)REGRestDayOT
,SUM(REGRestDayND)REGRestDayND
,SUM(REGRestDayNDOT)REGRestDayNDOT
,SUM(REGSpeDay)REGSpeDay
,SUM(SHOT)SHOT
,SUM(SHRestDay)SHRestDay
,SUM(SHRestDayOT)SHRestDayOT
,SUM(SHND)SHND
,SUM(SHRestDayND)SHRestDayND
,SUM(SHNDOT)SHNDOT
,SUM(SHRestDayNDOT)SHRestDayNDOT
,SUM(LHReg)LHReg
,SUM(LHOT)LHOT
,SUM(LHRD)LHRD
,SUM(LHRDOT)LHRDOT
,SUM(LHND)LHND
,SUM(LHRDND)LHRDND
,SUM(LHNDOT)LHNDOT
,SUM(LHRDNDOT)LHRDNDOT
FROM
(
SELECT
 eds.ID_Employee
,e.ID_Company
,e.ID_Department
,@StartDate StartDate
,@EndDate EndDate
,replace(replace(ps.Name,'Ñ','n'),'ñ','n') [Employee] 
,e.Code EmpCode
,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,dep.Name Department
,eds.Date
,CASE WHEN eds.LeaveWithPay = 1 AND eds.IsActualAbsent = 1 THEN eds.LeaveWithPay ELSE 0 END LeaveWithPay
,CASE WHEN eds.LeaveWithPay = 0 AND eds.IsActualAbsent = 1 AND eds.Absences = 1 THEN eds.Absences WHEN  eds.LeaveWithPay = 0 AND eds.IsActualAbsent = 1 AND eds.Absences = 0.5  THEN eds.Absences WHEN eds.IsActualAbsent = 0 AND Absences = 0.5 AND eds.ID_FirstHalfLeavePayrollItem IN (27) THEN eds.Absences WHEN eds.IsActualAbsent = 0 AND Absences = 0.5 AND eds.ID_FirstHalfLeavePayrollItem IN (48) THEN eds.Absences ELSE 0 END LeaveWithOPay
,CAST(eds.Absences as decimal (18,2))Absences
,CAST(eds.Absences as decimal (18,2))AbsencesDay
,CASE WHEN dt.ID = 1 THEN CAST(eds.Tardy as decimal (18,2)) ELSE 0 END TardyN
,CASE WHEN eds.Tardy > 0 THEN 1 ELSE 0 END DaysTardy
-- (CEILING(CAST(eds.Tardy  AS decimal(18,2)) / 60) / 8) 
,CASE WHEN dt.ID = 1 THEN (CAST(eds.UT AS decimal(18,2)) / 60.00) ELSE 0 END UTRH
,CASE WHEN eds.UT > 0 AND dt.ID = 1  THEN 1 ELSE 0 END DaysUT
,CASE WHEN dt.ID = 1 THEN CAST(eds.Tardy AS DECIMAL(18,2)) ELSE 0 END Tardy
,CASE WHEN dt.ID = 1 THEN CAST(eds.UT AS DECIMAL(18,2)) ELSE 0 END UT
,CASE WHEN dt.ID = 1 THEN CAST(eds.REG as decimal (18,2)) ELSE 0 END REG
,CASE WHEN dt.ID = 1 THEN CAST(eds.OT AS decimal(18,2)) ELSE 0 END OT
,CASE WHEN dt.ID = 1 THEN CAST(eds.ND AS DECIMAL(18,2)) ELSE 0 END ND
,CASE WHEN dt.ID = 1 THEN CAST(eds.NDOT AS DECIMAL(18,2)) ELSE 0 END NDOT
,CASE WHEN dt.ID = 2 THEN CAST(eds.REG AS DECIMAL(20,2)) ELSE 0 END REGRestDay
,CASE WHEN dt.ID = 2 THEN CAST(eds.OT AS DECIMAL(20,2)) ELSE 0 END REGRestDayOT
,CASE WHEN dt.ID = 2 THEN CAST(eds.ND AS DECIMAL(20,2)) ELSE 0 END REGRestDayND
,CASE WHEN dt.ID = 2 THEN CAST(eds.NDOT AS DECIMAL(20,2)) ELSE 0 END REGRestDayNDOT
,CASE WHEN dt.ID = 3 THEN CAST(eds.REG AS DECIMAL(20,2)) ELSE 0 END REGSpeDay
,CASE WHEN dt.ID = 3 THEN CAST(eds.OT AS DECIMAL(20,2)) ELSE 0 END SHOT
,CASE WHEN dt.ID = 4 THEN CAST(eds.REG AS DECIMAL(20,2))  ELSE 0 END SHRestDay
,CASE WHEN dt.ID = 4 THEN CAST(eds.OT AS DECIMAL(20,2))  ELSE 0 END SHRestDayOT
,CASE WHEN dt.ID = 3 THEN CAST(eds.ND AS DECIMAL(20,2)) ELSE 0 END SHND
,CASE WHEN dt.ID = 4 THEN CAST(eds.ND AS DECIMAL(20,2))  ELSE 0 END SHRestDayND
,CASE WHEN dt.ID = 3 THEN CAST(eds.NDOT AS DECIMAL(20,2)) ELSE 0 END SHNDOT
,CASE WHEN dt.ID = 4 THEN CAST(eds.NDOT AS DECIMAL(20,2))  ELSE 0 END SHRestDayNDOT
,CASE WHEN dt.ID = 5 THEN CAST(eds.REG AS DECIMAL(20,2))  ELSE 0 END LHReg
,CASE WHEN dt.ID = 5 THEN CAST(eds.OT AS DECIMAL(20,2)) ELSE 0 END LHOT
,CASE WHEN dt.ID = 6 THEN CAST(eds.REG AS DECIMAL(20,2)) ELSE 0 END LHRD
,CASE WHEN dt.ID = 6 THEN CAST(eds.OT AS DECIMAL(20,2)) ELSE 0 END LHRDOT
,CASE WHEN dt.ID = 5 THEN CAST(eds.ND AS DECIMAL(20,2)) ELSE 0 END LHND
,CASE WHEN dt.ID = 6 THEN CAST(eds.ND AS DECIMAL(20,2)) ELSE 0 END LHRDND
,CASE WHEN dt.ID = 5 THEN CAST(eds.NDOT AS DECIMAL(20,2)) ELSE 0 END LHNDOT
,CASE WHEN dt.ID = 6 THEN CAST(eds.NDOT AS DECIMAL(20,2))  ELSE 0 END LHRDNDOT
FROM tEmployeeDailySchedule eds
LEFT JOIN  tEmployee e ON e.ID = eds.ID_Employee
LEFT OUTER JOIN tBranch br ON br.ID = e.ID_Branch
LEFT OUTER JOIN tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
LEFT OUTER JOIN tPersona ps ON ps.ID = e.ID_Persona
LEFT OUTER JOIN tDepartment dep ON dep.ID = e.ID_Department
LEFT OUTER JOIN tCompany c ON c.ID = e.ID_Company
LEFT OUTER JOIN tDayType dt  ON dt.ID = eds.ID_DayType
LEFT OUTER JOIN tHoliday h ON h.Date = eds.Date AND (h.ID_Area = br.ID_Area OR h.ID_Area IS NULL)
LEFT OUTER JOIN tDayType hdt ON hdt.ID = h.ID_HolidayType
LEFT OUTER JOIN dbo.tPayrollItem flpi ON EDS.ID_FirstHalfLeavePayrollItem = flpi.ID 
LEFT OUTER JOIN dbo.tPayrollItem slpi ON EDS.ID_SecondHalfLeavePayrollItem = slpi.ID 
LEFT OUTER JOIN dbo.tPayrollItem lpi ON EDS.ID_LeavePayrollItem = lpi.ID
WHERE eds.date BETWEEN @StartDate AND @EndDate  
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1 AND e.IsActive = 1
AND (c.ID  > CASE WHEN @ID_Company IS NULL THEN 0 END OR c.ID = @ID_Company)
)a inner join tEmployee ea ON ea.ID = a.ID_Employee
LEFT OUTER JOIN tDesignation dsn ON dsn.ID = ea.ID_Designation
LEFT OUTER JOIN tJobClass jc ON jc.ID = dsn.ID_JobClass
GROUP BY Department, Employee, a.ID_Employee,  EmpCode, Company, a.ID_Company, a.ID_Department, dsn.ID_JobClass
GO