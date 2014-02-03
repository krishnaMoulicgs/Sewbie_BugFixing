ALTER TABLE [dbo].[Nop_BlogComment]
    ADD CONSTRAINT [FK_Nop_BlogComment_Nop_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[Nop_BlogPost] ([BlogPostID]) ON DELETE CASCADE ON UPDATE CASCADE;

