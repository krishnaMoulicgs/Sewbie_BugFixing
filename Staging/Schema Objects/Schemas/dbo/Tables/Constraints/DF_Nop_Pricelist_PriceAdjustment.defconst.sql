ALTER TABLE [dbo].[Nop_Pricelist]
    ADD CONSTRAINT [DF_Nop_Pricelist_PriceAdjustment] DEFAULT ((0)) FOR [PriceAdjustment];

