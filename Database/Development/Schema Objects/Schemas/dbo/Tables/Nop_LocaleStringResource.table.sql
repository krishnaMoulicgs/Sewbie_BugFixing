CREATE TABLE [dbo].[Nop_LocaleStringResource] (
    [LocaleStringResourceID] INT            IDENTITY (1, 1) NOT NULL,
    [LanguageID]             INT            NOT NULL,
    [ResourceName]           NVARCHAR (200) NOT NULL,
    [ResourceValue]          NVARCHAR (MAX) NOT NULL
);

