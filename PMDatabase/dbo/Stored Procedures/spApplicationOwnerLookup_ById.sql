CREATE PROCEDURE [dbo].[spApplicationOwnerLookup_ById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [UserId]
	FROM [dbo].[Applications]
	WHERE Id = @Id;
END;