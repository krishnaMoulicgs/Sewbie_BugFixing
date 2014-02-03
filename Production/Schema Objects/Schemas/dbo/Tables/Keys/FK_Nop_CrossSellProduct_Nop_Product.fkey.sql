ALTER TABLE [dbo].[Nop_CrossSellProduct]
    ADD CONSTRAINT [FK_Nop_CrossSellProduct_Nop_Product] FOREIGN KEY ([ProductId1]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

