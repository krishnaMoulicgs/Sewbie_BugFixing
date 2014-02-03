CREATE TABLE [dbo].[Nop_CheckoutAttribute] (
    [CheckoutAttributeID]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (100) NOT NULL,
    [TextPrompt]               NVARCHAR (300) NOT NULL,
    [IsRequired]               BIT            NOT NULL,
    [ShippableProductRequired] BIT            NOT NULL,
    [IsTaxExempt]              BIT            NOT NULL,
    [TaxCategoryID]            INT            NOT NULL,
    [AttributeControlTypeID]   INT            NOT NULL,
    [DisplayOrder]             INT            NOT NULL
);

