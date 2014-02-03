CREATE TABLE [dbo].[Nop_Product_Manufacturer_Mapping] (
    [ProductManufacturerID] INT IDENTITY (1, 1) NOT NULL,
    [ProductID]             INT NOT NULL,
    [ManufacturerID]        INT NOT NULL,
    [IsFeaturedProduct]     BIT NOT NULL,
    [DisplayOrder]          INT NOT NULL
);

