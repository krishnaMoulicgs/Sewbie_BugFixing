ALTER TABLE [dbo].[Nop_BlogPost]
    ADD CONSTRAINT [FK_Nop_BlogPost_Nop_Customer] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

