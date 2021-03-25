CREATE PROCEDURE [dbo].[spPasswordLookup_ForUser]
	@userId NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id
		, UserId
		, ApplicationId
		, PasswordAlias
		, Username
		, [Password]
		, CreateDate
		, UpdateDate
		, [Encrypted]
	FROM dbo.Passwords
	WHERE UserId = @userId;

END;