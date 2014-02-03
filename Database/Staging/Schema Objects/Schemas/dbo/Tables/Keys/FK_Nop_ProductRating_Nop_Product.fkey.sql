ALTER TABLE [dbo].[Nop_ProductRating]
    ADD CONSTRAINT [FK_Nop_ProductRating_Nop_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

