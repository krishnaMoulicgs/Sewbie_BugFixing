ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [FK_Nop_ProductVariant_Nop_Product1] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

