ALTER TABLE [dbo].[Nop_Poll]
    ADD CONSTRAINT [DF_Nop_Poll_ShowOnHomePage] DEFAULT ((0)) FOR [ShowOnHomePage];

