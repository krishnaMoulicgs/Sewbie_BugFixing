ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [dbSewbie_dev_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\dbSewbie_dev_log.ldf', SIZE = 6272 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

