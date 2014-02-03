ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_DiscountAmountInclTax] DEFAULT ((0)) FOR [DiscountAmountInclTax];

