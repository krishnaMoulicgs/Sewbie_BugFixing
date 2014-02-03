ALTER TABLE [dbo].[Nop_LocaleStringResource]
    ADD CONSTRAINT [FK_Nop_LocaleStringResource_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

