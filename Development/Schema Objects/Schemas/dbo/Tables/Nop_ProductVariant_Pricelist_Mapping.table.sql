CREATE TABLE [dbo].[Nop_ProductVariant_Pricelist_Mapping] (
    [ProductVariantPricelistID] INT      IDENTITY (1, 1) NOT NULL,
    [ProductVariantID]          INT      NOT NULL,
    [PricelistID]               INT      NOT NULL,
    [PriceAdjustmentTypeID]     INT      NOT NULL,
    [PriceAdjustment]           MONEY    NOT NULL,
    [UpdatedOn]                 DATETIME NOT NULL
);

