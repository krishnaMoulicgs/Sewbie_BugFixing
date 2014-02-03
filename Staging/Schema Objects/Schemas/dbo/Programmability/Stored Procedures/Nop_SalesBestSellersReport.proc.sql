

CREATE PROCEDURE [dbo].[Nop_SalesBestSellersReport]
(
	@LastDays int = 360,
	@RecordsToReturn int = 10,
	@OrderBy int = 1
)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @cmd varchar(500)
	
	CREATE TABLE #tmp (
		ID int not null identity,
		ProductVariantID int,
		SalesTotalCount int,
		SalesTotalAmount money)
	INSERT #tmp (
		ProductVariantID,
		SalesTotalCount,
		SalesTotalAmount)
	SELECT 
		s.ProductVariantId,
		s.SalesTotalCount, 
		s.SalesTotalAmount 
	FROM (SELECT opv.ProductVariantId, SUM(opv.Quantity) AS SalesTotalCount, SUM(opv.PriceExclTax) AS SalesTotalAmount
		  FROM [Nop_OrderProductVariant] opv
				INNER JOIN [Nop_Order] o on opv.OrderID = o.OrderID 
				WHERE o.CreatedOn >= dateadd(dy, -@LastDays, getdate())
				AND o.Deleted=0
		  GROUP BY opv.ProductVariantID 
		 ) s
		INNER JOIN [Nop_ProductVariant] pv with (nolock) on s.ProductVariantID = pv.ProductVariantID
		INNER JOIN [Nop_Product] p with (nolock) on pv.ProductID = p.ProductID
	WHERE p.Deleted = 0 
		AND p.Published = 1  
		AND pv.Published = 1 
		AND pv.Deleted = 0
	ORDER BY case @OrderBy when 1 then s.SalesTotalCount when 2 then s.SalesTotalAmount else s.SalesTotalCount end desc

	SET @cmd = 'SELECT TOP ' + convert(varchar(10), @RecordsToReturn ) + ' * FROM #tmp Order By ID'

	EXEC (@cmd)

	DROP TABLE #tmp
END