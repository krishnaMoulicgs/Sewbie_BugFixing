ALTER TABLE [dbo].[Nop_ShippingByWeight]
    ADD CONSTRAINT [FK_Nop_ShippingByWeight_Nop_ShippingMethod] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Nop_ShippingMethod] ([ShippingMethodID]) ON DELETE CASCADE ON UPDATE CASCADE;

