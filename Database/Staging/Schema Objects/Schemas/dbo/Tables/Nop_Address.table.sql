CREATE TABLE [dbo].[Nop_Address] (
    [AddressId]        INT            IDENTITY (1, 1) NOT NULL,
    [CustomerID]       INT            NOT NULL,
    [IsBillingAddress] BIT            NOT NULL,
    [FirstName]        NVARCHAR (100) NOT NULL,
    [LastName]         NVARCHAR (100) NOT NULL,
    [PhoneNumber]      NVARCHAR (50)  NOT NULL,
    [Email]            NVARCHAR (255) NOT NULL,
    [FaxNumber]        NVARCHAR (50)  NOT NULL,
    [Company]          NVARCHAR (100) NOT NULL,
    [Address1]         NVARCHAR (100) NOT NULL,
    [Address2]         NVARCHAR (100) NOT NULL,
    [City]             NVARCHAR (100) NOT NULL,
    [StateProvinceID]  INT            NOT NULL,
    [ZipPostalCode]    NVARCHAR (30)  NOT NULL,
    [CountryID]        INT            NOT NULL,
    [CreatedOn]        DATETIME       NOT NULL,
    [UpdatedOn]        DATETIME       NOT NULL
);

