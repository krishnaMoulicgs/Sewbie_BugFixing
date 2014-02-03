ALTER TABLE [dbo].[Nop_TierPrice]
    ADD CONSTRAINT [FK_Nop_TierPrice_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

