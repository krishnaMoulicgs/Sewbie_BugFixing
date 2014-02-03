ALTER TABLE [dbo].[Nop_DiscountUsageHistory]
    ADD CONSTRAINT [FK_Nop_DiscountUsageHistory_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

