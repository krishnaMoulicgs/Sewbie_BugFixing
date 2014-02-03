ALTER TABLE [dbo].[Nop_DiscountRestriction]
    ADD CONSTRAINT [FK_Nop_DiscountRestriction_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

