﻿ALTER TABLE [dbo].[Nop_Forums_Forum]
    ADD CONSTRAINT [PK_Nop_Forums_Forum] PRIMARY KEY CLUSTERED ([ForumID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

