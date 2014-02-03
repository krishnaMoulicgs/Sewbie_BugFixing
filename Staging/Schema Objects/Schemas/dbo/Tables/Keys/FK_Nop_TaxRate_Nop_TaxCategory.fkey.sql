ALTER TABLE [dbo].[Nop_TaxRate]
    ADD CONSTRAINT [FK_Nop_TaxRate_Nop_TaxCategory] FOREIGN KEY ([TaxCategoryID]) REFERENCES [dbo].[Nop_TaxCategory] ([TaxCategoryID]) ON DELETE CASCADE ON UPDATE CASCADE;

