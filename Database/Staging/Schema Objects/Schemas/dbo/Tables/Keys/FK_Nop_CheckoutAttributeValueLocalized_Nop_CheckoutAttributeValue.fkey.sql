ALTER TABLE [dbo].[Nop_CheckoutAttributeValueLocalized]
    ADD CONSTRAINT [FK_Nop_CheckoutAttributeValueLocalized_Nop_CheckoutAttributeValue] FOREIGN KEY ([CheckoutAttributeValueID]) REFERENCES [dbo].[Nop_CheckoutAttributeValue] ([CheckoutAttributeValueID]) ON DELETE CASCADE ON UPDATE CASCADE;

