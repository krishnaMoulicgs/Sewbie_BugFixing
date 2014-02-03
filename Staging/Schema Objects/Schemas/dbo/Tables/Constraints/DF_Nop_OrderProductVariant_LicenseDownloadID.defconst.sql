ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_LicenseDownloadID] DEFAULT ((0)) FOR [LicenseDownloadID];

