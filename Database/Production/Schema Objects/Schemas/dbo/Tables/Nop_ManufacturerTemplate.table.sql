CREATE TABLE [dbo].[Nop_ManufacturerTemplate] (
    [ManufacturerTemplateId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                   NVARCHAR (100) NOT NULL,
    [TemplatePath]           NVARCHAR (200) NOT NULL,
    [DisplayOrder]           INT            NOT NULL,
    [CreatedOn]              DATETIME       NOT NULL,
    [UpdatedOn]              DATETIME       NOT NULL
);

