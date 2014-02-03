ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_DisplayStockAvailability] DEFAULT ((1)) FOR [DisplayStockAvailability];

