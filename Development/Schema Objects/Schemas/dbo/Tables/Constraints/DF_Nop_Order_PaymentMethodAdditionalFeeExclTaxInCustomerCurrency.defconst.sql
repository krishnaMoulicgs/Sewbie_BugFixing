ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_PaymentMethodAdditionalFeeExclTaxInCustomerCurrency] DEFAULT ((0)) FOR [PaymentMethodAdditionalFeeExclTaxInCustomerCurrency];

