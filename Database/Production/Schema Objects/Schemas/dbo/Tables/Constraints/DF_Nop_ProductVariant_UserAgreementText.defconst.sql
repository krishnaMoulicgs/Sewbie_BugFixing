ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_UserAgreementText] DEFAULT ('') FOR [UserAgreementText];

