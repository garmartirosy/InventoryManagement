CREATE PROCEDURE DeleteProductCategory(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
UPDATE Product
SET CategoryId = NULL
WHERE CategoryId = @ProductCategoryId AND UserId = @UserId

DELETE FROM ProductCategory
WHERE Id = @ProductCategoryId AND UserId = @UserId
