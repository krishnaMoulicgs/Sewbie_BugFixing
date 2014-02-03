ALTER TABLE [dbo].[Nop_ShippingByWeightAndCountry]
    ADD CONSTRAINT [FK_Nop_ShippingByWeightAndCountry_Nop_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Nop_Country] ([CountryID]) ON DELETE CASCADE ON UPDATE CASCADE;

