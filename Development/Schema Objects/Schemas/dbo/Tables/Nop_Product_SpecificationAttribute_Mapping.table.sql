CREATE TABLE [dbo].[Nop_Product_SpecificationAttribute_Mapping] (
    [ProductSpecificationAttributeID] INT IDENTITY (1, 1) NOT NULL,
    [ProductID]                       INT NOT NULL,
    [SpecificationAttributeOptionID]  INT NOT NULL,
    [AllowFiltering]                  BIT NOT NULL,
    [ShowOnProductPage]               BIT NOT NULL,
    [DisplayOrder]                    INT NOT NULL
);

