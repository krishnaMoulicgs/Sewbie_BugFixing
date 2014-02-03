ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_PaymentMethodAdditionalFeeExclTax] DEFAULT ((0)) FOR [PaymentMethodAdditionalFeeExclTax];

