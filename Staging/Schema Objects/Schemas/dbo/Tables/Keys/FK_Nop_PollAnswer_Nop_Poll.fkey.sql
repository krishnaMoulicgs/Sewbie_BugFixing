ALTER TABLE [dbo].[Nop_PollAnswer]
    ADD CONSTRAINT [FK_Nop_PollAnswer_Nop_Poll] FOREIGN KEY ([PollID]) REFERENCES [dbo].[Nop_Poll] ([PollID]) ON DELETE CASCADE ON UPDATE CASCADE;

