﻿ALTER TABLE [dbo].[Nop_Forums_Subscription]
    ADD CONSTRAINT [PK_Nop_Forums_Subscription] PRIMARY KEY CLUSTERED ([SubscriptionID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

