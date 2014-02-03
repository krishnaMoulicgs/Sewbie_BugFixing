ALTER TABLE [dbo].[Nop_GiftCardUsageHistory]
    ADD CONSTRAINT [DF_Nop_GiftCardUsageHistory_UsedValueInCustomerCurrency] DEFAULT ((0)) FOR [UsedValueInCustomerCurrency];

