ALTER TABLE [dbo].[Nop_ProductVariantAttributeValueLocalized]
    ADD CONSTRAINT [FK_Nop_ProductVariantAttributeValueLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

