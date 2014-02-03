ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_Backorders] DEFAULT ((0)) FOR [Backorders];

