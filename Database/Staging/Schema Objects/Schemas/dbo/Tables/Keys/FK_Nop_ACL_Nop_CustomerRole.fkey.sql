ALTER TABLE [dbo].[Nop_ACL]
    ADD CONSTRAINT [FK_Nop_ACL_Nop_CustomerRole] FOREIGN KEY ([CustomerRoleID]) REFERENCES [dbo].[Nop_CustomerRole] ([CustomerRoleID]) ON DELETE CASCADE ON UPDATE CASCADE;

