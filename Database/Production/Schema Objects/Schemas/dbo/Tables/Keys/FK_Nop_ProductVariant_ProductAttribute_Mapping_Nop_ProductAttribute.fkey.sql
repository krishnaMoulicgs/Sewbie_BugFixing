ALTER TABLE [dbo].[Nop_ProductVariant_ProductAttribute_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductVariant_ProductAttribute_Mapping_Nop_ProductAttribute] FOREIGN KEY ([ProductAttributeID]) REFERENCES [dbo].[Nop_ProductAttribute] ([ProductAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

