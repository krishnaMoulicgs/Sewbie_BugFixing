CREATE TABLE [dbo].[Nop_BannedIpAddress] (
    [BannedIpAddressID] INT            IDENTITY (1, 1) NOT NULL,
    [Address]           NVARCHAR (50)  NOT NULL,
    [Comment]           NVARCHAR (500) NULL,
    [CreatedOn]         DATETIME       NOT NULL,
    [UpdatedOn]         DATETIME       NOT NULL
);

