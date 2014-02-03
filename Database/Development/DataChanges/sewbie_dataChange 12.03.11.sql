-- =============================================
-- Script Template
-- =============================================
--TODO: additions to the settings for paypal and sandbox.

--TODO: additions for payment method.


--Set all the products enabled for buying
--Set all products enabled for shipping.
UPDATE nop_productvariant
SET disablebuybutton = 0, isshipenabled = 1
WHERE deleted = 0