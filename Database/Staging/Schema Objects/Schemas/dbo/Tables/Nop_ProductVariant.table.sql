﻿CREATE TABLE [dbo].[Nop_ProductVariant] (
    [ProductVariantId]            INT             IDENTITY (1, 1) NOT NULL,
    [ProductID]                   INT             NOT NULL,
    [Name]                        NVARCHAR (400)  NOT NULL,
    [SKU]                         NVARCHAR (100)  NOT NULL,
    [Description]                 NVARCHAR (4000) NOT NULL,
    [AdminComment]                NVARCHAR (4000) NOT NULL,
    [ManufacturerPartNumber]      NVARCHAR (100)  NOT NULL,
    [IsGiftCard]                  BIT             NOT NULL,
    [GiftCardType]                INT             NOT NULL,
    [IsDownload]                  BIT             NOT NULL,
    [DownloadID]                  INT             NOT NULL,
    [UnlimitedDownloads]          BIT             NOT NULL,
    [MaxNumberOfDownloads]        INT             NOT NULL,
    [DownloadExpirationDays]      INT             NULL,
    [DownloadActivationType]      INT             NOT NULL,
    [HasSampleDownload]           BIT             NOT NULL,
    [SampleDownloadID]            INT             NOT NULL,
    [HasUserAgreement]            BIT             NOT NULL,
    [UserAgreementText]           NVARCHAR (MAX)  NOT NULL,
    [IsRecurring]                 BIT             NOT NULL,
    [CycleLength]                 INT             NOT NULL,
    [CyclePeriod]                 INT             NOT NULL,
    [TotalCycles]                 INT             NOT NULL,
    [IsShipEnabled]               BIT             NOT NULL,
    [IsFreeShipping]              BIT             NOT NULL,
    [AdditionalShippingCharge]    MONEY           NOT NULL,
    [IsTaxExempt]                 BIT             NOT NULL,
    [TaxCategoryID]               INT             NOT NULL,
    [ManageInventory]             INT             NOT NULL,
    [StockQuantity]               INT             NOT NULL,
    [DisplayStockAvailability]    BIT             NOT NULL,
    [DisplayStockQuantity]        BIT             NOT NULL,
    [MinStockQuantity]            INT             NOT NULL,
    [LowStockActivityID]          INT             NOT NULL,
    [NotifyAdminForQuantityBelow] INT             NOT NULL,
    [Backorders]                  INT             NOT NULL,
    [OrderMinimumQuantity]        INT             NOT NULL,
    [OrderMaximumQuantity]        INT             NOT NULL,
    [WarehouseID]                 INT             NOT NULL,
    [DisableBuyButton]            BIT             NOT NULL,
    [CallForPrice]                BIT             NOT NULL,
    [Price]                       MONEY           NOT NULL,
    [OldPrice]                    MONEY           NOT NULL,
    [ProductCost]                 MONEY           NOT NULL,
    [CustomerEntersPrice]         BIT             NOT NULL,
    [MinimumCustomerEnteredPrice] MONEY           NOT NULL,
    [MaximumCustomerEnteredPrice] MONEY           NOT NULL,
    [Weight]                      DECIMAL (18, 4) NOT NULL,
    [Length]                      DECIMAL (18, 4) NOT NULL,
    [Width]                       DECIMAL (18, 4) NOT NULL,
    [Height]                      DECIMAL (18, 4) NOT NULL,
    [PictureID]                   INT             NOT NULL,
    [AvailableStartDateTime]      DATETIME        NULL,
    [AvailableEndDateTime]        DATETIME        NULL,
    [Published]                   BIT             NOT NULL,
    [Deleted]                     BIT             NOT NULL,
    [DisplayOrder]                INT             NOT NULL,
    [VendorId]                    INT             NOT NULL,
    [CreatedOn]                   DATETIME        NOT NULL,
    [UpdatedOn]                   DATETIME        NOT NULL
);


