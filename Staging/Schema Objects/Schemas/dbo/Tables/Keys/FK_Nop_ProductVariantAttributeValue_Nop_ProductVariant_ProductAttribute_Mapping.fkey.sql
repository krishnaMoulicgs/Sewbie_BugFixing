ALTER TABLE [dbo].[Nop_ProductVariantAttributeValue]
    ADD CONSTRAINT [FK_Nop_ProductVariantAttributeValue_Nop_ProductVariant_ProductAttribute_Mapping] FOREIGN KEY ([ProductVariantAttributeID]) REFERENCES [dbo].[Nop_ProductVariant_ProductAttribute_Mapping] ([ProductVariantAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

