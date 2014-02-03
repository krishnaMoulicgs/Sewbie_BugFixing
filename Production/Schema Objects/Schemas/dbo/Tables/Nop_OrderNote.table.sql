CREATE TABLE [dbo].[Nop_OrderNote] (
    [OrderNoteID]       INT             IDENTITY (1, 1) NOT NULL,
    [OrderID]           INT             NOT NULL,
    [Note]              NVARCHAR (4000) NOT NULL,
    [DisplayToCustomer] BIT             NOT NULL,
    [CreatedOn]         DATETIME        NOT NULL
);

