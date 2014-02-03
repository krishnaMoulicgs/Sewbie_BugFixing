ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_SaltKey] DEFAULT ((0)) FOR [SaltKey];

