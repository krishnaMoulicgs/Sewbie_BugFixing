ALTER TABLE [dbo].[Nop_Address]
    ADD CONSTRAINT [DF_Nop_Address_UpdatedOn] DEFAULT (getutcdate()) FOR [UpdatedOn];

