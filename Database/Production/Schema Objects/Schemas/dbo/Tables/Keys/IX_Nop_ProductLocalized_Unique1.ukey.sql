﻿ALTER TABLE [dbo].[Nop_ProductLocalized]
    ADD CONSTRAINT [IX_Nop_ProductLocalized_Unique1] UNIQUE NONCLUSTERED ([ProductID] ASC, [LanguageID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];
