

CREATE PROCEDURE [dbo].[Nop_LanguagePackExport]
(
	@LanguageID int,
	@XmlPackage xml output
)
AS
BEGIN
	SET NOCOUNT ON
	SET @XmlPackage = 
	(
		SELECT l.Name as '@Name',
		(
			SELECT 
				lsr.ResourceName AS '@Name', 
				lsr.ResourceValue AS 'Value' 
			FROM 
				Nop_LocaleStringResource lsr 
			WHERE 
				lsr.LanguageID = l.LanguageID 
			ORDER BY 
				lsr.ResourceName
			FOR 
				XML PATH('LocaleResource'), TYPE
		),
		(
			SELECT
				mt.Name AS '@Name',
				mtl.Subject AS 'Subject', 
				mtl.Body AS 'Body'
			FROM 
				Nop_MessageTemplateLocalized mtl
			INNER JOIN
				Nop_MessageTemplate mt
			ON
				mt.MessageTemplateID = mtl.MessageTemplateID
			WHERE 
				mtl.LanguageID = l.LanguageID 
			FOR 
				XML PATH('MessageTemplate'), TYPE
		)
		FROM 
			Nop_Language l
		WHERE
			LanguageID = @LanguageID
		FOR 
			XML PATH('Language')
	)
END