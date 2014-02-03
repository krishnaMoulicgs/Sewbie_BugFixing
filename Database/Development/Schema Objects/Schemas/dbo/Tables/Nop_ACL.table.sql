CREATE TABLE [dbo].[Nop_ACL] (
    [ACLID]            INT IDENTITY (1, 1) NOT NULL,
    [CustomerActionID] INT NOT NULL,
    [CustomerRoleID]   INT NOT NULL,
    [Allow]            BIT NOT NULL
);

