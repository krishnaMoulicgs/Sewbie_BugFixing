ALTER TABLE [dbo].[Nop_CustomerRole_Discount_Mapping]
    ADD CONSTRAINT [FK_Nop_CustomerRole_Discount_Mapping_Nop_Discount] FOREIGN KEY ([DiscountID]) REFERENCES [dbo].[Nop_Discount] ([DiscountID]) ON DELETE CASCADE ON UPDATE CASCADE;

