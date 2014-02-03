ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_CustomerEntersPrice] DEFAULT ((0)) FOR [CustomerEntersPrice];

