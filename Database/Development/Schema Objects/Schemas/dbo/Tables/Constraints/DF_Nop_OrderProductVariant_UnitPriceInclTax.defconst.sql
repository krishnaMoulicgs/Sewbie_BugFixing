ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_UnitPriceInclTax] DEFAULT ((0)) FOR [UnitPriceInclTax];

