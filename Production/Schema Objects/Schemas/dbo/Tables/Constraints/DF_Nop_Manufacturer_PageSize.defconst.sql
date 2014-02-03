ALTER TABLE [dbo].[Nop_Manufacturer]
    ADD CONSTRAINT [DF_Nop_Manufacturer_PageSize] DEFAULT ((10)) FOR [PageSize];

