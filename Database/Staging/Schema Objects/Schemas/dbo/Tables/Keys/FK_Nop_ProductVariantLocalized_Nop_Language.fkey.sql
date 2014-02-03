ALTER TABLE [dbo].[Nop_ProductVariantLocalized]
    ADD CONSTRAINT [FK_Nop_ProductVariantLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

