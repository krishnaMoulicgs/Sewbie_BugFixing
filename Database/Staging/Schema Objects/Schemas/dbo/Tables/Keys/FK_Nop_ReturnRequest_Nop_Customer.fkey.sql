ALTER TABLE [dbo].[Nop_ReturnRequest]
    ADD CONSTRAINT [FK_Nop_ReturnRequest_Nop_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

