ALTER TABLE [dbo].[Nop_RelatedProduct]
    ADD CONSTRAINT [FK_Nop_RelatedProduct_Nop_Product] FOREIGN KEY ([ProductID1]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

