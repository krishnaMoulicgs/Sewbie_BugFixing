CREATE TABLE [dbo].[Nop_TaxRate] (
    [TaxRateID]       INT             IDENTITY (1, 1) NOT NULL,
    [TaxCategoryID]   INT             NOT NULL,
    [CountryID]       INT             NOT NULL,
    [StateProvinceID] INT             NOT NULL,
    [Zip]             NVARCHAR (50)   NOT NULL,
    [Percentage]      DECIMAL (18, 4) NOT NULL
);

