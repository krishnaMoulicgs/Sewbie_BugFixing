ALTER TABLE [dbo].[Nop_MessageTemplateLocalized]
    ADD CONSTRAINT [DF_Nop_MessageTemplateLocalized_BCCEmailAddresses] DEFAULT ('') FOR [BCCEmailAddresses];

