ALTER TABLE [dbo].[Nop_Forums_PrivateMessage]
    ADD CONSTRAINT [DF_Nop_Forums_PrivateMessage_IsDeletedByRecipient] DEFAULT ((0)) FOR [IsDeletedByRecipient];

