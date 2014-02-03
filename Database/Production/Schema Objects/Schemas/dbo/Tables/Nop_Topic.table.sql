CREATE TABLE [dbo].[Nop_Topic] (
    [TopicID]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (200) NOT NULL,
    [IsPasswordProtected] BIT            NOT NULL,
    [Password]            NVARCHAR (200) NOT NULL,
    [IncludeInSitemap]    BIT            NOT NULL
);

