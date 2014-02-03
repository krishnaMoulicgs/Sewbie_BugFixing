CREATE TABLE [dbo].[Nop_CrossSellProduct] (
    [CrossSellProductId] INT IDENTITY (1, 1) NOT NULL,
    [ProductId1]         INT NOT NULL,
    [ProductId2]         INT NOT NULL
);

