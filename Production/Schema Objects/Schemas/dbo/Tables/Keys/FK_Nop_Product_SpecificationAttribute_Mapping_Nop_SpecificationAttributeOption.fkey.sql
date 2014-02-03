ALTER TABLE [dbo].[Nop_Product_SpecificationAttribute_Mapping]
    ADD CONSTRAINT [FK_Nop_Product_SpecificationAttribute_Mapping_Nop_SpecificationAttributeOption] FOREIGN KEY ([SpecificationAttributeOptionID]) REFERENCES [dbo].[Nop_SpecificationAttributeOption] ([SpecificationAttributeOptionID]) ON DELETE CASCADE ON UPDATE CASCADE;

