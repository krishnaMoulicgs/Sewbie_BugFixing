ALTER TABLE [dbo].[Nop_Product_Category_Mapping]
    ADD CONSTRAINT [FK_Nop_Product_Category_Mapping_Nop_Product1] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

