ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_UnlimitedDownloads] DEFAULT ((1)) FOR [UnlimitedDownloads];

