CREATE TABLE [dbo].[Nop_SMSProvider] (
    [SMSProviderId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [ClassName]     NVARCHAR (500) NOT NULL,
    [SystemKeyword] NVARCHAR (500) NOT NULL,
    [IsActive]      BIT            NOT NULL
);

