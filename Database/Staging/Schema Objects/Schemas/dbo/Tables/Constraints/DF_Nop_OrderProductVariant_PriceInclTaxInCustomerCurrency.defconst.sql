ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_PriceInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [PriceInclTaxInCustomerCurrency];

