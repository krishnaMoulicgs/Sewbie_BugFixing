ALTER TABLE [dbo].[Nop_CheckoutAttributeLocalized]
    ADD CONSTRAINT [FK_Nop_CheckoutAttributeLocalized_Nop_CheckoutAttribute] FOREIGN KEY ([CheckoutAttributeID]) REFERENCES [dbo].[Nop_CheckoutAttribute] ([CheckoutAttributeID]) ON DELETE CASCADE ON UPDATE CASCADE;

