ALTER TABLE [dbo].[Nop_Forums_PrivateMessage]
    ADD CONSTRAINT [DF_Nop_Forums_PrivateMessage_IsDeletedByAuthor] DEFAULT ((0)) FOR [IsDeletedByAuthor];

