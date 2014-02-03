CREATE TABLE [dbo].[Nop_NewsComment] (
    [NewsCommentID] INT             IDENTITY (1, 1) NOT NULL,
    [NewsID]        INT             NOT NULL,
    [CustomerID]    INT             NOT NULL,
    [IPAddress]     NVARCHAR (100)  NOT NULL,
    [Title]         NVARCHAR (1000) NOT NULL,
    [Comment]       NVARCHAR (MAX)  NOT NULL,
    [CreatedOn]     DATETIME        NOT NULL
);

