ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_ProductCost] DEFAULT ((0)) FOR [ProductCost];

