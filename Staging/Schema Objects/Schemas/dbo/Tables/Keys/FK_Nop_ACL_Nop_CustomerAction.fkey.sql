ALTER TABLE [dbo].[Nop_ACL]
    ADD CONSTRAINT [FK_Nop_ACL_Nop_CustomerAction] FOREIGN KEY ([CustomerActionID]) REFERENCES [dbo].[Nop_CustomerAction] ([CustomerActionID]) ON DELETE CASCADE ON UPDATE CASCADE;

