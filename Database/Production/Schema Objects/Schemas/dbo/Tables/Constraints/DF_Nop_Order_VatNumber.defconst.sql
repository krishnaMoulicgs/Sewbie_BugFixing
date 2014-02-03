ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_VatNumber] DEFAULT ('') FOR [VatNumber];

