ALTER TABLE [dbo].[Nop_OrderNote]
    ADD CONSTRAINT [DF_Nop_OrderNote_DisplayToCustomer] DEFAULT ((0)) FOR [DisplayToCustomer];

