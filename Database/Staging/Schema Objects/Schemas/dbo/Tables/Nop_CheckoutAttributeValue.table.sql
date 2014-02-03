CREATE TABLE [dbo].[Nop_CheckoutAttributeValue] (
    [CheckoutAttributeValueID] INT             IDENTITY (1, 1) NOT NULL,
    [CheckoutAttributeID]      INT             NOT NULL,
    [Name]                     NVARCHAR (100)  NOT NULL,
    [PriceAdjustment]          MONEY           NOT NULL,
    [WeightAdjustment]         DECIMAL (18, 4) NOT NULL,
    [IsPreSelected]            BIT             NOT NULL,
    [DisplayOrder]             INT             NOT NULL
);

