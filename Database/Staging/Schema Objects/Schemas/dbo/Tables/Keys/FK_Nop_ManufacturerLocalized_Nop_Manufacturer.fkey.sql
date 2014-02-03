ALTER TABLE [dbo].[Nop_ManufacturerLocalized]
    ADD CONSTRAINT [FK_Nop_ManufacturerLocalized_Nop_Manufacturer] FOREIGN KEY ([ManufacturerID]) REFERENCES [dbo].[Nop_Manufacturer] ([ManufacturerID]) ON DELETE CASCADE ON UPDATE CASCADE;

