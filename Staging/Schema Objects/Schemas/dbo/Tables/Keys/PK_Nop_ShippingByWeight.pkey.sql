﻿ALTER TABLE [dbo].[Nop_ShippingByWeight]
    ADD CONSTRAINT [PK_Nop_ShippingByWeight] PRIMARY KEY CLUSTERED ([ShippingByWeightID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
