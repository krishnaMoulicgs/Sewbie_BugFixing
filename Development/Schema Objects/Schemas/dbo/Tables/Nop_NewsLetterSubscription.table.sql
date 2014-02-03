CREATE TABLE [dbo].[Nop_NewsLetterSubscription] (
    [NewsLetterSubscriptionID]   INT              IDENTITY (1, 1) NOT NULL,
    [NewsLetterSubscriptionGuid] UNIQUEIDENTIFIER NOT NULL,
    [Email]                      NVARCHAR (255)   NOT NULL,
    [Active]                     BIT              NOT NULL,
    [CreatedOn]                  DATETIME         NOT NULL
);

