ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_OrderMinimumQuantity] DEFAULT ((1)) FOR [OrderMinimumQuantity];

