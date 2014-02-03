ALTER TABLE [dbo].[Nop_ProductReview]
    ADD CONSTRAINT [DF_Nop_ProductReview_IPAddress] DEFAULT ('') FOR [IPAddress];

