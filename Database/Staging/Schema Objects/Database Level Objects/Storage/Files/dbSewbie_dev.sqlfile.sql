ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [dbSewbie_dev], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\dbSewbie_dev.mdf', SIZE = 9216 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];



