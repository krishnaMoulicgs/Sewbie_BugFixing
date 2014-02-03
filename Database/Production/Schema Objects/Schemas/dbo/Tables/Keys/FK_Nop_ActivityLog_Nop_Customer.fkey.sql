ALTER TABLE [dbo].[Nop_ActivityLog]
    ADD CONSTRAINT [FK_Nop_ActivityLog_Nop_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Nop_Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

