ALTER FUNCTION dbo.fzDailyScheduleView(@StartDate DATETIME,@EndDate DATETIME, @ID_Company INT, @ID_Session INT)
RETURNS
TABLE AS
RETURN

--DECLARE @StartDate DATETIME,@EndDate DATETIME
--Select @StartDate = '2018-02-01',@EndDate = '2019-02-15'



SELECT
eds.ID_Employee
,e.ID_Company
,e.ID_Department
,@StartDate StartDate
,@EndDate EndDate
,replace(replace(ps.Name,'Ñ','n'),'ñ','n') [Employee] 
,CASE WHEN @ID_Company IS NOT NULL THEN c.Name ELSE 'All Companies' END Company
,dep.Name Department
,eds.Date
,e.Code EmployeeCode
,dbo.fGetTimeIN(eds.ID_Employee,eds.Date,e.ID_Company) TimeIn
,dbo.fGetTimeOut(eds.ID_Employee,eds.Date,e.ID_Company) TimeOut
,RTRIM(ISNULL(lpi.Code + ' ', '') + ISNULL(flpi.Code + '-f ', '') + ISNULL(slpi.Code + '-s ', ''))  Leave
,dt.Code DayType
,hdt.Code HolidayType
,ds.Code ScheduleCode
,eds.Absences
--,(eds.Tardy / 60.00) Tardy
--,(eds.UT / 60.00) UT
,LEFT(eds.Tardy,4)Tardy
,LEFT(eds.UT,4)UT
,LEFT(eds.REG,4)REG
,LEFT(eds.OT,4)OT
,LEFT(eds.ND,4)ND
,LEFT(eds.NDOT,4)NDOT
,eal.RIn
,eal.BOut
,eal.BIn	
,eal.ROut
,(SELECT COUNT(DISTINCT eds.ID_Employee) FROM
tEmployeeDailySchedule eds WHERE eds.date BETWEEN @StartDate AND @EndDate
) SumEmployees
FROM
tEmployeeDailySchedule eds
LEFT JOIN tEmployee e ON e.ID = eds.ID_Employee
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
LEFT OUTER JOIN(
	SELECT 
	eds.ID_Employee
	,e.AccessNo
	,eds.Date
	,eal.RIn
	,CASE WHEN ds.FirstInLastOut = 1 THEN NULL ELSE CASE WHEN eal.BOut = eal.ROut AND eal.BOut > eal.BIn THEN NULL ELSE eal.BOut END END BOut
	,CASE WHEN ds.FirstInLastOut = 1 THEN NULL ELSE CASE WHEN CASE WHEN eal.BIn = eal.RIn THEN NULL ELSE eal.BIn END IS NOT NULL 
		THEN CASE WHEN DATEPART(HOUR,CASE WHEN eal.BIn = eal.RIn THEN NULL ELSE eal.BIn END) >= DATEPART(HOUR,ds.TimeOut) THEN NULL ELSE CASE WHEN eal.BIn = eal.RIn THEN NULL ELSE eal.BIn END END
	ELSE NULL END END BIn
	--,CASE WHEN eal.BIn = eal.RIn THEN NULL ELSE eal.BIn END BIn
	--,CASE WHEN eal.ROut = eal.BOut AND eal.ROut < eal.BIn THEN NULL ELSE eal.ROut END ROut
	,CASE WHEN eal.ROut = eal.BOut AND eal.ROut < eal.BIn THEN 
		CASE WHEN DATEPART(HOUR,eal.BIn) >= DATEPART(HOUR,ds.timeOut) THEN eal.BIn ELSE NULL END
	ELSE eal.ROut END ROut
	--,eal.RIn
	--,eal.BOut
	--,eal.BIn
	--,eal.ROut
	FROM
	dbo.tEmployeeDailySchedule eds
	LEFT OUTER JOIN tEmployee e ON e.ID = eds.ID_Employee
	LEFT OUTER JOIN dbo.tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
	LEFT OUTER JOIN
	(
		SELECT 
		eal.ID_Employee
		---,eal.AccessNo
		,eal.Workdate[Date]
		,MIN(CASE WHEN ID_AttendanceLogType = 1 THEN eal.DateTime ELSE NULL END) RIn	
		,min(CASE WHEN ID_AttendanceLogType = 2 THEN eal.DateTime ELSE NULL END) BOut
		,max(CASE WHEN ID_AttendanceLogType = 1 THEN eal.DateTime ELSE NULL END) BIn
		,Max(CASE WHEN ID_AttendanceLogType = 2 THEN eal.DateTime ELSE NULL END) ROut
		FROM tEmployeeAttendanceLog eal WHERE workdate BETWEEN @StartDate AND @EndDate
		GROUP BY eal.ID_Employee/*,eal.AccessNo*/,eal.Workdate
	)eal ON eal.ID_Employee = eds.ID_Employee AND eal.Date = eds.Date
)eal ON eal.ID_Employee = e.ID AND eal.Date = eds.Date
WHERE eds.date BETWEEN @StartDate AND @EndDate AND (C.ID > CASE WHEN @ID_Company IS NULL THEN 0 END OR C.ID = @ID_Company)
AND dbo.fEmployeeRights(@ID_Session,e.ID) = 1
GO

