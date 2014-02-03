ALTER TABLE [dbo].[Nop_BlogPost]
    ADD CONSTRAINT [DF_Nop_BlogPost_Tags] DEFAULT ('') FOR [Tags];

