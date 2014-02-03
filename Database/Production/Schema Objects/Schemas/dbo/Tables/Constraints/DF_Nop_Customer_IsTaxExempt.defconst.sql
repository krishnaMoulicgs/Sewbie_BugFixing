ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_IsTaxExempt] DEFAULT ((0)) FOR [IsTaxExempt];

