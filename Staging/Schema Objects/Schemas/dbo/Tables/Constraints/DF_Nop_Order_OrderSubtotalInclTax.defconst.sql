ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubtotalInclTax] DEFAULT ((0)) FOR [OrderSubtotalInclTax];

