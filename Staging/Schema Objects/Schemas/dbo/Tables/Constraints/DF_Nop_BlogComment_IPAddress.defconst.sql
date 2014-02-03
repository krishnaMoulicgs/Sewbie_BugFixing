ALTER TABLE [dbo].[Nop_BlogComment]
    ADD CONSTRAINT [DF_Nop_BlogComment_IPAddress] DEFAULT ('') FOR [IPAddress];

