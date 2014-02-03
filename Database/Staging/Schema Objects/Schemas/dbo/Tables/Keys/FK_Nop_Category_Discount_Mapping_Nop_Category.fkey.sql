ALTER TABLE [dbo].[Nop_Category_Discount_Mapping]
    ADD CONSTRAINT [FK_Nop_Category_Discount_Mapping_Nop_Category] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Nop_Category] ([CategoryID]) ON DELETE CASCADE ON UPDATE CASCADE;

