﻿ALTER TABLE [dbo].[Nop_Picture]
    ADD CONSTRAINT [Nop_Picture_PK] PRIMARY KEY CLUSTERED ([PictureID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

