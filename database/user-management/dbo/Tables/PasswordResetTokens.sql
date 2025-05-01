CREATE TABLE [dbo].[PasswordResetTokens] (
    [TokenID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]    INT           NULL,
    [Token]     VARCHAR (100) NOT NULL,
    [Expiry]    DATETIME      NOT NULL,
    [IsUsed]    BIT           DEFAULT ((0)) NULL,
    [CreatedAt] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([TokenID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
);

