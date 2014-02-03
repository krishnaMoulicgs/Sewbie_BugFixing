CREATE TABLE [dbo].[Nop_Forums_Forum] (
    [ForumID]        INT            IDENTITY (1, 1) NOT NULL,
    [ForumGroupID]   INT            NOT NULL,
    [Name]           NVARCHAR (200) NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [NumTopics]      INT            NOT NULL,
    [NumPosts]       INT            NOT NULL,
    [LastTopicID]    INT            NOT NULL,
    [LastPostID]     INT            NOT NULL,
    [LastPostUserID] INT            NOT NULL,
    [LastPostTime]   DATETIME       NULL,
    [DisplayOrder]   INT            NOT NULL,
    [CreatedOn]      DATETIME       NOT NULL,
    [UpdatedOn]      DATETIME       NOT NULL
);

