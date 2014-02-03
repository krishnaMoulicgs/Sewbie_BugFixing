CREATE TABLE [dbo].[Nop_Currency] (
    [CurrencyID]       INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)   NOT NULL,
    [CurrencyCode]     NVARCHAR (5)    NOT NULL,
    [DisplayLocale]    NVARCHAR (50)   NULL,
    [Rate]             DECIMAL (18, 4) NOT NULL,
    [CustomFormatting] NVARCHAR (50)   NOT NULL,
    [Published]        BIT             NOT NULL,
    [DisplayOrder]     INT             NOT NULL,
    [CreatedOn]        DATETIME        NOT NULL,
    [UpdatedOn]        DATETIME        NOT NULL
);

