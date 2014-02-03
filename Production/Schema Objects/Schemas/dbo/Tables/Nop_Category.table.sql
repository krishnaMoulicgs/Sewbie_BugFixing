CREATE TABLE [dbo].[Nop_Category] (
    [CategoryID]       INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (400)  NOT NULL,
    [Description]      NVARCHAR (MAX)  NOT NULL,
    [TemplateID]       INT             NOT NULL,
    [MetaKeywords]     NVARCHAR (400)  NOT NULL,
    [MetaDescription]  NVARCHAR (4000) NOT NULL,
    [MetaTitle]        NVARCHAR (400)  NOT NULL,
    [SEName]           NVARCHAR (100)  NOT NULL,
    [ParentCategoryID] INT             NOT NULL,
    [PictureID]        INT             NOT NULL,
    [PageSize]         INT             NOT NULL,
    [PriceRanges]      NVARCHAR (400)  NOT NULL,
    [ShowOnHomePage]   BIT             NOT NULL,
    [Published]        BIT             NOT NULL,
    [Deleted]          BIT             NOT NULL,
    [DisplayOrder]     INT             NOT NULL,
    [CreatedOn]        DATETIME        NOT NULL,
    [UpdatedOn]        DATETIME        NOT NULL
);

