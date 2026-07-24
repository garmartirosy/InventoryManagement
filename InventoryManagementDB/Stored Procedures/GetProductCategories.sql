CREATE PROCEDURE GetProductCategories(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM ProductCategory
WHERE UserId = @UserId
ORDER BY Name
