ALTER TABLE [dbo].[Nop_CategoryLocalized]
    ADD CONSTRAINT [FK_Nop_CategoryLocalized_Nop_Category] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Nop_Category] ([CategoryID]) ON DELETE CASCADE ON UPDATE CASCADE;

