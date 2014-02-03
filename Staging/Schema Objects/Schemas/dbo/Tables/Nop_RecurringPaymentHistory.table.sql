CREATE TABLE [dbo].[Nop_RecurringPaymentHistory] (
    [RecurringPaymentHistoryID] INT      IDENTITY (1, 1) NOT NULL,
    [RecurringPaymentID]        INT      NOT NULL,
    [OrderID]                   INT      NOT NULL,
    [CreatedOn]                 DATETIME NOT NULL
);

