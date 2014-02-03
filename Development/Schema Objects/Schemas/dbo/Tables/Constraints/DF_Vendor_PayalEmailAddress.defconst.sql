ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [DF_Vendor_PayalEmailAddress] DEFAULT ('') FOR [PaypalEmailAddress];

