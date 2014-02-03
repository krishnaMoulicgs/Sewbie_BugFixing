ALTER TABLE [dbo].[Nop_RecurringPaymentHistory]
    ADD CONSTRAINT [FK_Nop_RecurringPaymentHistory_Nop_RecurringPayment] FOREIGN KEY ([RecurringPaymentID]) REFERENCES [dbo].[Nop_RecurringPayment] ([RecurringPaymentID]) ON DELETE CASCADE ON UPDATE CASCADE;

