ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubTotalDiscountExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderSubTotalDiscountExclTaxInCustomerCurrency];

