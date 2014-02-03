ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_Email] DEFAULT ('') FOR [Email];

