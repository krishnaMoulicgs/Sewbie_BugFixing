ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_OrderMaximumQuantity] DEFAULT ((10000)) FOR [OrderMaximumQuantity];

