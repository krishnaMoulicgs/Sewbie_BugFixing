CREATE TABLE [dbo].[Nop_EmailAccount] (
    [EmailAccountId]        INT            IDENTITY (1, 1) NOT NULL,
    [Email]                 NVARCHAR (255) NOT NULL,
    [DisplayName]           NVARCHAR (255) NOT NULL,
    [Host]                  NVARCHAR (255) NOT NULL,
    [Port]                  INT            NOT NULL,
    [Username]              NVARCHAR (255) NOT NULL,
    [Password]              NVARCHAR (255) NOT NULL,
    [EnableSSL]             BIT            NOT NULL,
    [UseDefaultCredentials] BIT            NOT NULL
);

