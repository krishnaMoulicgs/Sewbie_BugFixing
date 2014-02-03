ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_PaypalVerified] DEFAULT ((0)) FOR [PaypalVerified];

