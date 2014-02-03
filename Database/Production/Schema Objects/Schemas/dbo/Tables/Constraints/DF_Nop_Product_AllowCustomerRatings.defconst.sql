ALTER TABLE [dbo].[Nop_Product]
    ADD CONSTRAINT [DF_Nop_Product_AllowCustomerRatings] DEFAULT ((1)) FOR [AllowCustomerRatings];

