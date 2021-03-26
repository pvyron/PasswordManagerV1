CREATE PROCEDURE [dbo].[spPasswordUpdate_ByUserById]
	@Id int,
	@UserId NVARCHAR(128),
	@ApplicationId int,
	@PasswordAlias NVARCHAR(256),
	@Username NVARCHAR(256),
	@Password NVARCHAR(256),
	@Encrypted BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Passwords] 

	SET [ApplicationId] = @ApplicationId,
		[PasswordAlias] = @PasswordAlias,
		[Username] = @Username,
		[Password] = @Password,
		[Encrypted] = @Encrypted,
		[UpdateDate] = GETUTCDATE()

	WHERE Id = @Id AND UserId = @UserId;

END;
