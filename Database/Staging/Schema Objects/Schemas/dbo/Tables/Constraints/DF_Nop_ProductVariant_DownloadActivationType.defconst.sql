ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_DownloadActivationType] DEFAULT ((1)) FOR [DownloadActivationType];

