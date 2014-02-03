ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_NotifyAdminForQuantityBelow] DEFAULT ((1)) FOR [NotifyAdminForQuantityBelow];

