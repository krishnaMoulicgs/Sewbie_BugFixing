ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [FK_Nop_TopicLocalized_Nop_Topic] FOREIGN KEY ([TopicID]) REFERENCES [dbo].[Nop_Topic] ([TopicID]) ON DELETE CASCADE ON UPDATE CASCADE;

