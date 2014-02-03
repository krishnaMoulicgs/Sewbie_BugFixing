ALTER TABLE [dbo].[Nop_Affiliate]
    ADD CONSTRAINT [DF_Nop_Affiliate_Deleted] DEFAULT ((0)) FOR [Deleted];

