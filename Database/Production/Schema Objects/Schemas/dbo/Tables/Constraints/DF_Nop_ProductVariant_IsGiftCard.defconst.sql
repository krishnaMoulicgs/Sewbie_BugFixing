ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_IsGiftCard] DEFAULT ((0)) FOR [IsGiftCard];

