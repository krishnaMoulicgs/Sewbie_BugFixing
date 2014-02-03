CREATE TABLE [dbo].[Nop_Log] (
    [LogID]       INT             IDENTITY (1, 1) NOT NULL,
    [LogTypeID]   INT             NOT NULL,
    [Severity]    INT             NOT NULL,
    [Message]     NVARCHAR (1000) NOT NULL,
    [Exception]   NVARCHAR (4000) NOT NULL,
    [IPAddress]   NVARCHAR (100)  NOT NULL,
    [CustomerID]  INT             NOT NULL,
    [PageURL]     NVARCHAR (100)  NOT NULL,
    [ReferrerURL] NVARCHAR (100)  NOT NULL,
    [CreatedOn]   DATETIME        NOT NULL
);

