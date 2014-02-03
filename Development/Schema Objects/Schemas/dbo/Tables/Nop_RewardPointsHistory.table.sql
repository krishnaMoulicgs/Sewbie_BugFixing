CREATE TABLE [dbo].[Nop_RewardPointsHistory] (
    [RewardPointsHistoryID]        INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]                   INT             NOT NULL,
    [OrderID]                      INT             NOT NULL,
    [Points]                       INT             NOT NULL,
    [PointsBalance]                INT             NOT NULL,
    [UsedAmount]                   MONEY           NOT NULL,
    [UsedAmountInCustomerCurrency] MONEY           NOT NULL,
    [CustomerCurrencyCode]         NVARCHAR (5)    NOT NULL,
    [Message]                      NVARCHAR (1000) NOT NULL,
    [CreatedOn]                    DATETIME        NOT NULL
);

