CREATE TABLE [dbo].[Nop_ProductReview] (
    [ProductReviewID] INT             IDENTITY (1, 1) NOT NULL,
    [ProductID]       INT             NOT NULL,
    [CustomerID]      INT             NOT NULL,
    [IPAddress]       NVARCHAR (100)  NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ReviewText]      NVARCHAR (MAX)  NOT NULL,
    [Rating]          INT             NOT NULL,
    [HelpfulYesTotal] INT             NOT NULL,
    [HelpfulNoTotal]  INT             NOT NULL,
    [IsApproved]      BIT             NOT NULL,
    [CreatedOn]       DATETIME        NOT NULL
);

