CREATE TABLE [dbo].[Nop_ProductVariantAttributeValueLocalized] (
    [ProductVariantAttributeValueLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [ProductVariantAttributeValueID]          INT            NOT NULL,
    [LanguageID]                              INT            NOT NULL,
    [Name]                                    NVARCHAR (100) NOT NULL
);

