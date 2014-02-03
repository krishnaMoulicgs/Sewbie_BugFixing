ALTER TABLE [dbo].[Nop_DiscountUsageHistory]
    ADD CONSTRAINT [FK_Nop_DiscountUsageHistory_Nop_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Nop_Order] ([OrderID]) ON DELETE CASCADE ON UPDATE CASCADE;

