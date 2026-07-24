CREATE PROCEDURE UpdateProductCategory(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
UPDATE ProductCategory
SET Name = @Name
WHERE Id = @ProductCategoryId AND UserId = @UserId
