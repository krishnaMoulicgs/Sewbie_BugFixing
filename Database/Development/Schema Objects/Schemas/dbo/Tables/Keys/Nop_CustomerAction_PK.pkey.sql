﻿ALTER TABLE [dbo].[Nop_CustomerAction]
    ADD CONSTRAINT [Nop_CustomerAction_PK] PRIMARY KEY CLUSTERED ([CustomerActionID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
