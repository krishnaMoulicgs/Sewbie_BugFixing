CREATE TABLE [dbo].[Nop_Setting] (
    [SettingID]   INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200)  NOT NULL,
    [Value]       NVARCHAR (2000) NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL
);

