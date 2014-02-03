CREATE TABLE [dbo].[Nop_ACLPerObject] (
    [ACLPerObjectId] INT IDENTITY (1, 1) NOT NULL,
    [ObjectId]       INT NOT NULL,
    [ObjectTypeId]   INT NOT NULL,
    [CustomerRoleId] INT NOT NULL,
    [Deny]           BIT NOT NULL
);

