ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubTotalDiscountInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [OrderSubTotalDiscountInclTaxInCustomerCurrency];

