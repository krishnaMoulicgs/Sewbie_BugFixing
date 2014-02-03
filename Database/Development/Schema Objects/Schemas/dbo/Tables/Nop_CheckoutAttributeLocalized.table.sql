CREATE TABLE [dbo].[Nop_CheckoutAttributeLocalized] (
    [CheckoutAttributeLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [CheckoutAttributeID]          INT            NOT NULL,
    [LanguageID]                   INT            NOT NULL,
    [Name]                         NVARCHAR (100) NOT NULL,
    [TextPrompt]                   NVARCHAR (300) NOT NULL
);

