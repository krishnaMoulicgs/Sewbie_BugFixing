ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_DiscountAmountExclTax] DEFAULT ((0)) FOR [DiscountAmountExclTax];

