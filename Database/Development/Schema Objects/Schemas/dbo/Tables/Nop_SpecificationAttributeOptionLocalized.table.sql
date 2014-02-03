CREATE TABLE [dbo].[Nop_SpecificationAttributeOptionLocalized] (
    [SpecificationAttributeOptionLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [SpecificationAttributeOptionID]          INT            NOT NULL,
    [LanguageID]                              INT            NOT NULL,
    [Name]                                    NVARCHAR (500) NOT NULL
);

