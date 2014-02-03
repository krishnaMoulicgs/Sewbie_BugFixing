CREATE TABLE [dbo].[Nop_CreditCardType] (
    [CreditCardTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [SystemKeyword]    NVARCHAR (100) NOT NULL,
    [DisplayOrder]     INT            NOT NULL,
    [Deleted]          BIT            NOT NULL
);

