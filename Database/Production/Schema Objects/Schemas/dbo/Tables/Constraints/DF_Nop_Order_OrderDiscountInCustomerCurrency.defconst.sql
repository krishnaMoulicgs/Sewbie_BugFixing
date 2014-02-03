ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderDiscountInCustomerCurrency] DEFAULT ((0)) FOR [OrderDiscountInCustomerCurrency];

