CREATE TABLE [dbo].[Nop_ShippingByWeight] (
    [ShippingByWeightID]       INT             IDENTITY (1, 1) NOT NULL,
    [ShippingMethodID]         INT             NOT NULL,
    [From]                     DECIMAL (18, 2) NOT NULL,
    [To]                       DECIMAL (18, 2) NOT NULL,
    [UsePercentage]            BIT             NOT NULL,
    [ShippingChargePercentage] DECIMAL (18, 2) NOT NULL,
    [ShippingChargeAmount]     DECIMAL (18, 2) NOT NULL
);

