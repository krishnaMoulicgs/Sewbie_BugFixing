﻿ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_DisplayStockQuantity] DEFAULT ((0)) FOR [DisplayStockQuantity];

