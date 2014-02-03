ALTER TABLE [dbo].[Nop_RewardPointsHistory]
    ADD CONSTRAINT [FK_Nop_RewardPointsHistory_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

