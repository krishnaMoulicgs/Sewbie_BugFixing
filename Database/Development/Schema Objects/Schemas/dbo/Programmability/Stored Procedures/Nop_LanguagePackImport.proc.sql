

CREATE PROCEDURE [dbo].[Nop_LanguagePackImport]
(
	@LanguageID int,
	@XmlPackage xml
)
AS
BEGIN
	IF EXISTS(SELECT * FROM [dbo].[Nop_Language] WHERE LanguageID = @LanguageID)
	BEGIN
		CREATE TABLE #LocaleStringResourceTmp
			(
				[LanguageID] [int] NOT NULL,
				[ResourceName] [nvarchar](200) NOT NULL,
				[ResourceValue] [nvarchar](max) NOT NULL
			)



		INSERT INTO #LocaleStringResourceTmp (LanguageID, ResourceName, ResourceValue)
		SELECT	@LanguageID, nref.value('@Name', 'nvarchar(200)'), nref.value('Value[1]', 'nvarchar(MAX)')
		FROM	@XmlPackage.nodes('//Language/LocaleResource') AS R(nref)

		DECLARE @ResourceName nvarchar(200)
		DECLARE @ResourceValue nvarchar(MAX)
		DECLARE cur_localeresource CURSOR FOR
		SELECT LanguageID, ResourceName, ResourceValue
		FROM #LocaleStringResourceTmp
		OPEN cur_localeresource
		FETCH NEXT FROM cur_localeresource INTO @LanguageID, @ResourceName, @ResourceValue
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF (EXISTS (SELECT 1 FROM Nop_LocaleStringResource WHERE LanguageID=@LanguageID AND ResourceName=@ResourceName))
			BEGIN
				UPDATE [Nop_LocaleStringResource]
				SET [ResourceValue]=@ResourceValue
				WHERE LanguageID=@LanguageID AND ResourceName=@ResourceName
			END
			ELSE 
			BEGIN
				INSERT INTO [Nop_LocaleStringResource]
				(
					[LanguageID],
					[ResourceName],
					[ResourceValue]
				)
				VALUES
				(
					@LanguageID,
					@ResourceName,
					@ResourceValue
				)
			END
			
			
			FETCH NEXT FROM cur_localeresource INTO @LanguageID, @ResourceName, @ResourceValue
			END
		CLOSE cur_localeresource
		DEALLOCATE cur_localeresource

		DROP TABLE #LocaleStringResourceTmp

		CREATE TABLE #MessageTemplateTmp
			(
				[LanguageID] [int] NOT NULL,
				[Name] [nvarchar](200) NOT NULL,
				[Subject] [nvarchar](200) NOT NULL,
				[Body] [nvarchar](max) NOT NULL
			)



		INSERT INTO #MessageTemplateTmp (LanguageID, [Name], [Subject], [Body])
		SELECT	@LanguageID, nref.value('@Name', 'nvarchar(200)'), nref.value('Subject[1]', 'nvarchar(200)'), nref.value('Body[1]', 'nvarchar(MAX)')
		FROM	@XmlPackage.nodes('//Language/MessageTemplate') AS R(nref)

		DECLARE @Name nvarchar(200)
		DECLARE @Subject nvarchar(200)
		DECLARE @Body nvarchar(MAX)
		DECLARE cur_messagetemplate CURSOR FOR
		SELECT LanguageID, [Name], [Subject], [Body]
		FROM #MessageTemplateTmp
		OPEN cur_messagetemplate
		FETCH NEXT FROM cur_messagetemplate INTO @LanguageID, @Name, @Subject, @Body
		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @MessageTemplateID int
			IF (EXISTS (SELECT 1 FROM Nop_MessageTemplate WHERE [Name]=@Name))
			BEGIN
				SET @MessageTemplateID = (SELECT MessageTemplateID FROM Nop_MessageTemplate WHERE [Name]=@Name);
			END
			ELSE 
			BEGIN
				INSERT INTO Nop_MessageTemplate ([Name]) VALUES (@Name);
				SET @MessageTemplateID = SCOPE_IDENTITY()
			END

			IF (EXISTS (SELECT 1 FROM Nop_MessageTemplateLocalized WHERE MessageTemplateID=@MessageTemplateID AND LanguageID=@LanguageID))
			BEGIN
				UPDATE [Nop_MessageTemplateLocalized]
				SET 
					[Subject]=@Subject,
					[Body] = @Body
				WHERE 
					MessageTemplateID=@MessageTemplateID AND LanguageID=@LanguageID
			END
			ELSE 
			BEGIN
				INSERT INTO [Nop_MessageTemplateLocalized]
				(
					[MessageTemplateID],
					[LanguageID],
					[Subject],
					[Body]
				)
				VALUES
				(
					@MessageTemplateID,
					@LanguageID,
					@Subject,
					@Body
				)
			END
			
			
			FETCH NEXT FROM cur_messagetemplate INTO @LanguageID, @Name, @Subject, @Body
			END
		CLOSE cur_messagetemplate
		DEALLOCATE cur_messagetemplate

		DROP TABLE #MessageTemplateTmp
	END
END