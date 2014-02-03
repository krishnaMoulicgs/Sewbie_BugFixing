CREATE TABLE [dbo].[Nop_Affiliate] (
    [AffiliateID]   INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (100) NOT NULL,
    [LastName]      NVARCHAR (100) NOT NULL,
    [MiddleName]    NVARCHAR (100) NOT NULL,
    [PhoneNumber]   NVARCHAR (50)  NOT NULL,
    [Email]         NVARCHAR (255) NOT NULL,
    [FaxNumber]     NVARCHAR (50)  NOT NULL,
    [Company]       NVARCHAR (100) NOT NULL,
    [Address1]      NVARCHAR (100) NOT NULL,
    [Address2]      NVARCHAR (100) NOT NULL,
    [City]          NVARCHAR (100) NOT NULL,
    [StateProvince] NVARCHAR (100) NOT NULL,
    [ZipPostalCode] NVARCHAR (30)  NOT NULL,
    [CountryId]     INT            NOT NULL,
    [Active]        BIT            NOT NULL,
    [Deleted]       BIT            NOT NULL
);

