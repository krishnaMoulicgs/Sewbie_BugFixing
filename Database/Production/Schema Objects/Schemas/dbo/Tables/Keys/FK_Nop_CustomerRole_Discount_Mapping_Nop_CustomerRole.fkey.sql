ALTER TABLE [dbo].[Nop_CustomerRole_Discount_Mapping]
    ADD CONSTRAINT [FK_Nop_CustomerRole_Discount_Mapping_Nop_CustomerRole] FOREIGN KEY ([CustomerRoleID]) REFERENCES [dbo].[Nop_CustomerRole] ([CustomerRoleID]) ON DELETE CASCADE ON UPDATE CASCADE;

