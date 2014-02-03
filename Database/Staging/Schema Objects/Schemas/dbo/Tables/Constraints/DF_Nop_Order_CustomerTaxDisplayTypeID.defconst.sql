ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_CustomerTaxDisplayTypeID] DEFAULT ((1)) FOR [CustomerTaxDisplayTypeID];

