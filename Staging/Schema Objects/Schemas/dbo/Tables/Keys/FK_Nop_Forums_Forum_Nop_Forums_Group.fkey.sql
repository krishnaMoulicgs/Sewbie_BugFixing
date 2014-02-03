ALTER TABLE [dbo].[Nop_Forums_Forum]
    ADD CONSTRAINT [FK_Nop_Forums_Forum_Nop_Forums_Group] FOREIGN KEY ([ForumGroupID]) REFERENCES [dbo].[Nop_Forums_Group] ([ForumGroupID]) ON DELETE CASCADE ON UPDATE CASCADE;

