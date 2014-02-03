ALTER TABLE [dbo].[Nop_NewsComment]
    ADD CONSTRAINT [DF_Nop_NewsComment_IPAddress] DEFAULT ('') FOR [IPAddress];

