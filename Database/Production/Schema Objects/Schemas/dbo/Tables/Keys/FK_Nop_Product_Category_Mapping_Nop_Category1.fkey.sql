ALTER TABLE [dbo].[Nop_Product_Category_Mapping]
    ADD CONSTRAINT [FK_Nop_Product_Category_Mapping_Nop_Category1] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Nop_Category] ([CategoryID]) ON DELETE CASCADE ON UPDATE CASCADE;

