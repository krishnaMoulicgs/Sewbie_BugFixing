ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_AdditionalShippingCharge] DEFAULT ((0)) FOR [AdditionalShippingCharge];

