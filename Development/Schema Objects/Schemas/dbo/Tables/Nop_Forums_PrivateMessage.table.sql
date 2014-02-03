CREATE TABLE [dbo].[Nop_Forums_PrivateMessage] (
    [PrivateMessageID]     INT            IDENTITY (1, 1) NOT NULL,
    [FromUserID]           INT            NOT NULL,
    [ToUserID]             INT            NOT NULL,
    [Subject]              NVARCHAR (450) NOT NULL,
    [Text]                 NVARCHAR (MAX) NOT NULL,
    [IsRead]               BIT            NOT NULL,
    [IsDeletedByAuthor]    BIT            NOT NULL,
    [IsDeletedByRecipient] BIT            NOT NULL,
    [CreatedOn]            DATETIME       NOT NULL
);

