CREATE TABLE [dbo].[Nop_ProductRating] (
    [ProductRatingID] INT      IDENTITY (1, 1) NOT NULL,
    [ProductID]       INT      NOT NULL,
    [CustomerID]      INT      NOT NULL,
    [Rating]          INT      NOT NULL,
    [RatedOn]         DATETIME NOT NULL
);

