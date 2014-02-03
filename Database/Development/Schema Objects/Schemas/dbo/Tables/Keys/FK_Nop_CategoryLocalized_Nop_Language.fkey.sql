ALTER TABLE [dbo].[Nop_CategoryLocalized]
    ADD CONSTRAINT [FK_Nop_CategoryLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

