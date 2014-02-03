ALTER TABLE [dbo].[Nop_ProductTag_Product_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductTag_Product_Mapping_Nop_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

