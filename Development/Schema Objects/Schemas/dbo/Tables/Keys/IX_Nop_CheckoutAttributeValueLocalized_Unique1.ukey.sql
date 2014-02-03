ALTER TABLE [dbo].[Nop_CheckoutAttributeValueLocalized]
    ADD CONSTRAINT [IX_Nop_CheckoutAttributeValueLocalized_Unique1] UNIQUE NONCLUSTERED ([CheckoutAttributeValueID] ASC, [LanguageID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

