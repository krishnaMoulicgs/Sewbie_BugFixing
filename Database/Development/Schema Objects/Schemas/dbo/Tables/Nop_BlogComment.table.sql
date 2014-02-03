CREATE TABLE [dbo].[Nop_BlogComment] (
    [BlogCommentID] INT            IDENTITY (1, 1) NOT NULL,
    [BlogPostID]    INT            NOT NULL,
    [CustomerID]    INT            NOT NULL,
    [IPAddress]     NVARCHAR (100) NOT NULL,
    [CommentText]   NVARCHAR (MAX) NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL
);

