CREATE TABLE [dbo].[Nop_RecurringPayment] (
    [RecurringPaymentID] INT      IDENTITY (1, 1) NOT NULL,
    [InitialOrderID]     INT      NOT NULL,
    [CycleLength]        INT      NOT NULL,
    [CyclePeriod]        INT      NOT NULL,
    [TotalCycles]        INT      NOT NULL,
    [StartDate]          DATETIME NOT NULL,
    [IsActive]           BIT      NOT NULL,
    [Deleted]            BIT      NOT NULL,
    [CreatedOn]          DATETIME NOT NULL
);

