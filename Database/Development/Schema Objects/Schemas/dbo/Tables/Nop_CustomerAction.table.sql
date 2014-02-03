CREATE TABLE [dbo].[Nop_CustomerAction] (
    [CustomerActionID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100)  NOT NULL,
    [SystemKeyword]    NVARCHAR (100)  NOT NULL,
    [Comment]          NVARCHAR (1000) NOT NULL,
    [DisplayOrder]     INT             NOT NULL
);

