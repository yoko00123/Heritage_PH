/****** Object:  StoredProcedure [dbo].[pEmployeeDailySchedule_ComputeHeader]    Script Date: 08/03/2011 11:52:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pEmployeeDailySchedule_ComputeHeader](@ID_EmployeeDailySchedule INT)
--20110803
WITH ENCRYPTION
AS
BEGIN
----------------------------------------------------------------------------DECLARATION---------------------------------------------------------|

DECLARE @LeaveType INT
DECLARE @LeaveWithPay INT
DECLARE @FirstHalfWorkingMinutes INT
DECLARE @SecondHalfWorkingMinutes INT

DECLARE @TardyHoursAsHalfDay INT
DECLARE @UTHoursAsHalfDay INT
DECLARE @OTRoundingFactor INT
DECLARE @MinOTHours DECIMAL(18,2)

DECLARE @ID_DayType INT
DECLARE @FirstMinuteIn INT
DECLARE @LastMinuteOut INT
DECLARE @MinuteIn INT
DECLARE @MinuteOut INT
DECLARE @FlexibleMinutes INT
DECLARE @AdjMinutes INT
DECLARE @IsWorking INT
DECLARE @FillRegHoursFirst INT
----------------------------------------------------------------------------DECLARATION---------------------------------------------------------|
----------------------------------------------------------------------------INITIALIZATION---------------------------------------------------------|
SELECT 	@FirstMinuteIn = MIN(MinuteIn) 
	,@LastMinuteOut = MAX(MinuteOut)
FROM tAttendance a
INNER JOIN tEmployeeDailySchedule eds ON eds.Date = a.Date AND eds.ID_Employee = a.ID_Employee
WHERE eds.ID = @ID_EmployeeDailySchedule
-----------------------------------------------------------------------------------------------|
SELECT 	@TardyHoursAsHalfday = (ISNULL(dbo.fGetSetting('TardyHoursAsHalfDay'),0) * 60 )
	,@UTHoursAsHalfday = (ISNULL(dbo.fGetSetting('UTHoursAsHalfDay'),0)  * 60 )
	,@OTRoundingFactor = ISNULL(dbo.fGetSetting('OTRoundingFactor'),1)
	,@MinOTHours = ISNULL(dbo.fGetSetting('MinOTHours'),0)
	,@FillRegHoursFirst = ISNULL(dbo.fGetSetting('FillRegHoursFirst'),0)
-----------------------------------------------------------------------------------------------|
SELECT 
	@MinuteIn = MinuteIn
	,@MinuteOut = MinuteOut
	,@FirstHalfWorkingMinutes =ISNULL(FirstHalfWorkingMinutes,(WorkingMinutes/2))
	,@SecondHalfWorkingMinutes = ISNULL(SecondHalfWorkingMinutes,(WorkingMinutes/2))
	,@ID_DayType = eds.ID_DayType
	, @FlexibleMinutes = FlexibleHours * 60.00
FROM tDailySchedule ds
INNER JOIN tEmployeeDailySchedule eds ON eds.ID_DailySchedule = ds.ID
WHERE eds.ID = @ID_EmployeeDailySchedule
-----------------------------------------------------------------------------------------------|
SELECT @AdjMinutes =CASE WHEN (@FirstMinuteIn > @MinuteIn + @FlexibleMinutes) THEN @FlexibleMinutes ELSE @FirstMinuteIn - @MinuteIn END
-----------------------------------------------------------------------------------------------|

-----------------------HOLIDAY------------------------------|
Select 
@IsWorking = ISNULL(h.IsWorking,0)
from tEmployeeDailySchedule eds
INNER JOIN tHoliday h ON h.date = eds.Date
Where eds.ID = @ID_EmployeedailySchedule AND eds.ID_DayType = 3
-----------------------HOLIDAY------------------------------|


SELECT @LeaveWithPay = 
		CASE	WHEN LeaveWithPay = 1 OR (FirstHalfLeaveWithPay = 1 AND SecondHalfLeaveWithPay =1  ) THEN 1   
			WHEN  FirstHalfLeaveWithPay =1 OR SecondHalfLeaveWithPay = 1 THEN 2
			 
		ELSE 0 END
	,@LeaveType = 
		CASE 	WHEN	@ID_DayType IN (2,4,5,6) then 4-- @ID_DayType <> 1 THEN 4
			WHEN	@ID_DayType = 3 AND @IsWorking = 0 then 4
			WHEN 	@ID_DayType = 3 AND @IsWorking = 1 then 5 
			WHEN 	eds.ID_LeavePayrollItem IS NOT NULL
				OR (eds.ID_FirstHalfLeavePayrollItem IS NOT NULL AND eds.ID_SecondHalfLeavePayrollItem IS NOT NULL)
				OR ((@FirstMinuteIn > = (@MinuteIn + @AdjMinutes + @TardyHoursAsHalfday )) AND (@LastMinuteOut < =  ( @MinuteOut + @AdjMinutes -@UTHoursAsHalfday )) AND @UTHoursAsHalfday > 0 AND @TardyHoursAsHalfday > 0 )
				--OR (eds.ID_FirstHalfLeavePayrollItem IS NOT NULL AND (@LastMinuteOut < =  ((@MinuteOut+@AdjMinutes) - @UTHoursAsHalfday ) AND @UTHoursAsHalfday > 0)  )
				--OR (eds.ID_SecondHalfLeavePayrollItem IS NOT NULL  AND (@FirstMinuteIn > = ((@MinuteIn+@AdjMinutes)  + @TardyHoursAsHalfday ) AND @TardyHoursAsHalfday > 0) OR  )
				OR (@FirstMinuteIn IS NULL OR @LastMinuteOut IS NULL)
				THEN 3 --Whole Day
			WHEN	eds.ID_FirstHalfLeavePayrollItem IS NOT NULL
				OR (@FirstMinuteIn > = ((@MinuteIn+@AdjMinutes)  + @TardyHoursAsHalfday ) AND @TardyHoursAsHalfday > 0 )
				THEN 1 --First Half
			WHEN 	eds.ID_SecondHalfLeavePayrollItem IS NOT NULL 
				OR (@LastMinuteOut < =  ((@MinuteOut+@AdjMinutes) -@UTHoursAsHalfday ) AND @UTHoursAsHalfday > 0) 
				THEN 2 --2nd Half
			ELSE 4 --none
		END
FROM tEmployeeDailySchedule eds 
WHERE eds.ID = @ID_EmployeeDailySchedule
----------------------------------------------------------------------------INITIALIZATION---------------------------------------------------------|
UPDATE eds SET
	  eds.REG = 0.0
	, eds.EXT = 0.0
	, eds.OT = 0.0
	, eds.ND = 0.0
	, eds.NDOT = 0.0
	, eds.TARDY = 0.0
	, eds.ActualTardy = 0.0
	, eds.UT = 0.0

FROM tEmployeeDailySchedule eds
WHERE ID = @ID_EmployeeDailySchedule
------------------------------------------------------------------------------------------------------|	
UPDATE eds  SET
	 REG = a.REG
	,OT = a.OT
	,ND = a.ND
	,NDOT = a.NDOT
	,TARDY = a.TARDY
	,ActualTardy = a.ActualTardy
FROM
	tEmployeeDailySchedule eds
INNER JOIN 
(
	SELECT 
		ID_EmployeeDailySchedule
		,SUM (REG) 	REG
		,SUM(OT)	OT
		,SUM(ND)	ND
		,SUM(NDOT)	NDOT
		,SUM(TARDY)	TARDY
		,SUM(ActualTardy)	ActualTardy
	FROM
	(
		SELECT
			ID_EmployeeDailySchedule
		        ,REG = 	CASE WHEN ID_Hourtype = 1 THEN SUM(ApprovedMinutes) ELSE 0 END
			,OT = 	CASE WHEN ID_Hourtype = 2 THEN SUM(ApprovedMinutes) ELSE 0 END
			,ND = 	CASE WHEN ID_Hourtype = 3 THEN SUM(ApprovedMinutes) ELSE 0 END
			,NDOT = CASE WHEN ID_Hourtype = 4 THEN SUM(ApprovedMinutes) ELSE 0 END
			,TARDY=	SUM(Tardy) 
			,ActualTardy=SUM(ActualTardy) 
		FROM 
			tEmployeeDailySchedule_Detail edsd 
			
		WHERE edsd.ID_EmployeeDailySchedule=@ID_EmployeeDailySchedule
			AND (Approved=1) and (ID_WorkCredit = 1)
		GROUP BY ID_EmployeeDailySchedule,ID_Hourtype
	
	) a
	GROUP BY ID_EmployeeDailySchedule
) a ON eds.ID = a.ID_EmployeeDailySchedule

------------------------------------------------------------------------------------------------------|	
UPDATE eds SET
eds.REG =	CASE	WHEN @LeaveType = 3 THEN 0
			WHEN @LeaveType = 2 THEN ISNULL(ds.FirstHalfWorkingMinutes,ds.WorkingMinutes/2.00)
			WHEN @LeaveType = 1 THEN ISNULL(ds.SecondHalfWorkingMinutes,ds.WorkingMinutes/2.00)
		ELSE ds.WorkingMinutes END
,eds.EXT = eds.REG -
		 CASE	WHEN @LeaveType = 3 THEN 0
			WHEN @LeaveType = 2 THEN ISNULL(ds.FirstHalfWorkingMinutes,ds.WorkingMinutes/2.00)
			WHEN @LeaveType = 1 THEN ISNULL(ds.SecondHalfWorkingMinutes,ds.WorkingMinutes/2.00)
		ELSE ds.WorkingMinutes END
FROM tEmployeeDailySchedule eds
INNER JOIN tDailySchedule ds ON ds.ID=eds.ID_DailySchedule
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
AND 	eds.REG > CASE	WHEN @LeaveType = 3 THEN 0
			WHEN @LeaveType = 2 THEN ISNULL(ds.FirstHalfWorkingMinutes,ds.WorkingMinutes/2.00)
			WHEN @LeaveType = 1 THEN ISNULL(ds.SecondHalfWorkingMinutes,ds.WorkingMinutes/2.00)
		ELSE ds.WorkingMinutes END
------------------------------------------------------------------------------------------------------|	MIN HOURS FOR OT
UPDATE eds SET
eds.OT = CASE 	WHEN eds.OT < (@MinOTHours * 60)  THEN 0 ELSE eds.OT END
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
AND 	eds.OT > 0
AND 	@MinOTHours > 0

------------------------------------------------------------------------------------------------------|Round off of OT
UPDATE eds SET
eds.OT = CASE 	WHEN @OTRoundingFactor < 0 THEN 
			eds.OT - (CAST(eds.OT AS INT) % (@OTRoundingFactor*-1) ) --Round Down
		ELSE
			CAST(ROUND((CAST(eds.OT AS DECIMAL(18,2)) / @OTRoundingFactor) ,0) * @OTRoundingFactor AS INT) --Round Up
	END
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
AND 	eds.OT > 0
------------------------------------------------------------------------------------------------------|	

UPDATE eds SET
	eds.REG =eds.REG +
				ISNULL(
				(
				SELECT SUM (Hours) Hours  
				FROM tDailySchedule_Detail xds
				WHERE (WithPAy = 1 and ID_Hourtype = 5) 
				AND xds.ID_DailySchedule =  eds.ID_DailySchedule
				AND (xds.StartMinute BETWEEN a.MinuteIn AND a.MinuteOut AND xds.EndMinute BETWEEN a.MinuteIn AND a.MinuteOut)
				)
				,0) * 60.00

FROM tEmployeeDailySchedule eds
LEFT JOIN (
	SELECT ID_EMployee
		,Date
		, MIN(MinuteIn) MinuteIn
		, MAX(MinuteOut)MinuteOut 
	FROM tAttendance 
	GROUP BY ID_EMployee,Date
	) a ON eds.ID_Employee=a.ID_Employee AND eds.Date = a.Date

WHERE 
eds.ID = @ID_EmployeeDailySchedule
AND eds.REG > 0
-----------------------------------------------------------------------------------------------------------

UPDATE eds SET
	  eds.REG = (eds.REG 	/ CONVERT(DECIMAL(18,2),60)) --+ CASE WHEN eds.REG > 0 THEN .5 ELSE 0 END
	, eds.EXT = eds.EXT 	/ CONVERT(DECIMAL(18,2),60)
	, eds.OT = eds.OT 	/ CONVERT(DECIMAL(18,2),60)
	, eds.ND = eds.ND  	/ CONVERT(DECIMAL(18,2),60)
	, eds.NDOT = eds.NDOT 	/ CONVERT(DECIMAL(18,2),60)
	, eds.UT =ISNULL(
			CASE WHEN ID_DayType <> 1 OR (REG+EXT+OT) = 0 THEN 0
			ELSE
				CASE	WHEN @LeaveType = 3 THEN 0
					WHEN @LeaveType = 2 THEN ISNULL(ds.FirstHalfWorkingMinutes,(ds.WorkingMinutes ) /2.00)
					WHEN @LeaveType = 1 THEN ISNULL(ds.SecondHalfWorkingMinutes,(ds.WorkingMinutes ) /2.00)
				ELSE ds.WorkingMinutes END
			- (eds.REG + eds.Tardy)
			END
		,0)
	, eds.IsForComputation = 0
FROM tEmployeeDailySchedule eds
LEFT JOIN tDailySchedule ds ON ds.ID=eds.ID_DailySchedule
WHERE eds.ID = @ID_EmployeeDailySchedule
------------------------------------------------------------------------------------------------------|
UPDATE eds SET
	  eds.Tardy = CASE WHEN eds.Tardy < 0 THEN 0 ELSE eds.Tardy END
	  ,eds.UT = ISNULL(CASE WHEN eds.UT < 0 THEN 0 ELSE eds.UT END,0)
FROM tEmployeeDailySchedule eds
WHERE eds.ID = @ID_EmployeeDailySchedule
------------------------------------------------------------------------------------------------------|
UPDATE eds
SET isActualAbsent =  CASE WHEN (eds.REG+eds.EXT+eds.OT) = 0 AND eds.ID_DayType =1  THEN 1 ELSE 0 END
FROM tEmployeeDailySchedule eds 
WHERE  eds.ID = @ID_EmployeeDailySchedule
------------------------------------------------------------------------------------------------------|
UPDATE eds
SET
Absences = 	CASE	WHEN @LeaveWithPay =1 THEN -- Paid WholeDay Leave
			0
			WHEN @LeaveWithPay = 2 THEN -- Paid HalfDay Leave
				CASE 	WHEN @LeaveType = 3 THEN 0.5 ELSE 	0 END
			WHEN @LeaveType = 5 AND eds.Reg <=0 then 1 
			ELSE
				CASE	WHEN @LeaveType = 3 THEN 1
					WHEN @LeaveType = 2 OR @LeaveType = 1 THEN 0.5
				ELSE 0	END 
		END
FROM tEmployeeDailySchedule eds 
WHERE  eds.ID = @ID_EmployeeDailySchedule

-------------------------------------------------------------------------------------------------------infarmco NDOT
UPDATE eds SET 
NDOT = Cast(NDHours as DECIMAL(18,2))
FROM 
tEmployeeDailySchedule eds 
INNER JOIN
(
	SELECT 
	ID_EMployeeDailySchedule
	,SUM(ISNULL(NDHours,0))NDHours
	FROM tEmployeeDailySchedule_Detail
	WHERE ID_EmployeeDailySchedule = @ID_EmployeeDailySchedule and ID_HourType =2 and Approved = 1
	GROUP BY ID_EMployeeDailySchedule
)edsd ON eds.ID = edsd.ID_EmployeeDailySchedule
WHERE eds.ID = @ID_EmployeeDailySchedule
-------------------------------------------------------------------------------------------------------infarmco NDOT
UPDATE eds SET 
ND = Cast(NDHours as DECIMAL(18,2))
FROM 
tEmployeeDailySchedule eds 
INNER JOIN
(
	SELECT 
	ID_EMployeeDailySchedule
	,SUM(ISNULL(NDHours,0))NDHours
	FROM tEmployeeDailySchedule_Detail
	WHERE ID_EmployeeDailySchedule = @ID_EmployeeDailySchedule and ID_HourType =1 and Approved = 1
	GROUP BY ID_EMployeeDailySchedule
)edsd ON eds.ID = edsd.ID_EmployeeDailySchedule
WHERE eds.ID = @ID_EmployeeDailySchedule

------------------------------------------------------------------------------------------------------|	MIN HOURS FOR NDOT
UPDATE eds SET
eds.NDOT = CASE WHEN eds.NDOT < (@MinOTHours)  THEN 0 ELSE eds.NDOT END
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
AND 	eds.NDOT  > 0
AND 	@MinOTHours > 0

------------------------------------------------------------------------------------------------------|Round off of NDOT	

UPDATE eds SET
eds.NDOT = 	CASE 	WHEN @OTRoundingFactor < 0 THEN 
				(eds.NDOT* 60.00) - (CAST(eds.NDOT* 60.00 AS INT) % (@OTRoundingFactor*-1) ) --Round Down

			ELSE
				CAST(ROUND((CAST(eds.NDOT * 60.00 AS INT) / @OTRoundingFactor) ,0) * @OTRoundingFactor AS INT) --Round Up
		END 	/ CONVERT(DECIMAL(18,2),60)
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
AND 	eds.OT > 0
AND 	@OTRoundingFactor <> 0
AND	@OTRoundingFactor <> 1


IF @FillRegHoursFirst =  1 
BEGIN

UPDATE eds SET
eds.OT = CASE WHEN (eds.OT - (eds.Tardy/60.00)) < 0 THEN 0 ELSE (eds.OT -(eds.Tardy/60.00)) END
,eds.REG = eds.REG + ((eds.Tardy/60.00) - (CASE WHEN ((eds.Tardy/60.00) - eds.OT) < 0 THEN 0 ELSE ((eds.Tardy/60.00) - eds.OT) END))
FROM tEmployeeDailySchedule eds 
WHERE  eds.ID = @ID_EmployeeDailySchedule
AND eds.Tardy > 0 AND eds.OT > 0

END

--------------------ROBBIE 20081214----------------OFFSETREG---------------------------------------------------------------------------------------------------------|
--actual hours before offset
UPDATE eds SET
	  ActualREG = REG 
	  ,ActualOT = OT
	  ,ActualND = ND 
	  ,ActualNDOT = NDOT
FROM tEmployeeDailySchedule eds
WHERE eds.ID = @ID_EmployeeDailySchedule
--------------------------------------------------------------------------------------------------------|
UPDATE eds SET
	   COMPUTEDREG = REG
	  ,COMPUTEDOT = OT
	  ,COMPUTEDND = ND
	  ,COMPUTEDNDOT = NDOT
	  ,REG = REG - OffsetREG
	  ,OT = OT - OffsetOT
	  ,ND = ND - OffsetND
	  ,NDOT = NDOT - OffsetNDOT
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
--------------------------------------------------------------------------------------------------------|
UPDATE eds SET
	   RatedREG = OffsetREG * ISNULL((SELECT RAte FROM tPAyrollITemRate pir WHERE pir.ID_PArameter = par.ID AND pir.ID_PayrollITem = eds.ID_DayType),1)
	  ,RatedOT = OffsetOT   * ISNULL((SELECT RAte FROM tPAyrollITemRate pir WHERE pir.ID_PArameter = par.ID AND pir.ID_PayrollITem = eds.ID_DayType + 6 ),1)
	  ,RatedND = OffsetND  * ISNULL((SELECT RAte FROM tPAyrollITemRate pir WHERE pir.ID_PArameter = par.ID AND pir.ID_PayrollITem = eds.ID_DayType + 12 ),1)
	  ,RatedNDOT = OffsetNDOT  * ISNULL((SELECT RAte FROM tPAyrollITemRate pir WHERE pir.ID_PArameter = par.ID AND pir.ID_PayrollITem = eds.ID_DayType + 18 ),1)
FROM tEmployeeDailySchedule eds
INNER JOIN tEmployee e ON e.ID = eds.ID_Employee
INNER JOIN tParameter par ON par.ID = e.ID_Parameter
INNER JOIN tPayrollItemRate r ON par.ID = r.ID_Parameter
WHERE eds.ID = @ID_EmployeeDailySchedule
--------------------------------------------------------------------------------------------------------|
UPDATE eds SET
	   OffsetRate = OffsetREG + RatedOT + RatedND + RatedNDOT 
FROM tEmployeeDailySchedule eds
WHERE 
	eds.ID = @ID_EmployeeDailySchedule
--------------------ROBBIE 20081214----------------OFFSETREG------------------------------------------------------------------------------------------------------|

---------ARVIN 20110519 NoAttendance regardless of DayType for non regular absences purpose
UPDATE eds SET 
IsNoAttendance = CASE WHEN (eds.REG+eds.EXT+eds.OT) = 0 THEN 
		1
	ELSE 
		CASE WHEN (eds.REG+eds.EXT+eds.OT) = ds.WorkingHours / 2.00 THEN 
			.5
		ELSE
			0
		END
	END
 FROM dbo.tEmployeeDailySchedule eds
LEFT JOIN dbo.tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
WHERE eds.ID = @ID_EmployeeDailySchedule					
---------ARVIN 20110519 NoAttendance regardless of DayType for non regular absences purpose

END
