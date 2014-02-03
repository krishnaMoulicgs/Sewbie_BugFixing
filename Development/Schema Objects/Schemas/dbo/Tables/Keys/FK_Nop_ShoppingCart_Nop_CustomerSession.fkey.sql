ALTER TABLE [dbo].[Nop_ShoppingCartItem]
    ADD CONSTRAINT [FK_Nop_ShoppingCart_Nop_CustomerSession] FOREIGN KEY ([CustomerSessionGUID]) REFERENCES [dbo].[Nop_CustomerSession] ([CustomerSessionGUID]) ON DELETE CASCADE ON UPDATE CASCADE;

