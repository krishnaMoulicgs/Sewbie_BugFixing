CREATE TABLE [dbo].[Nop_Product_Category_Mapping] (
    [ProductCategoryID] INT IDENTITY (1, 1) NOT NULL,
    [ProductID]         INT NOT NULL,
    [CategoryID]        INT NOT NULL,
    [IsFeaturedProduct] BIT NOT NULL,
    [DisplayOrder]      INT NOT NULL
);

