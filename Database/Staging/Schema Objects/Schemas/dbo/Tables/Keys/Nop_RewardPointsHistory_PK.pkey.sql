﻿ALTER TABLE [dbo].[Nop_RewardPointsHistory]
    ADD CONSTRAINT [Nop_RewardPointsHistory_PK] PRIMARY KEY CLUSTERED ([RewardPointsHistoryID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

