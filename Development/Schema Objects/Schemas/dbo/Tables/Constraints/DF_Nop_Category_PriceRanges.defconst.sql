ALTER TABLE [dbo].[Nop_Category]
    ADD CONSTRAINT [DF_Nop_Category_PriceRanges] DEFAULT ('') FOR [PriceRanges];

