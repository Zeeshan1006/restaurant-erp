CREATE TABLE [dbo].[Permissions] (
    [PermissionID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [Description]  VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([PermissionID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

