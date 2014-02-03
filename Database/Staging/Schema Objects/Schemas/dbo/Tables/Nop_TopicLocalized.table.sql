CREATE TABLE [dbo].[Nop_TopicLocalized] (
    [TopicLocalizedID] INT             IDENTITY (1, 1) NOT NULL,
    [TopicID]          INT             NOT NULL,
    [LanguageID]       INT             NOT NULL,
    [Title]            NVARCHAR (200)  NOT NULL,
    [Body]             NVARCHAR (MAX)  NOT NULL,
    [CreatedOn]        DATETIME        NOT NULL,
    [UpdatedOn]        DATETIME        NOT NULL,
    [MetaTitle]        NVARCHAR (400)  NOT NULL,
    [MetaKeywords]     NVARCHAR (400)  NOT NULL,
    [MetaDescription]  NVARCHAR (4000) NOT NULL
);

