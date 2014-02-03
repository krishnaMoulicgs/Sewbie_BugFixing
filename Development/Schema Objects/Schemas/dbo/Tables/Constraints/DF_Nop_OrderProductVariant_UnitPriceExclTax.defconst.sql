ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_UnitPriceExclTax] DEFAULT ((0)) FOR [UnitPriceExclTax];

