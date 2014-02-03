CREATE TABLE [dbo].[Nop_StateProvince] (
    [StateProvinceID] INT            IDENTITY (1, 1) NOT NULL,
    [CountryID]       INT            NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [Abbreviation]    NVARCHAR (30)  NOT NULL,
    [DisplayOrder]    INT            NOT NULL
);

