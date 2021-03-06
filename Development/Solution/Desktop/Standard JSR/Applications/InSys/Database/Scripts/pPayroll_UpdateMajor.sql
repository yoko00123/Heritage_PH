/****** Object:  StoredProcedure [dbo].[pPayroll_UpdateMajor]    Script Date: 08/03/2011 11:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[pPayroll_UpdateMajor]
@ID_PayrollPeriod INT,
@ID_Payroll INT = NULL
  --20110803
WITH ENCRYPTION
AS
SET NOCOUNT ON


DECLARE @e TABLE (ID_Employee INT, ID_Month INT, [Year] INT, monthlyrate  DECIMAL(18,9)) --dainippon 20080929
DECLARE @TotalContri TABLE (ID_Employee INT, ID_Month INT, [Year] INT,SSSEE DECIMAL(18,2),SSSER DECIMAL(18,2),SSSEC  DECIMAL(18,2),PHICEE  DECIMAL(18,2),PHICER  DECIMAL(18,2),HDMFEE  DECIMAL(18,2),HDMFER  DECIMAL(18,2))
DECLARE @MTD_Contri TABLE (ID_Employee INT, ID_Month INT, [Year] INT,SSSEE DECIMAL(18,2),SSSER DECIMAL(18,2),SSSEC  DECIMAL(18,2),PHICEE  DECIMAL(18,2),PHICER  DECIMAL(18,2),HDMFEE  DECIMAL(18,2),HDMFER  DECIMAL(18,2))
DECLARE @Contri TABLE (ID_Employee INT, ID_Month INT, [Year] INT,Amount DECIMAL(18,2),ID_PayrollItem INT)
DECLARE @Schedule INT,@UseMonthlyRate BIT
DECLARE @GSISEEID INT, @GSISERID INT 
DECLARE @PayDate DATETIME

SELECT @PayDate = PayDate
FROM tPayrollPeriod
WHERE ID = @ID_PayrollPeriod

INSERT INTO @e (ID_Employee, ID_Month, [Year])
SELECT p.ID_Employee, pp.ID_Month, pp.[Year]
FROM tPayroll p INNER JOIN
	tPayrollPeriod pp ON p.ID_PayrollPeriod = pp.ID 
WHERE (((p.ID_PayrollPeriod = @ID_PayrollPeriod) AND (p.IsProcessed = 0)) OR (p.Id = @ID_Payroll)) AND (p.IsBasicPay = 1)

SELECT 
@GSISEEID = CAST(ISNULL(dbo.fGetSetting('GSISEEID'),0) AS INT)
,@GSISERID = CAST(ISNULL(dbo.fGetSetting('GSISERID'),0) AS INT)

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|	
--totalContribution
INSERT INTO @TotalContri(ID_Employee,ID_Month,[Year],SSSEE,SSSER,SSSEC,PHICEE,PHICER,HDMFEE,HDMFER)
SELECT
	p.ID_Employee
	, pp.ID_Month
	, pp.[Year]
	, s.EE  AS SSSEE
	, s.ER   AS SSSER
	, s.EC   AS SSSEC 
	, ph.EE   AS PHICEE
	, ph.ER   AS PHICER 
	, h.EE   AS HDMFEE
	, h.ER   AS HDMFER 
FROM
tPayroll p
INNER JOIN tPayrollPeriod pp ON p.ID_PayrollPeriod = pp.ID 
INNER JOIN @e e ON e.ID_Employee = p.ID_Employee
LEFT OUTER JOIN tSSS  s ON p.SSSSubjectGross >= s.LBound AND p.SSSSubjectGross <= ISNULL(s.UBound, 999999999) 
LEFT OUTER JOIN tPHIC ph ON p.PHICSubjectGross >= ph.LBound AND p.PHICSubjectGross <= ISNULL(ph.UBound, 999999999) 
LEFT OUTER JOIN tHDMF h ON p.HDMFSubjectGross >= h.LBound AND p.HDMFSubjectGross <= ISNULL(h.UBound, 999999999)
WHERE
-- p.ID_PayrollPeriod = @ID_PayrollPeriod--
 (((p.ID_PayrollPeriod = @ID_PayrollPeriod) AND (p.IsProcessed = 0)) OR (p.Id = @ID_Payroll)) AND (p.IsBasicPay = 1)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|	
INSERT INTO @MTD_Contri(ID_Employee,ID_Month,[Year],SSSEE,SSSER,SSSEC,PHICEE,PHICER,HDMFEE,HDMFER)
SELECT
a.ID_Employee
,a.ID_Month
,a.Year
,SUM(CASE WHEN a.ID_PayrollItem = 29 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) SSSEE
,SUM(CASE WHEN a.ID_PayrollItem = 30 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) SSSER
,SUM(CASE WHEN a.ID_PayrollItem = 31 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) SSSEC
,SUM(CASE WHEN a.ID_PayrollItem = 32 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) PHICEE
,SUM(CASE WHEN a.ID_PayrollItem = 33 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) PHICER
,SUM(CASE WHEN a.ID_PayrollItem = 34 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) HDMFEE
,SUM(CASE WHEN a.ID_PayrollItem = 35 THEN  ISNULL(a.MTD_Contri, 0)  ELSE 0 END) HDMFER
FROM
(
	SELECT p.ID_Employee, d.ID_PayrollItem, pp.ID_Month, pp.[Year], SUM(d.Total) AS MTD_Contri
	FROM tPayroll p 
	INNER JOIN tPayroll_Detail d ON p.ID = d.ID_Payroll 
	INNER JOIN tPayrollItem i ON d.ID_PayrollItem = i.ID 
	INNER JOIN tPayrollPeriod pp ON p.ID_PayrollPeriod = pp.ID 
	INNER JOIN @e tmp ON p.ID_Employee = tmp.ID_Employee
	WHERE  (i.ID_Income = 15)  AND (pp.ID_Month = tmp.ID_Month) AND (pp.[Year] = tmp.[Year])
		AND pp.PayDate < @PayDate
	GROUP BY p.ID_Employee, d.ID_PayrollItem, pp.ID_Month, pp.[Year]
) a
GROUP BY 
a.ID_Employee
,a.ID_Month
,a.Year
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|	

UPDATE d
SET d.Amt = 
--SELECT 
ISNULL(CASE d.ID_PayrollItem
	WHEN 29 THEN SSSEE
	WHEN 30 THEN SSSER
	WHEN 31 THEN SSSEC
	WHEN 32 THEN PHICEE
	WHEN 33 THEN PHICER
	WHEN 34 THEN HDMFEE
	WHEN 35 THEN HDMFER
	WHEN @GSISEEID THEN   (p.DailyRate * (par.DaysPerYear / 12.00 ) * 0.09)
	WHEN @GSISERID THEN  (p.DailyRate * (par.DaysPerYear / 12.00 ) * 0.12)
ELSE 0 END,0)
FROM tPayroll_Detail d 
INNER JOIN tPayroll p ON d.ID_Payroll = p.ID 
INNER JOIN tPayrollPeriod pp ON p.ID_PayrollPeriod = pp.ID
INNER JOIN tParameter par ON par.ID = p.ID_Parameter
INNER JOIN 
(
SELECT
t.ID_Employee
,t.ID_Month
,t.[Year]
,(ISNULL(t.SSSEE,0) - ISNULL(m.SSSEE,0)) SSSEE
,(ISNULL(t.SSSER,0) - ISNULL(m.SSSER,0)) SSSER
,(ISNULL(t.SSSEC,0) - ISNULL(m.SSSEC,0)) SSSEC
,(ISNULL(t.PHICEE,0) - ISNULL(m.PHICEE,0)) PHICEE
,(ISNULL(t.PHICER,0) - ISNULL(m.PHICER,0)) PHICER
,(ISNULL(t.HDMFEE,0) - ISNULL(m.HDMFEE,0)) HDMFEE
,(ISNULL(t.HDMFER,0) - ISNULL(m.HDMFER,0)) HDMFER
FROM
@totalContri t
LEFT JOIN @mtd_Contri m ON t.ID_Employee = m.ID_Employee AND t.ID_Month = m.ID_Month AND t.[Year] = m.[Year]
) contri ON contri.ID_Employee = p.ID_Employee AND contri.ID_Month = pp.ID_Month AND contri.[Year] = pp.[Year]
INNER JOIN tPayrollItem i ON i.ID = d.ID_PayrollItem
INNER JOIN tEmployee e On e.ID = p.ID_Employee
LEFT OUTER JOIN tPayrollItemSetup pis ON pis.ID_Employee = e.ID AND d.ID_PayrollItem = pis.ID_PayrollItem
LEFT OUTER JOIN tPayrollItemSetup  pispar ON par.ID = pispar.ID_Parameter AND d.ID_PayrollItem = pispar.ID_PayrollItem
WHERE (i.ID_Income = 15) AND
 (((p.ID_PayrollPeriod = @ID_PayrollPeriod) AND (p.IsProcessed = 0)) OR (p.Id = @ID_Payroll)) AND (p.IsBasicPay = 1)	
AND d.IsScheduled = 1
AND (pis.ID IS NOT NULL OR pispar.ID IS NOT NULL)
AND d.Processed = 0