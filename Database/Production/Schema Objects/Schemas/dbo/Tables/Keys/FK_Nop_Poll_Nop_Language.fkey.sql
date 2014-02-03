ALTER TABLE [dbo].[Nop_Poll]
    ADD CONSTRAINT [FK_Nop_Poll_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

