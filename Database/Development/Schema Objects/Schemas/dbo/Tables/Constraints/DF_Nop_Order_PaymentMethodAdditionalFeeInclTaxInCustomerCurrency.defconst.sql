ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_PaymentMethodAdditionalFeeInclTaxInCustomerCurrency] DEFAULT ((0)) FOR [PaymentMethodAdditionalFeeInclTaxInCustomerCurrency];

