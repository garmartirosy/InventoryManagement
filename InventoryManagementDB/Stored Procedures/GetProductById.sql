CREATE PROCEDURE GetProductById(
	@ProductId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT p.Id, p.UserId, p.Name, p.Price,
	p.CategoryId, ISNULL(c.Name, '') AS CategoryName,
	p.SupplierId, ISNULL(s.Name, '') AS SupplierName
FROM Product p
LEFT JOIN ProductCategory c ON p.CategoryId = c.Id
LEFT JOIN Supplier s ON p.SupplierId = s.Id
WHERE p.Id = @ProductId AND p.UserId = @UserId
