CREATE TABLE [dbo].[Nop_Language] (
    [LanguageId]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (100) NOT NULL,
    [LanguageCulture]   VARCHAR (20)   NOT NULL,
    [FlagImageFileName] NVARCHAR (50)  NOT NULL,
    [Published]         BIT            NOT NULL,
    [DisplayOrder]      INT            NOT NULL
);

