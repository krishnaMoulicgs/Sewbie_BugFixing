ALTER TABLE [dbo].[Nop_Forums_Topic]
    ADD CONSTRAINT [FK_Nop_Forums_Topic_Nop_Forums_Forum] FOREIGN KEY ([ForumID]) REFERENCES [dbo].[Nop_Forums_Forum] ([ForumID]) ON DELETE CASCADE ON UPDATE CASCADE;

