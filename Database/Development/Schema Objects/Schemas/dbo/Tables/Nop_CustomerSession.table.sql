CREATE TABLE [dbo].[Nop_CustomerSession] (
    [CustomerSessionGUID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]          INT              NOT NULL,
    [LastAccessed]        DATETIME         NOT NULL,
    [IsExpired]           BIT              NOT NULL
);

