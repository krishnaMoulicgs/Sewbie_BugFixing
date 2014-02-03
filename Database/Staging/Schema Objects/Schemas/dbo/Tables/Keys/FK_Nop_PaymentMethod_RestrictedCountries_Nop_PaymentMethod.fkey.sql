ALTER TABLE [dbo].[Nop_PaymentMethod_RestrictedCountries]
    ADD CONSTRAINT [FK_Nop_PaymentMethod_RestrictedCountries_Nop_PaymentMethod] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[Nop_PaymentMethod] ([PaymentMethodID]) ON DELETE CASCADE ON UPDATE CASCADE;

