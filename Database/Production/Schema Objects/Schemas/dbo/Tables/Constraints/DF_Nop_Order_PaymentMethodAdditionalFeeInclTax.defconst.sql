ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_PaymentMethodAdditionalFeeInclTax] DEFAULT ((0)) FOR [PaymentMethodAdditionalFeeInclTax];

