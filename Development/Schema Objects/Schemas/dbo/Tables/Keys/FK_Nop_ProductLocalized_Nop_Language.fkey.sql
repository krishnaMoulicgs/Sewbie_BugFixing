ALTER TABLE [dbo].[Nop_ProductLocalized]
    ADD CONSTRAINT [FK_Nop_ProductLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

