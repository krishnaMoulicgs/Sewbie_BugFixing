ALTER TABLE [dbo].[Nop_ShoppingCartItem]
    ADD CONSTRAINT [FK_Nop_ShoppingCart_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE CASCADE ON UPDATE CASCADE;

