ALTER TABLE [dbo].[Nop_SpecificationAttributeOptionLocalized]
    ADD CONSTRAINT [FK_Nop_SpecificationAttributeOptionLocalized_Nop_SpecificationAttributeOption] FOREIGN KEY ([SpecificationAttributeOptionID]) REFERENCES [dbo].[Nop_SpecificationAttributeOption] ([SpecificationAttributeOptionID]) ON DELETE CASCADE ON UPDATE CASCADE;

