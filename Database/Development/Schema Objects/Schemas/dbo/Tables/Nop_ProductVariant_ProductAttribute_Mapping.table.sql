CREATE TABLE [dbo].[Nop_ProductVariant_ProductAttribute_Mapping] (
    [ProductVariantAttributeID] INT            IDENTITY (1, 1) NOT NULL,
    [ProductVariantID]          INT            NOT NULL,
    [ProductAttributeID]        INT            NOT NULL,
    [TextPrompt]                NVARCHAR (200) NOT NULL,
    [IsRequired]                BIT            NOT NULL,
    [AttributeControlTypeID]    INT            NOT NULL,
    [DisplayOrder]              INT            NOT NULL
);

