ALTER TABLE [dbo].[Nop_ProductVariant_Pricelist_Mapping]
    ADD CONSTRAINT [FK_Nop_ProductVariant_Pricelist_Mapping_Nop_Pricelist] FOREIGN KEY ([PricelistID]) REFERENCES [dbo].[Nop_Pricelist] ([PricelistID]) ON DELETE CASCADE ON UPDATE CASCADE;

