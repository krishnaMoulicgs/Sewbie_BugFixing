ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_CallForPrice] DEFAULT ((0)) FOR [CallForPrice];

