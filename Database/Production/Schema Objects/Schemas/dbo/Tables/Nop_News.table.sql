CREATE TABLE [dbo].[Nop_News] (
    [NewsID]        INT             IDENTITY (1, 1) NOT NULL,
    [LanguageID]    INT             NOT NULL,
    [Title]         NVARCHAR (1000) NOT NULL,
    [Short]         NVARCHAR (4000) NOT NULL,
    [Full]          NVARCHAR (MAX)  NOT NULL,
    [Published]     BIT             NOT NULL,
    [AllowComments] BIT             NOT NULL,
    [CreatedOn]     DATETIME        NOT NULL
);

