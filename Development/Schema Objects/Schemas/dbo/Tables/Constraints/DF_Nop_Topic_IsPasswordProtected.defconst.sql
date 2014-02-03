ALTER TABLE [dbo].[Nop_Topic]
    ADD CONSTRAINT [DF_Nop_Topic_IsPasswordProtected] DEFAULT ((0)) FOR [IsPasswordProtected];

