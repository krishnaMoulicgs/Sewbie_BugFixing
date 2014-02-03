ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [DF_Nop_TopicLocalized_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn];

