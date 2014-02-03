CREATE TABLE [dbo].[Nop_PollVotingRecord] (
    [PollVotingRecordID] INT IDENTITY (1, 1) NOT NULL,
    [PollAnswerID]       INT NOT NULL,
    [CustomerID]         INT NOT NULL
);

