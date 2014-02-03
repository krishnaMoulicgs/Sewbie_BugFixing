ALTER TABLE [dbo].[Nop_ShippingByTotal]
    ADD CONSTRAINT [FK_Nop_ShippingByTotal_Nop_ShippingMethod] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Nop_ShippingMethod] ([ShippingMethodID]) ON DELETE CASCADE ON UPDATE CASCADE;

