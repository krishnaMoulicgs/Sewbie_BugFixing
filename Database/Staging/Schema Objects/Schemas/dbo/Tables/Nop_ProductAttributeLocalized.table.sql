CREATE TABLE [dbo].[Nop_ProductAttributeLocalized] (
    [ProductAttributeLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [ProductAttributeID]          INT            NOT NULL,
    [LanguageID]                  INT            NOT NULL,
    [Name]                        NVARCHAR (100) NOT NULL,
    [Description]                 NVARCHAR (400) NOT NULL
);

