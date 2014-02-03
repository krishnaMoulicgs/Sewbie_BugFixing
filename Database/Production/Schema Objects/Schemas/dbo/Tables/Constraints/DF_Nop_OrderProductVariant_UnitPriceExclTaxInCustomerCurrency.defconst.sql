ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_UnitPriceExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [UnitPriceExclTaxInCustomerCurrency];

