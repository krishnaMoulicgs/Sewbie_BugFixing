ALTER TABLE [dbo].[Nop_Discount]
    ADD CONSTRAINT [DF_Nop_Discount_DiscountLimitationID] DEFAULT ((0)) FOR [DiscountLimitationID];

