ALTER TABLE [dbo].[Nop_ProductVariant]
    ADD CONSTRAINT [DF_Nop_ProductVariant_HasUserAgreement] DEFAULT ((0)) FOR [HasUserAgreement];

