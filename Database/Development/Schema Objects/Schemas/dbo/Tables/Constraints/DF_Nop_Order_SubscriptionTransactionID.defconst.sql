﻿ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_SubscriptionTransactionID] DEFAULT ('') FOR [SubscriptionTransactionID];

