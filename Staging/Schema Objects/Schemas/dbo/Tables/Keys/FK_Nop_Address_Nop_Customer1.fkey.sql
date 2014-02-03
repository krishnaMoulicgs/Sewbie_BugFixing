ALTER TABLE [dbo].[Nop_Address]
    ADD CONSTRAINT [FK_Nop_Address_Nop_Customer1] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

