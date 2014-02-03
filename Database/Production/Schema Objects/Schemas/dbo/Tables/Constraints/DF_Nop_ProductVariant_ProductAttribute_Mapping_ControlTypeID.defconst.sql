ALTER TABLE [dbo].[Nop_ProductVariant_ProductAttribute_Mapping]
    ADD CONSTRAINT [DF_Nop_ProductVariant_ProductAttribute_Mapping_ControlTypeID] DEFAULT ((1)) FOR [AttributeControlTypeID];

