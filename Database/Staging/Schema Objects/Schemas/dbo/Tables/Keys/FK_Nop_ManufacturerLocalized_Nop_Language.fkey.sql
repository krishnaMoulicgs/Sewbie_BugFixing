ALTER TABLE [dbo].[Nop_ManufacturerLocalized]
    ADD CONSTRAINT [FK_Nop_ManufacturerLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

