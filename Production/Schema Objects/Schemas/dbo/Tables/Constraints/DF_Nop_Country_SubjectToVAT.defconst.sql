ALTER TABLE [dbo].[Nop_Country]
    ADD CONSTRAINT [DF_Nop_Country_SubjectToVAT] DEFAULT ((0)) FOR [SubjectToVAT];

