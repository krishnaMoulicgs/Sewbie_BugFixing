ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_DownloadCount] DEFAULT ((0)) FOR [DownloadCount];

