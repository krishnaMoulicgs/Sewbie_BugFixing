ALTER TABLE [dbo].[Nop_ProductReviewHelpfulness]
    ADD CONSTRAINT [FK_Nop_ProductReviewHelpfulness_Nop_ProductReview] FOREIGN KEY ([ProductReviewID]) REFERENCES [dbo].[Nop_ProductReview] ([ProductReviewID]) ON DELETE CASCADE ON UPDATE CASCADE;

