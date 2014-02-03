CREATE TABLE [dbo].[Nop_CustomerRole] (
    [CustomerRoleID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (255) NOT NULL,
    [FreeShipping]   BIT            NOT NULL,
    [TaxExempt]      BIT            NOT NULL,
    [Active]         BIT            NOT NULL,
    [Deleted]        BIT            NOT NULL
);

