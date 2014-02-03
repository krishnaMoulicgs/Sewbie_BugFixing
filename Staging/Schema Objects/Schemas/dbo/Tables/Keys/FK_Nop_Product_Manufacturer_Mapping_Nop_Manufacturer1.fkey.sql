ALTER TABLE [dbo].[Nop_Product_Manufacturer_Mapping]
    ADD CONSTRAINT [FK_Nop_Product_Manufacturer_Mapping_Nop_Manufacturer1] FOREIGN KEY ([ManufacturerID]) REFERENCES [dbo].[Nop_Manufacturer] ([ManufacturerID]) ON DELETE CASCADE ON UPDATE CASCADE;

