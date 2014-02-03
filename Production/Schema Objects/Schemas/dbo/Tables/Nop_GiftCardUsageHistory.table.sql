CREATE TABLE [dbo].[Nop_GiftCardUsageHistory] (
    [GiftCardUsageHistoryID]      INT      IDENTITY (1, 1) NOT NULL,
    [GiftCardID]                  INT      NOT NULL,
    [CustomerID]                  INT      NOT NULL,
    [OrderID]                     INT      NOT NULL,
    [UsedValue]                   MONEY    NOT NULL,
    [UsedValueInCustomerCurrency] MONEY    NOT NULL,
    [CreatedOn]                   DATETIME NOT NULL
);

