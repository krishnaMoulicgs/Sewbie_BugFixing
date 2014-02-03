ALTER TABLE [dbo].[Nop_Product_SpecificationAttribute_Mapping]
    ADD CONSTRAINT [FK_Nop_Product_SpecificationAttribute_Mapping_Nop_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

