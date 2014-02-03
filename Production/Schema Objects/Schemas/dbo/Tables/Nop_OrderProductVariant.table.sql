CREATE TABLE [dbo].[Nop_OrderProductVariant] (
    [OrderProductVariantID]              INT              IDENTITY (1, 1) NOT NULL,
    [OrderID]                            INT              NOT NULL,
    [ProductVariantID]                   INT              NOT NULL,
    [UnitPriceInclTax]                   MONEY            NOT NULL,
    [UnitPriceExclTax]                   MONEY            NOT NULL,
    [PriceInclTax]                       MONEY            NOT NULL,
    [PriceExclTax]                       MONEY            NOT NULL,
    [UnitPriceInclTaxInCustomerCurrency] MONEY            NOT NULL,
    [UnitPriceExclTaxInCustomerCurrency] MONEY            NOT NULL,
    [PriceInclTaxInCustomerCurrency]     MONEY            NOT NULL,
    [PriceExclTaxInCustomerCurrency]     MONEY            NOT NULL,
    [AttributeDescription]               NVARCHAR (4000)  NOT NULL,
    [AttributesXML]                      XML              NOT NULL,
    [Quantity]                           INT              NOT NULL,
    [DiscountAmountInclTax]              DECIMAL (18, 4)  NOT NULL,
    [DiscountAmountExclTax]              DECIMAL (18, 4)  NOT NULL,
    [DownloadCount]                      INT              NOT NULL,
    [OrderProductVariantGUID]            UNIQUEIDENTIFIER NOT NULL,
    [IsDownloadActivated]                BIT              NOT NULL,
    [LicenseDownloadID]                  INT              NOT NULL
);

