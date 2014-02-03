CREATE TABLE [dbo].[Nop_ActivityLog] (
    [ActivityLogID]     INT             IDENTITY (1, 1) NOT NULL,
    [ActivityLogTypeID] INT             NOT NULL,
    [CustomerID]        INT             NOT NULL,
    [Comment]           NVARCHAR (4000) NOT NULL,
    [CreatedOn]         DATETIME        NOT NULL
);

