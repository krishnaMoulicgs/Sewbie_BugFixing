-- =============================================
-- Script Template
-- =============================================
Update Nop_LocaleStringResource
SET ResourceValue = 'Welcome to Sewbie! Your registration is complete.'
WHERE LocaleStringResourceID  = 3871

Update Nop_LocaleStringResource
SET ResourceValue = 'Continue to Shop'
WHERE LocaleStringResourceID  = 3868

INSERT INTO Nop_Setting(SettingId, Name, Value, Description)
VALUES(446, 'VendorAdmin.LoadAllProducts', 'true', 'load all values on initial load')

Update Nop_Setting
SET Value = 'false'
WHERE SettingID = 380

Update Nop_Setting
SET Value = 'false'
WHERE SettingID = 383