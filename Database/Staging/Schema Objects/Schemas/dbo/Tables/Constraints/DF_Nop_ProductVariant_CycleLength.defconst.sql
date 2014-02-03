ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_CycleLength] DEFAULT ((1)) FOR [CycleLength];

