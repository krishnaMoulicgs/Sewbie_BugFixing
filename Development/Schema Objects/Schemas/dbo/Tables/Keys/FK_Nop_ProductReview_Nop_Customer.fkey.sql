﻿ALTER TABLE [dbo].[Nop_ProductReview]
    ADD CONSTRAINT [FK_Nop_ProductReview_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

