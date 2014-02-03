ALTER TABLE [dbo].[Nop_TopicLocalized]
    ADD CONSTRAINT [DF_Nop_TopicLocalized_UpdatedOn] DEFAULT (getutcdate()) FOR [UpdatedOn];

