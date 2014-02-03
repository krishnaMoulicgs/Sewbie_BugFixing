ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_TaxDisplayTypeID] DEFAULT ((1)) FOR [TaxDisplayTypeID];

