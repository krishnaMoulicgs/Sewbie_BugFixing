﻿ALTER TABLE [dbo].[Nop_MessageTemplateLocalized]
    ADD CONSTRAINT [DF_Nop_MessageTemplateLocalized_EmailAccountId] DEFAULT ((0)) FOR [EmailAccountId];

