ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [DF_Nop_TopicLocalized_MetaKeywords] DEFAULT ('') FOR [MetaKeywords];

