ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_PaypalLastName] DEFAULT ('') FOR [PaypalLastName];

