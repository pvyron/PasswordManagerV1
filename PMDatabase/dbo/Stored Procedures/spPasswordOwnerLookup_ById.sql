CREATE PROCEDURE [dbo].[spPasswordOwnerLookup_ById]
	@Id int = 0
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [UserId] 
	FROM [dbo].[Passwords] 
	WHERE [Id] = @Id;

END;