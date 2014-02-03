ALTER TABLE [dbo].[Nop_ProductPicture]
    ADD CONSTRAINT [FK_Nop_ProductPicture_Nop_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

