﻿ALTER TABLE [dbo].[Nop_TierPrice]
    ADD CONSTRAINT [Nop_TierPrice_PK] PRIMARY KEY CLUSTERED ([TierPriceID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
