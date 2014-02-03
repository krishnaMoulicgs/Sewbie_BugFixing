ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_IsRecurring] DEFAULT ((0)) FOR [IsRecurring];

