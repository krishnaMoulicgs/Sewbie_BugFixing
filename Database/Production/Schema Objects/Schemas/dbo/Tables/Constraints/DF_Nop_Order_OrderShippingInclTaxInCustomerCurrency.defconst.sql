ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderShippingInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderShippingInclTaxInCustomerCurrency];

