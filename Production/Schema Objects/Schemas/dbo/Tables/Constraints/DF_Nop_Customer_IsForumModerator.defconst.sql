ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_IsForumModerator] DEFAULT ((0)) FOR [IsForumModerator];

