ALTER TABLE [dbo].[Nop_ShippingRateComputationMethod]
    ADD CONSTRAINT [DF_Nop_ShippingRateComputationMethod_IsActive] DEFAULT ((0)) FOR [IsActive];

