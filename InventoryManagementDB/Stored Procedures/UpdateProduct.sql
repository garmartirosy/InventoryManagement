CREATE PROCEDURE UpdateProduct(
	@ProductId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100),
	@Price DECIMAL(10,2),
	@CategoryId INT = NULL,
	@SupplierId INT = NULL
)
AS SET NOCOUNT ON
IF @CategoryId IS NULL OR EXISTS (SELECT 1 FROM ProductCategory WHERE Id = @CategoryId AND UserId = @UserId)
IF @SupplierId IS NULL OR EXISTS (SELECT 1 FROM Supplier WHERE Id = @SupplierId AND UserId = @UserId)
UPDATE Product
SET Name = @Name,
	Price = @Price,
	CategoryId = @CategoryId,
	SupplierId = @SupplierId
WHERE Id = @ProductId AND UserId = @UserId
