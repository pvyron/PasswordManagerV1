CREATE PROCEDURE [dbo].[spPasswordDelete_ById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Passwords] WHERE Id = @Id;
END;
