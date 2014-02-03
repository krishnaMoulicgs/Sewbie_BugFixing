ALTER TABLE [dbo].[Nop_ShoppingCartItem]
    ADD CONSTRAINT [CK_Nop_ShoppingCart_Quantity] CHECK ([quantity]>(0));

