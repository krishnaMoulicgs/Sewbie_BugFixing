CREATE TABLE [dbo].[Nop_ReturnRequest] (
    [ReturnRequestId]       INT            IDENTITY (1, 1) NOT NULL,
    [OrderProductVariantId] INT            NOT NULL,
    [Quantity]              INT            NOT NULL,
    [CustomerId]            INT            NOT NULL,
    [ReasonForReturn]       NVARCHAR (400) NOT NULL,
    [RequestedAction]       NVARCHAR (400) NOT NULL,
    [CustomerComments]      NVARCHAR (MAX) NOT NULL,
    [StaffNotes]            NVARCHAR (MAX) NOT NULL,
    [ReturnStatusId]        INT            NOT NULL,
    [CreatedOn]             DATETIME       NOT NULL,
    [UpdatedOn]             DATETIME       NOT NULL
);

