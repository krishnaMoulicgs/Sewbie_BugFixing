CREATE TABLE [dbo].[Nop_SearchLog] (
    [SearchLogID] INT            IDENTITY (1, 1) NOT NULL,
    [SearchTerm]  NVARCHAR (100) NOT NULL,
    [CustomerID]  INT            NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL
);

