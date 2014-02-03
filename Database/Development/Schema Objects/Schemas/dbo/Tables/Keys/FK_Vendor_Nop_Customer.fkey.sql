ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Nop_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

