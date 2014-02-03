ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_Active] DEFAULT ((1)) FOR [Active];

