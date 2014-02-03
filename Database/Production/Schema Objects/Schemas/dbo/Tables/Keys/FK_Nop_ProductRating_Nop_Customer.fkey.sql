ALTER TABLE [dbo].[Nop_ProductRating]
    ADD CONSTRAINT [FK_Nop_ProductRating_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

