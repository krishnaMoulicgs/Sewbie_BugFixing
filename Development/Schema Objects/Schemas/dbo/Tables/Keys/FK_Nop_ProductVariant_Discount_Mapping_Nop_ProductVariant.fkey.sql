ALTER TABLE [dbo].[Nop_ProductVariant_Discount_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductVariant_Discount_Mapping_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

