﻿ALTER TABLE [dbo].[Nop_SpecificationAttributeOptionLocalized]
    ADD CONSTRAINT [FK_Nop_SpecificationAttributeOptionLocalized_Nop_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Nop_Language] ([LanguageId]) ON DELETE CASCADE ON UPDATE CASCADE;

