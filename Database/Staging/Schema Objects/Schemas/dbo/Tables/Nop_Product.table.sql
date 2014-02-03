CREATE TABLE [dbo].[Nop_Product] (
    [ProductId]            INT             IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (400)  NOT NULL,
    [ShortDescription]     NVARCHAR (MAX)  NOT NULL,
    [FullDescription]      NVARCHAR (MAX)  NOT NULL,
    [AdminComment]         NVARCHAR (MAX)  NOT NULL,
    [TemplateID]           INT             NOT NULL,
    [ShowOnHomePage]       BIT             NOT NULL,
    [MetaKeywords]         NVARCHAR (400)  NOT NULL,
    [MetaDescription]      NVARCHAR (4000) NOT NULL,
    [MetaTitle]            NVARCHAR (400)  NOT NULL,
    [SEName]               NVARCHAR (100)  NOT NULL,
    [AllowCustomerReviews] BIT             NOT NULL,
    [AllowCustomerRatings] BIT             NOT NULL,
    [RatingSum]            INT             NOT NULL,
    [TotalRatingVotes]     INT             NOT NULL,
    [Published]            BIT             NOT NULL,
    [Deleted]              BIT             NOT NULL,
    [CreatedOn]            DATETIME        NOT NULL,
    [UpdatedOn]            DATETIME        NOT NULL
);

