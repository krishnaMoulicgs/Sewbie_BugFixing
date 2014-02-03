ALTER TABLE [dbo].[Nop_Order]
    ADD CONSTRAINT [DF_Nop_Order_TrackingNumber] DEFAULT ('') FOR [TrackingNumber];

