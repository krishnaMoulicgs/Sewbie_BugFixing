ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_MaximumCustomerEnteredPrice] DEFAULT ((1000)) FOR [MaximumCustomerEnteredPrice];

