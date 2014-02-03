ALTER TABLE [dbo].[Nop_Product]
    ADD CONSTRAINT [DF_Nop_Product_Activated] DEFAULT ((0)) FOR [Activated];

