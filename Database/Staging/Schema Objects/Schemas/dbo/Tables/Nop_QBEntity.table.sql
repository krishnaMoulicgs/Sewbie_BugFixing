CREATE TABLE [dbo].[Nop_QBEntity] (
    [EntityId]     INT           IDENTITY (1, 1) NOT NULL,
    [QBEntityId]   NVARCHAR (50) NOT NULL,
    [EntityTypeId] INT           NOT NULL,
    [NopEntityId]  INT           NOT NULL,
    [SynStateId]   INT           NOT NULL,
    [SeqNum]       NVARCHAR (20) NOT NULL,
    [CreatedOn]    DATETIME      NOT NULL,
    [UpdatedOn]    DATETIME      NOT NULL
);

