CREATE TABLE [dbo].[Nop_Country] (
    [CountryID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [AllowsRegistration] BIT            NOT NULL,
    [AllowsBilling]      BIT            NOT NULL,
    [AllowsShipping]     BIT            NOT NULL,
    [TwoLetterISOCode]   NVARCHAR (2)   NOT NULL,
    [ThreeLetterISOCode] NVARCHAR (3)   NOT NULL,
    [NumericISOCode]     INT            NOT NULL,
    [SubjectToVAT]       BIT            NOT NULL,
    [Published]          BIT            NOT NULL,
    [DisplayOrder]       INT            NOT NULL
);

