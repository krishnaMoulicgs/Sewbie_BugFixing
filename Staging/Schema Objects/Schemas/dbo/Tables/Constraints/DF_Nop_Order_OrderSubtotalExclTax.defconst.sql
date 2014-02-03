ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderSubtotalExclTax] DEFAULT ((0)) FOR [OrderSubtotalExclTax];

