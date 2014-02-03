CREATE TABLE [dbo].[Nop_PollAnswer] (
    [PollAnswerID] INT            IDENTITY (1, 1) NOT NULL,
    [PollID]       INT            NOT NULL,
    [Name]         NVARCHAR (400) NOT NULL,
    [Count]        INT            NOT NULL,
    [DisplayOrder] INT            NOT NULL
);

