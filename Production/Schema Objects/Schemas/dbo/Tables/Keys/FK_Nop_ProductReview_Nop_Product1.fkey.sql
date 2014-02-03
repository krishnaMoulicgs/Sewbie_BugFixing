ALTER TABLE [dbo].[Nop_ProductReview]
    ADD CONSTRAINT [FK_Nop_ProductReview_Nop_Product1] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Nop_Product] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE;

