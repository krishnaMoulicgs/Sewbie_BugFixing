ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_TotalForumPosts] DEFAULT ((0)) FOR [TotalForumPosts];

