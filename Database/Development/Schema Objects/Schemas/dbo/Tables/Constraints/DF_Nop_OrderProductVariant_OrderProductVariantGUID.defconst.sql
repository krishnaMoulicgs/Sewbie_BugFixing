ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_OrderProductVariantGUID] DEFAULT (newid()) FOR [OrderProductVariantGUID];

