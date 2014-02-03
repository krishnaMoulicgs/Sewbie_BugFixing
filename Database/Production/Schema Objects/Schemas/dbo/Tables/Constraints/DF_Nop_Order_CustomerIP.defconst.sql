ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_CustomerIP] DEFAULT ('') FOR [CustomerIP];

