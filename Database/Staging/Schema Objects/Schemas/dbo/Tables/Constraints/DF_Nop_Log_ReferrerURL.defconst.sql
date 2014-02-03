ALTER TABLE [dbo].[Nop_Log]
    ADD CONSTRAINT [DF_Nop_Log_ReferrerURL] DEFAULT ('') FOR [ReferrerURL];

