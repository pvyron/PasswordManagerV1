CREATE PROCEDURE [dbo].[spUserLookup]
	@Id NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id
		, FirstName
		, LastName
		, EmailAddress
		, CreateDate
		, UpdateDate
	FROM dbo.Users
	WHERE Id = @Id;
END