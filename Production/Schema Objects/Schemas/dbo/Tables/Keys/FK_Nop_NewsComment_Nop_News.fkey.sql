ALTER TABLE [dbo].[Nop_NewsComment]
    ADD CONSTRAINT [FK_Nop_NewsComment_Nop_News] FOREIGN KEY ([NewsID]) REFERENCES [dbo].[Nop_News] ([NewsID]) ON DELETE CASCADE ON UPDATE CASCADE;

