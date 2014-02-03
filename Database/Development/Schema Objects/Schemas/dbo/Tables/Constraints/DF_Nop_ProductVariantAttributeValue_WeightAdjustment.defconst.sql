ALTER TABLE [dbo].[Nop_ProductVariantAttributeValue]
    ADD CONSTRAINT [DF_Nop_ProductVariantAttributeValue_WeightAdjustment] DEFAULT ((0)) FOR [WeightAdjustment];

