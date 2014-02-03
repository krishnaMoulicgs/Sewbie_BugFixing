CREATE TABLE [dbo].[Nop_Forums_Topic] (
    [TopicID]        INT            IDENTITY (1, 1) NOT NULL,
    [ForumID]        INT            NOT NULL,
    [UserID]         INT            NOT NULL,
    [TopicTypeID]    INT            NOT NULL,
    [Subject]        NVARCHAR (450) NOT NULL,
    [NumPosts]       INT            NOT NULL,
    [Views]          INT            NOT NULL,
    [LastPostID]     INT            NOT NULL,
    [LastPostUserID] INT            NOT NULL,
    [LastPostTime]   DATETIME       NULL,
    [CreatedOn]      DATETIME       NOT NULL,
    [UpdatedOn]      DATETIME       NOT NULL
);

