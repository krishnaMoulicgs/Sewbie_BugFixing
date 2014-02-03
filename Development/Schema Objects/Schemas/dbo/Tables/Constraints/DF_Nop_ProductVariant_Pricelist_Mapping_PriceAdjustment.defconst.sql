ALTER TABLE [dbo].[Nop_ProductVariant_Pricelist_Mapping]
    ADD CONSTRAINT [DF_Nop_ProductVariant_Pricelist_Mapping_PriceAdjustment] DEFAULT ((0)) FOR [PriceAdjustment];

