ALTER TABLE [dbo].[Nop_DiscountUsageHistory]
    ADD CONSTRAINT [FK_Nop_DiscountUsageHistory_Nop_Discount] FOREIGN KEY ([DiscountID]) REFERENCES [dbo].[Nop_Discount] ([DiscountID]) ON DELETE CASCADE ON UPDATE CASCADE;

