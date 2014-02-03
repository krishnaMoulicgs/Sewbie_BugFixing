ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderShippingExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderShippingExclTaxInCustomerCurrency];

