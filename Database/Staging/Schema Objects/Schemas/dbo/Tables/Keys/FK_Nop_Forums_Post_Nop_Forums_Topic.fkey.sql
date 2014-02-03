ALTER TABLE [dbo].[Nop_Forums_Post]
    ADD CONSTRAINT [FK_Nop_Forums_Post_Nop_Forums_Topic] FOREIGN KEY ([TopicID]) REFERENCES [dbo].[Nop_Forums_Topic] ([TopicID]) ON DELETE CASCADE ON UPDATE CASCADE;

