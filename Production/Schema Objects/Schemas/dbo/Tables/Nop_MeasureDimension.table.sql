CREATE TABLE [dbo].[Nop_MeasureDimension] (
    [MeasureDimensionID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (100)  NOT NULL,
    [SystemKeyword]      NVARCHAR (100)  NOT NULL,
    [Ratio]              DECIMAL (18, 4) NOT NULL,
    [DisplayOrder]       INT             NOT NULL
);

