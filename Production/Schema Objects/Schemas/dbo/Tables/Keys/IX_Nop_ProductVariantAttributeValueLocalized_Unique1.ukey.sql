ALTER TABLE [dbo].[Nop_ProductVariantAttributeValueLocalized]
    ADD CONSTRAINT [IX_Nop_ProductVariantAttributeValueLocalized_Unique1] UNIQUE NONCLUSTERED ([ProductVariantAttributeValueID] ASC, [LanguageID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

