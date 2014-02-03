ALTER TABLE [dbo].[Nop_Category]
    ADD CONSTRAINT [FK_Nop_Category_Nop_CategoryTemplate] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Nop_CategoryTemplate] ([CategoryTemplateId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

