ALTER TABLE [dbo].[Nop_QueuedEmail]
    ADD CONSTRAINT [DF_Nop_QueuedEmail_EmailAccountId] DEFAULT ((0)) FOR [EmailAccountId];

