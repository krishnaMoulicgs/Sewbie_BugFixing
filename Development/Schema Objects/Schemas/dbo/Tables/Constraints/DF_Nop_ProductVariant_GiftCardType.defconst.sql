ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_GiftCardType] DEFAULT ((0)) FOR [GiftCardType];

