ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_CyclePeriod] DEFAULT ((0)) FOR [CyclePeriod];

