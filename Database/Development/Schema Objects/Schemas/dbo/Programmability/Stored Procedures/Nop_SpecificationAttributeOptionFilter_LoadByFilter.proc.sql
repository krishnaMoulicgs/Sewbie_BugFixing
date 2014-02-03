

CREATE PROCEDURE [dbo].[Nop_SpecificationAttributeOptionFilter_LoadByFilter]
(
	@CategoryID int,
	@LanguageID int
)
AS
BEGIN
	SELECT 
		sa.SpecificationAttributeId,
		dbo.NOP_getnotnullnotempty(sal.Name,sa.Name) as [SpecificationAttributeName],
		sa.DisplayOrder,
		sao.SpecificationAttributeOptionId,
		dbo.NOP_getnotnullnotempty(saol.Name,sao.Name) as [SpecificationAttributeOptionName]
	FROM Nop_Product_SpecificationAttribute_Mapping psam with (NOLOCK)
		INNER JOIN Nop_SpecificationAttributeOption sao with (NOLOCK) ON
			sao.SpecificationAttributeOptionID = psam.SpecificationAttributeOptionID
		INNER JOIN Nop_SpecificationAttribute sa with (NOLOCK) ON
			sa.SpecificationAttributeID = sao.SpecificationAttributeID	
		INNER JOIN Nop_Product_Category_Mapping pcm with (NOLOCK) ON 
			pcm.ProductID = psam.ProductID	
		INNER JOIN Nop_Product p ON 
			psam.ProductID = p.ProductID
		LEFT OUTER JOIN Nop_ProductVariant pv with (NOLOCK) ON 
			p.ProductID = pv.ProductID
		LEFT OUTER JOIN [Nop_SpecificationAttributeLocalized] sal with (NOLOCK) ON 
			sa.SpecificationAttributeID = sal.SpecificationAttributeID AND sal.LanguageID = @LanguageID	
		LEFT OUTER JOIN [Nop_SpecificationAttributeOptionLocalized] saol with (NOLOCK) ON 
			sao.SpecificationAttributeOptionID = saol.SpecificationAttributeOptionID AND saol.LanguageID = @LanguageID	
	WHERE 
			p.Published = 1
		AND 
			pv.Published = 1
		AND 
			p.Deleted=0
		AND
			pcm.CategoryID = @CategoryID
		AND
			psam.AllowFiltering = 1
		AND
			getutcdate() between isnull(pv.AvailableStartDateTime, '1/1/1900') and isnull(pv.AvailableEndDateTime, '1/1/2999')
	GROUP BY
		sa.SpecificationAttributeID, 
		dbo.NOP_getnotnullnotempty(sal.Name,sa.Name),
		sa.DisplayOrder,
		sao.SpecificationAttributeOptionID,
		dbo.NOP_getnotnullnotempty(saol.Name,sao.Name)
	ORDER BY sa.DisplayOrder, [SpecificationAttributeName], [SpecificationAttributeOptionName]
END