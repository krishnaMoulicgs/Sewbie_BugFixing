CREATE TABLE [dbo].[Nop_ProductVariantAttributeCombination] (
    [ProductVariantAttributeCombinationID] INT IDENTITY (1, 1) NOT NULL,
    [ProductVariantID]                     INT NOT NULL,
    [AttributesXML]                        XML NOT NULL,
    [StockQuantity]                        INT NOT NULL,
    [AllowOutOfStockOrders]                BIT NOT NULL
);

