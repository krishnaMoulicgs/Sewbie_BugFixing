﻿ALTER TABLE [dbo].[Nop_Category_Discount_Mapping]
    ADD CONSTRAINT [PK_Nop_Category_Discount_Mapping] PRIMARY KEY CLUSTERED ([CategoryID] ASC, [DiscountID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

