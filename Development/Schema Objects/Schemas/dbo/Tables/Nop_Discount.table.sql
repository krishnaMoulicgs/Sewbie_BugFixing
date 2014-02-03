CREATE TABLE [dbo].[Nop_Discount] (
    [DiscountID]                   INT             IDENTITY (1, 1) NOT NULL,
    [DiscountTypeID]               INT             NOT NULL,
    [DiscountRequirementID]        INT             NOT NULL,
    [RequirementSpentAmount]       MONEY           NOT NULL,
    [RequirementBillingCountryIs]  INT             NOT NULL,
    [RequirementShippingCountryIs] INT             NOT NULL,
    [DiscountLimitationID]         INT             NOT NULL,
    [LimitationTimes]              INT             NOT NULL,
    [Name]                         NVARCHAR (100)  NOT NULL,
    [UsePercentage]                BIT             NOT NULL,
    [DiscountPercentage]           DECIMAL (18, 4) NOT NULL,
    [DiscountAmount]               DECIMAL (18, 4) NOT NULL,
    [StartDate]                    DATETIME        NOT NULL,
    [EndDate]                      DATETIME        NOT NULL,
    [RequiresCouponCode]           BIT             NOT NULL,
    [CouponCode]                   NVARCHAR (100)  NOT NULL,
    [Deleted]                      BIT             NOT NULL
);

