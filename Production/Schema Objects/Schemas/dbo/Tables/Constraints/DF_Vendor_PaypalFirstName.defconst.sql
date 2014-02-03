ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_PaypalFirstName] DEFAULT ('') FOR [PaypalFirstName];

