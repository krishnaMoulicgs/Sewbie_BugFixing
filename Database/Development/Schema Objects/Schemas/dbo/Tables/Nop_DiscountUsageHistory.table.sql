CREATE TABLE [dbo].[Nop_DiscountUsageHistory] (
    [DiscountUsageHistoryID] INT      IDENTITY (1, 1) NOT NULL,
    [DiscountID]             INT      NOT NULL,
    [CustomerID]             INT      NOT NULL,
    [OrderID]                INT      NOT NULL,
    [CreatedOn]              DATETIME NOT NULL
);

