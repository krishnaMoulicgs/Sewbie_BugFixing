ALTER TABLE [dbo].[Nop_CustomerAttribute]
    ADD CONSTRAINT [FK_Nop_CustomerAttribute_Nop_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

