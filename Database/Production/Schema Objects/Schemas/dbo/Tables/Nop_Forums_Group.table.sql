CREATE TABLE [dbo].[Nop_Forums_Group] (
    [ForumGroupID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (200) NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [DisplayOrder] INT            NOT NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [UpdatedOn]    DATETIME       NOT NULL
);

