

CREATE PROCEDURE [dbo].[Nop_OrderProductVariantReport]
(
	@StartTime datetime = NULL,
	@EndTime datetime = NULL,
	@OrderStatusID int,
	@PaymentStatusID int,
	@BillingCountryID int
)
AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT opv.ProductVariantId, isnull(sum(opv.PriceExclTax), 0) as PriceExclTax, isnull(sum(opv.Quantity), 0) as Quantity
	FROM Nop_OrderProductVariant opv 
	INNER JOIN [Nop_Order] o ON o.OrderId = opv.OrderID
	WHERE
		(@StartTime is NULL or @StartTime <= o.CreatedOn) and
		(@EndTime is NULL or @EndTime >= o.CreatedOn) and 
		(@OrderStatusID IS NULL or @OrderStatusID=0 or o.OrderStatusID = @OrderStatusID) and
		(@PaymentStatusID IS NULL or @PaymentStatusID=0 or o.PaymentStatusID = @PaymentStatusID) and
		(@BillingCountryID IS NULL or @BillingCountryID=0 or o.BillingCountryID = @BillingCountryID) and
		(o.Deleted=0)
	GROUP BY opv.ProductVariantId
	ORDER BY PriceExclTax desc
END