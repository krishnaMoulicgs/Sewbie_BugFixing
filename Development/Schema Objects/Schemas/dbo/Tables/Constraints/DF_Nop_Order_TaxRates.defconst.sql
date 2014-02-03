ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_TaxRates] DEFAULT ('') FOR [TaxRates];

