CREATE TABLE [dbo].[Nop_CustomerRole_ProductPrice] (
    [CustomerRoleProductPriceID] INT   IDENTITY (1, 1) NOT NULL,
    [CustomerRoleID]             INT   NOT NULL,
    [ProductVariantID]           INT   NOT NULL,
    [Price]                      MONEY NOT NULL
);

