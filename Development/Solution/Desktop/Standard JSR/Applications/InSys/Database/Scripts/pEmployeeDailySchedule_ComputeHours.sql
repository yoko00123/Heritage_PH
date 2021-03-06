/****** Object:  StoredProcedure [dbo].[pEmployeeDailySchedule_ComputeHours]    Script Date: 08/03/2011 11:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[pEmployeeDailySchedule_ComputeHours](@ID_EmployeeDailySchedule INT)
--20110803
WITH ENCRYPTION
AS
BEGIN
--	DECLARE @ID_EmployeeDailySchedule INT
--	SELECT @ID_EmployeeDailySchedule = 86549


EXEC pEmployeeDailySchedule_UpdateLeaves @ID_EmployeeDailySchedule
EXEC pEmployeeDailySchedule_GenerateLogs @ID_EmployeeDailySchedule
EXEC pEmployeeDailySchedule_ArrangeLogs @ID_EmployeeDailySchedule
----------------------------------------------------------------------------DECLARATION---------------------------------------------------------|
----------------------------------------------------------------------------GENERAL---------------------------------------|
DECLARE @d 			DATETIME 
DECLARE @e			INT 
DECLARE @ds			INT
DECLARE @FirstMinuteIn		INT
DECLARE @LastMinuteOut 	INT
DECLARE @FlexibleMinutes 	INT
DECLARE @AdjMinutes 		INT
DECLARE @MinuteIn 		INT
DECLARE @MinuteOut 		INT
DECLARE @DeductGraceMinutes	BIT
DECLARE @GraceMinutes 	INT
DECLARE @ID_DayType		INT
DECLARE @IsRD 			BIT
----------------------------------------------------------------------------GENERAL---------------------------------------|
----------------------------------------------------------------------------SETTING----------------------------------------|
DECLARE @TardyRoundingFactor	INT
DECLARE @TardyHoursAsHalfday	DECIMAL(18,2)
DECLARE @UTHoursAsHalfday	DECIMAL(18,2)
DECLARE @IsExpirableFlexiHours	BIT
----------------------------------------------------------------------------SETTING----------------------------------------|
----------------------------------------------------------------------------HALFDAY----------------------------------------|
DECLARE @SecondHalfMinuteIn 	INT
DECLARE @FirstHalfMinuteOut 	INT
DECLARE @LeaveType 		INT
DECLARE @FirstHalfWorkingMinutes	INT
DECLARE @SecondHalfWorkingMinutes 	INT
----------------------------------------------------------------------------HALFDAY----------------------------------------|
----------------------------------------------------------------------------ND----------------------------------------|
DECLARE @NDAMStartMinute 	INT
	,@NDAMEndMinute 	INT
	,@NDPMStartMinute	INT
	,@NDPMEndMinute 	INT
----------------------------------------------------------------------------ND----------------------------------------|
----------------------------------------------------------------------------GRACEPERIOD----------------------------------------|
DECLARE @GracePeriodEndMinute INT
DECLARE @GraceMinutesComputation INT
DECLARE @GraceMinutesSetUp INT
DECLARE @MonthTardy INT
DECLARE @MonthActualTardy INT
DECLARE @GracePeriodFirstInOnly INT
----------------------------------------------------------------------------GRACEPERIOD----------------------------------------|

----------------------------------------------------------------------------DECLARATION-------------------------------------------------------------|

----------------------------------------------------------------------------TABLE DEFINITION---------------------------------------------|
----------------------------------------DAILYSCHEDULE-------------------------------------------------------|

CREATE TABLE #dsd
( 
	 SeqNo			INT
	,StartMinute 		INT
	,EndMinute 		INT
	,Minutes		 AS EndMinute - StartMinute
	,ID_HourType		INT
	,FlexibleMinutes 	INT
	,FirstIn			BIT
	,LastOut			BIT
	,FlexibleStartMinute  	INT
	,FlexibleEndMinute	 INT
	,AutoApprove 		BIT
	--,ForApproval BIT
)
----------------------------------------DAILYSCHEDULE-------------------------------------------------------|
----------------------------------------ATTENDANCE-------------------------------------------------------|
CREATE TABLE #att
( 
	ID INT IDENTITY  (1,1)
	,MinuteIn		INT
	,MinuteOut		INT
	,StartMinute 		INT
	,EndMinute 		INT
	,ID_HourType		INT
	,ActualIn		INT
	,ActualOut		INT
	,Minutes		INT
	,TotalMinutes		INT
	,ForTardy		BIT
	,ActualTardy		INT
	,Tardy			INT
	,FirstIn			BIT
	,LastOut			BIT
	,StartTime 		DATETIME
	,EndTime 		DATETIME
	,Approved 		BIT
	,ForApproval 		BIT
	,ConsideredHours 	DECIMAL(18,9)
	,NDAMMinuteIn 		INT
	,NDAMMinuteOut  	INT
	,NDPMMinuteIn 		INT
	,NDPMMinuteOut 	INT
	,NDAMMinutes 		INT
	,NDPMMinutes 		INT
)
----------------------------------------ATTENDANCE-------------------------------------------------------|
----------------------------------------BREAK-------------------------------------------------------|
CREATE TABLE #break
( 
	ID INT IDENTITY  (1,1)
	,StartMinute		INT
	,EndMinute		INT
)
----------------------------------------BREAK-------------------------------------------------------|
----------------------------------------------------------------------------TABLE DEFINITION---------------------------------------------|
----------------------------------------------------------------------------INITIALIZATION----------------------------------------------------------------------------------------------|
SELECT @TardyRoundingFactor = dbo.fGetSetting('TardyRoundingFactor')
SELECT @TardyHoursAsHalfday = ISNULL(dbo.fGetSetting('TardyHoursAsHalfday'),0) * 60
SELECT @UTHoursAsHalfday =ISNULL(dbo.fGetSetting('UTHoursAsHalfday'),0) * 60
SELECT @DeductGraceMinutes = CAST(ISNULL(dbo.fGetSetting('DeductGraceMinutes'),0) AS BIT)
SELECT @GracePeriodEndMinute = ISNULL(dbo.fGetSetting('GracePeriodEndMinute'),9999) --infinity!
SELECT @GraceMinutesSetUp = ISNULL(dbo.fGetSetting('GraceMinutesSetUp'),1) --per day
SELECT @GraceMinutesComputation = ISNULL(dbo.fGetSetting('GraceMinutesComputation'),1)
Select @GracePeriodFirstInOnly  = ISNULL(dbo.fGetSetting('GracePeriodFirstInOnly'),0)

-----------------------------------------------------------------------------------------------|
SELECT @AdjMinutes = 0
-----------------------------------------------------------------------------------------------|
SELECT 
	 @e=ID_Employee
	,@d=[Date]
	,@ds=ID_DailySchedule
	,@GraceMinutes = des.GraceMinutes
	,@ID_DayType = ID_DayType
	,@IsRD= IsRD
	,@FlexibleMinutes = FlexibleHours * 60.00
	,@MinuteIn = MinuteIn
	,@MinuteOut = MinuteOut
	,@SecondHalfMinuteIn = SecondHalfMinuteIn
	,@FirstHalfMinuteOut = FirstHalfMinuteOut
	,@FirstHalfWorkingMinutes =ISNULL(FirstHalfWorkingMinutes,0.00)
	,@SecondHalfWorkingMinutes = ISNULL(SecondHalfWorkingMinutes,0.00)
	,@IsExpirableFlexiHours = IsExpirableFlexiHours
	,@NDAMStartMinute = NDAMStartMinute
	,@NDAMEndMinute = NDAMEndMinute
	,@NDPMStartMinute = NDPMStartMinute
	,@NDPMEndMinute = NDPMEndMinute
FROM 
	tEmployeeDailySchedule eds
	INNER JOIN tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
	LEFT JOIN tEmployee e ON e.ID = eds.ID_Employee
	LEFT JOIN tDesignation des ON des.ID = e.ID_Designation
WHERE eds.ID=@ID_EmployeeDailySchedule
-----------------------------------------------------------------------------------------------|
SELECT 
	@FirstMinuteIn = MIN(MinuteIn) 
	,@LastMinuteOut = MAX(MinuteOut) 
FROM tAttendance a
WHERE 	
	a.ID_Employee = @e	
AND	a.[Date] = @d

-----------------------------------------------------------------------------------------------|
SELECT  @FlexibleMinutes = 0
WHERE 	@IsExpirableFlexiHours = 1
AND 	(@MinuteIn +@FlexibleMinutes <  @FirstMinuteIn) 
-----------------------------------------------------------------------------------------------|

----------------------------------------------------------------------------INITIALIZATION----------------------------------------------------------------------------------------------|

----------------------------------------------------------------------------INSERT DAILY SCHEDULE----------------------------------------------------------------------------------------------|
INSERT INTO #dsd
(
	 SeqNo
	,StartMinute
	,EndMinute
	,ID_HourType
	,FlexibleMinutes
	,FirstIn	
	,LastOut
	,AutoApprove
)
SELECT 
	 dsd.SeqNo
	--,ISNULL(dsd.StartMinute + ROUND((sa.Hours * 60),0),  dsd.StartMinute)
	--,ISNULL(dsd.EndMinute + ROUND((sa.Hours * 60),0),  dsd.EndMinute)
	,ISNULL(dsd.StartMinute,  dsd.StartMinute)
	,ISNULL(dsd.EndMinute,  dsd.EndMinute)
	,dsd.ID_HourType
	,dsd.FlexibleMinutes
	,dsd.FirstIn
	,dsd.LastOut
	,dsd.AutoApprove
FROM
	tDailySchedule_Detail dsd
	INNER JOIN tDailySchedule DS ON ds.ID=dsd.ID_DailySchedule
	INNER JOIN tEmployeeDailySchedule eds ON eds.ID = @ID_EmployeeDailySchedule
	--LEFT JOIN tScheduleAdjustment sa ON sa.ID_EmployeeDailySchedule=@ID_EmployeeDailySchedule AND dsd.ID=sa.ID_DailySchedule_Detail
WHERE 
	dsd.ID_DailySchedule=@ds
ORDER BY dsd.StartMinute

----------------------------------------------------------------------------UPDATE SCHEDULE FOR FLEXI----------------------------------------------------------------------------------------------|

UPDATE dsd SET --The startdate and enddate is moving. better update than include in insert
	FlexibleStartMinute = dsd.StartMinute + dsd.FlexibleMinutes
	,FlexibleEndMinute = dsd.EndMinute + dsd.FlexibleMinutes 
FROM #dsd dsd
----------------------------------------------------------------------------INSERT SCHEDULE ON BREAK----------------------------------------------------------------------------------------------|
INSERT INTO #break
	(
	 StartMinute
	,EndMinute
	)
	SELECT 
	 StartMinute
	,EndMinute
FROM
	(
	SELECT TOP 100 PERCENT
		MinuteOut StartMinute
		,dbo.fGetEmployeeSameDayNextMinuteIn(@e,@d,MinuteOut) EndMinute
	FROM
			tAttendance a
	WHERE 	
		a.ID_Employee = @e	
	AND	a.[Date] = @d
	ORDER BY MinuteOut
	) a
WHERE EndMinute IS NOT NULL
----------------------------------------------------------------------------ADJUST SCHEDULE FOR FLEXIBLE IN OUT---------------------------------------------------------------------------------------------|
UPDATE #dsd SET
	StartMinute = @FirstMinuteIn
	,@AdjMinutes = @FirstMinuteIn - StartMinute
WHERE 
	FirstIn = 1
AND	@FirstMinuteIn BETWEEN StartMinute AND StartMinute + @FlexibleMinutes
AND	@FlexibleMinutes > 0

-------------------------------------------------------------------------------------------------|order is important
UPDATE #dsd SET
	StartMinute = StartMinute + @FlexibleMinutes
	,@AdjMinutes = @FlexibleMinutes --20020212 
WHERE 
	FirstIn = 1
AND	@FirstMinuteIn > StartMinute + @FlexibleMinutes
AND	@FlexibleMinutes > 0
----------------------------------------------------
UPDATE #dsd SET
	EndMinute = EndMinute + @AdjMinutes
WHERE 
	LastOut = 1
-------------------------------------------------------------------------------------------------|adjust the startminute following the endminute
UPDATE dsd SET
	StartMinute = (SELECT EndMinute FROM #dsd a WHERE LastOut=1)
FROM #dsd dsd
WHERE 
	StartMinute = @MinuteOut
----------------------------------------------------------------------------ADJUST SCHEDULE FOR FLEXIBLE BREAK---------------------------------------------------------------------------------------------|
UPDATE dsd SET
	StartMinute  = a.StartMinute
	,EndMinute = CASE WHEN a.StartMinute + a.Minutes > a.FlexibleEndMinute THEN a.FlexibleEndMinute ELSE a.StartMinute + a.Minutes END 
FROM
#dsd dsd INNER JOIN 
	(
	SELECT 
		CASE WHEN b.StartMinute > dsd.StartMinute THEN 
			CASE WHEN b.StartMinute > dsd.FlexibleStartMinute THEN dsd.FlexibleStartMinute ELSE b.StartMinute END
		ELSE
			dsd.StartMinute
		END StartMinute
		,Minutes
		,FlexibleEndMinute
		,dsd.SeqNo 
	FROM 
	#dsd dsd
	INNER JOIN #break b ON (b.StartMinute BETWEEN dsd.StartMinute AND dsd.FlexibleEndMinute) OR (b.EndMinute BETWEEN dsd.StartMinute AND dsd.FlexibleEndMinute)
	WHERE 
	dsd.ID_HourType = 5 AND FlexibleMinutes > 0
	) a
ON dsd.SeqNo = a.SeqNo
-------------------------------------------------------------------------------------------------|
UPDATE dsd SET
	StartMinute = a.EndMinute
FROM #dsd dsd
INNER JOIN #dsd a ON a.FlexibleMinutes > 0 AND dsd.SeqNo = a.SeqNo + 1
-----------------------------------HALFDAY INITIALIZATION----------------------------------------------------------|
SELECT @LeaveType = 
		CASE 	WHEN	 @ID_DayType <> 1 THEN 4
			WHEN 	eds.ID_LeavePayrollItem IS NOT NULL
				OR (eds.ID_FirstHalfLeavePayrollItem IS NOT NULL AND eds.ID_SecondHalfLeavePayrollItem IS NOT NULL)
				OR ((@FirstMinuteIn > = (@MinuteIn + @AdjMinutes + @TardyHoursAsHalfday )) AND (@LastMinuteOut < =  ( @MinuteOut + @AdjMinutes -@UTHoursAsHalfday )) AND @UTHoursAsHalfday > 0 AND @TardyHoursAsHalfday > 0 )
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

----------------------------------------------------------------------------REMOVE EXCESS SCHEDULE----------------------------------------------------------------------------------------------|
--FirstHalf
DELETE
FROM #dsd 
WHERE 
	@LeaveType =1
AND 	EndMinute <= @SecondHalfMinuteIn
--Second half
DELETE
FROM #dsd 
WHERE 
	@LeaveType =2
AND	(StartMinute >= @FirstHalfMinuteOut
OR 	ID_HourType NOT IN (1,2,3,4))
--WholeDay
DELETE
FROM #dsd 
WHERE @LeaveType = 3
----------------------------------------------------------------------------UPDATE SCHEDULE BASED ON HALFDAY SETTING----------------------------------------------------------------------------------------------|
UPDATE dsd
SET dsd.StartMinute = @SecondHalfMinuteIn

FROM #dsd dsd
WHERE @LeaveType =1
AND dsd.SeqNo = (SELECT MIN(SeqNo) FROM #dsd)
AND @SecondHalfMinuteIn IS NOT NULL

UPDATE dsd
SET 	dsd.EndMinute = @FirstHalfMinuteOut

FROM #dsd dsd
WHERE @LeaveType =2
AND dsd.SeqNo = (SELECT MAX(SeqNo) FROM #dsd)
AND @FirstHalfMinuteOut IS NOT NULL

UPDATE dsd
SET	 FirstIn = 1 
FROM #dsd dsd
WHERE @LeaveType =1
AND dsd.StartMinute = @SecondHalfMinuteIn

UPDATE dsd
SET	LastOut =1
FROM #dsd dsd
WHERE @LeaveType =2
AND 
dsd.EndMinute = @FirstHalfMinuteOut
-------------------------------------------------------------------------------------------------|	
UPDATE T SET
EndMinute = 
	(SELECT TOP 1 StartMinute
	FROM #dsd DS 
	WHERE (DS.StartMinute>T.StartMinute)
	ORDER BY StartMinute)
FROM #dsd T
WHERE
@LeaveType NOT IN (1,2) --to enforce lastout for secondhalfleave
-------------------------------------------------------------------------------------------------|	
UPDATE T SET
EndMinute = (SELECT MIN (StartMinute) FROM #dsd) + 1440
FROM #dsd T 
WHERE 	
(EndMinute IS NULL)
AND @LeaveType NOT IN (1,2)  --to enforce lastout for secondhalfleave

----------------------------------------------------------------------------INSERT ATTENDANCE----------------------------------------------------------------------------------------------|
INSERT INTO #att

(
	 MinuteIn
	,MinuteOut
	,StartMinute
	,EndMinute
	,ID_HourType
	,ActualIn
	,ActualOut
	,FirstIn	
	,LastOut	

)
SELECT 
	 MinuteIn
	,MinuteOut
	,StartMinute
	,EndMinute
	,ID_HourType
	,CASE WHEN StartMinute > MinuteIn THEN StartMinute ELSE MinuteIn END ActualIn
	,CASE WHEN EndMinute   < MinuteOut THEN EndMinute ELSE MinuteOut END ActualOut
	,FirstIn	
	,LastOut	

FROM
tAttendance a
CROSS JOIN #dsd dsd
WHERE 	
	a.ID_Employee = @e	
AND	a.[Date] = @d
AND	(dsd.ID_Hourtype IN (1,2,3,4))
AND	(MinuteIn IS NOT NULL)
AND	(MinuteOut IS NOT NULL)
ORDER BY MinuteIn,StartMinute
---------------------------------------------------------------------------------------------|
UPDATE a SET
	Minutes=ActualOut-ActualIn
FROM #att a
---------------------------------------------------------------------------------------------|
DELETE FROM #att WHERE Minutes <= 0
----------------------------------------------------------------------------TARDY SETUP----------------------------------------------------------------------------------------------|

UPDATE a SET
	ForTardy = CASE WHEN (SELECT COUNT(*) FROM #att b WHERE b.StartMinute=a.StartMinute AND b.ActualIn < a.ActualIn ) = 0 THEN 1 ELSE 0 END
FROM #att a

UPDATE a SET
	ActualTardy = CASE WHEN (ActualIn <= StartMinute) OR (ID_HourType NOT IN (1,3)) OR (ForTardy=0) OR (@ID_DayType>1) THEN 0 ELSE ActualIn - StartMinute END
	,Tardy=0
FROM #att a
---------------------------------------------------------------------------------------------|
SELECT 
	@MonthActualTardy = SUM(ActualTardy)
	,@MonthTardy = SUM(Tardy)
FROM tEmployeeDailySchedule eds
WHERE 
	(eds.ID_Employee = @e)
AND	(MONTH(eds.date) = MONTH(@d))
AND	(YEAR(eds.date) = YEAR(@d))
AND	eds.Date<@d
---------------------------------------------------------------------------------------------|
SELECT 
	@MonthActualTardy = @MonthActualTardy + ActualTardy
FROM 
	#att a
---------------------------------------------------------------------------------------------|
UPDATE a SET
	Tardy = CASE WHEN (ISNULL(dbo.fGetSetting('AllowAllGracePeriod'),0)=1 OR (FirstIn=1)) AND (@GraceMinutes>0) AND	StartMinute<@GracePeriodEndMinute THEN 
			CASE @GraceMinutesSetUp 
				WHEN 1 THEN
					CASE WHEN (ActualTardy<=@GraceMinutes) THEN
						0
					ELSE
						ActualTardy
					END 
				WHEN 2 THEN
					CASE WHEN (@MonthActualTardy<=@GraceMinutes) THEN
						0
					ELSE
						@MonthActualTardy - (@MonthTardy + @GraceMinutes)
					END 
			END
		ELSE 
			ActualTardy 
		END
FROM #att a
---------------------------------------------------------------------------------------------|


UPDATE a SET
Tardy = CAsE WHEN (a.FirstIn = 1) 
			then CASE WHEN (a.Tardy > @GraceMinutes) 
				  THEN CASE WHEN @GraceMinutesComputation = 1 then a.Tardy - @GraceMinutes  Else a.Tardy  END
				  Else 0 END
			else
			a.Tardy
			END
FROM #att a
WHERE @DeductGraceMinutes = 1 AND @GracePeriodFirstInOnly = 1


UPDATE a SET
Tardy =CASE WHEN a.Tardy > @GraceMinutes THEN CASE WHEN @GraceMinutesComputation = 1 then a.Tardy - @GraceMinutes  Else a.Tardy  END ELSE  /*a.Tardy*/0 END
FROM #att a
WHERE @DeductGraceMinutes = 1 AND @GracePeriodFirstInOnly = 0
----------------------------------------ROUNDING OFF OF TARDY--------------------------------|
UPDATE a SET
  Tardy = CASE 	WHEN 	@TardyRoundingFactor < 0 THEN 
			Tardy - (CAST(Tardy AS INT) % (@TardyRoundingFactor*-1) ) --Round Down
		ELSE
			CAST(ceiling((CAST(Tardy AS DECIMAL(18,2)) / @TardyRoundingFactor)) * @TardyRoundingFactor AS INT) --Round Up
		END
FROM #att a
WHERE Tardy > 0 AND @TardyRoundingFactor >0
---------------------------------------------------------------------------------------------|

UPDATE a SET
  Tardy = tr.Value 
FROM #att a
INNER JOIN tTardinessRounding tr on tardy between tr.lbound and tr.ubound
WHERE Tardy > 0 AND @TardyRoundingFactor=0


UPDATE a SET
	TotalMinutes = Minutes + ActualTardy - Tardy
FROM #att a

----------------------------------------------------------------------------ND SETUP----------------------------------------------------------------------------------------------|
	
UPDATE a SET
	 --NDAMMinuteIn = CASE WHEN ActualIn > @NDAMStartMinute THEN ActualIn ELSE  @NDAMStartMinute END
	 NDAMMinuteIn = CASE WHEN isnull(StartMinute + Tardy,0) > @NDAMStartMinute THEN isnull(StartMinute + Tardy,0)  ELSE  @NDAMStartMinute END
	,NDAMMinuteOut = CASE WHEN ActualOut < @NDAMEndMinute THEN ActualOut ELSE  @NDAMEndMinute END
	,NDPMMinuteIn = CASE WHEN ActualIn > @NDPMStartMinute THEN ActualIn ELSE  @NDPMStartMinute END
	--,NDPMMinuteIn = CASE WHEN isnull(StartMinute + Tardy,0) > @NDPMStartMinute THEN isnull(StartMinute + Tardy,0) ELSE  @NDPMStartMinute END
	,NDPMMinuteOut = CASE WHEN ActualOut < @NDPMEndMinute THEN ActualOut ELSE  @NDPMEndMinute END
FROM #att a
---------------------------------------------------------------------------------------------|
UPDATE a SET
  	NDAMMinutes = CASE when ([NDAMMinuteOut] - [NDAMMinuteIN]) < 0 then 0 else ([NDAMMinuteOut] - [NDAMMinuteIN]) END
	,NDPMMinutes = CASE when ([NDPMMinuteOut] - [NDPMMinuteIn]) < 0 then 0 else ([NDPMMinuteOut] - [NDPMMinuteIn]) END
FROM #att a
----------------------------------------------------------------------------OT SETUP----------------------------------------------------------------------------------------------|
UPDATE a SET
	StartTime=DATEADD(MINUTE,ActualIn,@d) 
	,EndTime=DATEADD(MINUTE,ActualOut,@d) 
	,ForApproval=	CASE	WHEN (ht.IsForApproval=0) 
				AND (@ID_DayType=1 OR d.AutoApproveHoliday=1) 
				AND (@IsRD=0) 
			THEN 0 ELSE 1 END 
FROM #att a
	INNER JOIN tHourType ht ON ht.ID = a.ID_HourType
	LEFT JOIN tEmployee e ON e.ID=@e
	LEFT JOIN tDesignation d ON d.ID=e.ID_Designation
---------------------------------------------------------------------------------------------|
UPDATE a SET
 	Approved=CASE WHEN ForApproval=0 THEN 1 ELSE 0 END
FROM #att a
---------------------------------------------------------------------------------------------|
UPDATE a
SET Approved = 1
FROM #att a LEFT OUTER JOIN #dsd dsd ON a.StartMinute = dsd.StartMinute AND a.EndMinute = dsd.EndMinute AND a.ID_HourType = dsd.ID_HourType
	WHERE a.ForApproval = 1 AND dsd.AutoApprove =1	

----------------------------------------------------------------------------INSERT DAILY SCHEDULE DETAIL----------------------------------------------------------------------------------------------|
DELETE FROM tEmployeeDailySchedule_Detail
WHERE 
	(ID_EmployeeDailySchedule=@ID_EmployeeDailySchedule)

INSERT INTO
tEmployeeDailySchedule_Detail

(
	 ID_EmployeeDailySchedule
	,StartTime
	,EndTime
	,ID_HourType
	,Minutes
	,Tardy
	,ActualTardy
	,Approved
	,ForApproval
	,ConsideredHours
	,NDAMMinuteIn
	,NDAMMinuteOut 
	,NDPMMinuteIn
	,NDPMMinuteOut
	,NDAMMinutes
	,NDPMMinutes
	
)
SELECT 	
	 @ID_EmployeeDailySchedule
	,a.StartTime
	,a.EndTime
	,ID_HourType
	,ISNULL(TotalMinutes,0)
	,Tardy
	,ActualTardy
	,a.Approved
	,a.ForApproval
	,a.ConsideredHours
	,a.NDAMMinuteIn
	,a.NDAMMinuteOut 
	,a.NDPMMinuteIn
	,a.NDPMMinuteOut
  	,NDAMMinutes
	,NDPMMinutes 
FROM 	#att a
----------------------------------------------------------------------------FILED OT SETUP----------------------------------------------------------------------------------------------|
UPDATE edsd SET
 ConsideredHours = a.IntersectionMinutes
,NDAMMinutes = CASE WHEN a.IntersectionNDAMMinutes > 0 THEN IntersectionNDAMMinutes ELSE 0 END
,NDPMMinutes = CASE WHEN a.IntersectionNDPMMinutes > 0 THEN IntersectionNDPMMinutes ELSE 0 END
,Approved = 1
,ID_WorkCredit = a.ID_WorkCredit
FROM tEmployeeDailySchedule_Detail edsd
INNER JOIN
(
	SELECT 
		 SUM(dbo.fIntersectionMinutes(edsd.StartTime,edsd.EndTime,ot.ComputedStartTime,ot.ComputedEndTime) / 60.00) IntersectionMinutes
		 , SUM(dbo.fIntersectionMinutesA(edsd.StartTime,edsd.EndTime,ot.ComputedStartTime,ot.ComputedEndTime,(dateadd(minute,ds.NDAMStartMinute,eds.[Date])),(dateadd(minute,ds.NDAMEndMinute,eds.[Date])))) IntersectionNDAMMinutes
		 , SUM(dbo.fIntersectionMinutesA(edsd.StartTime,edsd.EndTime,ot.ComputedStartTime,ot.ComputedEndTime,(dateadd(minute,ds.NDPMStartMinute,eds.[Date])),(dateadd(minute,ds.NDPMEndMinute,eds.[Date])))) IntersectionNDPMMinutes
		,edsd.ID
		,edsd.StartTime
		,edsd.EndTime
		,eds.ID_Employee
		,eds.ID ID_EmployeeDailySchedule
		,ot.ID_WorkCredit
	FROM tEmployeeDailySchedule_Detail edsd
	INNER JOIn tEmployeeDailySchedule eds ON edsd.ID_EmployeeDailySchedule = eds.ID
	INNER JOIN tDailySchedule ds ON ds.ID = eds.ID_DailySchedule
	INNER JOIN tOvertime ot ON dbo.fIntersectionMinutes(edsd.StartTime,edsd.EndTime,ot.ComputedStartTime,ot.ComputedEndTime) > 0  AND eds.ID_Employee = ot.ID_Employee
	WHERE edsd.ForApproval = 1
	AND	ot.ID_FilingStatus = 2 
	AND eds.ID = @ID_EmployeeDailySchedule
	GROUP BY edsd.ID,edsd.ID,edsd.StartTime,edsd.EndTime, eds.ID_Employee,eds.ID,ot.ID_WorkCredit
) a ON 
a.StartTime = edsd.StartTime AND a.EndTime = edsd.EndTime AND 
a.ID_EmployeeDailySchedule = @ID_EmployeeDailySchedule
----------------------------------------------------------------------|

UPDATE eds SET
	 eds.ComputedREG = 0.0
	, eds.ComputedOT = 0.0
	, eds.ComputedND = 0.0
	, eds.ComputedNDOT = 0.0
	, eds.RatedREG = 0.0
	, eds.RatedOT = 0.0
	, eds.RatedND = 0.0
	, eds.RatedNDOT = 0.0
	, eds.OffsetREG = 0.0
	, eds.OffsetOT = 0.0
	, eds.OffsetND = 0.0
	, eds.OffsetNDOT = 0.0
FROM tEmployeeDailySchedule eds
WHERE ID = @ID_EmployeeDailySchedule


EXEC pEmployeeDailySchedule_ComputeHeader @ID_EmployeeDailySchedule

/*	
	SELECT * FROM #dsd
	SELECT * FROM #att
	SELECT * FROM #break


*/	
----------------------------------------------------------------------|
DROP TABLE #dsd
DROP TABLE #att
DROP TABLE #break
----------------------------------------------------------------------|
END

