ALTER TABLE [dbo].[Nop_Customer]
    ADD CONSTRAINT [DF_Nop_Customer_IsGuest] DEFAULT ((0)) FOR [IsGuest];

