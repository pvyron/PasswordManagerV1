CREATE PROCEDURE [dbo].[spApplicationLookup_ForUser]
	@userId NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id
		, UserId
		, ApplicationAlias
		, CreateDate
		, UpdateDate
		, DefaultEncryption
	FROM dbo.Applications
	WHERE UserId = @userId;

END;