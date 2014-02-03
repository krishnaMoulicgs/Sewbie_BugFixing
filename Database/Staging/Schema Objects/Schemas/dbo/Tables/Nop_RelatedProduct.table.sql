CREATE TABLE [dbo].[Nop_RelatedProduct] (
    [RelatedProductID] INT IDENTITY (1, 1) NOT NULL,
    [ProductID1]       INT NOT NULL,
    [ProductID2]       INT NOT NULL,
    [DisplayOrder]     INT NOT NULL
);

