ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_CustomerId] DEFAULT ((1)) FOR [CustomerId];

