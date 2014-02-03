ALTER TABLE [dbo].[Nop_PaymentMethod_RestrictedCountries]
    ADD CONSTRAINT [FK_Nop_PaymentMethod_RestrictedCountries_Nop_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Nop_Country] ([CountryID]) ON DELETE CASCADE ON UPDATE CASCADE;

