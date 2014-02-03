CREATE TABLE [dbo].[Nop_CategoryLocalized] (
    [CategoryLocalizedID] INT             IDENTITY (1, 1) NOT NULL,
    [CategoryID]          INT             NOT NULL,
    [LanguageID]          INT             NOT NULL,
    [Name]                NVARCHAR (400)  NOT NULL,
    [Description]         NVARCHAR (MAX)  NOT NULL,
    [MetaKeywords]        NVARCHAR (400)  NOT NULL,
    [MetaDescription]     NVARCHAR (4000) NOT NULL,
    [MetaTitle]           NVARCHAR (400)  NOT NULL,
    [SEName]              NVARCHAR (100)  NOT NULL
);

