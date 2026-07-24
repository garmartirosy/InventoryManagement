CREATE PROCEDURE CreateProductCategory(
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
INSERT INTO ProductCategory (UserId, Name)
VALUES (@UserId, @Name)
