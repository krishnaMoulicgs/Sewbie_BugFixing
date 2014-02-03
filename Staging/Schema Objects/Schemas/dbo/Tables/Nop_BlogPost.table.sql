CREATE TABLE [dbo].[Nop_BlogPost] (
    [BlogPostID]            INT             IDENTITY (1, 1) NOT NULL,
    [LanguageID]            INT             NOT NULL,
    [BlogPostTitle]         NVARCHAR (200)  NOT NULL,
    [BlogPostBody]          NVARCHAR (MAX)  NOT NULL,
    [BlogPostAllowComments] BIT             NOT NULL,
    [Tags]                  NVARCHAR (4000) NOT NULL,
    [CreatedByID]           INT             NOT NULL,
    [CreatedOn]             DATETIME        NOT NULL
);

