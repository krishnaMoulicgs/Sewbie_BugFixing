

CREATE PROCEDURE [dbo].[Nop_CustomerReportByAttributeKey]
(
	@CustomerAttributeKey nvarchar(100)
)
AS
BEGIN

	SELECT dbo.[NOP_getcustomerattributevalue] (c.CustomerId, @CustomerAttributeKey) as AttributeKey, 
		   Count(c.CustomerId) as CustomerCount
FROM [Nop_Customer] c
WHERE
	c.Deleted = 0
GROUP BY dbo.[NOP_getcustomerattributevalue] (c.CustomerId, @CustomerAttributeKey)
ORDER BY CustomerCount desc

END