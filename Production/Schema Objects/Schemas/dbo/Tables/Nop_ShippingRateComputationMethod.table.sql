﻿CREATE TABLE [dbo].[Nop_ShippingRateComputationMethod] (
    [ShippingRateComputationMethodID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]                            NVARCHAR (100)  NOT NULL,
    [Description]                     NVARCHAR (4000) NOT NULL,
    [ConfigureTemplatePath]           NVARCHAR (500)  NOT NULL,
    [ClassName]                       NVARCHAR (500)  NOT NULL,
    [IsActive]                        BIT             NOT NULL,
    [DisplayOrder]                    INT             NOT NULL
);

