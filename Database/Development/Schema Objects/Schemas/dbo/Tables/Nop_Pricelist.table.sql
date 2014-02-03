CREATE TABLE [dbo].[Nop_Pricelist] (
    [PricelistID]             INT            IDENTITY (1, 1) NOT NULL,
    [ExportModeID]            INT            NOT NULL,
    [ExportTypeID]            INT            NOT NULL,
    [AffiliateID]             INT            NOT NULL,
    [DisplayName]             NVARCHAR (100) NOT NULL,
    [ShortName]               NVARCHAR (20)  NOT NULL,
    [PricelistGuid]           NVARCHAR (40)  NOT NULL,
    [CacheTime]               INT            NOT NULL,
    [FormatLocalization]      NVARCHAR (5)   NOT NULL,
    [Description]             NVARCHAR (500) NOT NULL,
    [AdminNotes]              NVARCHAR (500) NOT NULL,
    [Header]                  NVARCHAR (500) NOT NULL,
    [Body]                    NVARCHAR (500) NOT NULL,
    [Footer]                  NVARCHAR (500) NOT NULL,
    [PriceAdjustmentTypeID]   INT            NOT NULL,
    [PriceAdjustment]         MONEY          NOT NULL,
    [OverrideIndivAdjustment] BIT            NOT NULL,
    [CreatedOn]               DATETIME       NOT NULL,
    [UpdatedOn]               DATETIME       NOT NULL
);

