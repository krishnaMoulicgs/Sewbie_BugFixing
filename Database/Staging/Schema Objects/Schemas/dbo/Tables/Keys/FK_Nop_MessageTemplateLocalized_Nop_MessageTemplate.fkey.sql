ALTER TABLE [dbo].[Nop_MessageTemplateLocalized]
    ADD CONSTRAINT [FK_Nop_MessageTemplateLocalized_Nop_MessageTemplate] FOREIGN KEY ([MessageTemplateID]) REFERENCES [dbo].[Nop_MessageTemplate] ([MessageTemplateID]) ON DELETE CASCADE ON UPDATE CASCADE;

