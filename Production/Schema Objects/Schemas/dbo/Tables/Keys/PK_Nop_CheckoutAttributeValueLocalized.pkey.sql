﻿ALTER TABLE [dbo].[Nop_CheckoutAttributeValueLocalized]
    ADD CONSTRAINT [PK_Nop_CheckoutAttributeValueLocalized] PRIMARY KEY CLUSTERED ([CheckoutAttributeValueLocalizedID] ASC) WITH (FILLFACTOR = 80, ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

