ALTER TABLE [dbo].[Nop_ProductVariantAttributeValueLocalized]
    ADD CONSTRAINT [FK_Nop_ProductVariantAttributeValueLocalized_Nop_ProductVariantAttributeValue] FOREIGN KEY ([ProductVariantAttributeValueID]) REFERENCES [dbo].[Nop_ProductVariantAttributeValue] ([ProductVariantAttributeValueID]) ON DELETE CASCADE ON UPDATE CASCADE;

