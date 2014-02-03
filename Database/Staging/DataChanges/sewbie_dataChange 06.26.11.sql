--This is required for the schema change in removing vendorid from vendor table.
--this will reassociate the foreign key to the correct vendor.

UPDATE nop_productvariant
SET vendorid = 37
where vendorid = 1