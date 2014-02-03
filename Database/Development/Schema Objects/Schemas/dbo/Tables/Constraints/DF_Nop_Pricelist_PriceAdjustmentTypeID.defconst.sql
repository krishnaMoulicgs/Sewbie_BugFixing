ALTER TABLE [dbo].[Nop_Pricelist]
    ADD CONSTRAINT [DF_Nop_Pricelist_PriceAdjustmentTypeID] DEFAULT ((0)) FOR [PriceAdjustmentTypeID];

