ALTER TABLE [dbo].[Nop_DiscountRestriction]
    ADD CONSTRAINT [FK_Nop_DiscountRestriction_Nop_Discount] FOREIGN KEY ([DiscountID]) REFERENCES [dbo].[Nop_Discount] ([DiscountID]) ON DELETE CASCADE ON UPDATE CASCADE;

