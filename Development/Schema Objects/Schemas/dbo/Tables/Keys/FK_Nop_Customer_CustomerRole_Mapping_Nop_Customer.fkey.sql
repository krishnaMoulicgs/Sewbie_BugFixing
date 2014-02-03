ALTER TABLE [dbo].[Nop_Customer_CustomerRole_Mapping]
    ADD CONSTRAINT [FK_Nop_Customer_CustomerRole_Mapping_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

