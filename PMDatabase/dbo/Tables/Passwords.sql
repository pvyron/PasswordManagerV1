CREATE TABLE [dbo].[Passwords]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] NVARCHAR (128) NOT NULL,
	[ApplicationId] INT NOT NULL,
	[PasswordAlias] NVARCHAR(256) NOT NULL,
	[Username] NVARCHAR(256) NOT NULL,
	[Password] NVARCHAR(256) NOT NULL,
	[CreateDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[UpdateDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[Encrypted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Passwords_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_Passwords_ToApplications] FOREIGN KEY ([ApplicationId]) REFERENCES [Applications]([Id])
)
