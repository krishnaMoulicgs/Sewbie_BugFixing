﻿ALTER TABLE [dbo].[Nop_GiftCardUsageHistory]
    ADD CONSTRAINT [Nop_GiftCardUsageHistory_PK] PRIMARY KEY CLUSTERED ([GiftCardUsageHistoryID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

