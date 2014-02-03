ALTER TABLE [dbo].[Nop_ProductVariantAttributeCombination]
    ADD CONSTRAINT [FK_Nop_ProductVariantAttributeCombination_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

