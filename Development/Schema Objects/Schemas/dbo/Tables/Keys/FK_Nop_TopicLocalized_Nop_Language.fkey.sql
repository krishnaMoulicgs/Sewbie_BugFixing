ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [FK_Nop_TopicLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

