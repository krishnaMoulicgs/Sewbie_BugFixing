ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_OrderShippingExclTax] DEFAULT ((0)) FOR [OrderShippingExclTax];

