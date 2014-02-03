ALTER TABLE [dbo].[Nop_CheckoutAttributeValue]
    ADD CONSTRAINT [FK_Nop_CheckoutAttributeValue_Nop_CheckoutAttribute] FOREIGN KEY ([CheckoutAttributeID]) REFERENCES [dbo].[Nop_CheckoutAttribute] ([CheckoutAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

