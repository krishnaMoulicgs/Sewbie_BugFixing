CREATE TABLE [dbo].[Nop_Campaign] (
    [CampaignID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    [Subject]    NVARCHAR (200) NOT NULL,
    [Body]       NVARCHAR (MAX) NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL
);

