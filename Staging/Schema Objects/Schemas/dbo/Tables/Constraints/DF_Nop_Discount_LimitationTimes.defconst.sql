ALTER TABLE [dbo].[Nop_Discount]
    ADD CONSTRAINT [DF_Nop_Discount_LimitationTimes] DEFAULT ((1)) FOR [LimitationTimes];

