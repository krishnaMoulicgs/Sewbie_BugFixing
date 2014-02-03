ALTER TABLE [dbo].[Nop_ProductAttributeLocalized]
    ADD CONSTRAINT [FK_Nop_ProductAttributeLocalized_Nop_ProductAttribute] FOREIGN KEY ([ProductAttributeID]) REFERENCES [dbo].[Nop_ProductAttribute] ([ProductAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

