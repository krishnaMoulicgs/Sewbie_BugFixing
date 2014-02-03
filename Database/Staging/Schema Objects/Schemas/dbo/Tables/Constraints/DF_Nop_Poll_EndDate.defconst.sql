ALTER TABLE [dbo].[Nop_Poll]
    ADD CONSTRAINT [DF_Nop_Poll_EndDate] DEFAULT (NULL) FOR [EndDate];

