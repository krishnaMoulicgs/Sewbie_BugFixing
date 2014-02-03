CREATE TABLE [dbo].[Nop_ShoppingCartItem] (
    [ShoppingCartItemID]   INT              IDENTITY (1, 1) NOT NULL,
    [ShoppingCartTypeID]   INT              NOT NULL,
    [CustomerSessionGUID]  UNIQUEIDENTIFIER NOT NULL,
    [ProductVariantID]     INT              NOT NULL,
    [AttributesXML]        XML              NOT NULL,
    [CustomerEnteredPrice] MONEY            NOT NULL,
    [Quantity]             INT              NOT NULL,
    [VendorID]             INT              NOT NULL,
    [CreatedOn]            DATETIME         NOT NULL,
    [UpdatedOn]            DATETIME         NOT NULL
);



