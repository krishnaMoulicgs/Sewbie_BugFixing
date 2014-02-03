CREATE TABLE [dbo].[Nop_Forums_Post] (
    [PostID]    INT            IDENTITY (1, 1) NOT NULL,
    [TopicID]   INT            NOT NULL,
    [UserID]    INT            NOT NULL,
    [Text]      NVARCHAR (MAX) NOT NULL,
    [IPAddress] NVARCHAR (100) NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [UpdatedOn] DATETIME       NOT NULL
);

