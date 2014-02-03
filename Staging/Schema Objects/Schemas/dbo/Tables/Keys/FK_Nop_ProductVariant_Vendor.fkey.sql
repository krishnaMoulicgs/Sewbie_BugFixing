ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [FK_Nop_ProductVariant_Vendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Vendor] ([CustomerId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



