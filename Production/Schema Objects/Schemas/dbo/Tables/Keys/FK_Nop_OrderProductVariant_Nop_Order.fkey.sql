ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [FK_Nop_OrderProductVariant_Nop_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Nop_Order] ([OrderID]) ON DELETE CASCADE ON UPDATE CASCADE;

