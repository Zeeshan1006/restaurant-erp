CREATE TABLE [dbo].[LoginHistory] (
    [LoginID]   INT          IDENTITY (1, 1) NOT NULL,
    [UserID]    INT          NULL,
    [LoginTime] DATETIME     DEFAULT (getdate()) NULL,
    [IPAddress] VARCHAR (45) NULL,
    [Success]   BIT          DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([LoginID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
);

