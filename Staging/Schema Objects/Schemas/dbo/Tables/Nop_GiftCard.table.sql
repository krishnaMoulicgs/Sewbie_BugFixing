CREATE TABLE [dbo].[Nop_GiftCard] (
    [GiftCardID]                     INT             IDENTITY (1, 1) NOT NULL,
    [PurchasedOrderProductVariantID] INT             NOT NULL,
    [Amount]                         MONEY           NOT NULL,
    [IsGiftCardActivated]            BIT             NOT NULL,
    [GiftCardCouponCode]             NVARCHAR (100)  NOT NULL,
    [RecipientName]                  NVARCHAR (100)  NOT NULL,
    [RecipientEmail]                 NVARCHAR (100)  NOT NULL,
    [SenderName]                     NVARCHAR (100)  NOT NULL,
    [SenderEmail]                    NVARCHAR (100)  NOT NULL,
    [Message]                        NVARCHAR (4000) NOT NULL,
    [IsRecipientNotified]            BIT             NOT NULL,
    [CreatedOn]                      DATETIME        NOT NULL
);

