ALTER TABLE [dbo].[Nop_ProductVariant_ProductAttribute_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductVariant_ProductAttribute_Mapping_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

