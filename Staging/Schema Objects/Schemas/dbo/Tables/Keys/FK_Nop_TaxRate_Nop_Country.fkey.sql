ALTER TABLE [dbo].[Nop_TaxRate]
    ADD CONSTRAINT [FK_Nop_TaxRate_Nop_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Nop_Country] ([CountryID]) ON DELETE CASCADE ON UPDATE CASCADE;

