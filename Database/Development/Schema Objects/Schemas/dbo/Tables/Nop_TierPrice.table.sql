CREATE TABLE [dbo].[Nop_TierPrice] (
    [TierPriceID]      INT   IDENTITY (1, 1) NOT NULL,
    [ProductVariantID] INT   NOT NULL,
    [Quantity]         INT   NOT NULL,
    [Price]            MONEY NOT NULL
);

