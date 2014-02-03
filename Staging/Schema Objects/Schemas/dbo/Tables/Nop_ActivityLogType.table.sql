CREATE TABLE [dbo].[Nop_ActivityLogType] (
    [ActivityLogTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [SystemKeyword]     NVARCHAR (50)  NOT NULL,
    [Name]              NVARCHAR (100) NOT NULL,
    [Enabled]           BIT            NOT NULL
);

