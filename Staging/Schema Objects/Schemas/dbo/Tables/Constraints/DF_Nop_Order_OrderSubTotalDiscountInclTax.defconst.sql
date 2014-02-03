ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubTotalDiscountInclTax] DEFAULT ((0)) FOR [OrderSubTotalDiscountInclTax];

