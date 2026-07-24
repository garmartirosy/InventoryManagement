CREATE PROCEDURE DeleteProduct(
	@ProductId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE st
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.ProductId = @ProductId AND p.UserId = @UserId

DELETE i
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.ProductId = @ProductId AND p.UserId = @UserId

DELETE FROM Product
WHERE Id = @ProductId AND UserId = @UserId
