ALTER TABLE [dbo].[Nop_Download]
    ADD CONSTRAINT [DF_Nop_Download_Filename] DEFAULT ('') FOR [Filename];

