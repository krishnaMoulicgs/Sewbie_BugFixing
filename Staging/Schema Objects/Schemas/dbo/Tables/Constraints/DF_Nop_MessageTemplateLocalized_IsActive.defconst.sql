ALTER TABLE [dbo].[Nop_MessageTemplateLocalized]
    ADD CONSTRAINT [DF_Nop_MessageTemplateLocalized_IsActive] DEFAULT ((1)) FOR [IsActive];

