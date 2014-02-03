ALTER TABLE [dbo].[Nop_CustomerRole_ProductPrice]
    ADD CONSTRAINT [FK_Nop_CustomerRole_ProductPrice_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

