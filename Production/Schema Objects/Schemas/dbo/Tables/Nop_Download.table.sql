CREATE TABLE [dbo].[Nop_Download] (
    [DownloadID]     INT             IDENTITY (1, 1) NOT NULL,
    [UseDownloadURL] BIT             NOT NULL,
    [DownloadURL]    NVARCHAR (400)  NOT NULL,
    [DownloadBinary] VARBINARY (MAX) NULL,
    [ContentType]    NVARCHAR (20)   NOT NULL,
    [Filename]       NVARCHAR (100)  NOT NULL,
    [Extension]      NVARCHAR (20)   NOT NULL,
    [IsNew]          BIT             NOT NULL
);

