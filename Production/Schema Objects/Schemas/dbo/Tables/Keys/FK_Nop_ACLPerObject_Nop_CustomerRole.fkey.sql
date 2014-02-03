ALTER TABLE [dbo].[Nop_ACLPerObject]
    ADD CONSTRAINT [FK_Nop_ACLPerObject_Nop_CustomerRole] FOREIGN KEY ([CustomerRoleId]) REFERENCES [dbo].[Nop_CustomerRole] ([CustomerRoleID]) ON DELETE CASCADE ON UPDATE CASCADE;

