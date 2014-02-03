ALTER TABLE [dbo].[Nop_GiftCardUsageHistory]
    ADD CONSTRAINT [FK_Nop_GiftCardUsageHistory_Nop_GiftCard] FOREIGN KEY ([GiftCardID]) REFERENCES [dbo].[Nop_GiftCard] ([GiftCardID]) ON DELETE CASCADE ON UPDATE CASCADE;

