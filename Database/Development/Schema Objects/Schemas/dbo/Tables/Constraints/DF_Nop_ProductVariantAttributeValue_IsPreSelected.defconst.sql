ALTER TABLE [dbo].[Nop_ProductVariantAttributeValue]
    ADD CONSTRAINT [DF_Nop_ProductVariantAttributeValue_IsPreSelected] DEFAULT ((0)) FOR [IsPreSelected];

