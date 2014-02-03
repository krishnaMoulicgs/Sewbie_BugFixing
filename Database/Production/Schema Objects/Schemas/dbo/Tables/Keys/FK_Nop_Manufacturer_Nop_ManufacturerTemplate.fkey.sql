ALTER TABLE [dbo].[Nop_Manufacturer]
    ADD CONSTRAINT [FK_Nop_Manufacturer_Nop_ManufacturerTemplate] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Nop_ManufacturerTemplate] ([ManufacturerTemplateId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

