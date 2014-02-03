ALTER TABLE [dbo].[Nop_Poll]
    ADD CONSTRAINT [DF_Nop_Poll_SystemKeyword] DEFAULT ('') FOR [SystemKeyword];

