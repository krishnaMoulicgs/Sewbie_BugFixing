ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_UnitPriceInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [UnitPriceInclTaxInCustomerCurrency];

