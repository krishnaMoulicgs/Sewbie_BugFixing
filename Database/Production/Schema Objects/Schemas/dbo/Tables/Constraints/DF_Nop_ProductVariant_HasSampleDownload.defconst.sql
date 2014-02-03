ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_HasSampleDownload] DEFAULT ((0)) FOR [HasSampleDownload];

