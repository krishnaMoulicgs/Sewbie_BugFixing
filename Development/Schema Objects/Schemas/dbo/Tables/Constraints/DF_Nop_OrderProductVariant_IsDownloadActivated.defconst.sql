ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [DF_Nop_OrderProductVariant_IsDownloadActivated] DEFAULT ((0)) FOR [IsDownloadActivated];

