ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_TotalCycles] DEFAULT ((1)) FOR [TotalCycles];

