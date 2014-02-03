ALTER TABLE [dbo].[Nop_GiftCard]
    ADD CONSTRAINT [FK_Nop_GiftCard_Nop_OrderProductVariant] FOREIGN KEY ([PurchasedOrderProductVariantID]) REFERENCES [dbo].[Nop_OrderProductVariant] ([OrderProductVariantID]) ON DELETE CASCADE ON UPDATE CASCADE;

