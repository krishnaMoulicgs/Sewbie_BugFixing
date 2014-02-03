ALTER TABLE [dbo].[Nop_Product]
    ADD CONSTRAINT [FK_Nop_Product_Nop_ProductTemplate] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Nop_ProductTemplate] ([ProductTemplateId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

