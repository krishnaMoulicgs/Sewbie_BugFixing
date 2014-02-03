ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_AllowStoringCreditCardNumber] DEFAULT ((0)) FOR [AllowStoringCreditCardNumber];

