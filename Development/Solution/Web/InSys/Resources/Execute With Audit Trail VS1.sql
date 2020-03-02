DECLARE @Menu VARCHAR(50) = '{{Menu}}', 
		@ID_Session INT = {{ID_Session}}, 
		@HostName VARCHAR(50) = '{{HostName}}',
		@TableName VARCHAR(200) = '{{TableName}}'

-- -------------------------
-- @LJGomez, 20160927
-- InSys Standard Web (ERP)
-- -> Save Info with Audit Trail
-- /* If You See this script and found errors, Need Modification or Improrvement, please refer to @Software Provider */
-- ------------------------

--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
--SET NOCOUNT ON
--SET XACT_ABORT ON

IF OBJECT_ID(N'tempdb.dbo.#STable') IS NOT NULL DROP TABLE #STable
IF OBJECT_ID(N'tempdb.dbo.#PTable') IS NOT NULL DROP TABLE #PTable
IF OBJECT_ID(N'tempdb.dbo.#DTable') IS NOT NULL DROP TABLE #DTable

CREATE TABLE #STable ({{SchemaTable}})
CREATE TABLE #PTable ({{SchemaTable}})
CREATE TABLE #DTable ([ID] INT)

{{InsertDataTable}}
INSERT INTO #DTable VALUES {{DeletedTable}}

DECLARE @tbl AS TABLE(ID INT, RowType INT, RID INT IDENTITY(1, 1))

-- Generic 
-- Delete
INSERT INTO #PTable ([ID], {{SchemaInsert}})
	SELECT T.ID, {{SelectInsert}} FROM {{TableName}} AS T WITH(NOLOCK) WHERE T.ID IN (SELECT ID FROM #DTable)

DELETE
	FROM 
	{{TableName}} 
	OUTPUT Deleted.ID, 2 INTO @tbl 
	WHERE ID IN (SELECT ID FROM #DTable)

IF EXISTS(SELECT * FROM #STable WHERE ID > 0)
BEGIN
	INSERT INTO #PTable ([ID], {{SchemaInsert}})
	SELECT T.ID, {{SelectInsert}} FROM {{TableName}} AS T WITH(NOLOCK) INNER JOIN #STable AS S ON T.ID = S.ID WHERE S.ID > 0

	UPDATE T SET 
	{{SchemaUpdate}}
	FROM {{TableName}} AS T WITH(NOLOCK) INNER JOIN #STable AS S ON T.ID = S.ID WHERE S.ID > 0
END

IF EXISTS(SELECT * FROM #STable WHERE ID <= 0)
BEGIN
	INSERT INTO {{TableName}}
	({{SchemaInsert}})
	OUTPUT Inserted.ID, 0 INTO @tbl
	SELECT {{ColumnInsert}} FROM #STable AS S WHERE S.ID <= 0

	-- update temp table for new values
	DELETE FROM #STable WHERE ID <= 0

	INSERT INTO #STable ([ID], {{SchemaInsert}})
	     SELECT T.ID, {{SelectInsert}} FROM {{TableName}} T WHERE T.ID IN (SELECT ID FROM @tbl)
END

INSERT INTO @tbl 
SELECT ID, 1 FROM #STable s
	WHERE NOT EXISTS(SELECT * FROM @tbl WHERE ID = s.ID) 

SELECT ID FROM @tbl

-- ---------------------- Audit Trail

-- save session

DECLARE @XML NVARCHAR(MAX)
DECLARE @AID INT, @TID INT 
  
-- Saved ID
EXEC @AID = dbo.p_AuditTrailRev @ID_Session, 2, @Menu, NULL, @HostName, @ID_Session
-- Table ID
EXEC @TID = dbo.p_AuditTrailRev @AID, 7, @TableName, NULL, @HostName, @ID_Session

declare @string varchar(20) = '345' -- [AuditTrailType] : InSert, Update, Delete
declare @index int = 1
declare @len int = Len(@string)
declare @char INT

WHILE @index<= @len
BEGIN
	Begin try
		set @char = SUBSTRING(@string, @index, 1)

		IF (@index - 1) = 0
		BEGIN -- insert
			SET @XML = (SELECT * FROM #STable AS ST WHERE ID IN (SELECT t.ID From @Tbl t Where t.RowType = (@index - 1)) FOR XML AUTO) 
		END
		ELSE IF (@index - 1) = 1
		BEGIN -- update
			SET @XML = (SELECT * FROM  (SELECT * FROM #STable AS ST WHERE ID IN (SELECT t.ID From @Tbl t Where t.RowType = (@index - 1))) a
				INNER JOIN #PTable AS PT ON pt.ID = a.ID FOR XML AUTO)
		END
		else  --Deleted
		begin
			SET @XML = (Select * from #PTable Where ID in (Select ID From @Tbl Where RowType = (@index - 1) ) FOR XML AUTO)
		end
	 
		if @XML is not null
		begin
			EXEC dbo.p_AuditTrailRev @TID, @char, @TableName, @XML, @HostName, @ID_Session
		end
	end try
	begin catch
		PRINT 'warning: data could not be save possible the data has been parse with invalid characters'
	end catch
SET @index= @index+ 1
END
	 
DROP TABLE #STable
DROP TABLE #PTable
DROP TABLE #DTable