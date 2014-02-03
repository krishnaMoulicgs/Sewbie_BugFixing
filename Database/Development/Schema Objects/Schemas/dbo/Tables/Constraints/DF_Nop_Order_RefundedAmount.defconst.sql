ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_RefundedAmount] DEFAULT ((0)) FOR [RefundedAmount];

