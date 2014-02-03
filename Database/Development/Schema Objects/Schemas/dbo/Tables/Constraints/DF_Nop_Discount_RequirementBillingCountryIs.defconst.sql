ALTER TABLE [dbo].[Nop_Discount]
    ADD CONSTRAINT [DF_Nop_Discount_RequirementBillingCountryIs] DEFAULT ((0)) FOR [RequirementBillingCountryIs];

