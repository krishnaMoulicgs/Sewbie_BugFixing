ALTER TABLE [dbo].[Nop_SpecificationAttributeOption]
    ADD CONSTRAINT [FK_Nop_SpecificationAttributeOption_Nop_SpecificationAttribute] FOREIGN KEY ([SpecificationAttributeID]) REFERENCES [dbo].[Nop_SpecificationAttribute] ([SpecificationAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

