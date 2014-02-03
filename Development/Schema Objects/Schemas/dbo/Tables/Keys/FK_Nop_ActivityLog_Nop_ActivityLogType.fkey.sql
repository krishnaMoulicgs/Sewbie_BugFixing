ALTER TABLE [dbo].[Nop_ActivityLog]
    ADD CONSTRAINT [FK_Nop_ActivityLog_Nop_ActivityLogType] FOREIGN KEY ([ActivityLogTypeID]) REFERENCES [dbo].[Nop_ActivityLogType] ([ActivityLogTypeID]) ON DELETE CASCADE ON UPDATE CASCADE;

