CREATE TABLE [dbo].[UserRoles] (
    [UserRoleID] INT IDENTITY (1, 1) NOT NULL,
    [UserID]     INT NULL,
    [RoleID]     INT NULL,
    PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID]) ON DELETE CASCADE,
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
);

