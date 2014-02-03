ALTER TABLE [dbo].[Nop_CheckoutAttributeLocalized]
    ADD CONSTRAINT [FK_Nop_CheckoutAttributeLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

