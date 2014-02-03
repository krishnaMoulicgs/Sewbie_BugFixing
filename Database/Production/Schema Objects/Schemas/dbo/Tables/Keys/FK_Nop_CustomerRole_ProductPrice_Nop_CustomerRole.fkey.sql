ALTER TABLE [dbo].[Nop_CustomerRole_ProductPrice]
    ADD CONSTRAINT [FK_Nop_CustomerRole_ProductPrice_Nop_CustomerRole] FOREIGN KEY ([CustomerRoleID]) REFERENCES [dbo].[Nop_CustomerRole] ([CustomerRoleID]) ON DELETE CASCADE ON UPDATE CASCADE;

