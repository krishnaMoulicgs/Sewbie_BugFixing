ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [DF_Nop_TopicLocalized_MetaTitle] DEFAULT ('') FOR [MetaTitle];

