ALTER TABLE [dbo].[Nop_OrderNote]
    ADD CONSTRAINT [FK_Nop_OrderNote_Nop_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Nop_Order] ([OrderID]) ON DELETE CASCADE ON UPDATE CASCADE;

