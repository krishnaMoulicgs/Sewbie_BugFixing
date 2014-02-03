ALTER TABLE [dbo].[Nop_Topic]
    ADD CONSTRAINT [DF_Nop_Topic_IncludeInSitemap] DEFAULT ((0)) FOR [IncludeInSitemap];

