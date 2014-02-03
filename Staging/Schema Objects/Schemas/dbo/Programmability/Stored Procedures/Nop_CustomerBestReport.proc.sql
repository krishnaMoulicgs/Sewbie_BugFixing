

CREATE PROCEDURE [dbo].[Nop_CustomerBestReport]
(
	@StartTime				datetime = NULL,
	@EndTime				datetime = NULL,
	@OrderStatusID			int,
	@PaymentStatusID		int,
	@ShippingStatusID		int,
	@OrderBy				int = 1 
)
AS
BEGIN

	SELECT TOP 20 c.CustomerId, SUM(o.OrderTotal) AS OrderTotal, COUNT(o.OrderID) AS OrderCount
	FROM [Nop_Customer] c
	INNER JOIN [Nop_Order] o
	ON c.CustomerID = o.CustomerID
	WHERE
		c.Deleted = 0 AND
		o.Deleted = 0 AND
		(@StartTime is NULL or @StartTime <= o.CreatedOn) AND
		(@EndTime is NULL or @EndTime >= o.CreatedOn) AND 
		(@OrderStatusID IS NULL or @OrderStatusID=0 or o.OrderStatusID = @OrderStatusID) AND
		(@PaymentStatusID IS NULL or @PaymentStatusID=0 or o.PaymentStatusID = @PaymentStatusID) AND
		(@ShippingStatusID IS NULL OR @ShippingStatusID = 0 OR o.ShippingStatusID = @ShippingStatusID) --AND
	GROUP BY c.CustomerID
	ORDER BY case @OrderBy when 1 then SUM(o.OrderTotal) when 2 then COUNT(o.OrderID) else SUM(o.OrderTotal) end desc

END