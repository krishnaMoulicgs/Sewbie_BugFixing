ALTER TABLE [dbo].[Nop_ShippingByWeightAndCountry]
    ADD CONSTRAINT [FK_Nop_ShippingByWeightAndCountry_Nop_ShippingMethod] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Nop_ShippingMethod] ([ShippingMethodID]) ON DELETE CASCADE ON UPDATE CASCADE;

