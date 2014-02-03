ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_MinimumCustomerEnteredPrice] DEFAULT ((0)) FOR [MinimumCustomerEnteredPrice];

