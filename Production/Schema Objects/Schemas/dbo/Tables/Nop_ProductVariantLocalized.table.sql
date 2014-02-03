CREATE TABLE [dbo].[Nop_ProductVariantLocalized] (
    [ProductVariantLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [ProductVariantID]          INT            NOT NULL,
    [LanguageID]                INT            NOT NULL,
    [Name]                      NVARCHAR (400) NOT NULL,
    [Description]               NVARCHAR (MAX) NOT NULL
);

