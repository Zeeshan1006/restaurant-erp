CREATE TABLE [dbo].[Roles] (
    [RoleID]      INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]    VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

