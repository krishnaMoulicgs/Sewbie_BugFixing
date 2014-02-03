CREATE TABLE [dbo].[Nop_SpecificationAttributeOption] (
    [SpecificationAttributeOptionID] INT            IDENTITY (1, 1) NOT NULL,
    [SpecificationAttributeID]       INT            NOT NULL,
    [Name]                           NVARCHAR (500) NOT NULL,
    [DisplayOrder]                   INT            NOT NULL
);

