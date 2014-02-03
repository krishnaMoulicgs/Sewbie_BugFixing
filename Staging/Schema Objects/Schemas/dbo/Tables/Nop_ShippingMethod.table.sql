CREATE TABLE [dbo].[Nop_ShippingMethod] (
    [ShippingMethodID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100)  NOT NULL,
    [Description]      NVARCHAR (2000) NOT NULL,
    [DisplayOrder]     INT             NOT NULL
);

