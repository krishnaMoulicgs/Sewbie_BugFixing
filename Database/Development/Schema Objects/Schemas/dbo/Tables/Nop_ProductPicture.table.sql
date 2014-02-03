CREATE TABLE [dbo].[Nop_ProductPicture] (
    [ProductPictureID] INT IDENTITY (1, 1) NOT NULL,
    [ProductID]        INT NOT NULL,
    [PictureID]        INT NOT NULL,
    [DisplayOrder]     INT NOT NULL
);

