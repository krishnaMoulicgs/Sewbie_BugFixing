CREATE TABLE [dbo].[Nop_ProductReviewHelpfulness] (
    [ProductReviewHelpfulnessID] INT IDENTITY (1, 1) NOT NULL,
    [ProductReviewID]            INT NOT NULL,
    [CustomerID]                 INT NOT NULL,
    [WasHelpful]                 BIT NOT NULL
);

