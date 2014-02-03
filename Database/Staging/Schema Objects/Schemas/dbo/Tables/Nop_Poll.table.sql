CREATE TABLE [dbo].[Nop_Poll] (
    [PollID]         INT            IDENTITY (1, 1) NOT NULL,
    [LanguageID]     INT            NOT NULL,
    [Name]           NVARCHAR (400) NOT NULL,
    [SystemKeyword]  NVARCHAR (400) NOT NULL,
    [Published]      BIT            NOT NULL,
    [ShowOnHomePage] BIT            NOT NULL,
    [DisplayOrder]   INT            NOT NULL,
    [StartDate]      DATETIME       NULL,
    [EndDate]        DATETIME       NULL
);

