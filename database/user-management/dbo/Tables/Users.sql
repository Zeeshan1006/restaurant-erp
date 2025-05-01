CREATE TABLE [dbo].[Users] (
    [UserID]           INT           IDENTITY (1, 1) NOT NULL,
    [Username]         VARCHAR (50)  NOT NULL,
    [Email]            VARCHAR (100) NOT NULL,
    [PasswordHash]     VARCHAR (255) NOT NULL,
    [IsActive]         BIT           DEFAULT ((1)) NULL,
    [TwoFactorEnabled] BIT           DEFAULT ((0)) NULL,
    [TwoFactorSecret]  VARCHAR (255) NULL,
    [CreatedAt]        DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

