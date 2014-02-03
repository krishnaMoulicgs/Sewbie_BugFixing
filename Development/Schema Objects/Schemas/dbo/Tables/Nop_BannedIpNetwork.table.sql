CREATE TABLE [dbo].[Nop_BannedIpNetwork] (
    [BannedIpNetworkID] INT            IDENTITY (1, 1) NOT NULL,
    [StartAddress]      NVARCHAR (50)  NOT NULL,
    [EndAddress]        NVARCHAR (50)  NOT NULL,
    [Comment]           NVARCHAR (500) NULL,
    [IpException]       NVARCHAR (MAX) NULL,
    [CreatedOn]         DATETIME       NOT NULL,
    [UpdatedOn]         DATETIME       NOT NULL
);

