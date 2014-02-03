ALTER TABLE [dbo].[Nop_CheckoutAttributeValueLocalized]
    ADD CONSTRAINT [FK_Nop_CheckoutAttributeValueLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

