USE Golverdimssql;
GO

ALTER DATABASE Golverdimssql SET RECOVERY SIMPLE WITH NO_WAIT
GO
DBCC SHRINKFILE(Golverdimssql_Log, 1)
GO
ALTER DATABASE Golverdimssql SET RECOVERY FULL WITH NO_WAIT
GO