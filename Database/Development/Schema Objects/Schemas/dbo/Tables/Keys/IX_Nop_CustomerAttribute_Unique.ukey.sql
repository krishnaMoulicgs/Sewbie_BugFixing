﻿ALTER TABLE [dbo].[Nop_CustomerAttribute]
    ADD CONSTRAINT [IX_Nop_CustomerAttribute_Unique] UNIQUE NONCLUSTERED ([CustomerId] ASC, [Key] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

