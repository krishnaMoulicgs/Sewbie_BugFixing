﻿ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_AdminComment] DEFAULT ('') FOR [AdminComment];

