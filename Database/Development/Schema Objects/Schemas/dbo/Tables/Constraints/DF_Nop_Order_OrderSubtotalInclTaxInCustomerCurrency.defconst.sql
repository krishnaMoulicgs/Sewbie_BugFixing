ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubtotalInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderSubtotalInclTaxInCustomerCurrency];

