ALTER TABLE [dbo].[Nop_Discount]
    ADD CONSTRAINT [DF_Nop_Discount_RequirementSpentAmount] DEFAULT ((0)) FOR [RequirementSpentAmount];

