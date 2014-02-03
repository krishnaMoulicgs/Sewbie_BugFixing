ALTER TABLE [dbo].[Nop_ShippingMethod_RestrictedCountries]
    ADD CONSTRAINT [FK_Nop_ShippingMethod_RestrictedCountries_Nop_ShippingMethod] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Nop_ShippingMethod] ([ShippingMethodID]) ON DELETE CASCADE ON UPDATE CASCADE;

