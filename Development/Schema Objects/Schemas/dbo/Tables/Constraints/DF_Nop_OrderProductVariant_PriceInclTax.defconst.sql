ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_PriceInclTax] DEFAULT ((0)) FOR [PriceInclTax];

