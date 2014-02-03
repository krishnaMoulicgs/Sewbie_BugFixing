ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [DF_Nop_TopicLocalized_MetaDescription] DEFAULT ('') FOR [MetaDescription];

