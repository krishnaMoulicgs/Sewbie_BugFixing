CREATE TABLE [dbo].[Nop_Forums_Subscription] (
    [SubscriptionID]   INT              IDENTITY (1, 1) NOT NULL,
    [SubscriptionGUID] UNIQUEIDENTIFIER NOT NULL,
    [UserID]           INT              NOT NULL,
    [ForumID]          INT              NOT NULL,
    [TopicID]          INT              NOT NULL,
    [CreatedOn]        DATETIME         NOT NULL
);

