ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_IsVendor] DEFAULT ((0)) FOR [IsVendor];

