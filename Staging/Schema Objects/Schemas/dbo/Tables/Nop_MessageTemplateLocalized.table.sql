CREATE TABLE [dbo].[Nop_MessageTemplateLocalized] (
    [MessageTemplateLocalizedID] INT            IDENTITY (1, 1) NOT NULL,
    [MessageTemplateID]          INT            NOT NULL,
    [LanguageID]                 INT            NOT NULL,
    [BCCEmailAddresses]          NVARCHAR (200) NOT NULL,
    [Subject]                    NVARCHAR (200) NOT NULL,
    [Body]                       NVARCHAR (MAX) NOT NULL,
    [IsActive]                   BIT            NOT NULL,
    [EmailAccountId]             INT            NOT NULL
);

