CREATE TABLE [dbo].[Nop_CustomerAttribute] (
    [CustomerAttributeId] INT             IDENTITY (1, 1) NOT NULL,
    [CustomerId]          INT             NOT NULL,
    [Key]                 NVARCHAR (100)  NOT NULL,
    [Value]               NVARCHAR (1000) NOT NULL
);

