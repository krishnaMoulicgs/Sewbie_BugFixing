


CREATE PROCEDURE [dbo].[Nop_Maintenance_ReindexTables]
AS
BEGIN
	--indexing
	DECLARE @TableName sysname
	DECLARE cur_reindex CURSOR FOR
	SELECT table_name
	FROM information_schema.tables
	WHERE table_type = 'base table'
	OPEN cur_reindex
	FETCH NEXT FROM cur_reindex INTO @TableName
	WHILE @@FETCH_STATUS = 0
	BEGIN
		--PRINT 'Reindexing ' + @TableName + ' table'
		DBCC DBREINDEX (@TableName, ' ', 80)
		FETCH NEXT FROM cur_reindex INTO @TableName
		END
	CLOSE cur_reindex
	DEALLOCATE cur_reindex
END