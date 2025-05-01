CREATE TABLE [dbo].[RolePermissions] (
    [RolePermissionID] INT IDENTITY (1, 1) NOT NULL,
    [RoleID]           INT NULL,
    [PermissionID]     INT NULL,
    PRIMARY KEY CLUSTERED ([RolePermissionID] ASC),
    FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[Permissions] ([PermissionID]) ON DELETE CASCADE,
    FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID]) ON DELETE CASCADE
);

