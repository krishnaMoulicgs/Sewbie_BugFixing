ALTER TABLE [dbo].[Nop_OrderProductVariant]
    ADD CONSTRAINT [FK_Nop_OrderProductVariant_Nop_ProductVariant] FOREIGN KEY ([ProductVariantID]) REFERENCES [dbo].[Nop_ProductVariant] ([ProductVariantId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

