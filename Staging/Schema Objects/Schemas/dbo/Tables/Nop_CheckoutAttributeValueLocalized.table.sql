CREATE TABLE [dbo].[Nop_CheckoutAttributeValueLocalized] (
    [CheckoutAttributeValueLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [CheckoutAttributeValueID]          INT            NOT NULL,
    [LanguageID]                        INT            NOT NULL,
    [Name]                              NVARCHAR (100) NOT NULL
);

