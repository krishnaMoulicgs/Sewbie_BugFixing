ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_VendorId] DEFAULT ((1)) FOR [VendorId];

