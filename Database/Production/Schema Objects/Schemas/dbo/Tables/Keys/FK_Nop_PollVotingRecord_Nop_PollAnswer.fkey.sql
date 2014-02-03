ALTER TABLE [dbo].[Nop_PollVotingRecord]
    ADD CONSTRAINT [FK_Nop_PollVotingRecord_Nop_PollAnswer] FOREIGN KEY ([PollAnswerID]) REFERENCES [dbo].[Nop_PollAnswer] ([PollAnswerID]) ON DELETE CASCADE ON UPDATE CASCADE;

