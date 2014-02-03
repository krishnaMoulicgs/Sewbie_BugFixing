-- =============================================
-- Script Template
-- =============================================

--change customer passwords to SewbieSewbieSewbie
UPDATE nop_customer
SET passwordhash ='AB8A7EFF1B95E4C2C114196E3EDAB9D9FBAECEE3',
	Saltkey='ByESoTs='
--change email server to point to mailtrap.io
UPDATE nop_emailaccount
SET	[Host] = 'mailtrap.io',
	Port = 2525,
	username = 'sewbie-test-4c646d759041c3c6',
	password = 'a266ac355bbbcfaf',
	enableSSL = 0,
	usedefaultcredentials = 0
	
--point paypal to test 
UPDATE nop_setting
SET Value = 'True'
WHERE Name = 'PaymentMethod.PaypalExpress.UseSandbox'

UPDATE nop_setting
SET Value = 'admin-facilitator_api1.sewbie.com'
WHERE Name = 'PaymentMethod.PaypalExpress.APIAccountName'

UPDATE nop_setting
SET Value = '1376364715'
WHERE Name = 'PaymentMethod.PaypalExpress.APIAccountPassword'

UPDATE nop_setting
SET Value = 'AFYfSm79FlOc2K.auzxtpTaeqZUYADOIONk0MMs2imE4TpRzskoiF33I'
WHERE Name = 'PaymentMethod.PaypalExpress.Signature'

UPDATE nop_setting
SET Value = 'Sale'
WHERE Name = 'PaymentMethod.PaypalExpress.TransactionMode'

UPDATE nop_setting
SET Value = 'http://69.248.109.13/PaypalExpressReturn.aspx'
WHERE Name = 'PaymentMethod.PaypalExpress.ReturnURL'

UPDATE nop_setting
SET Value = 'http://69.248.109.13/PaypalCancel.aspx'
WHERE Name = 'PaymentMethod.PaypalExpress.CancelURL'

UPDATE nop_setting
SET Value = 'http://69.248.109.13/PaypalIPNhandler.aspx'
WHERE Name = 'PaymentMethod.PaypalExpress.NotifyURL'


--change setting values to point to test.


