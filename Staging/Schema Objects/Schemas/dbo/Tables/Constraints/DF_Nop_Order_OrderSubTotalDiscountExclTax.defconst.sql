ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubTotalDiscountExclTax] DEFAULT ((0)) FOR [OrderSubTotalDiscountExclTax];

