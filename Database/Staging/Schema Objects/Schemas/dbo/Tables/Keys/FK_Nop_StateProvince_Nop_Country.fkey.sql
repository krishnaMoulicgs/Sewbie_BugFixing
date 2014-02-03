ALTER TABLE [dbo].[Nop_StateProvince]
    ADD CONSTRAINT [FK_Nop_StateProvince_Nop_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Nop_Country] ([CountryID]) ON DELETE CASCADE ON UPDATE CASCADE;

