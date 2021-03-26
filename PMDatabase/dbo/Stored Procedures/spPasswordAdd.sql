CREATE PROCEDURE [dbo].[spPasswordAdd]
	@UserId NVARCHAR (128),
    @ApplicationId INT,
    @PasswordAlias NVARCHAR (256),
    @Username NVARCHAR (256),
    @Password NVARCHAR (256),
    @Encrypted BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Passwords] ([UserId], [ApplicationId], [PasswordAlias], [Username], [Password], [Encrypted])
    VALUES (@UserId, @ApplicationId, @PasswordAlias, @Username, @Password, @Encrypted);

    SELECT @@IDENTITY;
END;