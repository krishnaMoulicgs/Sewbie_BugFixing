﻿ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [PK_Vendor_1] PRIMARY KEY CLUSTERED ([CustomerId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

