ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubtotalExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderSubtotalExclTaxInCustomerCurrency];

