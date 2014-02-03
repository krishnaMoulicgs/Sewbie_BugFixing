ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_CompanyName] DEFAULT ('') FOR [CompanyName];

