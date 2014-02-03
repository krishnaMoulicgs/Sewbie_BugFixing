ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_MaxNumberOfDownloads] DEFAULT ((0)) FOR [MaxNumberOfDownloads];

