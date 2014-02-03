ALTER TABLE [dbo].[Nop_Discount]
    ADD CONSTRAINT [DF_Nop_Discount_RequirementShippingCountryIs] DEFAULT ((0)) FOR [RequirementShippingCountryIs];

