

CREATE FUNCTION [dbo].[NOP_getcustomerattributevalue]
(
    @CustomerID int, 
    @AttributeKey nvarchar(100)
)
RETURNS nvarchar(1000)
AS
BEGIN
	
	DECLARE @AttributeValue nvarchar(1000)
	SET @AttributeValue = N''

	IF (EXISTS (SELECT ca.[Value] FROM [Nop_CustomerAttribute] ca 
				WHERE ca.CustomerID = @CustomerID AND
					  ca.[Key] = @AttributeKey))
	BEGIN
		SELECT @AttributeValue = ca.[Value] FROM [Nop_CustomerAttribute] ca 
				WHERE ca.CustomerID = @CustomerID AND
					  ca.[Key] = @AttributeKey	
	END
	  
	return @AttributeValue  
END