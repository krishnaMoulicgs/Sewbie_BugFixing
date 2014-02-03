ALTER TABLE [dbo].[Nop_GiftCard]
    ADD CONSTRAINT [DF_Nop_GiftCard_IsRecipientNotified] DEFAULT ((0)) FOR [IsRecipientNotified];

