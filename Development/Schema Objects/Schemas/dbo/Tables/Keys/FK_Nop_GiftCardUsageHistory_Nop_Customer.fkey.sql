ALTER TABLE [dbo].[Nop_GiftCardUsageHistory]
    ADD CONSTRAINT [FK_Nop_GiftCardUsageHistory_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

