ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_Deleted] DEFAULT ((0)) FOR [Deleted];

