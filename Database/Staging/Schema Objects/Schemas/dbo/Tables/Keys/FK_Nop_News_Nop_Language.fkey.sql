ALTER TABLE [dbo].[Nop_News]
    ADD CONSTRAINT [FK_Nop_News_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

