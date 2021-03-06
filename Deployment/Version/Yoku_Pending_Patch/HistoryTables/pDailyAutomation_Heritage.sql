
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------- ALTER DAILY AUTOMATION
ALTER PROCEDURE [dbo].[pDailyAutomation]
AS


BEGIN
DECLARE @DATE DATETIME
DECLARE @DateNow DATETIME
DECLARE @DataDate DATETIME

SELECT @DataDate = GETDATE()
SELECT @Date = Dateadd(DAy,1,Max(date)) from tDailyAutomation
SELECT @Date = Value
	FROM tSetting
	WHERE 
		(@Date IS NULL)
	AND	(Name='DailyAutomationStartDate')

IF @Date IS NOT NULL
BEGIN
	SELECT @DateNow = CAST(convert(varchar,GETDATE(),101) AS DATETIME)
	WHILE  @DATE <= @DateNow
		begin
		INSERT INTO tDailyAutomation
			([DATE])
		VALUES
			(@DATE)
		--EXEC pLeaveCreditAccrual @DATE
		--EXEC pUpdateFromFutureEmployeeMovement @Date
		
		SELECT @DATE = DATEADD(DAY,1,@DATE)
		EXEC dbo.pDataHistory @DataDate

		end
END
--SELECT @DATE

END

