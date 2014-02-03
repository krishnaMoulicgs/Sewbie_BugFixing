CREATE TABLE [dbo].[Nop_SpecificationAttributeLocalized] (
    [SpecificationAttributeLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [SpecificationAttributeID]          INT            NOT NULL,
    [LanguageID]                        INT            NOT NULL,
    [Name]                              NVARCHAR (100) NOT NULL
);

