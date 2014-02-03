CREATE TABLE [dbo].[Nop_ProductLocalized] (
    [ProductLocalizedID] INT             IDENTITY (1, 1) NOT NULL,
    [ProductID]          INT             NOT NULL,
    [LanguageID]         INT             NOT NULL,
    [Name]               NVARCHAR (400)  NOT NULL,
    [ShortDescription]   NVARCHAR (MAX)  NOT NULL,
    [FullDescription]    NVARCHAR (MAX)  NOT NULL,
    [MetaKeywords]       NVARCHAR (400)  NOT NULL,
    [MetaDescription]    NVARCHAR (4000) NOT NULL,
    [MetaTitle]          NVARCHAR (400)  NOT NULL,
    [SEName]             NVARCHAR (100)  NOT NULL
);

