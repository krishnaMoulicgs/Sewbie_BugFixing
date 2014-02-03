ALTER TABLE [dbo].[Nop_Download]
    ADD CONSTRAINT [DF_Nop_Nop_Download_UseDownloadURL] DEFAULT ((0)) FOR [UseDownloadURL];

