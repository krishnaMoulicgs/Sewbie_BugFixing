ALTER TABLE [dbo].[Nop_Manufacturer]
    ADD CONSTRAINT [DF_Nop_Manufacturer_PriceRanges] DEFAULT ('') FOR [PriceRanges];

