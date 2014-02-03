ALTER TABLE [dbo].[Nop_SpecificationAttributeLocalized]
    ADD CONSTRAINT [FK_Nop_SpecificationAttributeLocalized_Nop_SpecificationAttribute] FOREIGN KEY ([SpecificationAttributeID]) REFERENCES [dbo].[Nop_SpecificationAttribute] ([SpecificationAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

