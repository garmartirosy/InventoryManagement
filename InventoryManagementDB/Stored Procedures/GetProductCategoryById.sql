CREATE PROCEDURE GetProductCategoryById(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM ProductCategory
WHERE Id = @ProductCategoryId AND UserId = @UserId
