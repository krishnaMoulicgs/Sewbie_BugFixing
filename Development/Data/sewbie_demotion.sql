-- =============================================
-- Script Template

-- This script will take a production environment and bring it down to development or staging level
-- by changing all the database settings to those appropriate for the environment.
-- =============================================



-----------------CONFIGURATION!!!!---------------------------------------------------------------------------------------
UPDATE [dbo].[Nop_Setting] SET [Value] = '' WHERE [Name] = N'Analytics.GoogleID'

--INSERT [dbo].[Nop_EmailAccount] ([EmailAccountId], [Email], [DisplayName], [Host], [Port], [Username], [Password], [EnableSSL], [UseDefaultCredentials]) VALUES (1, N'info@sewbie.com', N'General contact', N'HV16ACCU1031.sewbie.com', 25, N'mailuser', N'M121lU53r!@', 0, 0)
--INSERT [dbo].[Nop_EmailAccount] ([EmailAccountId], [Email], [DisplayName], [Host], [Port], [Username], [Password], [EnableSSL], [UseDefaultCredentials]) VALUES (2, N'sell@sewbie.com', N'Seller Information', N'HV16ACCU1031.sewbie.com', 25, N'mailuser', N'M121lU53r!@', 0, 0)
--INSERT [dbo].[Nop_EmailAccount] ([EmailAccountId], [Email], [DisplayName], [Host], [Port], [Username], [Password], [EnableSSL], [UseDefaultCredentials]) VALUES (3, N'info@mail.com', N'Customer support', N'HV16ACCU1031.sewbie.com', 25, N'mailuser', N'M121lU53r!@', 0, 0)

UPDATE [dbo].[Nop_Setting] SET  [Value] = 'true' WHERE [Name] = 'PaymentMethod.PaypalAdaptive.UseSandbox'


DELETE FROM Nop_QueuedEmail

-----------------DATA!!!!---------------------------------------------------------------------------------------