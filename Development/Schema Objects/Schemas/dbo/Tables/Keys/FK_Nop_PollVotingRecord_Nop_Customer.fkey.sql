ALTER TABLE [dbo].[Nop_PollVotingRecord]
    ADD CONSTRAINT [FK_Nop_PollVotingRecord_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

