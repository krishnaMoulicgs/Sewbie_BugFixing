ALTER TABLE [dbo].[Nop_MessageTemplateLocalized]
    ADD CONSTRAINT [FK_Nop_MessageTemplateLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

