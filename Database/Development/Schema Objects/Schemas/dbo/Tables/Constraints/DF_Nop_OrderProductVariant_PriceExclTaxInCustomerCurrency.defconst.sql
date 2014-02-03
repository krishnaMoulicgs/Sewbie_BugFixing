ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_PriceExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [PriceExclTaxInCustomerCurrency];

