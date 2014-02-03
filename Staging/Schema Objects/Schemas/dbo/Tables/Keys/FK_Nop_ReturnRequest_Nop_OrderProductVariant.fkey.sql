ALTER TABLE [dbo].[Nop_ReturnRequest]
    ADD CONSTRAINT [FK_Nop_ReturnRequest_Nop_OrderProductVariant] FOREIGN KEY ([OrderProductVariantId]) REFERENCES [dbo].[Nop_OrderProductVariant] ([OrderProductVariantID]) ON DELETE CASCADE ON UPDATE CASCADE;

