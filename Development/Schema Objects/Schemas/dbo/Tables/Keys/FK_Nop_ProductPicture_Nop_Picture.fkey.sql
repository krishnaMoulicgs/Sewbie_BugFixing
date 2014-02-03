ALTER TABLE [dbo].[Nop_ProductPicture]
    ADD CONSTRAINT [FK_Nop_ProductPicture_Nop_Picture] FOREIGN KEY ([PictureID]) REFERENCES [dbo].[Nop_Picture] ([PictureID]) ON DELETE CASCADE ON UPDATE CASCADE;

