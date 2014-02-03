ALTER TABLE [dbo].[Nop_ProductLocalized]
    ADD CONSTRAINT [FK_Nop_ProductLocalized_Nop_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

