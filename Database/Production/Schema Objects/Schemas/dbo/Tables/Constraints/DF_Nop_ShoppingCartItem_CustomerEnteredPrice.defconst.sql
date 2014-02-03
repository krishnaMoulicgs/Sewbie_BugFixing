ALTER TABLE [dbo].[Nop_ShoppingCartItem]
    ADD CONSTRAINT [DF_Nop_ShoppingCartItem_CustomerEnteredPrice] DEFAULT ((0)) FOR [CustomerEnteredPrice];

