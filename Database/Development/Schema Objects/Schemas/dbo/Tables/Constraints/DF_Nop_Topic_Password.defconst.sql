ALTER TABLE [dbo].[Nop_Topic]
    ADD CONSTRAINT [DF_Nop_Topic_Password] DEFAULT ('') FOR [Password];

