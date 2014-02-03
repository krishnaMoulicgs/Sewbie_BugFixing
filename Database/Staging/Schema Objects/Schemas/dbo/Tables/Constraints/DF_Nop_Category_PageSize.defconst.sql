ALTER TABLE [dbo].[Nop_Category]
    ADD CONSTRAINT [DF_Nop_Category_PageSize] DEFAULT ((10)) FOR [PageSize];

