-- =============================================
-- Script Template
-- =============================================
update  Nop_LocaleStringResource
set ResourceValue = 'Seller:'
where Nop_LocaleStringResource.ResourceName ='Products.Manufacturer'

Update Nop_LocaleStringResource
set ResourceValue = 'Home'
where LocaleStringResourceID = 3916