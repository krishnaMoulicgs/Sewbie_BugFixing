﻿ALTER TABLE [dbo].[Nop_ACL]
    ADD CONSTRAINT [IX_Nop_ACL_Unique] UNIQUE NONCLUSTERED ([CustomerActionID] ASC, [CustomerRoleID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

