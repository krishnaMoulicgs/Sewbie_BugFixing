CREATE TABLE [dbo].[Nop_QueuedEmail] (
    [QueuedEmailID]  INT            IDENTITY (1, 1) NOT NULL,
    [Priority]       INT            NOT NULL,
    [From]           NVARCHAR (500) NOT NULL,
    [FromName]       NVARCHAR (500) NOT NULL,
    [To]             NVARCHAR (500) NOT NULL,
    [ToName]         NVARCHAR (500) NOT NULL,
    [Cc]             NVARCHAR (500) NOT NULL,
    [Bcc]            NVARCHAR (500) NOT NULL,
    [Subject]        NVARCHAR (500) NOT NULL,
    [Body]           NVARCHAR (MAX) NOT NULL,
    [CreatedOn]      DATETIME       NOT NULL,
    [SendTries]      INT            NOT NULL,
    [SentOn]         DATETIME       NULL,
    [EmailAccountId] INT            NOT NULL
);

