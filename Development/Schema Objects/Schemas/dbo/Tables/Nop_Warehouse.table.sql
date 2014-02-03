CREATE TABLE [dbo].[Nop_Warehouse] (
    [WarehouseID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (255) NOT NULL,
    [PhoneNumber]   NVARCHAR (50)  NOT NULL,
    [Email]         NVARCHAR (255) NOT NULL,
    [FaxNumber]     NVARCHAR (50)  NOT NULL,
    [Address1]      NVARCHAR (100) NOT NULL,
    [Address2]      NVARCHAR (100) NOT NULL,
    [City]          NVARCHAR (100) NOT NULL,
    [StateProvince] NVARCHAR (100) NOT NULL,
    [ZipPostalCode] NVARCHAR (30)  NOT NULL,
    [CountryId]     INT            NOT NULL,
    [Deleted]       BIT            NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL,
    [UpdatedOn]     DATETIME       NOT NULL
);

