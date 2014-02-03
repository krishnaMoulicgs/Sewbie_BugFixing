ALTER TABLE [dbo].[Nop_CustomerAction]
    ADD CONSTRAINT [DF_Nop_CustomerAction_DisplayOrder] DEFAULT ((1)) FOR [DisplayOrder];

