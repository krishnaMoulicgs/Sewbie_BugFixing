ALTER TABLE [dbo].[Nop_ProductTag_Product_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductTag_Product_Mapping_Nop_ProductTag] FOREIGN KEY ([ProductTagID]) REFERENCES [dbo].[Nop_ProductTag] ([ProductTagID]) ON DELETE CASCADE ON UPDATE CASCADE;

