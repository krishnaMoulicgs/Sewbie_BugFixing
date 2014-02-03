ALTER TABLE [dbo].[Nop_ProductVariantLocalized]
    ADD CONSTRAINT [FK_Nop_ProductVariantLocalized_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

