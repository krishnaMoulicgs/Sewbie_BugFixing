ALTER TABLE [dbo].[Nop_SpecificationAttributeLocalized]
    ADD CONSTRAINT [FK_Nop_SpecificationAttributeLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

