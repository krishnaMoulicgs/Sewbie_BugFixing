ALTER TABLE [dbo].[Nop_Customer_CustomerRole_Mapping]
    ADD CONSTRAINT [FK_Nop_Customer_CustomerRole_Mapping_Nop_CustomerRole] FOREIGN KEY ([CustomerRoleID]) REFERENCES [dbo].[Nop_CustomerRole] ([CustomerRoleID]) ON DELETE CASCADE ON UPDATE CASCADE;

